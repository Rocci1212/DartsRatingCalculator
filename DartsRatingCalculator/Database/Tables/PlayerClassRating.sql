/****** Object:  Table [dbo].[PlayerClassRating]    Script Date: 7/29/2015 1:03:25 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PlayerClassRating](
	[Player] [int] NOT NULL,
	[Class] [int] NOT NULL,
	[Wins] [int] NOT NULL CONSTRAINT [DF_PlayerClassRating_Wins]  DEFAULT ((0)),
	[Losses] [int] NOT NULL CONSTRAINT [DF_PlayerClassRating_Losses]  DEFAULT ((0)),
	[RatingMean] [numeric](19, 5) NOT NULL CONSTRAINT [DF_PlayerClassRating_RatingMean]  DEFAULT ((25)),
	[RatingDeviation] [numeric](19, 5) NOT NULL CONSTRAINT [DF_PlayerClassRating_RatingDeviation]  DEFAULT ((8.33333)),
 CONSTRAINT [PK_PlayerClassRating] PRIMARY KEY CLUSTERED 
(
	[Player] ASC,
	[Class] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

