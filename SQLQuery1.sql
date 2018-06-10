select * from Bono
select * from Bono_Tasa


DELETE FROM [dbo].[Bono]
      WHERE BonoID = 9
GO


INSERT INTO [dbo].[Bono_Tasa]
           ([TipoTasa_ID]
           ,[Bono_BonoID]
           ,[TasaInteres]
           ,[NroCuota]
           ,[capitalizacion])
     VALUES
           (2,1,123,1,1)
GO

USE [BonoCorpAleman]
GO

ALTER TABLE [dbo].[Bono_Tasa] DROP CONSTRAINT [Bono_Tasa_TipoTasa]
GO

ALTER TABLE [dbo].[Bono_Tasa] DROP CONSTRAINT [Bono_Tasa_Capitalizacion]
GO

ALTER TABLE [dbo].[Bono_Tasa] DROP CONSTRAINT [Bono_Tasa_Bono]
GO

/****** Object:  Table [dbo].[Bono_Tasa]    Script Date: 6/10/2018 02:53:21 ******/
DROP TABLE [dbo].[Bono_Tasa]
GO

/****** Object:  Table [dbo].[Bono_Tasa]    Script Date: 6/10/2018 02:53:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Bono_Tasa](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TipoTasa_ID] [int] NOT NULL,
	[Bono_BonoID] [bigint] NOT NULL,
	[TasaInteres] [int] NOT NULL,
	[NroCuota] [int] NOT NULL,
	[capitalizacion] [int] NULL,
 CONSTRAINT [Bono_Tasa_pk] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Bono_Tasa]  WITH CHECK ADD  CONSTRAINT [Bono_Tasa_Bono] FOREIGN KEY([Bono_BonoID])
REFERENCES [dbo].[Bono] ([BonoID])
GO

ALTER TABLE [dbo].[Bono_Tasa] CHECK CONSTRAINT [Bono_Tasa_Bono]
GO

ALTER TABLE [dbo].[Bono_Tasa]  WITH CHECK ADD  CONSTRAINT [Bono_Tasa_Capitalizacion] FOREIGN KEY([capitalizacion])
REFERENCES [dbo].[Capitalizacion] ([ID])
GO

ALTER TABLE [dbo].[Bono_Tasa] CHECK CONSTRAINT [Bono_Tasa_Capitalizacion]
GO

ALTER TABLE [dbo].[Bono_Tasa]  WITH CHECK ADD  CONSTRAINT [Bono_Tasa_TipoTasa] FOREIGN KEY([TipoTasa_ID])
REFERENCES [dbo].[TipoTasa] ([ID])
GO

ALTER TABLE [dbo].[Bono_Tasa] CHECK CONSTRAINT [Bono_Tasa_TipoTasa]
GO

