/****** Object:  StoredProcedure [dbo].[UpdateSquadInfo]    Script Date: 7/29/2015 1:06:07 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateSquadInfo]
(
	@SquadId	int,
	@TeamName	nvarchar(50),
	@Sponsor	nvarchar(50),
	@City		nvarchar(50)
)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	update Squad set Name = @TeamName, Sponsor = @Sponsor, City = @City where ID = @SquadId
END


GO

