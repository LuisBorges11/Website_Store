<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminEditarProduto.aspx.cs" Inherits="LojaaOnline.AdminEditarProduto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Estilos para o contentor de edição do produto */
        .product-edit-container {
            max-width: 600px;
            margin: 20px auto;
            padding: 20px;
            background-color: #fff;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        h1 {
            color: #333;
        }

        label {
            display: block;
            margin-bottom: 8px;
            color: #555;
        }

        input,
        textarea {
            width: 100%;
            padding: 8px;
            margin-bottom: 16px;
            box-sizing: border-box;
            border: 1px solid #ccc;
            border-radius: 4px;
        }

        button {
            background-color: #4caf50;
            color: #fff;
            padding: 10px 15px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }

        button:hover {
            background-color: #45a049;
        }

        /* Estilo para mensagens de estado */
        .message {
            color: green;
            text-align: center;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="product-edit-container">
        <!-- Rótulo e caixa de texto para o ID do produto -->
        <asp:Label ID="lblIdProduto" runat="server" Visible="false"></asp:Label>

        <!-- Campos de edição do produto -->
        <div>
            <asp:Label ID="lblNomeProduto" runat="server" Text="Nome do Produto:"></asp:Label>
            <asp:TextBox ID="txtNomeProduto" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="lblStock" runat="server" Text="Stock:"></asp:Label>
            <asp:TextBox ID="txtStock" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="lblDescricaoProduto" runat="server" Text="Descrição do Produto:"></asp:Label>
            <asp:TextBox ID="txtDescricaoProduto" runat="server" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="lblPrecoProduto" runat="server" Text="Preço do Produto:"></asp:Label>
            <asp:TextBox ID="txtPrecoProduto" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="lblCodTipo" runat="server" Text="Tipo de Produto:"></asp:Label>
            <!-- Opções de tipo de produto -->
            <asp:DropDownList ID="ddlCodTipo" runat="server">               
                <asp:ListItem Value="1">Placa Gráfica</asp:ListItem>
                <asp:ListItem Value="2">Processadores</asp:ListItem>
                <asp:ListItem Value="3">Rams</asp:ListItem>
                <asp:ListItem Value="4">Caixas</asp:ListItem>
                <asp:ListItem Value="5">Fontes</asp:ListItem>
                <asp:ListItem Value="6">MotherBoards</asp:ListItem>
            </asp:DropDownList>
        </div>

        <!-- Botões para editar e cancelar -->
        <br />
        <div>
            <asp:Button ID="btnSalvar" runat="server" Text="Editar produto" OnClick="SalvarProduto_Click" />
            <br />
            <asp:Button ID="btn_voltar" runat="server" Text="Cancelar" OnClick="voltar_Click" />
        </div>

        <!-- Mensagem de estado -->
        <div class="message">
            <h1>
                <!-- A mensagem será exibida aqui -->
                <asp:Label ID="lbl_mensagem" runat="server" ForeColor="Green"></asp:Label>
            </h1>
        </div>
    </div>
</asp:Content>