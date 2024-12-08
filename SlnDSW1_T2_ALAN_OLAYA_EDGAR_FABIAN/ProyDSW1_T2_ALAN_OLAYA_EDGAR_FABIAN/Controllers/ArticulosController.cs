using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProyDSW1_T2_ALAN_OLAYA_EDGAR_FABIAN.DAO;
using ProyDSW1_T2_ALAN_OLAYA_EDGAR_FABIAN.Models;


namespace ProyDSW1_T2_ALAN_OLAYA_EDGAR_FABIAN.Controllers
{
    public class ArticulosController : Controller
    {
        private readonly ArticulosDAO dao;

        List<Carrito> lista_carrito = new List<Carrito>();

        public ArticulosController(ArticulosDAO _dao)
        {
            this.dao = _dao;
        }

        //-- Serializar List<Carrito> a cadena
        void GrabarCarrito()
        {
            string cad_json = JsonConvert.SerializeObject(lista_carrito);
            HttpContext.Session.SetString("Carrito", cad_json);
        }

        //-- Deserializar la cadena y convertirlo en List<Carrito>
        List<Carrito> LeerCarrito()
        {
            string cad_json = HttpContext.Session.GetString("Carrito")!;
            return JsonConvert.DeserializeObject<List<Carrito>>(cad_json)!;
        }



        // GET: ArticulosController
        public ActionResult IndexArticulos(string? filtro = null)
        {
            if (HttpContext.Session.GetString("Carrito") == null)
            {
                GrabarCarrito();
            }

            var listado = string.IsNullOrEmpty(filtro)
                ? dao.GetArticulos()
                : dao.GetArticulosPorFiltro(filtro);
            //
            ViewBag.FiltroActual = filtro; // Guardamos el filtro para mostrarlo en la vista
            return View(listado);
        }

        // GET
        public ActionResult AgregarAlCarrito(string id)
        {
            return View(dao.BuscarArticulo(id));
        }

        // POST
        [HttpPost]
        public ActionResult AgregarAlCarrito(string id, int cantidad)
        {
            // recuperar los datos del producto
            var prod = dao.BuscarArticulo(id);
            // definir una variable del tipo carrito
            var car = new Carrito()
            {
                Codigo = prod.cod_art,
                NombreArticulo = prod.nom_art,
                Precio = prod.pre_art,
                Cantidad = cantidad
            };
            // recuperar el carrito de compra
            lista_carrito = LeerCarrito();
            // buscamos el codigo del objeto car en el carrito de compra
            var encontrado = lista_carrito.Find(x => x.Codigo.Equals(id));
            // si es nulo, el id no fue encontrado, entonces lo agregamos al
            // carrito
            if (encontrado == null)
            {
                lista_carrito.Add(car);
                ViewBag.mensaje = "Nuevo Articulo Agregado al Carrito";
                TempData["ArticuloAgregado"] = true;//chatgpt              
            }
            else // si ya existe en el carrito, aumentamos la cantidad
            {
                encontrado.Cantidad += cantidad;
                ViewBag.mensaje = "Nueva Cantidad del Articulo: " +
                          $"{encontrado.Codigo} = {encontrado.Cantidad}";
                TempData["ArticuloAgregado"] = true;  // Marcar como agregado chatgpt
            }
            // en cualquiera de los casos debemos grabar los cambios en el carrito
            GrabarCarrito();
            //
            return View(prod);
        }

        // GET
        public ActionResult VerCarrito()
        {
            lista_carrito = LeerCarrito();
            // si no hay productos en el carrito, lo
            // enviamos a la lista de productos
            if (lista_carrito.Count == 0)
            {
                return RedirectToAction("IndexArticulos");
            }
            // enviamos el importe total por los productos 
            // del carrito
            ViewBag.total = lista_carrito.Sum(c => c.Importe);
            //
            return View(lista_carrito);
        }

        //GET
        public ActionResult EliminarArticuloCarrito(string id)
        {
            lista_carrito = LeerCarrito();

            var buscado = lista_carrito.Find(p =>
                            p.Codigo.Equals(id));
            buscado.Cantidad -= 1;

            if (buscado.Cantidad == 0)
                lista_carrito.Remove(buscado);
            GrabarCarrito();
            return RedirectToAction("VerCarrito");
        }

        
    }
}
