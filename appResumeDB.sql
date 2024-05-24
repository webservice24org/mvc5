USE [appResumeDB]
GO

/****** Object:  Table [dbo].[Employees]    Script Date: 5/23/2024 6:49:45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Employees](
	[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeName] [nvarchar](20) NOT NULL,
	[JoinDate] [datetime] NOT NULL,
	[Picture] [nvarchar](max) NULL,
	[isActive] [bit] NOT NULL,
	[salary] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Employees] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Employees] ADD  DEFAULT ((0)) FOR [salary]
GO


USE [appResumeDB]
GO

/****** Object:  Table [dbo].[QualificationEntries]    Script Date: 5/23/2024 6:51:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[QualificationEntries](
	[QualificationEntryId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[QualificationId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.QualificationEntries] PRIMARY KEY CLUSTERED 
(
	[QualificationEntryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[QualificationEntries]  WITH CHECK ADD  CONSTRAINT [FK_dbo.QualificationEntries_dbo.Employees_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([EmployeeId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[QualificationEntries] CHECK CONSTRAINT [FK_dbo.QualificationEntries_dbo.Employees_EmployeeId]
GO

ALTER TABLE [dbo].[QualificationEntries]  WITH CHECK ADD  CONSTRAINT [FK_dbo.QualificationEntries_dbo.Qualifications_QualificationId] FOREIGN KEY([QualificationId])
REFERENCES [dbo].[Qualifications] ([QualificationId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[QualificationEntries] CHECK CONSTRAINT [FK_dbo.QualificationEntries_dbo.Qualifications_QualificationId]
GO


USE [appResumeDB]
GO

/****** Object:  Table [dbo].[Qualifications]    Script Date: 5/23/2024 6:51:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Qualifications](
	[QualificationId] [int] IDENTITY(1,1) NOT NULL,
	[QualificationName] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_dbo.Qualifications] PRIMARY KEY CLUSTERED 
(
	[QualificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


