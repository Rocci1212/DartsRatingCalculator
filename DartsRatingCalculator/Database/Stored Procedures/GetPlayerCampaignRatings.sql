SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetPlayerCampaignRatings]
(
	@PlayerID int
)
AS
BEGIN
	SET NOCOUNT ON;

    select 
		*
	from
		PlayerCampaignRating
	where
		Player = @PlayerID
END



GO

