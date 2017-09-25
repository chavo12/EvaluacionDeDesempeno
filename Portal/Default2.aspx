<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html>

<html lang="en">

<head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=10">
    <meta name="viewport" content="width=device-width, shrink-to-fit=no, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Simple Sidebar - Start Bootstrap Template</title>

    <!-- Bootstrap Core CSS -->
    <link href="Content/bootstrap.min.css" rel="stylesheet">

    <!-- Custom CSS -->
    <link href="Content/site.css" rel="stylesheet">

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

</head>

<body>
     <nav class="navbar navbar-default sidebar" role="navigation">
    <div id="wrapper">

        <!-- Sidebar -->
        <div id="sidebar-wrapper" >
            <ul class="sidebar-nav">
                                <li></li>
                                <li><a href="home.aspx">
                                    <asp:Literal ID="litHome" Text="home" runat="server"></asp:Literal></a></li>
                                <li><a href="responsabilidades.aspx">
                                    <asp:Literal ID="litResponsabilidades" Text="responsabilidades" runat="server"></asp:Literal></a></li>
                                <li><a href="compentencia.aspx">
                                    <asp:Literal ID="litCompetencias" Text="competencia" runat="server"></asp:Literal></a></li>
                                <li><a href="oportunidades.aspx">
                                    <asp:Literal ID="litOportunidades" Text="oportunidades" runat="server"></asp:Literal></a></li>
                                <li><a href="desempeno.aspx">
                                    <asp:Literal ID="litDesempeño" Text="desempeño" runat="server"></asp:Literal></a></li>
                            </ul>
        </div>
        <!-- /#sidebar-wrapper -->

        <!-- Page Content -->
      <div class="container-fluid">
                        <div class="navbar-header">
                            <button type="button" class="navbar-toggle" id="menu-toggle" data-toggle="collapse" data-target="#bs-sidebar-navbar-collapse-1">
                                <span class="sr-only">Toggle navigation</span>
                                <span class="icon-bar"></span>
                                <span class="icon-bar"></span>
                                <span class="icon-bar"></span>
                            </button>
                       
                    </div>
                </div>
            </div>
        <!-- /#page-content-wrapper -->

         </nav>
    <!-- /#wrapper -->

    <!-- jQuery -->
    <script src="Scripts/jquery.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="Scripts/bootstrap.min.js"></script>

    <!-- Menu Toggle Script -->
    <script>
    $("#menu-toggle").click(function(e) {
        e.preventDefault();
        $("#wrapper").toggleClass("toggled");
    });
    </script>

</body>

</html>
