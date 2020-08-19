using BLL;
using Consola.Helpers;
using Consola.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Consola.Controllers
{
    [SessionManage]
    public class InventarioController : Controller
    {
        // GET: Inventario
        public ActionResult Index()
        {
            try
            {
                List<Inventario> listaInventario = new List<Inventario>();
                clsInventario invent = new clsInventario();
                var data = invent.ConsultarInventario().ToList();

                foreach (var item in data)
                {
                    Inventario modelo = new Inventario();
                    modelo.idStock = item.idStock;
                    modelo.codigo = item.codigo;
                    modelo.nombreProducto = item.nombreProducto;
                    modelo.unidad = item.unidad;
                    modelo.idBodega = item.idBodega;
                    modelo.idProveedor = item.idProveedor;
                    modelo.estadoStock = item.estadoStock;

                    listaInventario.Add(modelo);
                }
                return View(listaInventario);
            }
            catch
            {
                string descMsg = "Error consultando la informacion del CLiente.";
                //Bitacora
                return RedirectToAction("Error", "Error");
            }
        }

        // GET: Inventario/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Inventario/Create
        public ActionResult Crear()
        {
            return View();
        }

        // POST: Inventario/Create
        [HttpPost]
        public ActionResult Crear(Inventario stocks)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    clsInventario objstock = new clsInventario();

                    bool Resultado = objstock.AgregarInventario(stocks.codigo, stocks.nombreProducto, stocks.unidad, stocks.idBodega, stocks.idProveedor, true);

                    if (Resultado)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View("Crear");
                    }
                }
                else
                {
                    return View("Crear");
                }
            }
            catch
            {
                TempData["errorMensaje"] = "Inserte correctamente el formato de los datos.";
                return View();
            }
        }
    }
}
