CREATE PROCEDURE [dbo].[GetResumen] --1914
	@idEmpleado int
AS
BEGIN
	SET NOCOUNT ON;

	select e.Estado, dbo.resumenTipoEvaluacion(e.IdEvaluacion,case when em.Pais = 'Brasil' then 24 else 2 end,0) as comunicacion,
	dbo.resumenTipoEvaluacion(e.IdEvaluacion,case when em.Pais = 'Brasil' then 24 else 2 end,1) as comunicacionSuper,
	dbo.resumenTipoEvaluacion(e.IdEvaluacion,case when em.Pais = 'Brasil' then 25 else 5 end,0) as gestion,
	dbo.resumenTipoEvaluacion(e.IdEvaluacion,case when em.Pais = 'Brasil' then 25 else 5 end,1) as gestionSuper,
	dbo.resumenTipoEvaluacion(e.IdEvaluacion,case when em.Pais = 'Brasil' then 26 else 8 end,0) as orientacion,
	dbo.resumenTipoEvaluacion(e.IdEvaluacion,case when em.Pais = 'Brasil' then 26 else 8 end,1) as orientacionSuper,
	dbo.resumenTipoEvaluacion(e.IdEvaluacion,case when em.Pais = 'Brasil' then 27 else 11 end,0) as satifaccion,
	dbo.resumenTipoEvaluacion(e.IdEvaluacion,case when em.Pais = 'Brasil' then 27 else 11 end,1) as satifaccionSuper,
	dbo.resumenTipoEvaluacion(e.IdEvaluacion,case when em.Pais = 'Brasil' then 28 else 14 end,0) as trabajo,
	dbo.resumenTipoEvaluacion(e.IdEvaluacion,case when em.Pais = 'Brasil' then 28 else 14 end,1) as trabajoSuper,
	dbo.resumenTipoEvaluacion(e.IdEvaluacion,case when em.Pais = 'Brasil' then 29 else 17 end,0) as integridad,
	dbo.resumenTipoEvaluacion(e.IdEvaluacion,case when em.Pais = 'Brasil' then 29 else 17 end,1) as integridadSuper,
	dbo.resumenTipoEvaluacion(e.IdEvaluacion,case when em.Pais = 'Brasil' then 30 else 20 end,0) as desarrollo,
	dbo.resumenTipoEvaluacion(e.IdEvaluacion,case when em.Pais = 'Brasil' then 30 else 20 end,1) as desarrolloSuper,
	dbo.resumenTipoEvaluacion(e.IdEvaluacion,case when em.Pais = 'Brasil' then 31 else 21 end,0) as liderazgo,
	dbo.resumenTipoEvaluacion(e.IdEvaluacion,case when em.Pais = 'Brasil' then 31 else 21 end,1) as liderazgoSuper,
	dbo.resumenTipoEvaluacion(e.IdEvaluacion,case when em.Pais = 'Brasil' then 33 else 32 end,0) as vision,
	dbo.resumenTipoEvaluacion(e.IdEvaluacion,case when em.Pais = 'Brasil' then 33 else 32 end,1) as visionSuper,
	dbo.resumenTipoEvaluacion(e.IdEvaluacion,23,0) as desempeno,
	dbo.resumenTipoEvaluacion(e.IdEvaluacion,23,1) as desempenoSuper
	from Evaluacion e
	inner join Empleados em on em.IdEmpleado = e.IdEmpleado
	where e.IdEmpleado = @idEmpleado

END