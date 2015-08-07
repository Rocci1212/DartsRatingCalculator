SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CommitMatch]
(
	@MatchId	int,
	@WeekNumber	nvarchar(50),
	@SquadId	int
)	
AS
BEGIN
	SET NOCOUNT ON;

    if not exists (select * from Match where id = @MatchId)
	begin
		insert into Match(id) values (@MatchId)
	end

	update 
		Match 
	set 
		WeekNumber = @WeekNumber, Campaign = Squad.Campaign
	from
		Squad
	where	
		Squad.ID = @SquadId and Match.ID = @MatchId
END




GO

