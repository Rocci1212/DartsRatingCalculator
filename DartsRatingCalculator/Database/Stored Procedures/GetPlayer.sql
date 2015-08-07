SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetPlayer]
(
	@PlayerID int
)
AS
BEGIN
	SET NOCOUNT ON;

    select top 1
		*
	from
		Player
	where
		ID = @PlayerID
END


GO

