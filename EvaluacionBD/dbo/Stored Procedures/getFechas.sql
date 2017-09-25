CREATE PROCEDURE [dbo].[getFechas] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select Inicio,Fin,InicioSuper,FinSuper from fechas
END