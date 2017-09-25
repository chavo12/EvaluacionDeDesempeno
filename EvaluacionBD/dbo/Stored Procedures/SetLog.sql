CREATE procedure [dbo].[SetLog]
 @IdEmpleado int,
 @pagina varchar(100),
 @detalle varchar(max) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	insert Logs(IdEmpleado,Pagina,Detalle,FechayHora)
	values(@IdEmpleado,@pagina,@detalle,getdate())

END