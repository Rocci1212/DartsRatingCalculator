SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[GetPlayerGameHistory]
(
    @PlayerID int,
	@RatingType nvarchar(8) = ''
)
As
    Set nocount on

	select 
		case
			when Game.Player = gameInfo.Player then 'P'
			when game.Result = gameInfo.Result then 'T'
			else 'O'
		end as playerType,
		gameinfo.Match,
		gameinfo.GameNumber,
		gameinfo.GameType,
		gameinfo.Player, 
		player.Name, 
		case
			when @RatingType = 'class' then gameInfo.PreClassRatingMean
			when @RatingType = 'campaign' then gameInfo.PreCampaignRatingMean
			else gameInfo.PreRatingMean
		end as PreRatingMean,
		case
			when @RatingType = 'class' then gameInfo.PreClassRatingDeviation
			when @RatingType = 'campaign' then gameInfo.PreCampaignRatingDeviation
			else gameInfo.PreRatingDeviation
		end as PreRatingDeviation,
		case 
			when @RatingType = 'class' then gameInfo.PostClassRatingMean
			when @RatingType = 'campaign' then gameInfo.PostCampaignRatingMean
			else gameInfo.PostRatingMean
		end as PostRatingDeviation,
		case
			when @RatingType = 'class' then gameInfo.PostClassRatingDeviation
			when @RatingType = 'campaign' then gameInfo.PostCampaignRatingDeviation
			else gameInfo.PostRatingDeviation
		end as PostRatingDeviation
	from 
		game inner join game gameInfo 
		on game.Match = gameInfo.Match and game.GameNumber = gameInfo.GameNumber

		inner join player
		on gameInfo.Player = Player.id

		--inner join Enumeration
		--on gameInfo.GameType = Enumeration.Code and Enumeration.Type = 'gametype'
	where
		game.Player = @PlayerID and 
		cast(gameInfo.match as nvarchar(8)) + '|' + cast(gameInfo.gamenumber as nvarchar(2)) in
		(
			select 
				cast(match as nvarchar(6)) + '|' + cast(gamenumber as nvarchar(2)) 
			from 
				game 
			where 
				player = @PlayerID
		)
	order by
		gameInfo.Match,
		gameInfo.GameNumber,
		case
			when Game.Player = gameInfo.Player then 'A'
			when game.Result = gameInfo.Result then 'B'
			else 'C'
		end
GO

