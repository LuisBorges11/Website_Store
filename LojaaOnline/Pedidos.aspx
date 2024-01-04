<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="Pedidos.aspx.cs" Inherits="LojaaOnline.Pedidos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">  
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br /><br />
    <!-- Título da página -->
    <div class="container">
        <br />
        <!-- Mensagens de erro ou sucesso -->
        <center><h1><asp:Label ID="lbl_mensagem" runat="server" ForeColor="Red"></asp:Label></h1></center>
        <center><h1><asp:Label ID="lbl_mensagem2" runat="server" ForeColor="Green"></asp:Label></h1></center>
        <br/><br/>
        <h1>Pedidos</h1>

        <!-- Tabela para mostrar os detalhes dos pedidos -->
        <table class="table table-striped">
            <thead>
                <tr>
                    <th>Nome produto</th>                    
                    <th>Telemovel</th>
                    <th>Morada</th>
                    <th>Código Postal</th>
                    <th>Data do Pedido</th>
                    <th>Status do Pedido</th>
                    <th>Quantidade</th>
                    <th>Preço Total</th>
                </tr>
            </thead>
            <tbody>
                <!-- Repeater para mostrar os detalhes de cada pedido -->
                <asp:Repeater ID="repeaterPedidos" runat="server">
                    <ItemTemplate>
                        <tr>
                            <!-- Mostra os detalhes do pedido -->
                            <td><%# GetNomeProduto(Eval("cod_produto")) %></td>                            
                            <td><%# Eval("telemovel") %></td>
                            <td><%# Eval("morada") %></td>
                            <td><%# Eval("codigopostal") %></td>
                            <td><%# Eval("datapedido", "{0:dd/MM/yyyy - HH:mm}") %></td>
                            <td style='<%# GetStatusStyle(Eval("StatusPedido")) %>'><%# Eval("StatusPedido") %></td>
                            <td><%# Eval("quantidade") %></td>
                            <td><%# Eval("precototal", "{0:C}") %></td>
                            <td>
                                <!-- Botão para gerar PDF do pedido -->
                                <asp:Button ID="btnGerarPDF" runat="server" Text="PDF" 
                                    CommandName="GerarPDF" CommandArgument='<%# Eval("id_pedido") %>'
                                    OnCommand="GerarPDF_Command" Visible='<%# IsStatusConfirmado(Eval("StatusPedido")) %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>

    <br /><br /> <br /><br />
</asp:Content>
