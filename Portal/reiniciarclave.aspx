<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="reiniciarclave.aspx.cs" Inherits="reiniciarclave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-4">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <label class="text-info">
                        Ingrese la nueva contraseña</label>

                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:HiddenField ID="hdReset" ClientIDMode="Static" runat="server" />
                            <asp:HiddenField ID="hdEmp" ClientIDMode="Static" runat="server" />
                            <input type="password" id="txtClave" placeholder="Nueva Clave" class="form-control" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <input type="password" id="txtClaveRep" placeholder="Repita la Nueva Clave" class="form-control" />
                        </div>
                    </div>
                    <div class="row" id="msgError"  style="display:none;">
                        <div class="alert alert-danger" role="alert">
                            <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                            <span class="sr-only">Error:</span>
                            Las claves ingresadas no son válidas o no son iguales.
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <button type="button" class="btn btn-info" onclick="javascript:guardarClave();">Guardar</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-2"></div>
    </div>
    <script src="Scripts/cambiarClave.js"></script>
</asp:Content>

