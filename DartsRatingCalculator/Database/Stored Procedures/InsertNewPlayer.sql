/****** Object:  StoredProcedure [dbo].[InsertNewPlayer]    Script Date: 7/29/2015 1:06:35 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertNewPlayer]
(
	@PlayerId			int,
	@PlayerName			nvarchar(50),
	@SquadId			int,
	@RatingMean			numeric(19,5),
	@RatingDeviation	numeric(19,5)
)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
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

