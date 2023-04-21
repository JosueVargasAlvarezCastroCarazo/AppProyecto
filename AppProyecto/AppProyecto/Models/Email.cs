using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace AutoAppo_JosueVa.Models
{
    public class Email
    {
        public String SendTo { get; set; }
        public String Subeject { get; set; }
        public String Message { get; set; }


        // metodo encargado de mandar un email
        public bool SendEmail()
        {
            bool R = false;

            try
            {
                if (
                    !string.IsNullOrEmpty(SendTo) &&
                    !string.IsNullOrEmpty(Subeject) &&
                    !string.IsNullOrEmpty(Message) 
                    )
                {
                    System.Net.Mail.MailMessage email = new System.Net.Mail.MailMessage();

                    email.From = new System.Net.Mail.MailAddress("jjSendPrCastro@gmail.com");
                    email.Subject = Subeject;
                    email.Body = Message;
                    email.To.Add(SendTo);

                    email.IsBodyHtml = false;

                    System.Net.Mail.SmtpClient server = new System.Net.Mail.SmtpClient();
                    server.Host = "smtp.gmail.com";
                    server.Port = 587;

                    server.EnableSsl = true;
                    server.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

                    // contraseña corriente txkzosgwwhipzjpr             contra acceso app jdpqqahkgrlhbqkj
                    server.Credentials = new NetworkCredential("jjSendPrCastro@gmail.com", "jdpqqahkgrlhbqkj");

                    server.Send(email);

                    R = true;

                }
            }
            catch (Exception e)
            {

                throw;
            }
            
            return R;
        } 

    }
}
