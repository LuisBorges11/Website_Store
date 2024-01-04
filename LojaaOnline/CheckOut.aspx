<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="CheckOut.aspx.cs" Inherits="LojaaOnline.CheckOut" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<!-- Estilo CSS -->
<style>
    #divFormularioEntrega {
        background-color: #f4f4f4;
        padding: 15px;
        border: 1px solid #ddd;
        border-radius: 5px;
        margin-bottom: 20px;
    }

    .form-control {
        width: 100%;
        padding: 10px;
        margin: 5px 0;
        border: 1px solid #ccc;
        border-radius: 5px;
    }

    .btn-success {
        background-color: #4caf50;
        color: #fff;
        padding: 10px 20px;
        border: none;
        border-radius: 5px;
        cursor: pointer;
    }

    .text-success {
        color: #4caf50;
        font-weight: bold;
    }

    .text-error {
        color: #ff0000;
        font-weight: bold;
    }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    </br></br></br></br>
    <div class="container">
        <h1>Checkout</h1>
        <div class="row">
            <div class="col-md-6">
                <div id="divFormularioEntrega" runat="server">
                    <!-- Informações de Entrega -->
                    <h2>Informações de Entrega</h2>
                    <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" placeholder="Nome" />
                    <asp:TextBox ID="txtMorada" runat="server" CssClass="form-control" placeholder="Morada" />
                    <asp:TextBox ID="txtTelemovel" runat="server" CssClass="form-control" placeholder="Telemóvel" />
                    <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-control" placeholder="Código Postal" />
                </div>
            </div>
            <div class="col-md-6">
                <!-- Resumo do Carrinho -->
                <h2>Resumo do Carrinho</h2>
                <asp:Repeater ID="repeaterDetalhesProduto" runat="server">
                    <ItemTemplate>
                        <div class="produto">
                            <h4><%# Eval("nome_produto") %></h4>
                            <p>Quantidade: <asp:Label ID="lblQuantidade" runat="server" Text='<%# Eval("quantidade") %>'></asp:Label></p>
                            <p>Preço Unitário: <asp:Label ID="lblPrecoProduto" runat="server" Text='<%# Eval("preco_produto", "{0:C}") %>'></asp:Label></p>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <p>Total de produtos: <asp:Label ID="lblTotalProdutos" runat="server" CssClass="text-success"></asp:Label></p>
                <p>Total do Carrinho: <asp:Label ID="lblTotalCarrinho" runat="server" CssClass="text-success"></asp:Label></p>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <!-- Botão de Checkout -->
                <asp:Button ID="btnCheckout" runat="server" Text="Confirmar Pedido" OnClick="btnCheckout_Click" CssClass="btn btn-success" />
                <asp:Button ID="btn_voltar" runat="server" Text="Voltar" OnClick="btn_voltar_Click" CssClass="btn btn-success" />
                <asp:Button ID="btn_pedidos" runat="server" Text="Ver Pedidos" OnClick="btn_pedidos_Click" CssClass="btn btn-success" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <!-- Mensagens de Feedback -->
                <asp:Label ID="lblMensagem" runat="server" CssClass="text-success" Visible="false"></asp:Label>
                <asp:Label ID="lbl_mensagemerro" runat="server" Visible="False" CssClass="text-error"></asp:Label>
            </div>
        </div>
    </div>
    </br></br></br></br><br /><br /><br />
</asp:Content>