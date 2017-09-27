function guardarClave()
{
    clave = $("#txtClave").val();
    claveRep = $("#txtClaveRep").val();

    if (clave.length > 0 && clave == claveRep) {
        $.ajax
         ({
             url: "AjaxCambiarClave.aspx",
            type: "POST",
            dataType: 'html',
            async: false,
            data:
            {
                clave:clave,
                claveRep: claveRep,
                reset: $('#hdReset').val(),
                empleadoid:$("#hdEmp").val()
            },
            success: function (ok) {
                window.location.href = "/logon.aspx";
            }
        });
    }
    else {
        $("#msgError").show();
    }
}