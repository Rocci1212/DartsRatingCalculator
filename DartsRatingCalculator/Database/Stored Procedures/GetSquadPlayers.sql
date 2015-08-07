SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetSquadPlayers]
(
	@SquadID int = null
)
AS
BEGIN
	SET NOCOUNT ON;

    select 
		*
	from
		SquadPlayer
	where
		Squad = @SquadID
END



GO

