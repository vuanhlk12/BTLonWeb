USE [BTLon2]
GO

/****** Object:  Table [dbo].[SV_DiaDiem]    Script Date: 11/23/2019 8:32:22 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SV_DiaDiem](
	[UserID] [uniqueidentifier] NULL,
	[DiaDiemID] [uniqueidentifier] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[SV_DiaDiem]  WITH CHECK ADD  CONSTRAINT [fk_SVDD_DiaDiem] FOREIGN KEY([DiaDiemID])
REFERENCES [dbo].[DiaDiem] ([DiaDiemID])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[SV_DiaDiem] CHECK CONSTRAINT [fk_SVDD_DiaDiem]
GO

ALTER TABLE [dbo].[SV_DiaDiem]  WITH CHECK ADD  CONSTRAINT [fk_SVDD_SV] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[SV_DiaDiem] CHECK CONSTRAINT [fk_SVDD_SV]
GO

