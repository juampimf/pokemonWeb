using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;


namespace PoquedexWeb
{
    public partial class _default : System.Web.UI.Page
    {
        public List<Pokemon> ListaPokemon { get; set; } // esta lista pokemon se va a cargar del resultado de ir a buscar datos a la base de datos
        protected void Page_Load(object sender, EventArgs e)
        {
            PokemonNegocio negocio = new PokemonNegocio();
            ListaPokemon = negocio.listarConSP(); //en esta linea fui a buscar los datos a la DB y ahora estan en la propiedad(ListaPokemon) de la clase 
            //una ves que la lista esté cargada se puede hacer uso de ella en la pagina default por medio de un foreach

            if (!IsPostBack)
            {
            repRepetidor.DataSource = ListaPokemon;
            repRepetidor.DataBind();

            }

        }

        protected void btnEjemplo_Click(object sender, EventArgs e) //el boton es el que lanza el evento, y el boton tiene un argumento. El object sender es el elemento que envia el evento
        {
            string valor = ((Button)sender).CommandArgument;                         //el sender es un object pero yo sé que es un boton por eso hago el casteo explisito y el .CommandArgument es para traer el valor que tiene el boton en el argumento. 
        }// en resumen: para capturar el valor que pasamos desde el reapeter, transformamos el sender (que es el objeto que lanzó el evento) en boton, luego capturamos el argument y lo guardamos en una variable de tipo string que despues usaremos para algo
    }
}