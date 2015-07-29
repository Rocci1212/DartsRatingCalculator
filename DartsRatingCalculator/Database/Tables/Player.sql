/****** Object:  Table [dbo].[Player]    Script Date: 7/29/2015 1:02:43 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Player](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL CONSTRAINT [DF_Player_Name]  DEFAULT (''),
	[Wins] [int] NOT NULL CONSTRAINT [DF_Player_Wins]  DEFAULT ((0)),
	[Losses] [int] NOT NULL CONSTRAINT [DF_Player_Losses]  DEFAULT ((0)),
	[RatingMean] [numeric](19, 5) NOT NULL CONSTRAINT [DF_Player_RatingMean]  DEFAULT ((25)),
	[RatingDeviation] [numeric](19, 5) NOT NULL CONSTRAINT [DF_Player_RatingDeviation]  DEFAULT ((8.33333)),
 CONSTRAINT [PK_Player] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

