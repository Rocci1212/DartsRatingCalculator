/****** Object:  Table [dbo].[Squad]    Script Date: 7/29/2015 1:03:46 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Squad](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL CONSTRAINT [DF_Squad_Name]  DEFAULT (''),
	[Sponsor] [nvarchar](50) NOT NULL CONSTRAINT [DF_Squad_Sponsor]  DEFAULT (''),
	[City] [nvarchar](50) NOT NULL CONSTRAINT [DF_Squad_City]  DEFAULT (''),
	[Campaign] [int] NOT NULL CONSTRAINT [DF_Squad_Campaign]  DEFAULT ((0)),
 CONSTRAINT [PK_Squad] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

