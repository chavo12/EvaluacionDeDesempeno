create PROCEDURE [dbo].[GuardarItems] 
	@descripcion varchar(max),
	@idTipoEvaluacion int,
	@Nivel varchar(50),
	@Escrito bit,
	@Idioma varchar(3)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	if(not exists(select 1 from ItemsEvaluacion where idTipoEvalucion = @idTipoEvaluacion and Nivel = @Nivel and Descripcion = @descripcion))
		insert ItemsEvaluacion (Descripcion,idTipoEvalucion,Nivel,Escrito,Idioma)
		values (@descripcion,@idTipoEvaluacion,@Nivel,@Escrito,@Idioma)

END