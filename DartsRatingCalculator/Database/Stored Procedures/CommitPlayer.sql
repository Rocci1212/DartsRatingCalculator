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
			PlayerCampaignRating
		where
			Player = @PlayerId and Campaign = (select top 1 Campaign from Squad where ID = @SquadId)
	)
	begin
		insert into PlayerCampaignRating(Player, Campaign)
		select
			@PlayerId, Campaign
		from
			Squad 
		where
			ID = @SquadId
	end

	if not exists 
	(
		select 
			* 
		from 
			PlayerClassRating
		where
			Player = @PlayerId and Class = (select top 1 Class from Squad inner join Campaign on Squad.Campaign = Campaign.ID where Squad.ID = @SquadId)
	)
	begin
		insert into PlayerClassRating(Player, Class)
		select
			@PlayerId, Campaign.Class
		from
			Squad inner join Campaign 
			on Squad.Campaign = Campaign.ID
		where
			Squad.id = @SquadId
	end
END




GO

