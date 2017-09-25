CREATE FUNCTION [dbo].[armarNombre]
(
	@Nombre varchar(100),
	@1erApellido varchar(50),
	@2doApellido varchar(50)
)
RETURNS varchar(200)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @result varchar(200)

	set @result = @Nombre + ' ' + @1erApellido + isnull(' ' + @2doApellido,'') 

	-- Return the result of the function
	RETURN @result

END