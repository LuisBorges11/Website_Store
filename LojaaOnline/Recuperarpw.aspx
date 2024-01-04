<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="Recuperarpw.aspx.cs" Inherits="LojaaOnline.Recuperarpw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Estrutura principal da página -->
    <div class="contact">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <!-- Título da Página -->
                    <div class="titlepage">
                        <h2>Recuperação da Palavra-Passe</h2>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-10 offset-md-1">
                    <!-- Formulário principal -->
                    <div id="request" class="main_form">
                        <div class="row">
                            <!-- Verificação da presença do parâmetro de e-mail na URL -->
                            <% if (string.IsNullOrEmpty(Request.QueryString["email"])) { %>
                                <!-- Etapa 1: Inserir o email -->
                                <div class="col-md-12">
                                    <!-- Caixa de texto para inserção do e-mail -->
                                    <asp:TextBox ID="tb_email_recuperar" class="contactus" type="email" placeholder="Inserir o email" runat="server"></asp:TextBox>
                                    <!-- Botão para iniciar o processo de recuperação -->
                                    <asp:Button ID="btn_active" class="send_btn" runat="server" Text="Recuperar" OnClick="btn_active_Click" />
                                    <br />
                                    <!-- Mensagem de feedback para o utilizador -->
                                    <h1><asp:Label ID="lbl_mensagem_recuperar" runat="server" Text="" CssClass="custom-validation-summary" ForeColor="Green"></asp:Label></h1>                                    
                                </div>
                            <% } else { %>
                                <!-- Etapa 2: Alterar a palavra-passe -->
                                <div class="col-md-12">
                                    <!-- Caixa de texto para nova palavra-passe -->
                                    <asp:TextBox ID="tb_nova_pw" ValidationGroup="valitators" class="contactus" type="password" placeholder="Nova Palavra-Passe" runat="server"></asp:TextBox>
                                    <!-- Validador personalizado para a força da palavra-passe -->
                                    <asp:CustomValidator ID="ValidatePassword" ValidationGroup="valitators" class="validator" runat="server" ErrorMessage="Password Fraca" ControlToValidate="tb_nova_pw" ForeColor="#FF3300" OnServerValidate="ValidatePassword_ServerValidate" Font-Bold="True" Text="^">^</asp:CustomValidator>
                                    <!-- Validador para garantir que a palavra-passe é inserida -->
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="valitators" runat="server" ErrorMessage="Password Requerida" ControlToValidate="tb_nova_pw" Font-Bold="True" ForeColor="Red">^</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-12">
                                    <!-- Caixa de texto para confirmar a nova palavra-passe -->
                                    <asp:TextBox ID="tb_nova_pw2" ValidationGroup="valitators" class="contactus" type="password" placeholder="Confirme a Palavra-Passe" runat="server"></asp:TextBox>
                                    <!-- Validador para comparar a confirmação da palavra-passe -->
                                    <asp:CompareValidator ID="ComparePass" ValidationGroup="valitators" runat="server" ErrorMessage="Confirmação Inválida" ControlToCompare="tb_nova_pw" ControlToValidate="tb_nova_pw2" Font-Bold="True" ForeColor="Red">^</asp:CompareValidator>
                                    <!-- Validador para garantir que a confirmação da palavra-passe é inserida -->
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="valitators" runat="server" ErrorMessage="Password Requerida" ControlToValidate="tb_nova_pw2" Font-Bold="True" ForeColor="Red">^</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-12">
                                    <!-- Botão para finalizar o processo de recuperação -->
                                    <asp:Button ID="btn_guardar" ValidationGroup="valitators" class="send_btn" runat="server" Text="Guardar" OnClick="btn_guardar_Click" />
                                    <br />
                                    <!-- Mensagem de feedback para o utilizador após a conclusão do processo -->
                                    <h1><asp:Label ID="lbl_mensagem_recuperar1" runat="server" Text="" CssClass="custom-validation-summary" ForeColor="Green"></asp:Label></h1>   
                                    <!-- Resumo das mensagens de validação -->
                                    <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="valitators" runat="server" ForeColor="#FF3300" Font-Bold="False" CssClass="custom-validation-summary" DisplayMode="List" />
                                </div>
                            <% } %>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
