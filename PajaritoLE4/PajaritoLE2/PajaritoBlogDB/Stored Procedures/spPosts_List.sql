CREATE PROCEDURE [dbo].[spPosts_List]
AS
begin
	set nocount on;

	SELECT [p].[Id], [p].[Title], [p].[Body], [p].[DateCreated], [u].[UserName], [u].[firstName], [u].[LastName]
	FROM dbo.Posts p
	INNER JOIN dbo.Users u
	On p.UserId = u.Id
end
RETURN 0