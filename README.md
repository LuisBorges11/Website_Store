# Loja Online em C# ASP.NET

Bem-vindo ao projeto da Loja Online desenvolvido em C# utilizando ASP.NET. Este projeto simula uma plataforma de e-commerce onde os usuários podem navegar, pesquisar, adicionar produtos ao carrinho e fazer compras online.

## Descrição

A Loja Online é uma aplicação web completa que permite aos usuários explorar diferentes categorias de produtos, visualizar detalhes dos itens, adicioná-los ao carrinho e finalizar a compra. A aplicação também inclui uma interface de administração para gerenciar produtos, categorias e pedidos.

## Funcionalidades

- **Catálogo de Produtos**: Navegue e pesquise por produtos em diversas categorias.
- **Carrinho de Compras**: Adicione produtos ao carrinho e veja um resumo do pedido.
- **Finalização de Compra**: Complete a compra fornecendo informações de pagamento e envio.
- **Autenticação e Autorização**: Cadastro e login de usuários, com funções específicas para administradores.
- **Administração**: Interface para gerenciar produtos, categorias, usuários e pedidos.

## Requisitos

- .NET Core SDK 3.1 ou superior
- Sistema Operacional: Windows, macOS ou Linux
- Banco de Dados: SQL Server, SQLite ou outro compatível

## Como Usar

1. Clone este repositório para sua máquina local.
    ```bash
    git clone https://github.com/seuusuario/loja-online-aspnet.git
    ```
2. Navegue até o diretório do projeto.
    ```bash
    cd loja-online-aspnet
    ```
3. Restaure as dependências do projeto.
    ```bash
    dotnet restore
    ```
4. Configure o banco de dados no arquivo `appsettings.json`.
    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=LojaOnlineDb;Trusted_Connection=True;MultipleActiveResultSets=true"
      }
    }
    ```
5. Atualize o banco de dados.
    ```bash
    dotnet ef database update
    ```
6. Compile e execute o projeto.
    ```bash
    dotnet run
    ```
7. Abra o navegador e acesse `http://localhost:5000` para visualizar a loja online.

## Estrutura do Projeto

- **/LojaOnline**: Contém o código fonte da aplicação.
  - **Controllers**: Controladores responsáveis pelo fluxo da aplicação.
  - **Models**: Modelos que representam os dados da aplicação.
  - **Views**: Páginas HTML renderizadas pelo servidor.
  - **wwwroot**: Arquivos estáticos como CSS, JavaScript e imagens.
  - **Data**: Contexto do banco de dados e inicialização.
  - **Services**: Serviços que encapsulam a lógica de negócio.

## Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues e pull requests para melhorar a loja online.

## Licença

Este projeto está licenciado sob a MIT License. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

## Contato

Para mais informações, entre em contato:

- Email: filipefigueiredo39@gmail.com
- LinkedIn: [Luis Borges](https://www.linkedin.com/in/luis-figueiredo-232897258)
