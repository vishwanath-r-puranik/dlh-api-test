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
ARG dlhdbserver="169.55.186.119, 9443"
ARG dlhdbname="DLHDevDb"
ARG dlhdbuserid="sa"
ARG dlhdbpassword="Sql2019isfast"
ARG auditdbserver="169.55.186.119, 9443"
ARG auditdbname="DLHDevAudit"
ARG auditdbuserid="sa"
ARG auditdbpassword="Sql2019isfast"
ENV DlhDBServer=$dlhdbserver  
ENV DlhDBName=$dlhdbname  
ENV DlhDbUserId=$dlhdbuserid
ENV DlhDbPassword=$dlhdbpassword
ENV AuditDBServer=$auditdbserver
ENV AuditDBName=$auditdbname
ENV AuditDbUserId=$auditdbuserid
ENV AuditDbPassword=$auditdbpassword
ENV DocMergrUri="https://mock12-mock-doc-merge.apps.pesdev.hcscloud.net/mergepdfservice"
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Org.OpenAPITools.dll"]
