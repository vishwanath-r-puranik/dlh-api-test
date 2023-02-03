Create Database [DLHDevDb]
GO
USE [DLHDevDb]
GO
/****** Object:  Schema [DLH]    Script Date: 2023-01-26 2:08:06 PM ******/
CREATE SCHEMA [DLH]
GO
/****** Object:  Table [DLH].[Clients]    Script Date: 2023-01-26 2:08:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [DLH].[Clients](
	[ClientID] [bigint] NOT NULL,
	[MVID] [numeric](9, 0) NOT NULL,
	[LastName] [varchar](44) NULL,
	[FirstName] [varchar](12) NULL,
	[MiddleName] [varchar](12) NULL,
	[DOB] [date] NULL,
	[ProtectedFlag] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [DLH].[Condition]    Script Date: 2023-01-26 2:08:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [DLH].[Condition](
	[CondID] [int] NOT NULL,
	[Condition] [varchar](2) NULL,
	[Description] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [DLH].[DLHistoryInfo]    Script Date: 2023-01-26 2:08:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [DLH].[DLHistoryInfo](
	[DLHisInfoID] [int] NOT NULL,
	[MVID] [numeric](9, 0) NULL,
	[ServiceType] [varchar](8) NULL,
	[IssueDate] [date] NULL,
	[LicClassID] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [DLH].[Licence]    Script Date: 2023-01-26 2:08:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [DLH].[Licence](
	[LicenceID] [bigint] NOT NULL,
	[MVID] [numeric](9, 0) NOT NULL,
	[LicNumber] [varchar](16) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [DLH].[LicenceClass]    Script Date: 2023-01-26 2:08:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [DLH].[LicenceClass](
	[LicClassID] [int] NOT NULL,
	[LicClass] [varchar](8) NULL,
	[Description] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [DLH].[LicenceCondition]    Script Date: 2023-01-26 2:08:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [DLH].[LicenceCondition](
	[LicCondID] [int] NOT NULL,
	[LicCond] [int] NULL,
	[CondID] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [DLH].[LicenceDetails]    Script Date: 2023-01-26 2:08:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [DLH].[LicenceDetails](
	[LicDetailID] [bigint] NOT NULL,
	[MVID] [numeric](9, 0) NULL,
	[IssueDate] [date] NULL,
	[ExpiryDate] [date] NULL,
	[ServiceType] [varchar](8) NULL,
	[LicClassID] [int] NULL,
	[GDLStatus] [varchar](1) NULL,
	[GDLExitDate] [date] NULL,
	[LicCond] [int] NOT NULL
) ON [PRIMARY]
GO

INSERT [DLH].[Condition] ([CondID], [Condition], [Description]) VALUES (1, N'A', N'ADEQUATE LENSES')
GO
INSERT [DLH].[Condition] ([CondID], [Condition], [Description]) VALUES (2, N'B', N'SPECIAL CONDITION CODE')
GO
INSERT [DLH].[Condition] ([CondID], [Condition], [Description]) VALUES (3, N'C', N'PERIODIC MEDICAL')
GO
INSERT [DLH].[Condition] ([CondID], [Condition], [Description]) VALUES (4, N'D', N'PERIODIC VISION REPORT')
GO
INSERT [DLH].[Condition] ([CondID], [Condition], [Description]) VALUES (5, N'E', N'PERIODIC DRIVER EXAMINATION')
GO
INSERT [DLH].[Condition] ([CondID], [Condition], [Description]) VALUES (6, N'F', N'VALID WITHOUT PHOTO (TEMP)')
GO
INSERT [DLH].[Condition] ([CondID], [Condition], [Description]) VALUES (7, N'G', N'TEMPORARY RESIDENT')
GO
INSERT [DLH].[Condition] ([CondID], [Condition], [Description]) VALUES (8, N'H', N'DAYLIGHT DRIVING ONLY')
GO
INSERT [DLH].[Condition] ([CondID], [Condition], [Description]) VALUES (10, N'J', N'BOTH OUTSIDE MIRRORS')
GO
INSERT [DLH].[Condition] ([CondID], [Condition], [Description]) VALUES (11, N'K', N'AUTOMATIC TRANSMISSION')
GO
INSERT [DLH].[Condition] ([CondID], [Condition], [Description]) VALUES (12, N'L', N'ADEQUATE HAND CONTROLS')
GO
INSERT [DLH].[Condition] ([CondID], [Condition], [Description]) VALUES (13, N'M', N'Under Review with Alberta Transport 780-427-8230')
GO
INSERT [DLH].[Condition] ([CondID], [Condition], [Description]) VALUES (14, N'N', N'EXCLUDE CARRYING PASSENGERS FOR HIRE')
GO
INSERT [DLH].[Condition] ([CondID], [Condition], [Description]) VALUES (16, N'P', N'TESTING TO BE CONDUCTED BY PROVINCIAL DRIVER EXAMI')
GO
INSERT [DLH].[Condition] ([CondID], [Condition], [Description]) VALUES (17, N'Q', N'AIR BRAKE ENDORSEMENT')
GO
INSERT [DLH].[Condition] ([CondID], [Condition], [Description]) VALUES (18, N'R', N'AUTOMATIC TRANSMISSION RESTRICTION')
GO
INSERT [DLH].[Condition] ([CondID], [Condition], [Description]) VALUES (19, N'S', N'SCHOOL BUS ENDORSEMENT')
GO
INSERT [DLH].[Condition] ([CondID], [Condition], [Description]) VALUES (20, N'T', N'SPECIAL MEDICAL')
GO
INSERT [DLH].[Condition] ([CondID], [Condition], [Description]) VALUES (22, N'V', N'THREE-WHEELED MOTORCYCLE RESTRICTION')
GO
INSERT [DLH].[Condition] ([CondID], [Condition], [Description]) VALUES (23, N'W', N'Restricted Commercial Class - Canada Only')
GO
INSERT [DLH].[Condition] ([CondID], [Condition], [Description]) VALUES (26, N'Z', N'ORGAN DONOR')
GO
INSERT [DLH].[Condition] ([CondID], [Condition], [Description]) VALUES (21, N'U', N'COMPLETED DRIVER COURSE')
GO
INSERT [DLH].[Licence] ([LicenceID], [MVID], [LicNumber]) VALUES (12, CAST(123456789 AS Numeric(9, 0)), N'345667-234')
GO
INSERT [DLH].[Licence] ([LicenceID], [MVID], [LicNumber]) VALUES (23, CAST(223456789 AS Numeric(9, 0)), N'0320-38112')
GO
INSERT [DLH].[Licence] ([LicenceID], [MVID], [LicNumber]) VALUES (34, CAST(123412345 AS Numeric(9, 0)), N'233445-123')
GO
INSERT [DLH].[Licence] ([LicenceID], [MVID], [LicNumber]) VALUES (45, CAST(234567890 AS Numeric(9, 0)), N'456789-345')
GO
INSERT [DLH].[Licence] ([LicenceID], [MVID], [LicNumber]) VALUES (56, CAST(345678901 AS Numeric(9, 0)), N'555678-456')
GO
INSERT [DLH].[Licence] ([LicenceID], [MVID], [LicNumber]) VALUES (67, CAST(456789012 AS Numeric(9, 0)), N'678899-678')
GO
INSERT [DLH].[LicenceClass] ([LicClassID], [LicClass], [Description]) VALUES (101, N'1', N'Class 1')
GO
INSERT [DLH].[LicenceClass] ([LicClassID], [LicClass], [Description]) VALUES (102, N'16', N'Class 16')
GO
INSERT [DLH].[LicenceClass] ([LicClassID], [LicClass], [Description]) VALUES (201, N'2', N'Class 2')
GO
INSERT [DLH].[LicenceClass] ([LicClassID], [LicClass], [Description]) VALUES (202, N'26', N'Class 26')
GO
INSERT [DLH].[LicenceClass] ([LicClassID], [LicClass], [Description]) VALUES (301, N'3', N'Class 3')
GO
INSERT [DLH].[LicenceClass] ([LicClassID], [LicClass], [Description]) VALUES (302, N'34', N'Class 34')
GO
INSERT [DLH].[LicenceClass] ([LicClassID], [LicClass], [Description]) VALUES (303, N'346', N'Class 346')
GO
INSERT [DLH].[LicenceClass] ([LicClassID], [LicClass], [Description]) VALUES (401, N'4', N'Class 4')
GO
INSERT [DLH].[LicenceClass] ([LicClassID], [LicClass], [Description]) VALUES (402, N'46', N'Class 46')
GO
INSERT [DLH].[LicenceClass] ([LicClassID], [LicClass], [Description]) VALUES (501, N'5', N'Class 5')
GO
INSERT [DLH].[LicenceClass] ([LicClassID], [LicClass], [Description]) VALUES (502, N'56', N'Class 56')
GO
INSERT [DLH].[LicenceClass] ([LicClassID], [LicClass], [Description]) VALUES (601, N'6', N'Class 6')
GO
INSERT [DLH].[LicenceClass] ([LicClassID], [LicClass], [Description]) VALUES (801, N'8', N'Class 8')
GO
INSERT [DLH].[LicenceDetails] ([LicDetailID], [MVID], [IssueDate], [ExpiryDate], [ServiceType], [LicClassID], [GDLStatus], [GDLExitDate], [LicCond]) VALUES (1, CAST(123456789 AS Numeric(9, 0)), CAST(N'2018-07-13' AS Date), CAST(N'2022-07-13' AS Date), N'O-RECL', 502, N'N', NULL, 111)
GO
INSERT [DLH].[LicenceDetails] ([LicDetailID], [MVID], [IssueDate], [ExpiryDate], [ServiceType], [LicClassID], [GDLStatus], [GDLExitDate], [LicCond]) VALUES (7, CAST(223456789 AS Numeric(9, 0)), CAST(N'2021-01-21' AS Date), CAST(N'2025-10-12' AS Date), N'o-Renew', 101, N'Y', CAST(N'2022-08-31' AS Date), 444)
GO
INSERT [DLH].[LicenceDetails] ([LicDetailID], [MVID], [IssueDate], [ExpiryDate], [ServiceType], [LicClassID], [GDLStatus], [GDLExitDate], [LicCond]) VALUES (2, CAST(123412345 AS Numeric(9, 0)), CAST(N'2022-02-28' AS Date), CAST(N'2026-02-28' AS Date), N'O-FIRST', 501, N'Y', CAST(N'2024-02-28' AS Date), 222)
GO
INSERT [DLH].[LicenceDetails] ([LicDetailID], [MVID], [IssueDate], [ExpiryDate], [ServiceType], [LicClassID], [GDLStatus], [GDLExitDate], [LicCond]) VALUES (3, CAST(234567890 AS Numeric(9, 0)), CAST(N'2016-05-20' AS Date), CAST(N'2020-05-20' AS Date), N'O-RENEW', 303, N'N', NULL, 333)
GO
INSERT [DLH].[LicenceDetails] ([LicDetailID], [MVID], [IssueDate], [ExpiryDate], [ServiceType], [LicClassID], [GDLStatus], [GDLExitDate], [LicCond]) VALUES (4, CAST(345678901 AS Numeric(9, 0)), CAST(N'2020-02-01' AS Date), CAST(N'2024-02-01' AS Date), N'O-RENEW', 801, N'N', NULL, 433)
GO
INSERT [DLH].[LicenceDetails] ([LicDetailID], [MVID], [IssueDate], [ExpiryDate], [ServiceType], [LicClassID], [GDLStatus], [GDLExitDate], [LicCond]) VALUES (5, CAST(456789012 AS Numeric(9, 0)), CAST(N'2021-12-15' AS Date), CAST(N'2024-12-15' AS Date), N'O-FIRST', 601, N'Y', CAST(N'2023-12-15' AS Date), 555)
GO
INSERT [DLH].[LicenceCondition] ([LicCondID], [LicCond], [CondID]) VALUES (1, 111, 1)
GO
INSERT [DLH].[LicenceCondition] ([LicCondID], [LicCond], [CondID]) VALUES (2, 111, 2)
GO
INSERT [DLH].[Clients] ([ClientID], [MVID], [LastName], [FirstName], [MiddleName], [DOB], [ProtectedFlag]) VALUES (1234, CAST(123456789 AS Numeric(9, 0)), N'Jones', N'Mary', N'', CAST(N'1986-07-13' AS Date), 0)
GO
INSERT [DLH].[Clients] ([ClientID], [MVID], [LastName], [FirstName], [MiddleName], [DOB], [ProtectedFlag]) VALUES (2234, CAST(223456789 AS Numeric(9, 0)), N'Jane', N'Doe', N'stella', CAST(N'1992-11-12' AS Date), NULL)
GO
INSERT [DLH].[Clients] ([ClientID], [MVID], [LastName], [FirstName], [MiddleName], [DOB], [ProtectedFlag]) VALUES (12345, CAST(123412345 AS Numeric(9, 0)), N'Smith', N'John', NULL, CAST(N'2002-02-28' AS Date), 0)
GO
INSERT [DLH].[Clients] ([ClientID], [MVID], [LastName], [FirstName], [MiddleName], [DOB], [ProtectedFlag]) VALUES (2345, CAST(234567890 AS Numeric(9, 0)), N'Forest', N'Tricia', NULL, CAST(N'1996-05-20' AS Date), 0)
GO
INSERT [DLH].[Clients] ([ClientID], [MVID], [LastName], [FirstName], [MiddleName], [DOB], [ProtectedFlag]) VALUES (3456, CAST(345678901 AS Numeric(9, 0)), N'O''Neil', N'Garret', N'James', CAST(N'1935-02-01' AS Date), 0)
GO
INSERT [DLH].[Clients] ([ClientID], [MVID], [LastName], [FirstName], [MiddleName], [DOB], [ProtectedFlag]) VALUES (4567, CAST(456789012 AS Numeric(9, 0)), N'Cartwright', N'Olivia', N'C', CAST(N'2001-12-15' AS Date), 0)
GO
INSERT [DLH].[DLHistoryInfo] ([DLHisInfoID], [MVID], [ServiceType], [IssueDate], [LicClassID]) VALUES (1, CAST(123456789 AS Numeric(9, 0)), N'O-RENEW', CAST(N'2023-01-01' AS Date), 101)
GO
INSERT [DLH].[DLHistoryInfo] ([DLHisInfoID], [MVID], [ServiceType], [IssueDate], [LicClassID]) VALUES (2, CAST(123456789 AS Numeric(9, 0)), N'O-RECL', CAST(N'2023-01-15' AS Date), 102)
GO
INSERT [DLH].[DLHistoryInfo] ([DLHisInfoID], [MVID], [ServiceType], [IssueDate], [LicClassID]) VALUES (5, CAST(223456789 AS Numeric(9, 0)), N'renewal', CAST(N'2023-01-11' AS Date), 101)
GO
INSERT [DLH].[DLHistoryInfo] ([DLHisInfoID], [MVID], [ServiceType], [IssueDate], [LicClassID]) VALUES (6, CAST(234567890 AS Numeric(9, 0)), N'O-RENEW', CAST(N'2012-05-20' AS Date), 303)
GO
INSERT [DLH].[DLHistoryInfo] ([DLHisInfoID], [MVID], [ServiceType], [IssueDate], [LicClassID]) VALUES (7, CAST(345678901 AS Numeric(9, 0)), N'O-RENEW', CAST(N'2016-02-01' AS Date), 801)
GO

/****** Object:  Index [PK_CLIENTS]    Script Date: 2023-01-26 2:08:06 PM ******/
ALTER TABLE [DLH].[Clients] ADD  CONSTRAINT [PK_CLIENTS] PRIMARY KEY NONCLUSTERED 
(
	[ClientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK_CONDITION]    Script Date: 2023-01-26 2:08:06 PM ******/
ALTER TABLE [DLH].[Condition] ADD  CONSTRAINT [PK_CONDITION] PRIMARY KEY NONCLUSTERED 
(
	[CondID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK_DLHISTORYINFO]    Script Date: 2023-01-26 2:08:06 PM ******/
ALTER TABLE [DLH].[DLHistoryInfo] ADD  CONSTRAINT [PK_DLHISTORYINFO] PRIMARY KEY NONCLUSTERED 
(
	[DLHisInfoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK_LICENCE]    Script Date: 2023-01-26 2:08:06 PM ******/
ALTER TABLE [DLH].[Licence] ADD  CONSTRAINT [PK_LICENCE] PRIMARY KEY NONCLUSTERED 
(
	[LicenceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [AK_KEY_MVID]    Script Date: 2023-01-26 2:08:06 PM ******/
ALTER TABLE [DLH].[Licence] ADD  CONSTRAINT [AK_KEY_MVID] UNIQUE NONCLUSTERED 
(
	[MVID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK_LICENCECLASS]    Script Date: 2023-01-26 2:08:06 PM ******/
ALTER TABLE [DLH].[LicenceClass] ADD  CONSTRAINT [PK_LICENCECLASS] PRIMARY KEY NONCLUSTERED 
(
	[LicClassID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK_LICENCECONDITION]    Script Date: 2023-01-26 2:08:06 PM ******/
ALTER TABLE [DLH].[LicenceCondition] ADD  CONSTRAINT [PK_LICENCECONDITION] PRIMARY KEY NONCLUSTERED 
(
	[LicCondID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK_LICENCEDETAILS]    Script Date: 2023-01-26 2:08:06 PM ******/
ALTER TABLE [DLH].[LicenceDetails] ADD  CONSTRAINT [PK_LICENCEDETAILS] PRIMARY KEY NONCLUSTERED 
(
	[LicDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [AK_KEY_LICCOND]    Script Date: 2023-01-26 2:08:06 PM ******/
ALTER TABLE [DLH].[LicenceDetails] ADD  CONSTRAINT [AK_KEY_LICCOND] UNIQUE NONCLUSTERED 
(
	[LicCond] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [DLH].[Clients]  WITH CHECK ADD  CONSTRAINT [FK_Client_LICENCE_MVID] FOREIGN KEY([MVID])
REFERENCES [DLH].[Licence] ([MVID])
GO
ALTER TABLE [DLH].[Clients] CHECK CONSTRAINT [FK_Client_LICENCE_MVID]
GO
ALTER TABLE [DLH].[DLHistoryInfo]  WITH CHECK ADD  CONSTRAINT [FK_Product_CLIENT_MVID] FOREIGN KEY([MVID])
REFERENCES [DLH].[Licence] ([MVID])
GO
ALTER TABLE [DLH].[DLHistoryInfo] CHECK CONSTRAINT [FK_Product_CLIENT_MVID]
GO
ALTER TABLE [DLH].[DLHistoryInfo]  WITH CHECK ADD  CONSTRAINT [FK_Product_LICCLASS] FOREIGN KEY([LicClassID])
REFERENCES [DLH].[LicenceClass] ([LicClassID])
GO
ALTER TABLE [DLH].[DLHistoryInfo] CHECK CONSTRAINT [FK_Product_LICCLASS]
GO
ALTER TABLE [DLH].[LicenceCondition]  WITH CHECK ADD  CONSTRAINT [FK_LicCond_COND] FOREIGN KEY([CondID])
REFERENCES [DLH].[Condition] ([CondID])
GO
ALTER TABLE [DLH].[LicenceCondition] CHECK CONSTRAINT [FK_LicCond_COND]
GO
ALTER TABLE [DLH].[LicenceCondition]  WITH CHECK ADD  CONSTRAINT [FK_LicCond_LICDETAILS] FOREIGN KEY([LicCond])
REFERENCES [DLH].[LicenceDetails] ([LicCond])
GO
ALTER TABLE [DLH].[LicenceCondition] CHECK CONSTRAINT [FK_LicCond_LICDETAILS]
GO
ALTER TABLE [DLH].[LicenceDetails]  WITH CHECK ADD  CONSTRAINT [FK_LicDetails_LICCLASS] FOREIGN KEY([LicClassID])
REFERENCES [DLH].[LicenceClass] ([LicClassID])
GO
ALTER TABLE [DLH].[LicenceDetails] CHECK CONSTRAINT [FK_LicDetails_LICCLASS]
GO
ALTER TABLE [DLH].[LicenceDetails]  WITH CHECK ADD  CONSTRAINT [FK_LicDetails_LICENCE_MVID] FOREIGN KEY([MVID])
REFERENCES [DLH].[Licence] ([MVID])
GO
ALTER TABLE [DLH].[LicenceDetails] CHECK CONSTRAINT [FK_LicDetails_LICENCE_MVID]
GO