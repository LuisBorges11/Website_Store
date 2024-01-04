<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminMain.aspx.cs" Inherits="LojaaOnline.AdminMain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    </div>
    <div class="clearfix"></div>
    <br/>

    <!-- Seção de blocos de informações -->
    <div class="col-div-3">
        <!-- Bloco de informações para Utilizadores -->
        <div class="box">
            <p>
                <asp:Label ID="Label1" runat="server" Text="Label" style="font-size: 30px; font-weight: bold;"></asp:Label>
                <br/><span>Utilizadores</span>
            </p>
            <i class="fa fa-users box-icon"></i>
        </div>
    </div>
    <div class="col-div-3">
        <!-- Bloco de informações para Pedidos -->
        <div class="box">
            <p>
                <asp:Label ID="Label2" runat="server" Text="Label" style="font-size: 30px; font-weight: bold;"></asp:Label>
                <br/><span>Pedidos</span>
            </p>
            <i class="fa fa-shopping-bag box-icon"></i>
        </div>
    </div>
    <div class="col-div-3">
        <!-- Bloco de informações para Produtos -->
        <div class="box">
            <p>
                <asp:Label ID="Label3" runat="server" Text="Label" style="font-size: 30px; font-weight: bold;"></asp:Label>
                <br/><span>Produtos</span>
            </p>
            <i class="fa fa-tasks box-icon"></i>
        </div>
    </div>

    <!-- Limpa a flutuação (clear) -->
    <div class="clearfix"></div>

    <br/><br/>
    <div class="col-div-8">
        <!-- Caixa de conteúdo para Produtos com menos stock -->
        <div class="box-8">
            <div class="content-box">
                <p>Produtos com menos stock</p>
                <div style="overflow-y: auto; max-height: 280px;">
                    <table>
                        <tr>
                            <th>Produto</th>
                            <th>Stock</th>
                        </tr>
                        <!-- Loop para exibir produtos com menos stock -->
                        <% foreach (var produto in ListaDeProdutosOrdenadosPorStock()) { %>
                            <tr>
                                <td><%= produto.Nome %></td>
                                <td><%= produto.Stock %></td>
                            </tr>
                        <% } %>
                    </table>
                </div>
            </div>
        </div>
    </div>

    <div class="col-div-4">
        <!-- Caixa de conteúdo para Total de Vendas -->
        <div class="box-4">
            <div class="content-box">
                <p>Total de vendas este mês</p>
                <!-- Círculo de exibição de percentagem de venda -->
                <div class="circle-wrap">
                    <div class="circle">
                        <div class="mask full">
                            <div class="fill"></div>
                        </div>
                        <div class="mask half">
                            <div class="fill"></div>
                        </div>
                        <div class="inside-circle"> 70% </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
