<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminInserirProduto.aspx.cs" Inherits="LojaaOnline.AdminInserirProduto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Estilos para o formulário */
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

        .form-group {
            margin-bottom: 20px;
        }

        label {
            display: block;
            font-weight: bold;
            color: #555;
            margin-bottom: 5px;
        }

        input[type="text"],
        select,
        textarea {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-sizing: border-box;
        }

        .error-message {
            color: red;
            font-size: 12px;
            margin-top: -10px;
        }

        input[type="file"] {
            margin-bottom: 20px;
        }

        .btn-container {
            text-align: center;
            margin-top: 20px;
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

        .message {
            color: red;
            text-align: center;
        }

        /* Estilos específicos para o botão inserir */
        #btn_inserir {
            background-color: #4CAF50; 
            color: white; 
            padding: 10px 20px; 
            font-size: 16px; 
            border-radius: 5px; 
            cursor: pointer; 
            transition: background-color 0.3s;
        }

        #btn_inserir:hover {
            background-color: #45a049; 
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
    
    <div id="form-container">
        <h1>Inserir um produto</h1>

        <!-- Campos do formulário -->
        <div class="form-group">
            <label for="tb_nome_produto">Nome do produto:</label>
            <asp:TextBox ID="tb_nome_produto" runat="server"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="tb_stock">Quantidade Stock:</label>
            <asp:TextBox ID="tb_stock" runat="server"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="ddl_tipo">Tipo de Produto:</label>
            <asp:DropDownList ID="ddl_tipo" runat="server" DataSourceID="SqlDataSource1" DataTextField="tipo"
                DataValueField="cod_tipo"></asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                ConnectionString="<%$ ConnectionStrings:LojaOnlineConnectionString %>"
                SelectCommand="SELECT * FROM [tipo_produto]"></asp:SqlDataSource>
        </div>

        <div class="form-group">
            <label for="tb_descricao_produto">Descrição do produto:</label>
            <asp:TextBox ID="tb_descricao_produto" runat="server" TextMode="MultiLine" Rows="4" Columns="50"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="tb_preco">Preço do Produto:</label>
            <asp:TextBox ID="tb_preco" runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="rev_stock" runat="server" ControlToValidate="tb_preco"
                ValidationExpression="^\d+(\.\d{1,2})?$" ErrorMessage="Informe um número decimal válido (ex: 10 ou 10.50)"
                Display="Dynamic" ForeColor="Red" />
        </div>

        <div class="form-group">
            <label for="FileUpload1">Imagem do Produto:</label>
            <asp:FileUpload ID="FileUpload1" runat="server" Width="100%" />
        </div>

        <!-- Botão de inserção -->
        <div class="btn-container">
            <asp:Button ID="btn_inserir" runat="server" Text="Inserir" OnClick="btn_inserir_Click" />
        </div>

        <!-- Mensagem de feedback -->
        <div class="message">
            <h1><asp:Label ID="lbl_mensagem" runat="server" ForeColor="Green" CssClass="mensagem-label"></asp:Label></h1>
            <h1><asp:Label ID="lbl_mensagem_erro" runat="server" ForeColor="Red" CssClass="mensagem-label"></asp:Label></h1>
        </div>
    </div>
</asp:Content>