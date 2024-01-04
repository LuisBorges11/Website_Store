<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminPedidos.aspx.cs" Inherits="LojaaOnline.AdminPedidos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Estilos CSS -->
    <style>
        .pedido-table {
            width: 100%;
            border: 1px solid #ccc;
            margin-bottom: 20px;
            border-collapse: collapse;
        }

        .pedido-table th {
            background-color: #f2f2f2;
            color: black;
        }

        .price-column {
            color: #008000;
        }

        .confirmar-button {
            background-color: #4CAF50;
            color: white;
            border: none;
            border-radius: 5px;
            padding: 10px 20px;
            cursor: pointer;
            transition: background-color 0.3s;
        }

        .confirmar-button:hover {
            background-color: #45a049;
        }

        .eliminar-button {
            background-color: #f44336;
            color: white;
            border: none;
            border-radius: 5px;
            padding: 10px 20px;
            cursor: pointer;
            transition: background-color 0.3s;
        }

        .eliminar-button:hover {
            background-color: #d32f2f;
        }

        .semPedidosLabel {
            display: block;
            text-align: center;
            color: white;
            font-size: 24px;
            margin-top: 20px;
        }
    </style>

    <!-- Scripts JavaScript -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // Adicionar cores dinamicamente com base no status
            $(".status-column").each(function () {
                var status = $(this).text().trim();
                if (status === "Pendente") {
                    $(this).css("color", "#FFA500");
                } else if (status === "Confirmado") {
                    $(this).css("color", "green");
                }
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <!-- Label para indicar que não há pedidos visíveis -->
    <asp:Label ID="lblSemPedidos" runat="server" Text="Sem Pedidos" Visible="false" CssClass="semPedidosLabel"></asp:Label>
    <!-- Repeater para exibir a tabela de pedidos -->
    <asp:Repeater ID="repeaterPedidos" runat="server">
        <ItemTemplate>
            <table class="table table-striped pedido-table">
                <thead>
                    <tr>
                        <th>ID do Pedido</th>
                        <th>ID do Carrinho</th>
                        <th>Nome produto</th>
                        <th>Nome utilizador</th>
                        <th>Quantidade</th>
                        <th>Data do Pedido</th>
                        <th>Status do Pedido</th>
                        <th>Preço Total</th>
                        <th>Morada</th>
                        <th>Telemóvel</th>
                        <th>Código Postal</th>
                        <th>Ação</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <!-- Dados do pedido -->
                        <td><%# Eval("id_pedido") %></td>
                        <td><%# Eval("id_carrinho") %></td>
                        <td><%# Eval("nome_produto") %></td>
                        <td><%# Eval("nome_utilizador") %></td>
                        <td><%# Eval("quantidade") %></td>
                        <td><%# Eval("datapedido", "{0:dd/MM/yyyy}") %></td>
                        <td class="status-column" id="status<%# Eval("id_pedido") %>"><%# Eval("StatusPedido") %></td>
                        <td class="price-column"><%# Eval("precototal") %></td>
                        <td><%# Eval("morada") %></td>
                        <td><%# Eval("telemovel") %></td>
                        <td><%# Eval("codigopostal") %></td>
                        <td>
                            <!-- Botões para confirmar ou eliminar pedido -->
                            <asp:Button ID="btn_confirmar" runat="server" Text="Confirmar" CssClass="confirmar-button" OnCommand="Confirmarpedido" CommandName="Confirmar" CommandArgument='<%# Eval("id_pedido") %>' />
                            <asp:Button ID="btn_eliminar" runat="server" Text="  Eliminar " CssClass="eliminar-button" OnCommand="Eliminarpedido" CommandName="Eliminar" CommandArgument='<%# Eval("id_pedido") %>' />
                        </td>
                    </tr>
                </tbody>
            </table>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
