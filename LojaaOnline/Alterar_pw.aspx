<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="Alterar_pw.aspx.cs" Inherits="LojaaOnline.Alterar_pw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="contact">
   <div class="container">
      <div class="row">
         <div class="col-md-12">
            <!-- Título da página -->
            <div class="titlepage">
               <h2>Alterar a Palavra-Passe</h2>
            </div>
         </div>
      </div>
      <div class="row">
         <div class="col-md-10 offset-md-1">
            <!-- Formulário para alterar a palavra-passe -->
            <div id="request" class="main_form">
               <div class="row">
                  <div class="col-md-12 ">
                     <!-- Campo para a palavra-passe atual -->
                     <asp:TextBox ID="tb_pw_atual" runat="server" class="contactus" placeholder="Palavra-passe atual"></asp:TextBox>
                  </div>
                  <div class="col-md-12">
                     <!-- Campo para a nova palavra-passe -->
                     <asp:TextBox ID="tb_pw_nova" runat="server" class="contactus" placeholder="Palavra-passe nova"></asp:TextBox>
                  </div>
                  <div class="col-md-12">
                     <!-- Campo para confirmar a nova palavra-passe -->
                     <asp:TextBox ID="tb_pw_confirme" runat="server" class="contactus" placeholder="Confirmar palavra-passe nova"></asp:TextBox>
                  </div> 
                  <div class="col-md-12">
                     <!-- Botão para acionar a alteração da palavra-passe -->
                     <asp:Button ID="btn_alterar" runat="server" Text="Alterar" class="send_btn" OnClick="btn_alterar_Click" />
                  </div>
                  <div class="col-md-12">
                     <!-- Botão para voltar -->
                     <asp:Button ID="btn_voltar" runat="server" Text="Voltar" class="send_btn" OnClick="btn_voltar_Click" />
                  </div>
                  <!-- Mensagem de feedback -->
                  <h1> <asp:Label ID="lbl_mensagem" runat="server" ForeColor="#FF3300"></asp:Label> </h1>
               </div>
            </div>
         </div>
      </div>
   </div>
</div>

</asp:Content>
