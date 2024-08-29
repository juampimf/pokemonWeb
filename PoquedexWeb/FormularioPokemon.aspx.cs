using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace PoquedexWeb
{
    public partial class FomularioPokemon : System.Web.UI.Page
    {
        public bool ConfirmaEliminacion { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            txtId.Enabled = false;
            ConfirmaEliminacion = false;
            try
            {
                // configuracion inicial de la pantalla.
                if (!IsPostBack)
                {
                    ElementoNegocio negocio = new ElementoNegocio();// en estas dos lineas me traigo la lista de la base de datos una sola vez, la guardo en el objeto(lista) y con esa lista voy a cargar los dos desplegables
                    List<Elemento> lista = negocio.listar();

                    ddlTipo.DataSource = lista;              // acá configuramos cual va a ser el dato que va a mostrar en pantalla y cual va a ser el dato que va a llevar escondido. El dato que vamos a tener oculto va a ser el value que despues vamos a querer capturar de la seleccion de esos elementos (no confundir con el index)  
                    ddlTipo.DataValueField = "id";
                    ddlTipo.DataTextField = "Descripcion";  // despues configuramos lo que queremos que muestre poniendo el nombre de la propiedad de la clase
                    ddlTipo.DataBind();

                    ddlDebilidad.DataSource = lista;
                    ddlDebilidad.DataValueField = "id";
                    ddlDebilidad.DataTextField = "Descripcion";
                    ddlDebilidad.DataBind();
                }

                //configuracion si estamos modificando.
                string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : ""; // si el request.QuerySring["id"]es distinto de nulo entonces le voy a guardar lo que tiene el querystring.toString si nó le vamos a guardar vacio 
                if (id != "" && !IsPostBack)  //
                {
                    PokemonNegocio negocio = new PokemonNegocio();
                    //List<Pokemon> lista = negocio.listar(id); //acá guardo en una lista lo que sé que me va a devolver una list y despues abajo pido el primer elemento
                    //Pokemon seleccionado = lista[0];
                    Pokemon seleccionado = (negocio.listar(id))[0]; //como yo ya se que esto va a devolver una lisa, entonces de esta forma digo que de la lista que me va a devolver este metodo, tome el primer elemento.

                    //guardo pokemon seleccionado en session
                    Session.Add("pokeSeleccionado", seleccionado);


                    //precargar todos los campos...

                    txtId.Text = id;
                    txtNombre.Text = seleccionado.Nombre;
                    txtDescripcion.Text = seleccionado.Descripcion;
                    txtImagenUrl.Text = seleccionado.UrlImagen;
                    txtNumero.Text = seleccionado.Numero.ToString();

                    ddlTipo.SelectedValue = seleccionado.Tipo.Id.ToString();
                    ddlDebilidad.SelectedValue = seleccionado.Debilidad.Id.ToString();
                    txtImagenUrl_TextChanged(sender, e);

                    //configurar acciones
                    if (!seleccionado.Activo)
                        btnInactivar.Text = "Reactivar";

                }

            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
                //redireccion pantalla error
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Pokemon nuevo = new Pokemon();
                PokemonNegocio negocio = new PokemonNegocio();


                nuevo.Numero = int.Parse(txtNumero.Text);
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.UrlImagen = txtImagenUrl.Text;

                nuevo.Tipo = new Elemento();  //aca se genera el new elemento para que tenga un objeto y asi poder manipularlo
                nuevo.Tipo.Id = int.Parse(ddlTipo.SelectedValue);
                nuevo.Debilidad = new Elemento();
                nuevo.Debilidad.Id = int.Parse(ddlDebilidad.SelectedValue);

                if (Request.QueryString["id"] != null)
                {
                    nuevo.Id = int.Parse(txtId.Text);
                    negocio.modificarConSP(nuevo);
                }
                else
                    negocio.agregarConSP(nuevo);

                Response.Redirect("PokemonLista.aspx", false);

            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
        }

        protected void txtImagenUrl_TextChanged(object sender, EventArgs e)
        {
            imgPokemon.ImageUrl = txtImagenUrl.Text;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ConfirmaEliminacion = true;
        }

        protected void btnConfirmaEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkConfirmaEliminacion.Checked)
                {
                    PokemonNegocio negocio = new PokemonNegocio();
                    negocio.eliminar(int.Parse(txtId.Text));
                    Response.Redirect("PokemonLista.aspx");
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
        }

        protected void btnInactivar_Click(object sender, EventArgs e)
        {
            try
            {
                PokemonNegocio negocio = new PokemonNegocio();
                Pokemon seleccionado = (Pokemon)Session["pokeSeleccionado"];

                negocio.eliminarLogico(seleccionado.Id, !seleccionado.Activo);
                Response.Redirect("PokemonLista.aspx");
            }
            catch (Exception ex)
            {

                Session.Add("error", ex);
            }
        }
    }
}