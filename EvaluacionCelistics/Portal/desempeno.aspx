<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="desempeno.aspx.cs" Inherits="desempeno" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <div class="row">
        <div class="col-md-11">
            <asp:Panel ID="pnEvaluar" CssClass="panel panel-info" runat="server">
                 <div class="panel-body">
                      <label class="text-info text-center">
            <asp:Literal ID="litevaluar" runat="server"></asp:Literal></label>
                     </div>
            </asp:Panel>
        </div>
    </div>
    <div id="preg1">
        <div class="row">
            <div class="col-md-11">
                <h3>
                    <label class="text-info">
                        <asp:Literal ID="litTitulo" runat="server"></asp:Literal></label></h3>
            </div>
        </div>

        <div class="row">
            <div class="col-md-11">
                <div class="panel panel-info">
                    <div class="panel-body">
                        <p class="text-info">
                            <asp:Literal ID="litdesemp" runat="server"></asp:Literal>
                        </p>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-11">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <label class="text-info">
                            <asp:Literal ID="litOpciones" Text="Opciones" runat="server"></asp:Literal></label>
                    </div>
                    <div class="panel-body">
                        <table>
                            <thead>
                                 
                                <tr>
                                    <th style="width: 20%; text-align:center"> <input type="radio"  onchange="javascript:marcarValor('1','1');" <asp:Literal ID="lit1" runat="server"></asp:Literal> name="prueba" /></th>
                                    <th style="width: 20%; text-align:center">
                                        <input type="radio" onchange="javascript:marcarValor('2','1');" <asp:Literal ID="lit2" runat="server"></asp:Literal> name="prueba" />
                                    </th>
                                    <th style="width: 20%; text-align:center">
                                        <input type="radio" onchange="javascript:marcarValor('3','1');" <asp:Literal ID="lit3" runat="server"></asp:Literal> name="prueba" />
                                    </th>
                                    <th style="width: 20%; text-align:center">
                                        <input type="radio" onchange="javascript:marcarValor('4','1');" <asp:Literal ID="lit4" runat="server"></asp:Literal> name="prueba" />
                                    </th>
                                    <th style="width: 20%; text-align:center">
                                        <input type="radio" onchange="javascript:marcarValor('5','1');" <asp:Literal ID="lit5" runat="server"></asp:Literal> name="prueba" />
                                    </th>
                                </tr>
                            </thead>
                            <tr>
                                <td>
                                   <p class="text-info  letrachica text-center"><asp:Literal ID="Literal1" runat="server"></asp:Literal></p>
                                </td>
                                <td>
                                    <p class="text-info  letrachica text-center"><asp:Literal ID="Literal2" runat="server"></asp:Literal></p>
                                </td>
                                <td>
                                    <p class="text-info  letrachica text-center"><asp:Literal ID="Literal3" runat="server"></asp:Literal></p>
                                </td>
                                <td>
                                    <p class="text-info  letrachica text-center"><asp:Literal ID="Literal4" runat="server"></asp:Literal></p>
                                </td>
                                <td>
                                    <p class="text-info  letrachica text-center"><asp:Literal ID="Literal5" runat="server"></asp:Literal></p>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p class="text-info letrachica text-center"><asp:Literal ID="Literal6" runat="server"></asp:Literal></p>
                                </td>
                                <td>
                                    <p class="text-info letrachica text-center"><asp:Literal ID="Literal7" runat="server"></asp:Literal></p>
                                </td>
                                <td>
                                    <p class="text-info letrachica text-center"><asp:Literal ID="Literal8" runat="server"></asp:Literal></p>
                                </td>
                                <td>
                                    <p class="text-info letrachica text-center"><asp:Literal ID="Literal9" runat="server"></asp:Literal></p>
                                    
                                </td>
                                <td>
                                    <p class="text-info letrachica text-center"><asp:Literal ID="Literal10" runat="server"></asp:Literal></p>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                <p class="text-info letrachica"><asp:Literal ID="Literal11" runat="server"></asp:Literal></p>
                                </td>
                                <td>
                                    <p class="text-info letrachica"><asp:Literal ID="Literal12" runat="server"></asp:Literal></p>
                                </td>
                                <td>
                                    <p class="text-info letrachica"><asp:Literal ID="Literal13" runat="server"></asp:Literal></p>
                                </td>
                                <td>
                                    <p class="text-info letrachica"><asp:Literal ID="Literal14" runat="server"></asp:Literal></p>

                                </td>
                                <td>
                                    <p class="text-info letrachica"><asp:Literal ID="Literal15" runat="server"></asp:Literal></p>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-11">
                
                <div class="panel panel-info">
                    <div class="panel-body">
                        <label class="text-info">
                            <asp:Literal ID="litSubtitulo" runat="server"></asp:Literal></label>

                        <textarea rows="5" placeholder="Escriba su respuesta aquí" class="textarea form-control" cols="130" <asp:Literal ID="litLecturaEscrito" runat="server"></asp:Literal> id="escrito"><asp:Literal ID="litEscrito" runat="server"></asp:Literal></textarea>
                    </div>
                </div>
            </div>
        </div>
        <asp:Panel ID="pnSupervisor" runat="server">
              <div class="row">
            <div class="col-md-11">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                           <label class="text-info">
                            <asp:Literal ID="litSupervisorTexto" Text="Evalúe al Empleado de acuerdo a la siguiente escala el desempeño global durante el año 2016. Respalde el mismo con un breve comentario." runat="server"></asp:Literal></label>
                    </div>
                    <div class="panel-body">
                        <table style="width:100%;">
                            <thead>
                                <asp:Literal ID="litOpcionesSuper" Text="" runat="server"></asp:Literal>
                            </thead>
                            <tr>
                                <td class="fondoSupervisor">
                                   <p class="text-info  text-center"><asp:Literal ID="Literal16" runat="server"></asp:Literal></p>
                                </td>
                                <td class="fondoSupervisor">
                                    <p class="text-info  text-center"><asp:Literal ID="Literal17" runat="server"></asp:Literal></p>
                                </td>
                                <td class="fondoSupervisor">
                                    <p class="text-info  text-center"><asp:Literal ID="Literal18" runat="server"></asp:Literal></p>
                                </td>
                                <td class="fondoSupervisor">
                                    <p class="text-info  text-center"><asp:Literal ID="Literal19" runat="server"></asp:Literal></p>
                                </td>
                                <td class="fondoSupervisor">
                                    <p class="text-info  text-center"><asp:Literal ID="Literal20" runat="server"></asp:Literal></p>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-11">
                
                <div class="panel panel-danger">
                    <div class="panel-heading">
                           <label class="text-info">
                            <asp:Literal ID="litComentarioSupervisor" Text="Comentario del Supervisor" runat="server"></asp:Literal></label>
                    </div>
                    <div class="panel-body">
                        <div class="row"><div class="col-md-10">
                        <textarea rows="5" placeholder="Escriba su respuesta aquí" class="textarea form-control" cols="130" <asp:Literal ID="litEscritoSuperLectura" runat="server"></asp:Literal> id="escritoSuper"><asp:Literal ID="litEscritoSuper" runat="server"></asp:Literal></textarea>
                            </div></div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="panel panel-danger">
                                    <div class="panel-body">
                                         <label class="text-info"><asp:Literal ID="Literal21" Text="Fortalezas" runat="server"></asp:Literal></label>
                                        <textarea rows="2" placeholder="Escriba su respuesta aquí" class="textarea form-control" cols="130" <asp:Literal ID="litFortalezaLectura" runat="server"></asp:Literal> id="fortaleza"><asp:Literal ID="litFortaleza" runat="server"></asp:Literal></textarea>
                                    </div>
                                </div>
                            </div>
                             <div class="col-md-6">
                                <div class="panel panel-danger">
                                    <div class="panel-body">
                                         <label class="text-info"><asp:Literal ID="Literal22" Text="Oportunidades de mejora" runat="server"></asp:Literal></label>
                                        <textarea rows="2" placeholder="Escriba su respuesta aquí" class="textarea form-control" cols="130" <asp:Literal ID="litOportunidadLectura" runat="server"></asp:Literal> id="oportunidad"><asp:Literal ID="litOportunidad" runat="server"></asp:Literal></textarea>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
            </asp:Panel>
        <div class="row">
            <div class="col-md-11">
                <asp:HiddenField ClientIDMode="Static" ID="hdSupervisa" runat="server" />
                <asp:HiddenField ID="id" ClientIDMode="Static" runat="server" />
                            <input type="hidden" id="valor1"/>
                   <asp:HiddenField ID="idEval" ClientIDMode="Static" runat="server" />
                <asp:Literal ID="litFinalizar" runat="server"></asp:Literal>
               
                
                
                <a href="/oportunidades.aspx" class="btn btn-info pull-right" onclick="javascript:responderDesempeno('NO','NO');" role="button">Anterior</a>
                 <asp:Literal ID="litGuardar" runat="server"></asp:Literal>
                <asp:Literal ID="litImprimir" runat="server"></asp:Literal>
                
             
            </div>
        </div>
    </div>
</asp:Content>
