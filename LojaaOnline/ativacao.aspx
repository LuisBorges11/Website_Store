<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="ativacao.aspx.cs" Inherits="LojaaOnline.ativacao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Seção principal do conteúdo da página -->

    <!-- Seção de contato -->
    <div class="contact">
        <div class="container">
            <!-- Título da página -->
            <div class="row">
                <div class="col-md-12">
                    <div class="titlepage">
                        <h2>Ativação</h2>
                    </div>
                </div>
            </div>
            
            <!-- Conteúdo principal -->
            <div class="row">
                <div class="col-md-10 offset-md-1">
                    <div id="request" class="main_form">
                        <div class="row">
                            <!-- Exibição da mensagem de ativação -->
                            <h1>
                                <asp:Label ID="lbl_mensagem" runat="server" Text="Label" ForeColor="Red"></asp:Label>
                            </h1>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Fim da seção de contato -->

</asp:Content>
