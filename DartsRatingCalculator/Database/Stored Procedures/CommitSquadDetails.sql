SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CommitSquadDetails]
(
	@SquadId	int,
	@TeamName	nvarchar(50),
	@Sponsor	nvarchar(50),
	@City		nvarchar(50)
)	
AS
BEGIN
	SET NOCOUNT ON;

	update Squad set Name = @TeamName, Sponsor = @Sponsor, City = @City where ID = @SquadId
END




GO

