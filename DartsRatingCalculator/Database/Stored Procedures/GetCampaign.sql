SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetCampaign]
(
	@CampaignID int
)
AS
BEGIN
	SET NOCOUNT ON;

    select top 1
		*
	from
		Campaign
	where
		ID = @CampaignID
END


GO

