SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetSquad]
(
	@SquadID int = null,
	@Name nvarchar(50) = null,
	@Campaign int = null
)
AS
BEGIN
	SET NOCOUNT ON;

    select
		*
	from
		Squad
	where
		(@SquadID is null or ID = @SquadID)
		and
		(@Name is null or Name = @Name)
		and
		(@Campaign is null or Campaign = @Campaign)
END


GO

