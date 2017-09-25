$(function () {
    load();

});

function load() {
    $("#grid-data").bootgrid({
        caseSensitive: false,
        labels: {
            noResults: "No hay resultados",
            infos: "{{ctx.start}} a {{ctx.end}} de {{ctx.total}} filas",
            search: "Buscar"
        },
        converters: {
            datetime: {
                from: function (value) {
                    var fech = moment(value, "DD/MM/YYYY");
                    if (fech && fech.isValid()) return fech;
                },
                to: function (value) { if (value && value.isValid()) return value.locale('es').format("l"); else return ""; }
            }
        },
        css: {
            pagination: "pagination pagination-sm", table: "table table-condensed table-hover table-striped wrap table-bordered"
        },
        formatters: {
            "editar": function (column, row) {
                
                if (row.estado == "Autoevaluación") return "<a class=\"btn btn-info\" role=\"button\" href=\"#\" title=\"Descargar Evaluación\"  onclick=\"javascript:toPdf('0','" + row.IdEmpleado.trim() + "','5','0','1');\"><span class=\"glyphicon glyphicon-download-alt\"></span></a>"
                else if (row.estado == "Finalizada") return "<a class=\"btn btn-info\" role=\"button\" href=\"home.aspx?idEmpleado=" + row.IdEmpleado.trim() + "\"><span class=\"glyphicon glyphicon-eye-open\"></span></a>" +
                                                            "<a class=\"btn btn-info\" role=\"button\" href=\"#\" title=\"Descargar Resumen\" onclick=\"javascript:toResumen(" + row.IdEmpleado.trim() + ");\"><span class=\"glyphicon glyphicon-stats\"></span></a>" +
                                                            "<a class=\"btn btn-info\" role=\"button\" href=\"#\" title=\"Descargar Evaluación\"  onclick=\"javascript:toPdf('0','" + row.IdEmpleado.trim() + "','5','0','1');\"><span class=\"glyphicon glyphicon-download-alt\"></span></a>";
                else return "<a class=\"btn btn-info\" role=\"button\" href=\"home.aspx?idEmpleado=" + row.IdEmpleado.trim() + "\"><span class=\"glyphicon glyphicon-pencil\"></span></a>" +
                        "<a class=\"btn btn-info\" role=\"button\" href=\"#\" title=\"Descargar Evaluación\"  onclick=\"javascript:toPdf('0','" + row.IdEmpleado.trim() + "','5','0','1');\"><span class=\"glyphicon glyphicon-download-alt\"></span></a>";
            }
        },
    });
}

function excel() {
    window.open("/excelempleados.aspx?idSupervisor=" + $('#idsuper').val());
}