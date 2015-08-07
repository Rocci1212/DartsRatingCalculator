SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[BlindCommitPlayer]
(
	@PlayerName	nvarchar(50),
	@SquadID	int
)
AS
BEGIN
	SET NOCOUNT ON;

    declare @i int;
	select @i = min(id) from Player

	if @i >= -1
	begin
		set @i = -2
	end
	else
	begin
		set @i = @i - 1
	end

	exec CommitPlayer @i, @PlayerName, @SquadID

	select @i
END

GO

