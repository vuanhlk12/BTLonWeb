USE [BTLon2]
GO

/****** Object:  Table [dbo].[MonThi]    Script Date: 11/23/2019 8:30:51 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MonThi](
	[MonThiID] [uniqueidentifier] NOT NULL,
	[MonThiIdFake] [nvarchar](50) NULL,
	[MonThiName] [nvarchar](50) NULL,
	[GiaoVien] [nvarchar](50) NULL,
 CONSTRAINT [PK_MonHoc] PRIMARY KEY CLUSTERED 
(
	[MonThiID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

