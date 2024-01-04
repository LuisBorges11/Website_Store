<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="Produtos.aspx.cs" Inherits="LojaaOnline.Produtos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Estilos para a página de produtos */

        .custom-dropdown {
            padding: 5px;
            border: 1px solid #ccc;
            border-radius: 5px;
            background-color: #f8f8f8;
            font-size: 14px;
            color: #333;
        }

        .custom-button {
            padding: 10px 20px;
            background-color: #48ca95;
            color: #fff;
            border: none;
            border-radius: 5px;
            font-size: 16px;
            cursor: pointer;
        }

        .custom-button:hover {
            background-color: black;
        }

        .search-and-sort-container {
            display: flex;
            justify-content: space-around;
            align-items: center;
            margin-bottom: 20px;
        }

        .dropdown-container,
        .search-container {
            text-align: center;
        }

        .search-container {
            flex-grow: 1;
            margin: 0 10px;
        }

        .custom-button-repeater {
            background-color: #48ca95;
            color: #fff;
            border: none;
            border-radius: 5px;
            padding: 10px 20px;
            margin-top: 10px;
        }

        .custom-button-repeater:hover {
            background-color: black;
        }

        .pagination {
            display: flex;
            justify-content: center;
            align-items: center;
            margin-top: 20px;
        }

        .pagination-link {
            padding: 8px 16px;
            margin: 0 4px;
            border: 1px solid #ddd;
            text-decoration: none;
            color: #fff;
            cursor: pointer;
            border-radius: 4px;
            background-color: #48ca95;
            transition: background-color 0.3s;
        }

        .pagination-link2 {
            padding: 8px 16px;
            margin: 0 4px;
            border: 1px solid #ddd;
            text-decoration: none;
            color: #333;
            cursor: pointer;
            border-radius: 4px;
            background-color: #fff;
            transition: background-color 0.3s;
        }

        .pagination-link:hover {
            background-color: black;
        }

        .pagination-link.current {
            background-color: #48ca95;
            color: #fff;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br/><br/>
    <!-- Mensagens de erro e de sucesso -->
    <center>
        <h1>
            <asp:Label ID="lbl_mensagem" runat="server" ForeColor="Red"></asp:Label>
            <asp:Label ID="lbl_mensagem_sucesso" runat="server" ForeColor="Green"></asp:Label>
        </h1>
    </center>

    <div class="products">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="titlepage">
                        <h2>Os nossos produtos</h2>
                    </div>
                </div>
            </div>

            <div class="search-and-sort-container">
                <div class="dropdown-container">
                    <!-- Dropdown para seleção de critério de ordenação -->
                    <asp:DropDownList ID="ddlCriterioOrdenacao" runat="server" CssClass="custom-dropdown">
                        <asp:ListItem Text="Nome" Value="nome_produto"></asp:ListItem>
                        <asp:ListItem Text="Preço" Value="preco_produto"></asp:ListItem>
                    </asp:DropDownList>

                    <!-- Dropdown para seleção de ordem (ascendente/descendente) -->
                    <asp:DropDownList ID="ddlOrdem" runat="server" CssClass="custom-dropdown">
                        <asp:ListItem Text="Ascendente" Value="ASC"></asp:ListItem>
                        <asp:ListItem Text="Descendente" Value="DESC"></asp:ListItem>
                    </asp:DropDownList>

                    <!-- Botão para iniciar a ordenação -->
                    <asp:Button ID="btnOrdenar" runat="server" Text="Ordenar" OnClick="btnOrdenar_Click" CssClass="custom-button" Height="55px" Width="100px" />
                </div>

                <div class="search-container">
                    <!-- Campo de pesquisa por nome -->
                    <asp:TextBox ID="txtPesquisa" runat="server" CssClass="custom-dropdown" placeholder="Pesquisar por nome..." />
                    <!-- Botão para iniciar a pesquisa -->
                    <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" CssClass="custom-button" OnClick="btnPesquisar_Click" />
                </div>

                <div class="dropdown-container">
                    <!-- Dropdown para seleção do tipo de produto -->
                    <asp:DropDownList ID="ddlTipoProduto" runat="server" CssClass="custom-dropdown" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoProduto_SelectedIndexChanged">
                        <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Placa Gráfica" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Processadores" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Rams" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Caixas" Value="4"></asp:ListItem>
                        <asp:ListItem Text="Fontes" Value="5"></asp:ListItem>
                        <asp:ListItem Text="MotherBoards" Value="6"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <div class="our_products">
                        <div class="row">
                            <!-- Repeater para mostrar os produtos -->
                            <asp:Repeater ID="reptProduct" runat="server">
                                <ItemTemplate>
                                    <div class="col-md-4 margin_bottom1">
                                        <div class="product_box">
                                            <!-- Imagem do produto -->
                                            <img src='<%# "data:image/jpeg;base64," + Convert.ToBase64String((byte[])Eval("imagem_produto")) %>' alt="Imagem do Produto" />
                                            <!-- Nome do produto -->
                                            <h3>
                                                <asp:Label runat="server" ID="lblName" Text='<%# Eval("nome_produto") %>'></asp:Label>
                                            </h3>
                                            <!-- Descrição do produto -->
                                            <p>
                                                <asp:Label runat="server" ID="Label2" Text='<%# Eval("descricao_produto") %>'></asp:Label>
                                            </p>
                                            <!-- Preço do produto -->
                                            <p>
                                                <asp:Label runat="server" ID="Label1" Text='<%# Eval("preco_produto") %>'></asp:Label>€
                                            </p>
                                            <!-- Botão para adicionar ao carrinho -->
                                            <asp:Button ID="btn_adicionarcarrinho" runat="server" Text="Carrinho" OnClick="btn_adicionarcarrinho_Click"
                                                CommandArgument='<%# Eval("cod_produto") %>' CssClass="custom-button-repeater" />
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Paginação -->
            <center>
                <div class="pagination">
                    <!-- Link para página anterior -->
                    <asp:LinkButton ID="lnkAnterior" runat="server" OnClick="lnkAnterior_Click" Text="Anterior" CssClass="pagination-link"></asp:LinkButton>
                    <!-- Repeater para os links de páginas -->
                    <asp:Repeater ID="rptPager" runat="server" OnItemCommand="lnkPagina_Click">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkPage" runat="server" OnClick="lnkPagina_Click" CssClass="pagination-link2" CommandArgument='<%# Container.DataItem %>'><%# Container.DataItem %></asp:LinkButton>
                        </ItemTemplate>
                    </asp:Repeater>
                    <!-- Link para página seguinte -->
                    <asp:LinkButton ID="lnkProxima" runat="server" OnClick="lnkProxima_Click" Text="Próxima" CssClass="pagination-link"></asp:LinkButton>
                </div>
            </center>
        </div>
    </div>
</asp:Content>
