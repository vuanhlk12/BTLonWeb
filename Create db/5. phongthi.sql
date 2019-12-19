USE [BTLon2]
GO

/****** Object:  Table [dbo].[PhongThi]    Script Date: 11/23/2019 8:31:04 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PhongThi](
	[PhongThiID] [uniqueidentifier] NOT NULL,
	[PhongThiName] [nvarchar](50) NULL,
	[ComputerNumber] [int] NULL,
 CONSTRAINT [PK_PhongThi] PRIMARY KEY CLUSTERED 
(
	[PhongThiID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

