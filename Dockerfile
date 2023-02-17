#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DLHApi.OpenApiSpec/DLHApi.OpenApiSpec.csproj", "DLHApi.OpenApiSpec/"]
COPY ["DLHApi.DAL/DLHApi.DAL.csproj", "DLHApi.DAL/"]
COPY ["DLHApi.DTO.V1/DLHApi.DTO.V1.csproj", "DLHApi.DTO.V1/"]
COPY ["DLHApi.Common/DLHApi.Common.csproj", "DLHApi.Common/"]
COPY ["DLHApi.EIS/DLHApi.EIS.csproj", "DLHApi.EIS/"]
RUN dotnet restore "DLHApi.OpenApiSpec/DLHApi.OpenApiSpec.csproj"
COPY . .
WORKDIR "/src/DLHApi.OpenApiSpec"
RUN dotnet build "DLHApi.OpenApiSpec.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DLHApi.OpenApiSpec.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
ARG dlhdbserver="199.215.140.171, 1433"
ARG dlhdbname="MOVES_DLH_APP_DEV"
ARG dlhdbuserid="dlhapi"
ARG dlhdbpassword="G9420hD7%*3hvsi4b"
ARG auditdbserver="199.215.140.171, 1433"
ARG auditdbname="MOVES_DLH_APP_DEV"
ARG auditdbuserid="dlhapi"
ARG auditdbpassword="G9420hD7%*3hvsi4b"
ENV DlhDBServer=$dlhdbserver  
ENV DlhDBName=$dlhdbname  
ENV DlhDbUserId=$dlhdbuserid
ENV DlhDbPassword=$dlhdbpassword
ENV AuditDBServer=$auditdbserver
ENV AuditDBName=$auditdbname
ENV AuditDbUserId=$auditdbuserid
ENV AuditDbPassword=$auditdbpassword
ENV DocMergrUri="https://mock21-mock-doc-merge.apps.pesdev.hcscloud.net/mergepdfservice"
ENV KeyCloakUri="https://keycloak-keycloak.apps.pesdev.hcscloud.net/realms/pesrealm/protocol/openid-connect/token"
ENV KeyCloakUsername="test"
ENV KeyCloakPassword="test123"
ENV KeyCloakClient_id="pesclient"
ENV KeyCloakClient_secret="EVWo7ElvQU97ANVBjL0oXR2CxbiFRUxy"
ENV KeyCloakGrant_type="password"
ENV DMSUri="https://apiuat.gov.ab.ca/uat/dms/merge/render-template-with-regions"
ENV DMS_AccesToken_Uri="https://idpdev.gov.ab.ca/auth/realms/ServiceIntegration/protocol/openid-connect/token"
ENV DMS_ClientID="dlh-app-uat"
ENV DMS_ClientSecret="9fd027ec-aa4e-4894-922e-71ae450e5e9a"
ENV DMS_GrantType="client_credentials"
ENV CorsPolicy="dlhapiCors"
ENV AuditEndPointBase="https://audit-api-dlh-dev.apps.pesdev.hcscloud.net/"
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["DLHApi.OpenApiSpec/Files/dlhwithouthistory.docx", "/app/Files/"]
COPY ["DLHApi.OpenApiSpec/Files/Dlhwithhistory.docx", "/app/Files/"]
COPY ["DLHApi.OpenApiSpec/Files/Dlhwithouttable.docx", "/app/Files/"]
ENTRYPOINT ["dotnet", "Org.OpenAPITools.dll"]
