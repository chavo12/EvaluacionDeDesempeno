$(function () {
    load();
    $(".chosenselect").chosen({ width: "100%" });
    
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
            pagination: "pagination pagination-sm", table: "table table-condensed table-hover table-striped wrap table-bordered letrachica"
        },
        formatters: {
            "borrar": function (column, row) {
                if (row.estado == "Finalizada") return "<a class=\"btn btn-info btn-xs\" role=\"button\" href=\"#\" onclick=\"borrar('" + row.IdEvaluacion + "','" + row.Id + "')\" title=\"Borrar Evaluación\" ><span class=\"glyphicon glyphicon-trash\"></span></a>" + "<a class=\"btn btn-info btn-xs\" role=\"button\" href=\"#\" title=\"Volver a Autoevaluación\" onclick=\"editar('" + row.IdEvaluacion + "','" + row.Id + "')\" ><span class=\"glyphicon glyphicon-repeat\"></span></a>" +
                    "<a class=\"btn btn-info btn-xs\" role=\"button\" title=\"Editar Empleado\"  href=\"/empleado.aspx?idEmpleado=" + row.Id + "\"><span class=\"glyphicon glyphicon-pencil\"></span></a>" +
                    "<a class=\"btn btn-info btn-xs\" role=\"button\" href=\"#\" title=\"Descargar Resumen\" onclick=\"javascript:toResumen(" + row.Id.trim() + ");\"><span class=\"glyphicon glyphicon-stats\"></span></a>" +
                                                            "<a class=\"btn btn-info btn-xs\" role=\"button\" href=\"#\" title=\"Descargar Evaluación\"  onclick=\"javascript:toPdf('0','" + row.Id.trim() + "','5','0','1');\"><span class=\"glyphicon glyphicon-download-alt\"></span></a>";
                else return "<a class=\"btn btn-info btn-xs\" role=\"button\" href=\"#\" onclick=\"borrar('" + row.IdEvaluacion + "','" + row.Id + "')\" title=\"Borrar Evaluación\" ><span class=\"glyphicon glyphicon-trash\"></span></a>" + "<a class=\"btn btn-info btn-xs\" role=\"button\" href=\"#\" title=\"Volver a Autoevaluación\" onclick=\"editar('" + row.IdEvaluacion + "','" + row.Id + "')\" ><span class=\"glyphicon glyphicon-repeat\"></span></a>" +
                    "<a class=\"btn btn-info btn-xs\" role=\"button\" title=\"Editar Empleado\"  href=\"/empleado.aspx?idEmpleado=" + row.Id + "\"><span class=\"glyphicon glyphicon-pencil\"></span></a>" +
                    "<a class=\"btn btn-info btn-xs\" role=\"button\" href=\"#\" title=\"Descargar Evaluación\"  onclick=\"javascript:toPdf('0','" + row.Id.trim() + "','5','0','1');\"><span class=\"glyphicon glyphicon-download-alt\"></span></a>";
            }
        },
    });
      $("#grid-resp").bootgrid({
        caseSensitive: false,
        labels: {
            noResults: "No hay responsabilidades",
            infos: "{{ctx.start}} a {{ctx.end}} de {{ctx.total}} filas",
            search: "Buscar"
        },
        css: {
            pagination: "pagination pagination-sm", table: "table table-condensed table-hover table-striped wrap table-bordered letrachica"
        },
        formatters: {
            "editar": function (column, row) {
                return "<a class=\"btn btn-info btn-xs\" role=\"button\" href=\"#\" onclick=\"borrarResp('" + row.id + "')\" title=\"Borrar\" ><span class=\"glyphicon glyphicon-trash\"></span></a>";
            }
        },
      });
      $("#grid-op").bootgrid({
          caseSensitive: false,
          labels: {
              noResults: "No hay Oportunidades",
              infos: "{{ctx.start}} a {{ctx.end}} de {{ctx.total}} filas",
              search: "Buscar"
          },
          css: {
              pagination: "pagination pagination-sm", table: "table table-condensed table-hover table-striped wrap table-bordered letrachica"
          },
          formatters: {
              "editar": function (column, row) {
                  return "<a class=\"btn btn-info btn-xs\" role=\"button\" href=\"#\" onclick=\"borrarOp('" + row.id + "')\" title=\"Borrar\" ><span class=\"glyphicon glyphicon-trash\"></span></a>";
              }
          },
      });
}

function editar(idEvaluacion,id)
{
    $.ajax
              ({
                  url: "AjaxEditar.aspx",
                  type: "POST",
                  dataType: 'html',
                  async: false,
                  data:
                  {
                      idEvaluacion: idEvaluacion,
                      idAdmin: $('#idadmin').val(),
                      idEmpleado : id,
                      pais: ""
                  },
                  success: function (ok) {
                      alert("El cambio de estado se ha realizado con éxito");
                      $("#divGrilla").html(ok);
                      load();
                      }
              });
    return true;

}

function borrar(idEvaluacion, idAdmin) {
    $.ajax
              ({
                  url: "AjaxBorrar.aspx",
                  type: "POST",
                  dataType: 'html',
                  async: false,
                  data:
                  {
                      idEvaluacion: idEvaluacion,
                      idAdmin: idAdmin,
                      pais: ""
                  },
                  success: function (ok) {
                      alert("La evaluación fue borrada exitosamente");
                      $("#divGrilla").html(ok);
                      load();
                  }
              });
    return true;

}

function verificarEmpelado()
{
    if ($('#nombre').val() == "") {
        alert("Debe ingresar un nombre");
        return false;
    }
    if ($('#papellido').val() == "") {
        alert("Debe ingresar un apellido");
        return false;
    }
    if ($('#ingreso').val() == "") {
        alert("Debe ingresar una fecha de ingreso");
        return false;
    }
    if ($('#numpia').val() == "") {
        alert("Debe ingresar un número de Pia");
        return false;
    }
    if ($('#negocio').val() == "") {
        alert("Debe ingresar un negocio");
        return false;
    }
    if ($('#departamento').val() == "") {
        alert("Debe ingresar un departamento");
        return false;
    }
    if ($('#empleadoid').val() == "") {
        alert("Debe ingresar un empleadoId");
        return false;
    }
    //if ($('#alcance').val() == "") {
    //    alert("Debe ingresar un alcance");
    //    return false;
    //}
    if ($('#nivel').val() == "") {
        alert("Debe ingresar un nivel");
        return false;
    }
    if ($('#correo').val() == "") {
        alert("Debe ingresar un correo electrónico");
        return false;
    }
    if ($('#cargo').val() == "") {
        alert("Debe ingresar un cargo");
        return false;
    }
    if ($('#rol').val() == "") {
        alert("Debe ingresar un rol");
        return false;
    }
    if ($('#pais').val() == "") {
        alert("Debe ingresar un pais");
        return false;
    }
    if ($('#supervisor').val() == "") {
        alert("Debe ingresar un supervisor");
        return false;
    }
    if ($('#tipoempleado').val() == "") {
        alert("Debe ingresar un tipo de empleado");
        return false;
    }
    var result;
    $.ajax
             ({
                 url: "AjaxGuardarEmpleado.aspx",
                 type: "POST",
                 dataType: 'html',
                 async: false,
                 data:
                 {
                     idEmpleado: $('#hdid').val(),
                     idAdmin: $('#hdidAdmin').val(),
                     pais: $('#pais').val(),
                     nombre: $('#nombre').val(),
                     correo: $('#correo').val(),
                     empleadoid: $('#empleadoid').val(),
                     papellido: $('#papellido').val(),
                     sapellido: $('#sapellido').val(),
                     ingreso: $('#ingreso').val(),
                     numpia: $('#numpia').val(),
                     tipoempleado: $('#tipoempleado').val(),
                     negocio: $('#negocio').val(),
                     departamento: $('#departamento').val(),
                     alcance: $('#alcance').val(),
                     cargo: $('#cargo').val(),
                     nivel: $('#nivel').val(),
                     rol: $('#rol').val(),
                     supervisor: $('#supervisor').val(),
                     inicio: $('#inicio').val(),
                     inicioSuper: $('#inicioSuper').val(),
                     fin: $('#fin').val(),
                     finSuper: $('#finSuper').val(),
                     clave: $('#hdClave').val(),
                     resetClave: $('#hdResetClave').val(),
                     fechaResetClave: $('#hdFechaResetClave').val()
                 },
                 success: function (ok) {
                     if (ok == "responsabilidad") {
                         alert('Debe agregar al menos una Responsabilidad al empleado');
                         result = false;
                     }
                     else if (ok == "oportunidad")
                     {
                         alert('Debe agregar al menos una Competencias al empleado');
                         result = false;
                     }
                     else result = ok;
                 }
             });
    return result;
}

function buscar()
{
    var result;
    $.ajax
             ({
                 url: "AjaxBuscar.aspx",
                 type: "POST",
                 dataType: 'html',
                 async: false,
                 data:
                 {
                     idadmin: $('#idadmin').val(),
                     pais: $('#pais').val(),
                     barra: 0,
                     inicio: $('#inicio').val(),
                     fin: $('#fin').val(),
                     estado: $('#estado').val(),
                     supervisorid: $('#supervisor').val(),
                     departamento: $('#departamento').val(),
                 },
                 success: function (ok) {
                     result = ok;
                 }
             });
    $("#divGrilla").html(result);
    $.ajax
             ({
                 url: "AjaxBuscar.aspx",
                 type: "POST",
                 dataType: 'html',
                 async: false,
                 data:
                 {
                     idadmin: $('#idadmin').val(),
                     barra:1,
                     pais: $('#pais').val(),
                     inicio: $('#inicio').val(),
                     fin: $('#fin').val(),
                     estado: $('#estado').val(),
                     supervisorid: $('#supervisor').val(),
                     departamento: $('#departamento').val(),
                 },
                 success: function (ok) {
                     result = ok;
                 }
             });
    $("#estadosBarra").html(result);
    load();
}

function fechaEvaluacion()
{
    if ($('#inicio').val() == "") {
        alert("Debe ingresar una fecha de inicio para la Autoevaluación");
        return false;
    }
    if ($('#fin').val() == "") {
        alert("Debe ingresar una fecha de fin para la Autoevaluación");
        return false;
    }
    if ($('#inicioSuper').val() == "") {
        alert("Debe ingresar una fecha de inicio para el supervisor");
        return false;
    }
    if ($('#finSuper').val() == "") {
        alert("Debe ingresar una fecha de fin para el supervisor");
        return false;
    }

    $.ajax
           ({
               url: "AjaxGuardarFechaEvaluacion.aspx",
               type: "POST",
               dataType: 'html',
               async: false,
               data:
               {
                   inicio: $('#inicio').val(),
                   inicioSuper: $('#inicioSuper').val(),
                   fin: $('#fin').val(),
                   finSuper: $('#finSuper').val()
               },
               success: function (ok) {
                   result = ok;
               }
           });

}


function agregarResp() {
    if ($('#descrip').val().trim() == "") {
        alert("Debe ingresar una responsabilidad");
        return false;
    }

    $.ajax
           ({
               url: "AjaxResponsabilidad.aspx",
               type: "POST",
               dataType: 'html',
               async: false,
               data:
               {
                   descrip: $('#descrip').val(),
                   idResp: 0
               },
               success: function (ok) {
                   if (ok != 'error') {
                       $('#grillaResp').html(ok);
                       load();
                       $('#descrip').val("");
                   }

               }
           });

}

function borrarResp(id)
{
    $.ajax
          ({
              url: "ajaxBorrarResponsabilidad.aspx",
              type: "POST",
              dataType: 'html',
              async: false,
              data:
              {
                  idItem: id,
              },
              success: function (ok) {
                  if (ok != 'error') {
                      $('#grillaResp').html(ok);
                      load();
                  }

              }
          });
}

function editarResp(id, descrip)
{
    $('#descrip').val(descrip);
    $('#idResp').val(id);
}


function agregarOp() {
    if ($('#descripOp').val().trim() == "") {
        alert("Debe ingresar una Competencia");
        return false;
    }

    $.ajax
           ({
               url: "AjaxOportunidades.aspx",
               type: "POST",
               dataType: 'html',
               async: false,
               data:
               {
                   descrip: $('#descripOp').val(),
                   idTipo: $('#tipoOp').val(),
                   idOp: 0
               },
               success: function (ok) {
                   if (ok != 'error') {
                       $('#grillaOp').html(ok);
                       load();
                       $('#descripOp').val("");
                   }

               }
           });

}

function borrarOp(id) {
    $.ajax
          ({
              url: "ajaxBorrarOportunidad.aspx",
              type: "POST",
              dataType: 'html',
              async: false,
              data:
              {
                  idItem: id,
              },
              success: function (ok) {
                  if (ok != 'error') {
                      $('#grillaOp').html(ok);
                      load();
                  }

              }
          });
}

function excel()
{
    window.open("/excelempleados.aspx?idadmin=" + $('#idadmin').val() + '&pais=' + $('#pais').val() + '&inicio=' + $('#inicio').val() + '&fin=' + $('#fin').val() + '&estado=' + $('#estado').val() + '&supervisorid=' + '' + '&departamento=' + $('#departamento').val(), '_blank');
}

function descargaEvaluaciones()
{
    var res = $('#hdfinalizados').val().split('|');
    res.forEach(evalpdf);
}

function evalpdf (item,index)
{
    window.open("/descargaevalempleados.aspx?id=" + item , '_blank');
}

function borrarEmpleado()
{
        var result;
        $.ajax
                 ({
                     url: "AjaxBorrarEmpleado.aspx",
                     type: "POST",
                     dataType: 'html',
                     async: false,
                     data:
                     {
                         idEmpleado: $('#hdid').val()
                     },
                     success: function (ok) {
                         if (ok != "ok") {
                             alert('Error al eliminar un empleado');
                             result = false;
                         }
                         else result = ok;
                     }
                 });
        return result;
}

function resetClave()
{
    $.ajax
        ({
            url: "AjaxResetClave.aspx",
            type: "POST",
            dataType: 'html',
            async: false,
            data:
            {
                empleadoid: $("#hdid").val()
            },
            success: function (ok) {
                $("#msgCambioOk").show();
            }
        });
}

