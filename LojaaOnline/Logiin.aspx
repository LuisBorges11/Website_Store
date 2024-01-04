<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="Logiin.aspx.cs" Inherits="LojaaOnline.Logiin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Página de Login -->
    <div class="contact">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <!-- Título da Página -->
                    <div class="titlepage">
                        <h2>Login</h2>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-10 offset-md-1">
                    <!-- Formulário de Login -->
                    <div id="request" class="main_form">
                        <div class="row">
                            <!-- Campo Nome -->
                            <div class="col-md-12">
                                <asp:TextBox ID="tb_nome" runat="server" class="contactus" placeholder="Nome"></asp:TextBox>
                            </div>
                            <!-- Campo Password -->
                            <div class="col-md-12">
                                <asp:TextBox ID="tb_pw" runat="server" type="password" class="contactus" placeholder="Password"></asp:TextBox>
                                 <!-- Link "Esqueceu-se da palavra-passe?" -->
                                 <a href="Recuperarpw.aspx" style="display: block; margin-top: 5px; color: white;">Esqueceu-se da palavra-passe? Clique aqui!</a>
                            </div>                           
                            <!-- Botão de Entrar -->
                            <div class="col-md-12">
                                <asp:Button ID="btn_entrar" runat="server" Text="Entrar" class="send_btn" OnClick="btn_entrar_Click" />
                            </div>
                            <!-- Botão de Criar Conta -->
                            <div class="col-md-12">
                                <asp:Button ID="btn_registrar" runat="server" Text="Criar Conta" class="send_btn" OnClick="btn_registrar_Click"/>
                            </div>
                            <!-- Mensagem de Feedback -->
                            <h1><asp:Label ID="lbl_mensagem" runat="server" ForeColor="#FF3300"></asp:Label></h1>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
