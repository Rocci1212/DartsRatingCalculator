SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CommitPlayer]
(
	@PlayerId			int,
	@PlayerName			nvarchar(50),
	@SquadId			int
)	
AS
BEGIN
	SET NOCOUNT ON;

    if not exists (select * from Player where id = @PlayerId)
	begin
		insert into Player(id, name) values (@PlayerId, @PlayerName)
	end

	if not exists (select * from SquadPlayer where Player = @PlayerId and Squad = @SquadId)
	begin
		insert into SquadPlayer select @SquadId, @PlayerId
	end

	if not exists 
	(
		select 
			* 
		from 
			Player inner join SquadPlayer 
			on Player.ID = SquadPlayer.Player 
	
			inner join Squad 
			on SquadPlayer.Squad = Squad.ID 
	
			inner join PlayerCampaignRating 
			on Player.ID = PlayerCampaignRating.Player and Squad.Campaign = PlayerCampaignRating.Campaign 
		where
			SquadPlayer.squad = @SquadId
	)
	begin
		insert into PlayerCampaignRating(Player, Campaign)
		select
			Player.ID, Squad.Campaign
		from
			Player inner join SquadPlayer 
			on Player.ID = SquadPlayer.Player
	
			inner join Squad 
			on SquadPlayer.Squad = Squad.ID 
		where
			SquadPlayer.squad = @SquadId
	end

	if not exists 
	(
		select 
			* 
		from 
			Player inner join SquadPlayer 
			on Player.ID = SquadPlayer.Player 
	
			inner join Squad 
			on SquadPlayer.Squad = Squad.ID 
	
			inner join Campaign 
			on Squad.Campaign = Campaign.ID
			
			inner join PlayerClassRating
			on Campaign.Class = PlayerClassRating.Class 
		where
			SquadPlayer.squad = @SquadId
	)
	begin
		insert into PlayerClassRating(Player, Class)
		select
			Player.ID, Campaign.Class
		from
			Player inner join SquadPlayer 
			on Player.ID = SquadPlayer.Player
	
			inner join Squad 
			on SquadPlayer.Squad = Squad.ID 

			inner join Campaign 
			on Squad.Campaign = Campaign.ID
		where
			SquadPlayer.squad = @SquadId
	end
END



GO

