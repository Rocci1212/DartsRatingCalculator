/****** Object:  Table [dbo].[Enumeration]    Script Date: 7/29/2015 12:23:04 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Enumeration](
	[Type] [nvarchar](20) NOT NULL,
	[Code] [int] NOT NULL,
	[Value] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Enumeration] PRIMARY KEY CLUSTERED 
(
	[Type] ASC,
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

INSERT [dbo].[Enumeration] ([Type], [Code], [Value]) VALUES (N'Class', 0, N'SuperA')
GO
INSERT [dbo].[Enumeration] ([Type], [Code], [Value]) VALUES (N'Class', 1, N'A')
GO
INSERT [dbo].[Enumeration] ([Type], [Code], [Value]) VALUES (N'Class', 2, N'B')
GO
INSERT [dbo].[Enumeration] ([Type], [Code], [Value]) VALUES (N'Class', 3, N'C')
GO
INSERT [dbo].[Enumeration] ([Type], [Code], [Value]) VALUES (N'Class', 4, N'D')
GO
INSERT [dbo].[Enumeration] ([Type], [Code], [Value]) VALUES (N'Class', 5, N'E')
GO
INSERT [dbo].[Enumeration] ([Type], [Code], [Value]) VALUES (N'Conference', 1, N'Boston')
GO
INSERT [dbo].[Enumeration] ([Type], [Code], [Value]) VALUES (N'Conference', 2, N'Central')
GO
INSERT [dbo].[Enumeration] ([Type], [Code], [Value]) VALUES (N'Conference', 3, N'NorthShore')
GO
INSERT [dbo].[Enumeration] ([Type], [Code], [Value]) VALUES (N'Conference', 4, N'SouthShore')
GO
INSERT [dbo].[Enumeration] ([Type], [Code], [Value]) VALUES (N'Season', 0, N'Fall')
GO
INSERT [dbo].[Enumeration] ([Type], [Code], [Value]) VALUES (N'Season', 1, N'Spring')
GO
