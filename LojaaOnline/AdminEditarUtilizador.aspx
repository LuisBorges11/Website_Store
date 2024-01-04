<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminEditarUtilizador.aspx.cs" Inherits="LojaaOnline.AdminEditarUtilizador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>       
        /* Estilos para o contentor de edição de utilizador */
        .user-edit-container {
            max-width: 600px;
            margin: 20px auto;
            padding: 20px;
            background-color: #fff;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        h1 {
            color: #333;
            text-align: center;
        }

        label {
            display: block;
            margin-bottom: 8px;
            color: #555;
        }

        input,
        select {
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

        /* Estilos específicos para o botão de voltar */
        #btn_voltar {
            background-color: #dc3545;
            margin-left: 10px;
        }

        #btn_voltar:hover {
            background-color: #c82333;
        }

        /* Estilo para as labels */
        .mensagem-label {
            font-size: 16px; /* Tamanho do texto */
            color: red; /* Cor do texto */
            margin-top: 5px; /* Espaço acima da label */
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">   
    <div class="user-edit-container">
        <!-- Rótulo e caixa de texto para o ID do utilizador -->
        <asp:Label ID="lblIdUtilizador" runat="server" Visible="false"></asp:Label>
        <h1>Editar Utilizador</h1>

        <!-- Campos da edição do utilizador -->
        <div>
            <label for="txtNome">Nome do Utilizador:</label>
            <asp:TextBox ID="txtNome" runat="server"></asp:TextBox>
        </div>
        <div>
            <label for="txtEmail">Email:</label>
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
        </div>        
        <div>
            <label for="ddlPerfil">Perfil:</label>
            <!-- Opções de tipo de perfil -->
            <asp:DropDownList ID="ddlPerfil" runat="server">               
                <asp:ListItem Text="Cliente" Value="3" />
                <asp:ListItem Text="Revenda" Value="2" />
                <asp:ListItem Text="Admin" Value="1" />
            </asp:DropDownList>
        </div>
        <br />
        <!-- Botões para editar e cancelar -->
        <div>
            <asp:Button ID="btnEditarUtilizador" runat="server" Text="Editar Utilizador" OnClick="btnEditarUtilizador_Click" />
            <asp:Button ID="btn_voltar" runat="server" Text="Cancelar" OnClick="voltar_Click" />
        </div>
        <!-- Mensagem de estado -->
        <div class="message">
            <h1><asp:Label ID="lbl_mensagem" runat="server" ForeColor="Green" CssClass="mensagem-label"></asp:Label></h1>
        </div>
    </div>
</asp:Content>