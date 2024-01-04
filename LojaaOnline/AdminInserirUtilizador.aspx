<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminInserirUtilizador.aspx.cs" Inherits="LojaaOnline.AdminInserirUtilizador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Estilo para o formulário */
        #form-container {
            max-width: 600px;
            margin: 50px auto;
            background-color: #fff;
            padding: 25px;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        h1 {
            text-align: center;
            color: #333;
        }

        /* Estilo para grupos de formulários */
        .form-group {
            margin-bottom: 20px;
        }

        label {
            display: block;
            font-weight: bold;
            color: #555;
            margin-bottom: 5px;
        }

        /* Estilo para os campos de entrada */
        input[type="text"],
        input[type="email"],
        input[type="password"],
        select {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-sizing: border-box;
        }

        .form-group label, .form-group select {
            display: inline-block;
            width: 48%;
            margin-right: 2%;
        }

        .form-group select {
            width: 48%;
            margin-right: 0;
        }

        /* Estilo para o botão */
        .btn-container {
            text-align: center;
        }

        button {
            background-color: #007BFF;
            color: #fff;
            padding: 12px 25px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }

        button:hover {
            background-color: #0056b3;
        }

        /* Estilo para as labels */
        .mensagem-label {
            font-size: 16px; 
            color: red; 
            margin-top: 5px; 
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br /><br /><br />
    <div id="form-container">
        <!-- Título do formulário -->
        <h1>Inserir um Utilizador</h1>

        <!-- Campo para o nome do utilizador -->
        <div class="form-group">
            <label for="txtUtilizador">Nome do Utilizador:</label>
            <asp:TextBox ID="tb_utilizador" runat="server" />
        </div>

        <!-- Campo para o email -->
        <div class="form-group">
            <label for="txtEmail">Email:</label>
            <asp:TextBox ID="tb_email" runat="server" />
        </div>

        <!-- Campo para a palavra-passe -->
        <div class="form-group">
            <label for="txtPalavraPasse">Palavra-Passe:</label>
            <asp:TextBox ID="tb_pw" runat="server" TextMode="Password" />
        </div>

        <!-- Campo para selecionar o perfil -->
        <div class="form-group">
            <label for="ddlPerfil">Perfil:</label>
            <asp:DropDownList ID="ddlPerfil" runat="server">
                <asp:ListItem Text="Admin" Value="1" />
                <asp:ListItem Text="Revenda" Value="2" />
                <asp:ListItem Text="Cliente" Value="3" />
            </asp:DropDownList>
        </div>

        <!-- Botão para inserir o utilizador -->
        <div class="btn-container">
            <asp:Button ID="btnInserirUtilizador" runat="server" Text="Inserir Utilizador" OnClick="btnInserirUtilizador_Click" />
        </div>

        <!-- Mensagens  -->
        <div class="message">
            <h1><asp:Label ID="lbl_mensagem" runat="server" ForeColor="Green" CssClass="mensagem-label"></asp:Label></h1>
             <h1><asp:Label ID="lbl_mensagem_erro" runat="server" ForeColor="Red" CssClass="mensagem-label"></asp:Label></h1>
        </div>       
    </div>
</asp:Content>