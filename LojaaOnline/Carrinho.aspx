<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="Carrinho.aspx.cs" Inherits="LojaaOnline.Carrinho" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br /><br /><br />
    <!-- Container para exibir o carrinho de compras -->
    <div class="container">
        <br/>     
        <!-- Mensagem de aviso em vermelho -->
        <center><h1><asp:Label ID="lbl_mensagem" runat="server" ForeColor="Red"></asp:Label></h1></center>
        <!-- Mensagem indicando que o produto foi eliminado com sucesso em verde -->
        <asp:Label ID="lblProdutoEliminado" runat="server" ForeColor="Green" Visible="false"></asp:Label>
        <br/><br/><br/>
        <!-- Tabela para exibir os detalhes dos produtos no carrinho -->
        <table class="table table-bordered">
            <thead>
                <!-- Cabeçalho da tabela -->
                <tr>
                    <th>Nome do Produto</th>
                    <th>Preço</th>
                    <th>Imagem do Produto</th>
                    <th>Quantidade</th>
                    <th>Preço Total do Produto</th>
                    <th>Ações</th>
                </tr>
            </thead>
            <tbody>
                <!-- Repeater para exibir detalhes de cada produto no carrinho -->
                <asp:Repeater ID="repeaterDetalhesProduto" runat="server">
                    <ItemTemplate>
                        <!-- Linha para cada produto no carrinho -->
                        <tr>
                            <!-- Nome do produto -->
                            <td><%# Eval("nome_produto") %></td>
                            <!-- Preço do produto -->
                            <td><asp:Label ID="lblprecoproduto" runat="server" Text='<%# Eval("preco_produto", "{0:C}") %>'></asp:Label></td>
                            <!-- Imagem do produto -->
                            <td>
                                <img src='<%# "data:image/jpeg;base64," + Convert.ToBase64String((byte[])Eval("imagem_produto")) %>'
                                    alt="Imagem do Produto" style="max-width: 100px; max-height: 100px;" />
                            </td>
                            <!-- Quantidade do produto com botões para aumentar e diminuir -->
                            <td>
                                <asp:Label ID="lblQuantidade" runat="server" Text='<%# Eval("quantidade") %>'></asp:Label>
                                <asp:Button ID="btnAumentarQuantidade" runat="server" Text="+" OnCommand="AumentarQuantidade" CommandName="Aumentar" CommandArgument='<%# Eval("id_carrinho") %>' />
                                <asp:Button ID="btnDiminuirQuantidade" runat="server" Text="-" OnCommand="DiminuirQuantidade" CommandName="Diminuir" CommandArgument='<%# Eval("id_carrinho") %>' />
                            </td>
                            <!-- Preço total do produto -->
                            <td>
                                <asp:Label ID="lblTotalProduto" runat="server" Text='<%# Eval("precoproduto", "{0:C}") %>'></asp:Label>
                            </td>
                            <!-- Botão para eliminar o produto do carrinho -->
                            <td>
                                <asp:Button ID="btn_eliminar" runat="server" Text="Eliminar" OnCommand="EliminarProduto" CommandName="Eliminar"
                                    CommandArgument='<%# Eval("id_carrinho") %>' CssClass="btn btn-danger" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                
                <!-- Linha para exibir o total do carrinho -->
                <tr>
                    <td colspan="4" class="text-right">
                        <strong>Total do Carrinho:</strong> <asp:Label ID="lblTotalCarrinho" runat="server" CssClass="text-success"></asp:Label>
                    </td>
                </tr>
            </tbody>
        </table>
        </br>
        <!-- Botão para realizar o checkout -->
        <asp:Button ID="btn_checkout" runat="server" Text="Fazer Checkout" OnClick="btn_checkout_Click" CssClass="btn btn-success" />
    </div>
    </br></br></br></br><br /><br /><br />
</asp:Content>