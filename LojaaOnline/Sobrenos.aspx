<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="Sobrenos.aspx.cs" Inherits="LojaaOnline.Sobrenos" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Sobre Nós</title>

    <style>
          

        section {
            padding: 2em;
            text-align: center;
        }

        h2 {
            color: #333;
        }

        p {
            color: #555;
        }

        .team-member {
            display: flex;
            justify-content: space-around;
            flex-wrap: wrap;
            margin-top: 2em;
        }

        .member-card {
            width: 300px;
            padding: 1em;
            margin: 1em;
            background-color: white;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            transition: transform 0.3s ease-in-out;
        }

        .member-card:hover {
            transform: scale(1.05);
        }

        @keyframes fadeIn {
            from {
                opacity: 0;
            }
            to {
                opacity: 1;
            }
        }
    </style>


</asp:Content>






<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br /><br /><br /><br /><br /><br />

    <section>
        <h2>Quem Somos</h2>
        <p>Somos uma equipe dedicada apaixonada por proporcionar a melhor experiência de compra online.</p>
    </section>

    <section>
        <h2>Nossa Equipa</h2>
        <div class="team-member">           
            <div class="member-card" style="animation: fadeIn 1s 0.4s;">
                <h3>Daniel Gonçalves</h3>
                <p>Desenvolvedor</p>
            </div>
        </div>
    </section>

     <br /><br /><br /><br /><br /><br />

</asp:Content>
