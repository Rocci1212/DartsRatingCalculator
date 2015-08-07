SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[CommitCampaign]
(
	@Season int,
	@Year int,
	@Class int,
	@Conference int,
	@Identifier int = null
)
AS
BEGIN
	SET NOCOUNT ON;

    insert into Campaign (Season, Year, Class, Conference, Identifier)
	select @Season, @Year, @Class, @Conference, @Identifier
	where not exists
	(
		SELECT 1 from Campaign where Season = @Season and Year = @Year and Class = @Class and Conference = @Conference and isnull(Identifier, -1) = isnull(@Identifier, -1)
	)

	select * from Campaign where Season = @Season and Year = @Year and Class = @Class and Conference = @Conference and isnull(Identifier, -1) = isnull(@Identifier, -1)
END


GO

