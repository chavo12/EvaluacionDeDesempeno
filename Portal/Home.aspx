<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="Home.aspx.cs" Inherits="Home" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-10">
            <asp:Literal ID="litTitulo" runat="server"></asp:Literal>
        </div>
    </div>
    <div class="row">
        <div class="col-md-10">
            <asp:Literal ID="litSubtitulo" runat="server"></asp:Literal>
        </div>
    </div>
    <div class="row">
        <div class="col-md-10">
            <div class="panel panel-info">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Literal ID="litNombre" runat="server"></asp:Literal>
                        </div>
                        <div class="col-md-6">
                            <asp:Literal ID="litPia" runat="server"></asp:Literal>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Literal ID="litIngreso" runat="server"></asp:Literal>
                        </div>
                        <div class="col-md-6">
                            <asp:Literal ID="litNegocio" runat="server"></asp:Literal>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Literal ID="litDepartamento" runat="server"></asp:Literal>
                        </div>
                        <div class="col-md-6">
                            <asp:Literal ID="litPais" runat="server"></asp:Literal>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Literal ID="litCargo" runat="server"></asp:Literal>
                        </div>
                        <div class="col-md-6">
                            <asp:Literal ID="litNivel" runat="server"></asp:Literal>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Literal ID="litSupervisor" runat="server"></asp:Literal>
                        </div>
                        <div class="col-md-6">
                            <asp:Literal ID="litEstado" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-10">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <label class="text-info">
                        <asp:Literal ID="litProgreso" Text="Progreso" runat="server"></asp:Literal></label>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-3">
                            <label class="text-info">
                                <asp:Literal ID="litResponsabilidades" Text="Responsabilidades" runat="server"></asp:Literal></label>
                        </div>
                        <div class="col-md-3">
                            <div class="progress">
                                <div class="progress-bar progress-bar-success" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" <asp:Literal ID="litResponsabilidadWidth" Text="" runat="server"></asp:Literal>>
                                    <asp:Literal ID="litResponsabilidadNum" Text="" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label class="text-info">
                                <asp:Literal ID="litCompetencias" Text="Competencias" runat="server"></asp:Literal></label>
                        </div>
                        <div class="col-md-3">
                            <div class="progress">
                                <div class="progress-bar progress-bar-success" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" <asp:Literal ID="litCompetenciaWidth" Text="" runat="server"></asp:Literal>>
                                    <asp:Literal ID="litCompetenciaNUm" Text="" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <label class="text-info">
                                <asp:Literal ID="litOportunidades" Text="Oportunidades" runat="server"></asp:Literal></label>
                        </div>
                        <div class="col-md-3">
                            <div class="progress">
                                <div class="progress-bar progress-bar-success" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" <asp:Literal ID="litOportunidadWidth" Text="" runat="server"></asp:Literal>>
                                        <asp:Literal ID="litOportunidadNum" Text="" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label class="text-info">
                                <asp:Literal ID="litDesempeno" Text="Desempeño Global" runat="server"></asp:Literal></label>
                        </div>
                        <div class="col-md-3">
                            <div class="progress">
                                <div class="progress-bar progress-bar-success" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" <asp:Literal ID="litDesempenowidth" Text="" runat="server"></asp:Literal>>
                                    <asp:Literal ID="litDesempenonum" Text="" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Panel ID="pnComenzar" CssClass="row" runat="server">
        <div class="col-md-10">
            <a href="/responsabilidades.aspx" class="btn btn-info pull-right" role="button"><asp:Literal ID="litComenzar" Text="Empleados a evaluar" runat="server"></asp:Literal></a>
            <asp:Literal ID="litdescargar" Text="" runat="server"></asp:Literal>
        </div>
        </asp:Panel>
       <asp:Panel ID="pnenviado" CssClass="row" runat="server">
        <div class="col-md-10">
            <asp:Literal ID="litenviado" Text="" runat="server"></asp:Literal>
        </div>
        </asp:Panel>
        <asp:Panel ID="pnFinalizada" CssClass="row" runat="server">
        <div class="col-md-10">
            <asp:Literal ID="litResumen" Text="" runat="server"></asp:Literal>
        </div>
        </asp:Panel>
    <div class="row">
        <div class="col-md-10">
            <asp:Panel ID="pnEmpleados" CssClass="panel panel-info" runat="server">
                <div class="panel-heading">
                    <label class="text-info">
                        <asp:Literal ID="litEmpleados" Text="Empleados a evaluar" runat="server"></asp:Literal></label>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-7">
                             <label class="text-info">Empleados evaluados</label>
                             <div class="progress">
                                <div class="progress-bar progress-bar-success" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" <asp:Literal ID="litProgresoSuperwidth" Text="" runat="server"></asp:Literal>>
                                        <asp:Literal ID="litProgresoSuper" Text="" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                             <a href="#" onclick="javascript:excel();" class="btn btn-info pull-left" role="button">Convertir a Excel <span class="glyphicon glyphicon-th-list"></span></a>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-10">
                            <asp:HiddenField ID="idsuper" ClientIDMode="Static" runat="server" />
                     <asp:Literal ID="litGrilla" Text="Empleados a evaluar" runat="server"></asp:Literal>
                            </div></div>
                </div>

            </asp:Panel>
        </div>
    </div>
   
<script src="Scripts/home.js"></script>
</asp:Content>

