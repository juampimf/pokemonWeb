<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="PoquedexWeb._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row row-cols-1 row-cols-md-3 g-4">
      <%--  <%
            foreach (dominio.Pokemon poke in ListaPokemon)// de esta forma yo ya tengo el pokemon con el tipo que corresponde
            {
        %>


        <div class="col">
            <div class="card">
                <img src="<%: poke.UrlImagen %>" class="card-img-top" alt="...">
                <div class="card-body">
                    <h5 class="card-title"><%: poke.Nombre %></h5>
                    <p class="card-text"><%: poke.Descripcion %></p>
                    <a href="DetallePokemon.aspx?id=<%: poke.Id %>">Ver Detalle</a>
                </div>
            </div>
        </div>
        <%}%>--%>

        <asp:Repeater ID="repRepetidor" runat="server">
            <ItemTemplate>
                <div class="col">
                    <div class="card">
                        <img src="<%# Eval("UrlImagen") %>" class="card-img-top" alt="...">
                        <div class="card-body">
                            <h5 class="card-title"><%# Eval("Nombre")%></h5>
                            <p class="card-text"><%# Eval("Descripcion")%></p>
                            <a href="DetallePokemon.aspx?id=<%# Eval("id")%>">Ver Detalle</a>
                           <asp:Button Text="Ejemplo" CssClass="btn btn-primary" runat="server" ID="btnEjemplo" CommandArgument='<%#Eval("id") %>' CommandName="PokemonId" OnClick="btnEjemplo_Click" /> <%-- lo que nos permite el reapeter es mandar un argumento por un elemento de asp como es el boton en este caso. Eso lo hacemos agregando el CommandArgument que tiene que ir entre comillas simples. tmb un CommandName para manejarnos en el codebehind, que en este caso no usamos y el evento onclick que en este caso es un boton  --%>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>


    </div>

</asp:Content>
