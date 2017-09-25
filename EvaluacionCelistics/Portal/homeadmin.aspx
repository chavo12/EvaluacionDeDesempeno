<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="homeadmin.aspx.cs" Inherits="homeadmin" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <div class="row">
        <div class="col-md-11">
            <h3>
                <label class="text-info">
                    <asp:Literal ID="litTitulo" Text="Gestion de Empleados" runat="server"></asp:Literal></label></h3>
        </div>
    </div>
    <div class="row">
        <div class="col-md-11">
            <div class="panel panel-info">
                <div class="panel-body">
                    <div class="row">
                         <div class="col-md-4">
                             <label class="text-info">Departamento</label>
                             <select id="departamento" data-placeholder="Departamentos" class="chosenselect"  style="width:100%;">
                                          <asp:Literal ID="litDepartamentos" runat="server"></asp:Literal>
                               </select>
                        </div>
                  
                        <div class="col-md-4">
                             <label class="text-info">Fecha de Inicio</label>
                              <input type="text" id="inicio" class="form-control" placeholder="Fecha de Inicio"/> 
                        </div>
                        <div class="col-md-4">
                             <label class="text-info">Fecha Fin</label>
                            <input type="text" id="fin" class="form-control" placeholder="Fecha de Fin"/> 
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                             
                            <label class="text-info">Estado</label>
                              <select id="estado" data-placeholder="Estado" class="chosenselect"  style="width:100%;">
                                  <option value="">Todos</option>
                                        <option value="Autoevaluación">Autoevaluación</option>
                                  <option value="Enviado al Supervisor">Enviado al Supervisor</option>
                                  <option value="Finalizada">Finalizada</option>
                               </select>
                        </div>
                             <div class="col-md-4">
                             <asp:HiddenField ID="idadmin" ClientIDMode="Static" runat="server" />
                            <label class="text-info">País</label>
                              <select id="pais" data-placeholder="Pais" class="chosenselect"  style="width:100%;">
                                          <asp:Literal ID="litPaisoption" runat="server"></asp:Literal>
                               </select>
                        </div>
                        <div class="col-md-4">

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-11">
                            <a href="#" class="btn btn-info pull-right" onclick="javascript:return buscar();" role="button">Buscar <span class="glyphicon glyphicon-search"></span></a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
   
        </div>

      <div class="row">
        <div class="col-md-11" id="estadosBarra">
            <asp:Literal ID="litEstadosBarra" runat="server"></asp:Literal>
        </div>
    </div>



    <div class="row">
        <div class="col-md-11">
            <asp:Panel ID="pnEmpleados" CssClass="panel panel-info" runat="server">
                <div class="panel-heading">
                    <label class="text-info">
                        <asp:Literal ID="litEmpleados" Text="Empleados" runat="server"></asp:Literal></label>
                      
                </div>
                <div class="panel-body" >
                    <div class="row">
                        <div class="col-md-12">
                            <a href="empleado.aspx" class="btn btn-info pull-left" role="button">Nuevo Empleado <span class="glyphicon glyphicon-plus"></span></a>
                            <a href="fechaEvaluacion.aspx" class="btn btn-info pull-left" role="button">Fecha Evaluación <span class="glyphicon glyphicon-calendar"></span></a>
                            <a href="#" onclick="javascript:excel();" class="btn btn-info pull-left" role="button">Convertir a Excel <span class="glyphicon glyphicon-th-list"></span></a>
                            <a href="#" onclick="javascript:descargaEvaluaciones();" class="btn btn-info pull-left" role="button">Descargar Evaluaciones Finalizadas <span class="glyphicon glyphicon-download-alt"></span></a>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" id="divGrilla">
                    <asp:Literal ID="litGrilla" Text="" runat="server"></asp:Literal>

                            </div>
                    </div>
                </div>

            </asp:Panel>
        </div>
    </div>
   <script src="Scripts/chosen.jquery.min.js"></script>
    <script src="Scripts/homeAdmin.js"></script>
    <script src="Scripts/trivia.js"></script>
    <script type="text/javascript">
        $.datepicker.regional['es'] = {
            closeText: 'Cerrar',
            prevText: '<Ant',
            nextText: 'Sig>',
            currentText: 'Hoy',
            monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
            monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
            dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
            dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
            dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
            weekHeader: 'Sm',
            dateFormat: 'dd/mm/yy',
            firstDay: 1,
            isRTL: false,
            showMonthAfterYear: false,
            yearSuffix: ''
        };
        $.datepicker.setDefaults($.datepicker.regional['es']);
        $('#inicio').datepicker();
        $('#fin').datepicker();
    </script>
</asp:Content>


