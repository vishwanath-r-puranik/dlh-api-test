﻿Create database DLHDevAudit
GO

USE [DLHDevAudit]
GO

CREATE TABLE [DlhRequestAudit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RequestDate] [datetime]  NULL,
	[MVid] [varchar](50) NULL,
	[ReqStatus]	[varchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO