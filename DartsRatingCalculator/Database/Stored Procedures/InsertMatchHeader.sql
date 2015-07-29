/****** Object:  StoredProcedure [dbo].[InsertNewPlayer]    Script Date: 7/29/2015 6:10:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertMatchHeader]
(
	@MatchId	int,
	@WeekNumber	nvarchar(50),
	@SquadId	int
)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
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


GO


