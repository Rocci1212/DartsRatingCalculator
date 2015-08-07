SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[CommitGame]
(
	@Match int,
	@GameNumber int,
	@GameType int,
	@Player int,
	@Result int,
	@NewRatingMean numeric(19,5),
	@NewRatingDeviation numeric(19,5),
	@RatingToAdjust nvarchar(8) -- initial, player, class, or campaign
)

AS
BEGIN
	SET NOCOUNT ON;

    if @RatingToAdjust = 'initial'
	begin
		insert into Game
		select
			@Match,
			@GameNumber,
			@GameType,
			@Player,
			@Result,
			Player.RatingMean,
			Player.RatingDeviation,
			PlayerCampaignRating.RatingMean,
			PlayerCampaignRating.RatingDeviation,
			PlayerClassRating.RatingMean,
			PlayerClassRating.RatingDeviation,
			Player.RatingMean,
			Player.RatingDeviation,
			PlayerCampaignRating.RatingMean,
			PlayerCampaignRating.RatingDeviation,
			PlayerClassRating.RatingMean,
			PlayerClassRating.RatingDeviation
		from
			Player inner join Match
			on Match.ID = @Match

			inner join Campaign
			on Match.Campaign = Campaign.ID
			
			inner join PlayerCampaignRating
			on Player.ID = PlayerCampaignRating.Player and PlayerCampaignRating.Campaign = Campaign.ID

			inner join PlayerClassRating
			on Player.ID = PlayerClassRating.Player and PlayerClassRating.Class = Campaign.Class
		where
			Player.ID = @Player

		update
			Player
		set
			Wins = case when @Result = 1 then Wins + 1 else Wins end,
			Losses = case when @Result = 0 then Losses + 1 else Losses end
		where
			Player.ID = @Player

		update
			PlayerCampaignRating
		set
			Wins = case when @Result = 1 then Wins + 1 else Wins end,
			Losses = case when @Result = 0 then Losses + 1 else Losses end
		from
			Match inner join PlayerCampaignRating
			on Match.Campaign = PlayerCampaignRating.Campaign
		where
			PlayerCampaignRating.Player = @Player and Match.ID = @Match

		update
			PlayerClassRating
		set
			Wins = case when @Result = 1 then Wins + 1 else Wins end,
			Losses = case when @Result = 0 then Losses + 1 else Losses end
		from
			Match inner join Campaign
			on Match.Campaign = Campaign.ID

			inner join PlayerClassRating
			on Campaign.Class = PlayerClassRating.Class
		where
			PlayerClassRating.Player = @Player and Match.ID = @Match
	end
	if @RatingToAdjust = 'player'
	begin
		update
			Game
		set
			PostRatingMean = @NewRatingMean,
			PostRatingDeviation = @NewRatingDeviation
		where
			Match = @Match and GameNumber = @GameNumber and GameType = @GameType and Player = @Player

		update
			Player
		set
			RatingMean = @NewRatingMean,
			RatingDeviation = @NewRatingDeviation
		where
			ID = @Player
	end

	if @RatingToAdjust = 'campaign'
	begin
		update
			Game
		set
			PostCampaignRatingMean = @NewRatingMean,
			PostCampaignRatingDeviation = @NewRatingDeviation
		where
			Match = @Match and GameNumber = @GameNumber and GameType = @GameType and Player = @Player

		update
			PlayerCampaignRating
		set
			RatingMean = @NewRatingMean,
			RatingDeviation = @NewRatingDeviation
		from
			Match inner join PlayerCampaignRating
			on Match.Campaign = PlayerCampaignRating.Campaign
		where
			Player = @Player and Match.ID = @Match
	end

	if @RatingToAdjust = 'class'
	begin
		update
			Game
		set
			PostClassRatingMean = @NewRatingMean,
			PostClassRatingDeviation = @NewRatingDeviation
		where
			Match = @Match and GameNumber = @GameNumber and GameType = @GameType and Player = @Player

		update
			PlayerClassRating
		set
			RatingMean = @NewRatingMean,
			RatingDeviation = @NewRatingDeviation
		from
			Match inner join Campaign
			on Match.Campaign = Campaign.ID

			inner join PlayerClassRating
			on Campaign.Class = PlayerClassRating.Class
		where
			Player = @Player and Match.ID = @Match
	end
END


GO

