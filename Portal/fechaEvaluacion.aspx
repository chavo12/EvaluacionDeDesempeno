<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master" CodeFile="fechaEvaluacion.aspx.cs" Inherits="fechaEvaluacion" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <script src="Scripts/jquery-ui.js"></script>
    <div class="row">
        <div class="col-md-12">
            <h3>
                <label class="text-info">
                    <asp:Literal ID="litTitulo" Text="Empleado" runat="server"></asp:Literal></label></h3>
        </div>
    </div>
    <div class="row">
        <div class="col-md-10">
              <asp:Panel ID="pnEvaluacion" CssClass="panel panel-info" runat="server">
                   <div class="panel-heading">
                        <label class="text-info">
                        <asp:Literal ID="litEvaluacion" Text="Evaluación" runat="server"></asp:Literal></label>
                       </div>
                  <div class="panel-body">
                      <div class="row">
                          <div class="col-md-10">
                                <label class="text-info">
                        <asp:Literal ID="litAutoEvaluacion" Text="Auto Evalucación" runat="server"></asp:Literal></label>
                          </div>
                      </div>
                      <div class="row">
                          <div class="col-md-6">
                              <input type="text" id="inicio" class="form-control" placeholder="Inicio" <asp:Literal ID="Literal1" runat="server"></asp:Literal> /> 
                          </div>
                          <div class="col-md-6">
                              <input type="text" id="fin" class="form-control" placeholder="Fin" <asp:Literal ID="Literal2" runat="server"></asp:Literal> /> 
                          </div>
                      </div>
                       <div class="row">
                          <div class="col-md-10">
                                <label class="text-info">
                        <asp:Literal ID="litSupervisorFecha" Text="Supervisor" runat="server"></asp:Literal></label>
                          </div>
                      </div>
                      <div class="row">
                          <div class="col-md-6">
                              <input type="text" id="inicioSuper" class="form-control" placeholder="Inicio" <asp:Literal ID="Literal4" runat="server"></asp:Literal> /> 
                          </div>
                          <div class="col-md-6">
                              <input type="text" id="finSuper" class="form-control" placeholder="Fin" <asp:Literal ID="Literal5" runat="server"></asp:Literal> /> 
                          </div>
                      </div>
                  </div>
                  </asp:Panel>
        </div>
    </div>
    <div class="row">
        <div class="col-md-10">
                 <a href="homeadmin.aspx" class="btn btn-info pull-right" onclick="javascript:return fechaEvaluacion();" role="button">Guardar</a>
                <a href="/homeadmin.aspx" class="btn btn-info pull-right" role="button">Cancelar</a>
        </div>
    </div>
    <script src="Scripts/chosen.jquery.min.js"></script>
    <script src="Scripts/homeAdmin.js"></script>
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
        $('#ingreso').datepicker();
        $('#fin').datepicker();
        $('#finSuper').datepicker();
        $('#inicio').datepicker();
        $('#inicioSuper').datepicker();
    </script>
</asp:Content>
