<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="Registrar.aspx.cs" Inherits="LojaaOnline.Regiistrar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Definição de estilos CSS para a página Registrar.aspx */
       
        .dropdown {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
            background-color: #fff;
            color: #333;
        }
     
        .dropdown select {
            width: 100%;
            border: none;
            outline: none;
            background: transparent;
            -webkit-appearance: none;
            -moz-appearance: none;
            appearance: none;
            cursor: pointer;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Seção do conteúdo da página -->

    <!-- Seção da criação da conta -->
    <div class="contact">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="titlepage">
                        <h2>Criar Conta</h2>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-10 offset-md-1">
                    <div id="request" class="main_form">
                        <div class="row">
                            <!-- Campo para inserir o nome -->
                            <div class="col-md-12">
                                <asp:TextBox ID="tb_nome" runat="server" class="contactus" placeholder="Nome"></asp:TextBox>
                            </div>
                            <!-- Campo para inserir o email -->
                            <div class="col-md-12">
                                <asp:TextBox ID="tb_email" runat="server" class="contactus" placeholder="Email"></asp:TextBox>
                            </div>
                            <!-- Campo para definir a palavra-passe -->
                            <div class="col-md-12">
                                <asp:TextBox ID="tb_pw" runat="server" class="contactus" placeholder="Password" type="password"></asp:TextBox>
                                 <!-- Validador personalizado para a força da palavra-passe -->
                                 <asp:CustomValidator ID="ValidatePassword" ValidationGroup="valitators" class="validator" runat="server" ErrorMessage="Password Fraca" ControlToValidate="tb_pw" ForeColor="#FF3300" OnServerValidate="ValidatePassword_ServerValidate" Font-Bold="True" Text="^">^</asp:CustomValidator>
                                 <!-- Validador para garantir que a palavra-passe é inserida -->
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="valitators" runat="server" ErrorMessage="Password Requerida" ControlToValidate="tb_pw" Font-Bold="True" ForeColor="Red">^</asp:RequiredFieldValidator>
                            </div>
                             <!-- Campo para confirmar a palavra-passe -->
                             <div class="col-md-12">
                                 <asp:TextBox ID="tb_pw2" runat="server" class="contactus" placeholder="Confirmar password" type="password"></asp:TextBox>
                                 <!-- Validador para comparar a confirmação da palavra-passe -->
                                 <asp:CompareValidator ID="ComparePass" ValidationGroup="valitators" runat="server" ErrorMessage="Confirmação Inválida" ControlToCompare="tb_pw" ControlToValidate="tb_pw2" Font-Bold="True" ForeColor="Red">^</asp:CompareValidator>
                                 <!-- Validador para garantir que a confirmação da palavra-passe é inserida -->
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="valitators" runat="server" ControlToValidate="tb_pw2" Font-Bold="True" ForeColor="Red">^</asp:RequiredFieldValidator>
                             </div>
                            <!-- Dropdown para o tipo de perfil -->
                            <div class="col-md-12">
                                <asp:DropDownList ID="ddl_perfil" runat="server" class="dropdown" DataSourceID="SqlDataSource1" DataTextField="perfil" DataValueField="cod_perfil"></asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:LojaOnlineConnectionString %>" SelectCommand="SELECT * FROM [perfis]"></asp:SqlDataSource>
                            </div>
                            <!-- Botão para registrar -->
                            <div class="col-md-12">
                                <asp:Button ID="btn_registrar" runat="server" ValidationGroup="valitators" Text="Registrar" class="send_btn" OnClick="btn_registrar_Click" />
                            </div>
                            <!-- Botão para voltar -->
                            <div class="col-md-12">
                                <asp:Button ID="btn_voltar" runat="server" Text="Voltar" class="send_btn" OnClick="btn_voltar_Click"/>
                            </div>
                            <!-- Mensagem de feedback -->
                            <h1><asp:Label ID="lbl_mensagem" runat="server" ForeColor="#FF3300"></asp:Label></h1>
                            <!-- Resumo das mensagens de validação -->
                            <h1><asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="valitators" runat="server" ForeColor="#FF3300" Font-Bold="False" CssClass="custom-validation-summary" DisplayMode="List"/></h1>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Fim da seção de criação de conta -->

</asp:Content>
