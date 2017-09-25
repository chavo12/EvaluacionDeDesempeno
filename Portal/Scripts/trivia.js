

function responderResponsabilidad(i) {

    //var valorControl = document.getElementById(control).value;
    var escrito = $('#escrito' + i).val();
    var result = true;
    var valor = "1";
    var id = $('#id' + i).val();
    $.ajax
                ({
                    url: "AjaxResponder.aspx",
                    type: "POST",
                    dataType: 'html',
                    async: false,
                    data:
                    {
                        idRespuesta: id,
                        supervisor: "NO",
                        valor: valor,
                        escrito: escrito,
                        finalizar: "NO"

                    },
                    success: function (ok) {
                        if (ok != "ok") {
                            alert("La selección no pudo ser guardada");
                            result = false;

                        }
                        }
                });
    return result;
}

function responderCompetenecia(i, supervisor, finaliza) {

    //var valorControl = document.getElementById(control).value;
    var escrito = "";
    var result = true;
    var valor = $("#valor" + i).val();
    var id = $('#id' + i).val();
    supervisor = $('#hdSupervisa').val();
    $.ajax
                ({
                    url: "AjaxResponder.aspx",
                    type: "POST",
                    dataType: 'html',
                    async: false,
                    data:
                    {
                        idRespuesta: id,
                        supervisor: supervisor,
                        valor: valor,
                        escrito: escrito,
                        finalizar: finaliza

                    },
                    success: function (ok) {
                        if (ok != "ok") {
                            result = false;
                            alert("La selección no pudo ser guardada");
                        }
                    }
                });
    return result;
}

function responderOportunidad() {

    //var valorControl = document.getElementById(control).value;
    var escrito = $("#escrito").val();
    var valor = "1";
    var result = true;
    var id = $("#id").val();
    $.ajax
                ({
                    url: "AjaxResponder.aspx",
                    type: "POST",
                    dataType: 'html',
                    async: false,
                    data:
                    {
                        idRespuesta: id,
                        supervisor: "NO",
                        valor: valor,
                        escrito: escrito,
                        finalizar: "NO"

                    },
                    success: function (ok) {
                        if (ok != "ok") {
                            alert("La selección no pudo ser guardada");
                            result = false;
                        }
                    }
                });
    return result;
}

function responderDesempeno(supervisa,finaliza) {

    //var valorControl = document.getElementById(control).value;
    supervisa = $('#hdSupervisa').val();
    var escrito;
    var result = true;
    if (supervisa == 'NO') {
        escrito = $("#escrito").val();
    }
    else {
        escrito = $("#escritoSuper").val() + "|" + $("#fortaleza").val() + "|" + $("#oportunidad").val();
    }
    var valor = $("#valor1").val();
    var id = $("#id").val();
    
    var idEval = $("#idEval").val();
    var result = false;
    $.ajax
                ({
                    url: "AjaxResponder.aspx",
                    type: "POST",
                    dataType: 'html',
                    async: false,
                    data:
                    {
                        idRespuesta: id,
                        supervisor: supervisa,
                        valor: valor,
                        escrito: escrito,
                        finalizar: finaliza,
                        idEvaluacion: idEval

                    },
                    success: function (ok) {
                        if (ok != "ok") {
                            if (ok == "Debe completar toda la evalucación antes de finalizarla")
                                alert("Debe completar toda la evalucación antes de finalizarla");
                            else
                                if (ok.indexOf('completa') > 0) alert(ok);
                                else alert("La selección no pudo ser guardada");
                            result = false;
                        }
                        else result = true;
                    }
                });
    return result;
}

function marcarValor(valor,id)
{
    $('#valor' + id).val(valor);
}

function siguiente(id)
{
    $('.itemDiv').hide();
    $('#item' + id).show();
}

function toPdf(id, idEmpleado,tipo,pos,supervisor)
{
    window.open("/pdfDesempeno.aspx?idEmpleado=" + idEmpleado + "&idRespuesta=" + id + "&tipo=" + tipo + "&pos=" + pos + "&supervisor=" + supervisor);
}


function toResumen(id) {
    window.open("/pdfResumen.aspx?idEmpleado=" + id);
}

