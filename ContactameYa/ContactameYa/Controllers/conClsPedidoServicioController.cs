using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ContactameYa.Models;
using ContactameYa.Filters;
using System.Net.Mail;
using System.Text;
using System.Net;

namespace ContactameYa.Controllers
{
    public class conClsPedidoServicioController : Controller
    {
        private conSERpServicio PobjServicio = new conSERpServicio();
        private conPDSpPedidoServicio PobjPedidoServicio = new conPDSpPedidoServicio();


        private conDTOtDepartamento PobjDepartamento = new conDTOtDepartamento();


        public ActionResult conFrmPedidoResumenVista()
        {
            return View();
        }

        //vista de un pedido
        public ActionResult conFrmPedidoVista(int id = 0) //xGintIdServicio
        {

            conPDSpPedidoServicio LobjPedidoServicio = new conPDSpPedidoServicio();
            if (id > 0)
            {
                ViewBag.GobjServicio = PobjServicio.mtdObtener(id);
            }
            LobjPedidoServicio.SERid_servicio = id;

            return View(LobjPedidoServicio);
        }

        // CONTROLAR MIS PEDIDOS SERVICIOS
        public ActionResult conFrmControlarPedidosVista()
        {
            ViewBag.lstDepartamentos = mtdCargarDepartamentos();

            return View(PobjPedidoServicio.mtdListarPedidosProveedor());
        }

        // VER MIS PEDIDIOS (CLIENTE)
        public ActionResult conFrmMisPedidosVista()
        {            
            return View();
        }


        public object mtdCargarDepartamentos()
        {
            return PobjDepartamento.mtdListar();
        }

        public JsonResult mtdActualizarEstado(int xGintIdPedidoServicio, string xGstrEStado)
        {
            var LobjPedidoServicio = PobjPedidoServicio.mtdObtener(xGintIdPedidoServicio);

            if (LobjPedidoServicio.PDSestado.Equals("E"))
            {
                if(xGstrEStado.Equals("T"))
                {
                    mtdEnviarCorreo(LobjPedidoServicio);
                }
            }

            //var nombre_servicio = LobjPedidoServicio.PDS;
            LobjPedidoServicio.PDSestado = xGstrEStado;
            LobjPedidoServicio.mtdGuardar();
            return Json(xGstrEStado, JsonRequestBehavior.AllowGet);
        }

        public void mtdEnviarCorreo(conPDSpPedidoServicio xLobjPedidoServicio)
        {
            var fromAddress = "contactameya123@gmail.com";
            var password = "Poiuy123";

            var toAddress = xLobjPedidoServicio.conUSUpUsuario.USUemail;

            string smtpType = "";
            if (toAddress.Contains("@gmail.com")) smtpType = "smtp.gmail.com";
            if (toAddress.Contains("@hotmail.") || toAddress.Contains("@outlook.") || toAddress.Contains("@upt.")) smtpType = "smtp.live.com";

            string email =
                        String.Format(
                            "Estimado <b>{0} {1}</b>. <br />" +
                            " El Servicio con el código : {2} ha sido completado satisfactoriamente. <br /> Atentamente, <br /> ContactameYa"
                        , xLobjPedidoServicio.conUSUpUsuario.USUnombre, xLobjPedidoServicio.conUSUpUsuario.USUnombre, xLobjPedidoServicio.PDSid_pedidoServicio);
            // Enviar el Correo
            MailMessage mail = new MailMessage();
            mail.To.Add(toAddress);
            mail.From = new MailAddress(fromAddress, "Contactame Ya", Encoding.UTF8);
            mail.Subject = "Se Pedido ha Finalizado !";
            mail.SubjectEncoding = Encoding.UTF8;
            mail.Body = email;
            mail.BodyEncoding = Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.Credentials = new NetworkCredential(fromAddress, password);
            client.Port = 587;
            client.Host = smtpType;
            client.EnableSsl = true;
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
    }
}