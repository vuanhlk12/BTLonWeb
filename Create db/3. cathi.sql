USE [BTLon2]
GO

/****** Object:  Table [dbo].[CaThi]    Script Date: 11/23/2019 8:30:33 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CaThi](
	[CaThiID] [uniqueidentifier] NOT NULL,
	[CaThiIdFake] [nvarchar](50) NULL,
	[CaThiName] [nvarchar](50) NULL,
	[KyThiID] [uniqueidentifier] NULL,
	[Date] [date] NULL,
	[Start] [datetime] NULL,
	[Stop] [datetime] NULL,
 CONSTRAINT [PK_CaThi] PRIMARY KEY CLUSTERED 
(
	[CaThiID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CaThi]  WITH CHECK ADD  CONSTRAINT [fk_CaThi_KyThi] FOREIGN KEY([KyThiID])
REFERENCES [dbo].[KyThi] ([KyThiID])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[CaThi] CHECK CONSTRAINT [fk_CaThi_KyThi]
GO

