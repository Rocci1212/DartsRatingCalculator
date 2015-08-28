SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[GetPlayerGameHistory]
(
    @PlayerID int,
	@RatingType nvarchar(8) = '',
	@Param1 int = -1
)
As
    Set nocount on;

	select
		case
			when Game.Player = gameInfo.Player then 'P'
			when game.Result = gameInfo.Result then 'T'
			else 'O'
		end as playerType,
		gameinfo.Match,
		gameinfo.GameNumber,
		Squad.ID as Squad,
		Squad.Name as SquadName,
		ROW_NUMBER() over (
			partition by 
				gameInfo.match, 
				gameInfo.GameNumber, 
				case when Game.Player = gameInfo.Player then 'P' when game.Result = gameInfo.Result then 'T' else 'O' end
			order by
				gameInfo.Match,
				gameInfo.GameNumber,
				case
					when Game.Player = gameInfo.Player then 'A'
					when game.Result = gameInfo.Result then 'B'
					else 'C'
				end,
				gameInfo.Player
		) as participantIndex,
		Enumeration.Value as GameType,
		game.Result,
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
		end as PostRatingMean,
		case
			when @RatingType = 'class' then gameInfo.PostClassRatingDeviation
			when @RatingType = 'campaign' then gameInfo.PostCampaignRatingDeviation
			else gameInfo.PostRatingDeviation
		end as PostRatingDeviation
	into
		#gameInfo
	from 
		game inner join game gameInfo 
		on game.Match = gameInfo.Match and game.GameNumber = gameInfo.GameNumber

		inner join player
		on gameInfo.Player = Player.id

		inner join Enumeration
		on gameInfo.GameType = Enumeration.Code and Enumeration.Type = 'gametype'

		inner join Match
		on game.Match = Match.ID

		inner join SquadPlayer
		on gameInfo.Player = SquadPlayer.Player

		inner join Squad
		on SquadPlayer.Squad = Squad.id and Match.Campaign = Squad.Campaign

		inner join Campaign
		on Match.Campaign = Campaign.ID
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
		and
		((@Param1 = -1) or (@RatingType = 'class' and Campaign.Class = @Param1) or (@RatingType = 'campaign' and Match.Campaign = @Param1))
	order by
		gameInfo.Match,
		gameInfo.GameNumber,
		case
			when Game.Player = gameInfo.Player then 'A'
			when game.Result = gameInfo.Result then 'B'
			else 'C'
		end,
		gameInfo.Player

	select
		P1.Match, P1.GameNumber, O1.Squad, O1.SquadName, P1.GameType, P1.Result, P1.Player, P1.Name, 
		round((P1.PreRatingMean - P1.PreRatingDeviation) * 60, 0) as PreRating, round((P1.PostRatingMean - P1.PostRatingDeviation) * 60, 0) as PostRating,

		-- i am the absolute fucking worst.
		T1.Player as T1Player, 
		T1.Name + ' (' + replace(cast(
			round((T1.PreRatingMean - T1.PreRatingDeviation) * 60, 0) 
		as nvarchar(20)), '.00000', '') + 
		case when P1.Result = 1 then '+' else '-' end + 
		replace(cast(
			abs(round((T1.PostRatingMean - T1.PostRatingDeviation - T1.PreRatingMean + T1.PreRatingDeviation) * 60, 0))
		as nvarchar(20)), '.00000', '') + ')' as T1Name, 

		T2.Player as T2Player, 
		T2.Name + ' (' + replace(cast(
			round((T2.PreRatingMean - T2.PreRatingDeviation) * 60, 0) 
		as nvarchar(20)), '.00000', '') + 
		case when P1.Result = 1 then '+' else '-' end + 
		replace(cast(
			abs(round((T2.PostRatingMean - T2.PostRatingDeviation - T2.PreRatingMean + T2.PreRatingDeviation) * 60, 0))
		as nvarchar(20)), '.00000', '') + ')' as T2Name, 

		O1.Player as O1Player, 
		O1.Name + ' (' + replace(cast(
			round((O1.PreRatingMean - O1.PreRatingDeviation) * 60, 0) 
		as nvarchar(20)), '.00000', '') + 
		case when P1.Result = 0 then '+' else '-' end + 
		replace(cast(
			abs(round((O1.PostRatingMean - O1.PostRatingDeviation - O1.PreRatingMean + O1.PreRatingDeviation) * 60, 0))
		as nvarchar(20)), '.00000', '') + ')' as O1Name, 

		O2.Player as O2Player, 
		O2.Name + ' (' + replace(cast(
			round((O2.PreRatingMean - O2.PreRatingDeviation) * 60, 0) 
		as nvarchar(20)), '.00000', '') + 
		case when P1.Result = 0 then '+' else '-' end + 
		replace(cast(
			abs(round((O2.PostRatingMean - O2.PostRatingDeviation - O2.PreRatingMean + O2.PreRatingDeviation) * 60, 0))
		as nvarchar(20)), '.00000', '') + ')' as O2Name, 
 
		O3.Player as O3Player, 
		O3.Name + ' (' + replace(cast(
			round((O3.PreRatingMean - O3.PreRatingDeviation) * 60, 0) 
		as nvarchar(20)), '.00000', '') + 
		case when P1.Result = 0 then '+' else '-' end + 
		replace(cast(
			abs(round((O3.PostRatingMean - O3.PostRatingDeviation - O3.PreRatingMean + O3.PreRatingDeviation) * 60, 0))
		as nvarchar(20)), '.00000', '') + ')' as O3Name

		--T1.Player as T1Player, T1.Name as T1Name, round((T1.PreRatingMean - T1.PreRatingDeviation) * 60, 0) as T1Rating, 
		--T2.Player as T2Player, T2.Name as T2Name, round((T2.PreRatingMean - T2.PreRatingDeviation) * 60, 0) as T2Rating, 
		--O1.Player as O1Player, O1.Name as O1Name, round((O1.PreRatingMean - O1.PreRatingDeviation) * 60, 0) as O1Rating, 
		--O2.Player as O2Player, O2.Name as O2Name, round((O2.PreRatingMean - O2.PreRatingDeviation) * 60, 0) as O2Rating, 
		--O3.Player as O3Player, O3.Name as O3Name, round((O3.PreRatingMean - O3.PreRatingDeviation) * 60, 0) as O3Rating
	from
		(select * from #gameInfo where playerType = 'P') P1 
		
		left outer join (select * from #gameInfo where playerType = 'T' and participantIndex = 1) T1
		on P1.Match = T1.Match and P1.GameNumber = T1.GameNumber

		left outer join (select * from #gameInfo where playerType = 'T' and participantIndex = 2) T2
		on P1.Match = T2.Match and P1.GameNumber = T2.GameNumber

		left outer join (select * from #gameInfo where playerType = 'O' and participantIndex = 1) O1
		on P1.Match = O1.Match and P1.GameNumber = O1.GameNumber

		left outer join (select * from #gameInfo where playerType = 'O' and participantIndex = 2) O2
		on P1.Match = O2.Match and P1.GameNumber = O2.GameNumber

		left outer join (select * from #gameInfo where playerType = 'O' and participantIndex = 3) O3
		on P1.Match = O3.Match and P1.GameNumber = O3.GameNumber
	order by 
		P1.Match, P1.GameNumber
GO

