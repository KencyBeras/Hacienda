using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace CrudHacienda.ClasesAuxiliares
{
    public class Correo
    {
        public static int EnviarCorreo(string correoPersona, string asunto, string contenido, string rutaArchivo)
        {
            int respuesta = 0;
            try
            {
                string correo = ConfigurationManager.AppSettings["Correo"];
                string clave = ConfigurationManager.AppSettings["CLave"];
                string servidor = ConfigurationManager.AppSettings["Servidor"];
                int puerto = int.Parse(ConfigurationManager.AppSettings["Puerto"]);
                //Data del correo (definimos)
                MailMessage mail = new MailMessage();
                mail.Subject = asunto;
                mail.IsBodyHtml = true;
                mail.Body = contenido;
                mail.From = new MailAddress(correo);
                mail.To.Add(new MailAddress(correoPersona));
                //Envio de correo
                SmtpClient smtp = new SmtpClient();
                smtp.EnableSsl = true;
                smtp.Host = servidor;
                smtp.Port = puerto;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(correo,clave);
                smtp.Send(mail);
                respuesta = 1;
            }
            catch (Exception)
            {

                respuesta = 0;
            }


            return respuesta;

        }
    }
}