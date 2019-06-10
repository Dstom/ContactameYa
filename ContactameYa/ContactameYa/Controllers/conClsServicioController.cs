using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ContactameYa.Models;

namespace ContactameYa.Controllers
{
    public class conClsServicioController : Controller
    {
        private conSERpServicio PobjServicio = new conSERpServicio();
        private conCATtCategoria PobjCategoriaServicio = new conCATtCategoria();

        private conDTOtDepartamento PobjDepartamento = new conDTOtDepartamento();
        private conPRVtProvincia PobjProvincia = new conPRVtProvincia();
        private conDSTtDistrito PobjDistrito = new conDSTtDistrito();
        private conUSUpUsuario PobjUsuario = new conUSUpUsuario();


        // GET: conClsServicio
        public ActionResult conFrmServicioVista(int id = 0)
        {
            //Listas
            ViewBag.lstCategoriaServicios = mtdListarCategoria();
            ViewBag.lstServicios = mtdCargarServicios();
            //Departamento, Provincia, Ciudad
            ViewBag.lstDepartamentos = mtdCargarDepartamentos();

            return View(
                id == 0 ? new conSERpServicio() :
                PobjServicio.mtdObtener(id)
                );
        }

        public ActionResult conFrmMisServiciosVista()
        {
            //Listas
            ViewBag.lstCategoriaServicios = mtdListarCategoria();         
            //Departamento, Provincia, Ciudad
            ViewBag.lstDepartamentos = mtdCargarDepartamentos();

            return View(mtdCargarServicios());
        }

        public ActionResult conFrmVerServicioVista(int id = 0)
        {
            return View();
        }
       
        public ActionResult mtdGuardar(conSERpServicio PobjServicioModelo, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if(!string.IsNullOrEmpty(PobjServicioModelo.SERimagenes))
                {
                    if(file != null)
                    {
                        string fullPath = Server.MapPath("~/Uploads/" + PobjServicioModelo.SERimagenes);
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                        PobjServicioModelo.SERimagenes = mtdSubirImagen(file);
                    }
                }
                else
                {
                    if(file != null)
                    {
                        PobjServicioModelo.SERimagenes = mtdSubirImagen(file);
                    }

                }
                PobjServicioModelo.mtdGuardar();
            }
            else
            {
                ViewBag.lstCategoriaServicios = mtdListarCategoria();
                ViewBag.lstServicios = mtdCargarServicios();
                ViewBag.lstDepartamentos = mtdCargarDepartamentos();

                return View("conFrmServicioVista", PobjServicioModelo);
            }
            return Redirect("~/conClsServicio/conFrmMisServiciosVista");
        }

        public string mtdSubirImagen(HttpPostedFileBase file)
        {
            string fileName = Path.GetFileName(file.FileName);
            string fileLocationString = "~/Uploads";
            string fileLocation = Path.Combine(Server.MapPath(fileLocationString), fileName);
            file.SaveAs(fileLocation);
            return file.FileName;           
        }
        public object mtdListarCategoria()
        {
            return PobjCategoriaServicio.Listar();
        }

        public object mtdCargarServicios()
        {
            return PobjServicio.mtdListar();
        }

        public object mtdCargarDepartamentos()
        {
            return PobjDepartamento.mtdListar();
        }

        public ActionResult conFrmFiltrarLocalizacionVista(int xGintIdDepartamento, int xGintIdProvincia, int xGintIdDistrito)
        {
            //Listas
            ViewBag.lstCategoriaServicios = mtdListarCategoria();
            ViewBag.lstServicios = mtdCargarServicios();
            //Departamento, Provincia, Ciudad
            ViewBag.lstDepartamentos = mtdCargarDepartamentos();
            //obtenermos longitud y latidu del distrito
            var LobjDistrito = PobjDistrito.mtdObtenerLatitudLongitud(xGintIdDepartamento, xGintIdProvincia, xGintIdDistrito);
            var LobjMarkers = PobjUsuario.mtdListarProveedores();
            
            //Viewbag para los maracadores
            ViewBag.GobjMarkers = LobjMarkers;
            ViewBag.GdecLatitud = LobjDistrito.DSTlatitud.ToString().Replace(",",".");
            ViewBag.GdecLongitud= LobjDistrito.DSTlongitud.ToString().Replace(",", ".");

            return View();
        }

        public JsonResult mtdActualizarEstado(int id_servicio, string estado)
        {
            var LobjServicio = PobjServicio.mtdObtener(id_servicio);
            var nombre_servicio = LobjServicio.SERnombre_servicio;

            LobjServicio.SERestado = estado;
            LobjServicio.mtdGuardar();
            return Json(nombre_servicio, JsonRequestBehavior.AllowGet);
        }
        public JsonResult mtdListarProvincias(int id_departamento)
        {
            if(id_departamento > 0)
            {
                var lstProvincias = PobjProvincia.mtdListar(id_departamento).Select(a => new {
                    PRVid_provincia = a.PRVid_provincia,
                    PRVnombre = a.PRVnombre
                });
                return Json(lstProvincias, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Error");
            }
        }
        public JsonResult mtdListarDistritos(int id_provincia)
        {
            if (id_provincia > 0)
            {
                var lstDistritos = PobjDistrito.mtdListar(id_provincia).Select(a => new {
                    DSTid_distrito = a.DSTid_distrito,
                    DSTnombre = a.DSTnombre
                });

                return Json(lstDistritos, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Error");
            }
           
        }
    }
}