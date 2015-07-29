/****** Object:  StoredProcedure [dbo].[InsertNewSquad]    Script Date: 7/29/2015 1:05:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertNewSquad]
(
	@SquadId	int,
	@CampaignId	int
)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    if not exists (select * from Squad where ID = @SquadId)
	insert into Squad(ID) values (@SquadId)

	update Squad set Campaign = @CampaignId where ID = @SquadId
END

GO

