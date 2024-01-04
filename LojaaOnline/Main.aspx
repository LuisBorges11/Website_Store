<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="LojaaOnline.Main" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">

    <!-- Configuração da página -->
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <style>
        /* Estilos CSS para a página Main.aspx */
        figure img {
          border-radius: 10px;    
        }

        .banner_main {
            background: url(../images/banner2.jpg);
            background-repeat: no-repeat;
            min-height: 900px;
            background-size: 100% 100%;
            display: flex;
            justify-content: center;
            align-content: center;
            align-items: center;
            position: relative;
        }
        
        .produtos-em-destaque {
            padding: 80px 0;
        }

        .produto {
            border: 1px solid #ddd;
            padding: 20px;
            margin-bottom: 20px;
        }

        .produto-img img {
            max-width: 100%;
            height: auto;
        }

        .produto-info h3 {
            margin-top: 15px;
        }

        .produto-info p {
            margin-bottom: 15px;
        }

        .produto-info a {
            display: inline-block;
            background-color: #4CAF50;
            color: white;
            padding: 10px 20px;
            text-decoration: none;
        }
    </style>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Seção do banner -->
    <section class="banner_main">
        <!-- Configurações do banner -->
        <div id="banner1" class="carousel slide" data-ride="carousel">
            <ol class="carousel-indicators">
               <li data-target="#banner1" data-slide-to="0" class="active"></li>
               <li data-target="#banner1" data-slide-to="1"></li>
               <li data-target="#banner1" data-slide-to="2"></li>
               <li data-target="#banner1" data-slide-to="3"></li>             
            </ol>
            <div class="carousel-inner">
               <!-- Slides do banner -->
               <div class="carousel-item active">
                  <!-- Conteúdo do primeiro slide -->
                  <div class="container">
                     <div class="carousel-caption">
                        <div class="row">
                           <div class="col-md-6">
                              <div class="text-bg">
                                 <span>Placas Gráficas</span>
                                 <h1>As Melhores</h1>
                                 <p>Existem muitas variações de passagens de Lorem Ipsum disponíveis, mas a maioria sofreu alguma alteração, por humor injetado ou </p>
                                 <a href="Produtos.aspx">Comprar </a> <a href="contact.html">Contactar </a>
                              </div>
                           </div>
                           <div class="col-md-6">
                              <div class="text_img">
                                    <figure>
                                       <img src="images/Placa-de-video-AMD-Radeon.jpg" alt="#">
                                    </figure>
                              </div>
                           </div>
                        </div>
                     </div>
                  </div>
               </div>
               <!-- Conteúdo do segundo slide -->
               <div class="carousel-item">
                   <div class="container">
                      <div class="carousel-caption">
                         <div class="row">
                            <div class="col-md-6">
                               <div class="text-bg">
                                  <span>Memorias Ram</span>
                                  <h1>As Melhores</h1>
                                  <p>There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or </p>
                                   <a href="Produtos.aspx">Comprar </a> <a href="contact.html">Contact </a>
                               </div>
                            </div>
                            <div class="col-md-6">
                               <div class="text_img">
                                  <figure><img src="images/rams.jpg" alt="#"/></figure>
                               </div>
                            </div>
                         </div>
                      </div>
                   </div>
                </div>
                <!-- Conteúdo do terceiro slide -->
                <div class="carousel-item">
                   <div class="container">
                      <div class="carousel-caption">
                         <div class="row">
                            <div class="col-md-6">
                               <div class="text-bg">
                                  <span>Processadores</span>
                                  <h1>Os Melhores</h1>
                                  <p>There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or </p>
                                   <a href="Produtos.aspx">Comprar </a> <a href="contact.html">Contact </a>
                               </div>
                            </div>
                            <div class="col-md-6">
                               <div class="text_img">
                                  <figure><img src="images/processador.jpg" alt="#"/></figure>
                               </div>
                            </div>
                         </div>
                      </div>
                   </div>
                </div>
                <!-- Conteúdo do quarto slide -->
                <div class="carousel-item">
                   <div class="container">
                      <div class="carousel-caption">
                         <div class="row">
                            <div class="col-md-6">
                               <div class="text-bg">
                                  <span>MotherBoards</span>
                                  <h1>As Melhores</h1>
                                  <p>There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or </p>
                                   <a href="Produtos.aspx">Comprar </a> <a href="contact.html">Contact </a>
                               </div>
                            </div>
                            <div class="col-md-6">
                               <div class="text_img">
                                  <figure><img src="images/motheboards.jpg" alt="#"/></figure>
                               </div>
                            </div>
                         </div>
                      </div>
                   </div>
                </div>  
            </div>
            <!-- Controlos de navegação do banner -->
            <a class="carousel-control-prev" href="#banner1" role="button" data-slide="prev">
                <i class="fa fa-chevron-left" aria-hidden="true"></i>
            </a>
            <a class="carousel-control-next" href="#banner1" role="button" data-slide="next">
                <i class="fa fa-chevron-right" aria-hidden="true"></i>
            </a>
        </div>
    </section>
    <!-- Fim da seção do banner -->

    <!-- Seção de Produtos em Destaque -->
    <section class="produtos-em-destaque">
        <!-- Configurações da seção de produtos em destaque -->
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="titlepage">
                        <h2>Produtos em Destaque</h2>
                    </div>
                </div>
            </div>
            <div class="row">
                <!-- Repeater para mostrar produtos em destaque -->
                <asp:Repeater ID="repeaterProdutosDestaque" runat="server">
                    <ItemTemplate>
                        <div class="col-md-4">
                            <div class="produto">
                                <div class="produto-img">
                                    <!-- Mostra a imagem do produto convertida para Base64 -->
                                    <CENTER><img src='<%# "data:image/jpeg;base64," + Convert.ToBase64String((byte[])Eval("imagem_produto")) %>' alt="Imagem do Produto" /></CENTER>
                                </div>
                                <div class="produto-info">
                                    <!-- Mostra o nome e a descrição do produto -->
                                    <h3><%# Eval("nome_produto") %></h3>
                                    <p><%# Eval("descricao_produto") %></p>                                      
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </section>
    <!-- Fim da Seção de Produtos em Destaque -->
</asp:Content>
