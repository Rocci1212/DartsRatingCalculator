SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CommitSquad]
(
	@SquadId	int,
	@CampaignId	int
)	
AS
BEGIN
	SET NOCOUNT ON;

    if not exists (select * from Squad where ID = @SquadId)
	insert into Squad(ID) values (@SquadId)

	update Squad set Campaign = @CampaignId where ID = @SquadId
END



GO

