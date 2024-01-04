<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminUsers.aspx.cs" Inherits="LojaaOnline.AdminUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Estilos para exibição de utilizador */
        .display-users {
            display: flex;
            flex-wrap: wrap;
            justify-content: space-around;
        }

        .user-container {
            margin: 20px;
            max-width: 400px;
            border: 1px solid #ddd;
            border-radius: 8px;
            overflow: hidden;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            background-color: #fff;
        }

        .user-table {
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
            /* Estilo do cabeçalho da tabela */
            background-color: #272c4a;
            color: white;
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
            /* Estilo para o scroll */
            overflow-y: auto;
            max-height: 900px; 
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="display-users">
        <!-- Repeater para exibir detalhes dos utilizadores -->
        <asp:Repeater ID="RepeaterUsers" runat="server">
            <ItemTemplate>
                <!-- Container para cada utilizador com estilos específicos -->
                <div class="user-container">
                    <!-- Tabela para mostrar detalhes do utilizador -->
                    <table class="user-table">
                        <tr>
                            <!-- Cabeçalho -->
                            <th colspan="2">Utilizador <%# Eval("cod") %></th>
                        </tr>
                        <!-- Linhas com detalhes específicos do utilizador -->
                        <tr>
                            <td>Nome do Utilizador</td>
                            <td><%# Eval("utilizador") %></td>
                        </tr>
                        <tr>
                            <td>Email</td>
                            <td><%# Eval("email") %></td>
                        </tr>
                        <tr>
                            <td>Data de Registro</td>
                            <td><%# Eval("data") %></td>
                        </tr>
                        <tr>
                            <td>Tipo</td>
                            <td><%# Eval("cod_perfil") %></td>
                        </tr>
                        <tr>
                            <!-- Botões de ação para cada utilizador -->
                            <td class="action-buttons" colspan="2">
                                <asp:Button ID="btn_eliminar" runat="server" Text="Eliminar" CssClass="eliminar-button" OnCommand="EliminarUtilizador" CommandName="Eliminar" CommandArgument='<%# Eval("cod") %>' />
                                <asp:Button ID="btn_editar" runat="server" Text="Editar" CssClass="editar-button" OnCommand="EditarUtilizador" CommandName="Editar" CommandArgument='<%# Eval("cod") %>' />
                            </td>
                        </tr>
                    </table>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>