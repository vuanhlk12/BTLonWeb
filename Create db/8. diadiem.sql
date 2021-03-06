USE [BTLon2]
GO

/****** Object:  Table [dbo].[DiaDiem]    Script Date: 11/23/2019 8:32:04 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DiaDiem](
	[DiaDiemID] [uniqueidentifier] NOT NULL,
	[PhongThiID] [uniqueidentifier] NULL,
	[CaMtID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_DiaDiem] PRIMARY KEY CLUSTERED 
(
	[DiaDiemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[DiaDiem]  WITH CHECK ADD  CONSTRAINT [FK_DiaDiem_CaThi_MonThi] FOREIGN KEY([CaMtID])
REFERENCES [dbo].[CaThi_MonThi] ([CaMtID])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[DiaDiem] CHECK CONSTRAINT [FK_DiaDiem_CaThi_MonThi]
GO

ALTER TABLE [dbo].[DiaDiem]  WITH CHECK ADD  CONSTRAINT [FK_DiaDiem_PhongThi] FOREIGN KEY([PhongThiID])
REFERENCES [dbo].[PhongThi] ([PhongThiID])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[DiaDiem] CHECK CONSTRAINT [FK_DiaDiem_PhongThi]
GO

