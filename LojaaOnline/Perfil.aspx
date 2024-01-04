<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="LojaaOnline.Perfil"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- Estilo CSS -->
    <style type="text/css">
        .textoBranco {
            color: white;
        }

        .col-md-12 h1{
            color: white;
        }

        .linkVerde {
            color: green;
        }

    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Página de perfil do utilizador -->
    <div class="contact">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <!-- Título da página -->
                    <div class="titlepage">
                        <h2>Perfil</h2>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-10 offset-md-1">
                    <div id="request" class="main_form">
                        <div class="row">

                            <!-- Informações do utilizador: Nome, Email, ID -->
                            <div class="col-md-12">
                                <h1>Nome: <asp:Label runat="server" ID="lbl_nome" CssClass="textoBranco"></asp:Label></h1>                      
                            </div>

                            <div class="col-md-12">
                                <h1>Email: <asp:Label runat="server" ID="lbl_email" CssClass="textoBranco"></asp:Label></h1>
                            </div>

                            <div class="col-md-12">
                                <h1>ID: <asp:Label runat="server" ID="lbl_id" CssClass="textoBranco"></asp:Label></h1>
                            </div>

                            <!-- LinkButton para permitir a alteração da palavra-passe -->
                            <div class="col-md-12">
                                <h1><asp:LinkButton ID="lb_alterarPW" runat="server" OnClick="lb_alterarPW_Click" CssClass="linkVerde">Alterar Palavra Passe</asp:LinkButton></h1>
                            </div>  

                            <!-- Botão para voltar à página anterior -->
                            <div class="col-md-12">
                                <asp:Button ID="btn_voltar" runat="server" Text="Voltar" class="send_btn" OnClick="btn_voltar_Click" />
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>