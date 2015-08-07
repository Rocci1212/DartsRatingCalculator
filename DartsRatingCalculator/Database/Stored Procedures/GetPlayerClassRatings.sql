SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetPlayerClassRatings]
(
	@PlayerID int
)
AS
BEGIN
	SET NOCOUNT ON;

    select 
		*
	from
		PlayerClassRating
	where
		Player = @PlayerID
END



GO

