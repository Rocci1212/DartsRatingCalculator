/****** Object:  StoredProcedure [dbo].[CommitMatch]    Script Date: 8/5/2015 12:29:37 PM ******/
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
		Squad.ID = @SquadId
END


