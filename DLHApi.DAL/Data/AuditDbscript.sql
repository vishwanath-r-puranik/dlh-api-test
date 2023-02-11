Create database DLHDevAudit
GO

USE [DLHDevAudit]
GO

CREATE TABLE [dbo].[DlhRequestAudit](
	[RequestId] varchar(50)  NOT NULL,
	RequestDateTimeStamp  [datetime] NULL,
	PaymentDateTimeStamp  [datetime] NULL,
	[MVid] [varchar](20) NULL,
	RecordStatus  [varchar](50) NULL,
	MOVESTxServiceNo [varchar](50) NULL,
	ROADSUserID [varchar](50) NULL,
	MOVESSessionID [varchar](50) NULL,
	
PRIMARY KEY CLUSTERED 
(
	[RequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO