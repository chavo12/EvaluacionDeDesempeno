<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master" CodeFile="empleado.aspx.cs" Inherits="empleado" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <div class="row">
        <div class="col-md-12">
            <h3>
                <label class="text-info">
                    <asp:Literal ID="litTitulo" Text="Empleado" runat="server"></asp:Literal></label></h3>
        </div>
    </div>
    <div class="row">
        <div class="col-md-10">
            <asp:Panel ID="pnEmpleados" CssClass="panel panel-info" runat="server">
                <div class="panel-body" id="divGrilla">
                                                <div class="row">
                                    <div class="col-md-4"> 
                                            <label class="text-info">Nombre</label>
                                        <input type="text" id="nombre" class="form-control" placeholder="Nombre" <asp:Literal ID="litNombre" runat="server"></asp:Literal> /> 
                                        <asp:HiddenField ID="hdid" ClientIDMode="Static" Value="0" runat="server" />
                                        <asp:HiddenField ID="hdidAdmin" ClientIDMode="Static" Value="0" runat="server" />
                                        <asp:HiddenField ID="hdClave" ClientIDMode="Static" Value="" runat="server" /> 
                                        <asp:HiddenField ID="hdResetClave" ClientIDMode="Static" Value="" runat="server" /> 
                                        <asp:HiddenField ID="hdFechaResetClave" ClientIDMode="Static" Value="" runat="server" /> 
                                    </div>
                                    <div class="col-md-4">
                                            <label class="text-info">Primer Apellido</label>
                                        <input type="text" id="papellido" class="form-control" placeholder="Apellido" <asp:Literal ID="litPapellido" runat="server"></asp:Literal> /> 
                                    </div>
                                    <div class="col-md-4">
                                            <label class="text-info">Segundo Apellido</label>
                                        <input type="text" id="sapellido" class="form-control" placeholder="Apellido" <asp:Literal ID="litSapellido" runat="server"></asp:Literal> /> 
                                    </div>
                                </div>
                                  <div class="row">
                                    <div class="col-md-4">
                                            <label class="text-info">Fecha de Ingreso</label>
                                        <input type="text" id="ingreso" class="form-control" placeholder="Fecha de Ingreso" <asp:Literal ID="litIngreso" runat="server"></asp:Literal> /> 
                                    </div>
                                    <div class="col-md-4">
                                            <label class="text-info">Id-Pia</label>
                                        <input type="text" id="numpia" class="form-control" placeholder="Id-Pia" <asp:Literal ID="litPia" runat="server"></asp:Literal> /> 
                                    </div>
                                    <div class="col-md-4">
                                            <label class="text-info">País</label>
                                        <select id="pais" data-placeholder="Pais" class="chosenselect"  style="width:100%;">
                                              <asp:Literal ID="litPaisoption" runat="server"></asp:Literal>
                                            </select>
                                    </div>
                                </div>
                                  <div class="row">
                                    <div class="col-md-4">
                                            <label class="text-info">Negocio</label>
                                        <input type="text" id="negocio" class="form-control" placeholder="Negocio" <asp:Literal ID="litNegocio" runat="server"></asp:Literal> /> 
                                    </div>
                                    <div class="col-md-4">
                                            <label class="text-info">Departamento</label>
                                        <input type="text" id="departamento" class="form-control" placeholder="Departamento" <asp:Literal ID="litDepartamento" runat="server"></asp:Literal> /> 
                                    </div>
                                    <div class="col-md-4">
                                        <input type="hidden"  id="alcance" class="form-control" <asp:Literal ID="litAlcance" runat="server"></asp:Literal> /> 
                                            <label class="text-info">Correo Electrónico</label>
                                        <input type="text" id="correo" class="form-control" placeholder="Correo electrónico" <asp:Literal ID="litCorreo" runat="server"></asp:Literal> /> 
                                    </div>
                                </div>
                                  <div class="row">
                                    <div class="col-md-4">
                                            <label class="text-info">Cargo</label>
                                        <input type="text" id="cargo" class="form-control" placeholder="Cargo" <asp:Literal ID="litCargo" runat="server"></asp:Literal> /> 
                                    </div>
                                    <div class="col-md-4">
                                            <label class="text-info">Nivel</label>
                                        <select id="nivel" data-placeholder="Nivel" class="chosenselect"  style="width:100%;">
                                              <asp:Literal ID="litniveloption" runat="server"></asp:Literal>
                                            </select>
                                    </div>
                                    <div class="col-md-4">
                                            <label class="text-info">Supervisor</label>
                                         <select id="supervisor" data-placeholder="Supervisor" class="chosenselect"  style="width:100%;">
                                             <asp:Literal ID="litSupervisorOPtion" runat="server"></asp:Literal>
                                             </select>
                                    </div>
                                </div>
                                  <div class="row">
                                    <div class="col-md-4">
                                            <label class="text-info">Tipo de Empleado</label>
                                        <select id="tipoempleado" data-placeholder="Nivel" class="chosenselect"  style="width:100%;">
                                                    <asp:Literal ID="littipooption" runat="server"></asp:Literal>
                                            </select>
                                       
                                    </div>
                                    <div class="col-md-4">
                                            <label class="text-info">Empleado Id</label>
                                          <input type="text" id="empleadoid" class="form-control" placeholder="Empleado Id" <asp:Literal ID="litEmpleadoId" runat="server"></asp:Literal> /> 
                                    </div>
                                    <div class="col-md-4">
                                        
                                    </div>
                                </div>

                </div>
            </asp:Panel>
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
        <asp:Panel ID="pnResponsabilidades" CssClass="panel panel-info" runat="server">
                 <div class="panel-heading">
                        <label class="text-info">
                        Responsabilidades</label>
                 </div>
              <div class="panel-body">
                    <div class="row">
                        <div class="col-md-10">
                            <a href="#" class="btn btn-info pull-left" role="button" data-toggle="modal" onclick="javascript:$('#descrip').focus();" data-target="#myModal">Agregar <span class="glyphicon glyphicon-plus"></span></a>
                        </div>
                    </div>
                  <div class="row">
                      <div class="col-md-10" id="grillaResp">
                          <asp:Literal ID="litGrillaResponsabilidad" runat="server"></asp:Literal>
                      </div>
                  </div>
                  <div class="modal fade" id="myModal" role="dialog">
                     <div class="modal-dialog">
                         <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title">Responsabilidad</h4>
        </div>
        <div class="modal-body">
          <p>Descripción</p>
             <textarea class="form-control" rows="5" id="descrip">
                <asp:Literal ID="litDescripresp" runat="server"></asp:Literal>
             </textarea>
        </div>
        <div class="modal-footer">
          <button type="button" onclick="javascript:agregarResp()" class="btn btn-default" data-dismiss="modal">Aceptar</button>
        </div>
      </div>
                     </div>
                   </div>
                        <div class="modal fade" id="myModalEdit" role="dialog">
                     <div class="modal-dialog">
                         <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title">Responsabilidad</h4>
        </div>
        <div class="modal-body">
                        <asp:HiddenField ID="idResp" ClientIDMode="Static" runat="server" />
          <p>Descripción</p>
             <textarea class="form-control" rows="5" id="descripEdit">
                <asp:Literal ID="litDescriprespEdit" runat="server"></asp:Literal>
             </textarea>
        </div>
        <div class="modal-footer">
          <button type="button" onclick="javascript:agregarResp()" class="btn btn-default" data-dismiss="modal">Agregar</button>
        </div>
      </div>
                     </div>
                   </div>
              </div>
        </asp:Panel>
        </div>
    </div>
      <div class="row">
        <div class="col-md-10">
        <asp:Panel ID="pnOportunidades" CssClass="panel panel-info" runat="server">
                 <div class="panel-heading">
                        <label class="text-info">
                        Competencias Core</label>
                 </div>
              <div class="panel-body">
                    <div class="row">
                        <div class="col-md-10">
                            <a href="#" class="btn btn-info pull-left" role="button" data-toggle="modal" data-target="#myModalOp">Agregar <span class="glyphicon glyphicon-plus"></span></a>
                        </div>
                    </div>
                  <div class="row">
                      <div class="col-md-10" id="grillaOp">
                          <asp:Literal ID="litGrillaOp" runat="server"></asp:Literal>
                      </div>
                  </div>
                  <div class="modal fade" id="myModalOp" role="dialog">
                     <div class="modal-dialog">
                         <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title">Competencias</h4>
        </div>
        <div class="modal-body">
          <p>Descripción</p>
               <select id="tipoOp" data-placeholder="Nivel" class="chosenselect"  style="width:100%;">
                                 <option value="2">Comunicación</option>
                   <option value="5">Gestión de Cambio</option>
                   <option value="8">Orientación de los Resultados</option>
                   <option value="11">Satisfacción al Cliente Interno / Externo</option>
                   <option value="14">Trabajo en Equipo</option>
                   <option value="17">Integridad</option>
                   <option value="20">Desarrollo de Personas</option>
                   <option value="21">Liderazgo</option>
                   <option value="32">Visión estratégica del Negocio</option>
                                            </select>
             <textarea class="form-control" rows="5" id="descripOp">
                <asp:Literal ID="Literal6" runat="server"></asp:Literal>
             </textarea>
        </div>
        <div class="modal-footer">
          <button type="button" onclick="javascript:agregarOp()" class="btn btn-default" data-dismiss="modal">Aceptar</button>
        </div>
      </div>
                     </div>
                   </div>
             
              </div>
        </asp:Panel>
        </div>
    </div>
    <div class="row">
        <div class="col-md-10">
            <div class="panel panel-info">
                <div class="panel-body">
                 <a href="homeadmin.aspx" class="btn btn-info pull-right" onclick="javascript:return verificarEmpelado();" role="button">Guardar</a>
                <a href="/homeadmin.aspx" class="btn btn-info pull-right" role="button">Cancelar</a>
                <a href="/homeadmin.aspx" class="btn btn-info pull-right" onclick="javascript:if(confirm('¿Está seguro que desea elminar el empleado?')) return borrarEmpleado(); else return false;" role="button">Eliminar Empleado</a>
                </div>
            </div>
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
