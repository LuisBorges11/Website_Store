<%@ Page  EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminProdutos.aspx.cs" Inherits="LojaaOnline.AdminProdutos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Estilos para exibição de produtos */
        .display-products {
            display: flex;
            flex-wrap: wrap;
            justify-content: space-around;
        }

        .product-container {
            margin: 20px;
            max-width: 300px;
            border: 1px solid #ddd;
            border-radius: 8px;
            overflow: hidden;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            background-color: #fff;
        }

        .product-table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
        }

        th, td {
            padding: 12px;
            text-align: left;
            border-bottom: 1px solid #ddd;
            color: black;
        }

        th {
            background-color: #272c4a;
            color: white;
        }

        .product-image {
            max-width: 100%;
            height: auto;
            border-radius: 8px 8px 0 0;
        }

        .action-buttons {
            display: flex;
            justify-content: space-between;
            padding: 12px;
        }

        .eliminar-button,
        .editar-button {
            flex-grow: 1;
            margin-right: 5px;
            cursor: pointer;
        }

        /* Estilos para botão de eliminar */
        .eliminar-button {
            background-color: #ff6347;
            color: #fff;
            padding: 8px;
            border: none;
            border-radius: 4px;
        }

        .eliminar-button:hover {
            background-color: #d9534f;
        }

        /* Estilos para botão de editar */
        .editar-button {
            background-color: #2201fb;
            color: #fff;
            padding: 8px;
            border: none;
            border-radius: 4px;
        }

        .editar-button:hover {
            background-color: #d9534f;
        }

        .content-scroll {
            overflow-y: auto;
            max-height: 900px; 
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-scroll">
        <!-- Título centralizado com cor de texto branca -->
        <center>
            <h1><asp:Label ID="lbl_mensagem" runat="server" ForeColor="White"></asp:Label></h1>
        </center>
        <!-- Mostra os produtos usando um Repeater -->
        <div class="display-products">
            <asp:Repeater ID="RepeaterProducts" runat="server">
                <ItemTemplate>
                    <!-- Container para cada produto com estilos específicos -->
                    <div class="product-container">
                        <!-- Tabela para mostrar detalhes do produto -->
                        <table class="product-table">
                            <tr>
                                <!-- Cabeçalho  -->
                                <th colspan="2">Produto <%# Eval("cod_produto") %></th>
                            </tr>
                            <!-- Linhas com detalhes específicos do produto -->
                            <tr>
                                <td>Nome do Produto</td>
                                <td><%# Eval("nome_produto") %></td>
                            </tr>
                            <tr>
                                <td>Stock</td>
                                <td><%# Eval("stock") %></td>
                            </tr>
                            <tr>
                                <td>Descrição do Produto</td>
                                <td><%# Eval("descricao_produto") %></td>
                            </tr>
                            <tr>
                                <td>Preço do Produto</td>
                                <td><%# Eval("preco_produto", "{0:C}") %></td>
                            </tr>
                            <tr>
                                <td>Imagem do Produto</td>
                                <td>
                                    <!-- Imagem do produto convertida de base64 -->
                                    <img src='<%# "data:image/jpeg;base64," + Convert.ToBase64String((byte[])Eval("imagem_produto")) %>' alt="Imagem do Produto" class="product-image" />
                                </td>
                            </tr>
                            <tr>
                                <!-- Botões de ação para cada produto -->
                                <td class="action-buttons" colspan="2">
                                    <asp:Button ID="btn_eliminar" runat="server" Text="Eliminar" CssClass="eliminar-button" OnCommand="Eliminarproduto" CommandName="Eliminar" CommandArgument='<%# Eval("cod_produto") %>' />
                                    <asp:Button ID="btn_editar" runat="server" Text="Editar" CssClass="editar-button" OnCommand="EditarProduto" CommandName="Editar" CommandArgument='<%# Eval("cod_produto") %>' />
                                </td>
                            </tr>
                        </table>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>