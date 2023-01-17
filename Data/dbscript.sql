Create database DLHDevDb
GO

USE [DLHDevDb]
GO

CREATE TABLE [dbo].[DlhModel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Address] [varchar](250) NULL,
	[Dob] [datetime] NULL,
	[DateOfIssue] [datetime] NULL,
	[DateOfExpire] [datetime] NULL,
	[MVID] [varchar](50) NULL,
	[PDF] [varbinary](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

INSERT INTO [DLHDevDb].[dbo].[DlhModel]( Name, Address, Dob, DateOfIssue, DateOfExpire, MVID,PDF )
SELECT  'Test', '101  University',1996-10-23, 2020-11-01, 2024-09-01,'34567893',BulkColumn 
FROM Openrowset( Bulk 'C:\Projects\Alberta Moves Modernization\DLH\Code\DLH\Data\Driver''s Licence History Report Template (sample only).pdf', Single_Blob) as PDF
GO

