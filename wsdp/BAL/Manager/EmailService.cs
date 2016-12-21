using BAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DTO;
using System.Net.Mail;
using DAL.Interface;
using System.Net;
using System.Configuration;

namespace BAL.Manager
{
    public class EmailService : BaseManager, IEmailService
    {
        public EmailService(IUnitOfWork uOW) : base(uOW)
        {
        }
        /// <summary>
        /// Method to send message via email about price fall
        /// </summary>
        /// <param name="model"></param>
        /// <param name="price"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool SendEmail(PriceFollowerDTO model, decimal? price, string email, string password)
        {

            var good = uOW.GoodRepo.GetByID(model.Good_Id);
            var user = uOW.UserRepo.GetByID(model.User_Id);

            var _email = email;
            var pass = password;

            try
            {
                using (SmtpClient client = new SmtpClient())
                {
                    client.EnableSsl = true;
                    client.Port = 587;
                    client.Host = "smtp.gmail.com";
                    client.UseDefaultCredentials = false;

                    client.Credentials = new NetworkCredential(_email, pass);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;


                    var from = _email;
                    var to = user.Email;
                    MailMessage message = new MailMessage(from, to);
					string mailBody = "Hello " + user.UserName + ". Do you now that the price of " + good.Name + " fell." +
				  " Now the price is just " + good.Price + ". Follow this link for details:" + good.UrlLink;

					var htmlView = AlternateView.CreateAlternateViewFromString(mailBody+"<br/><br/><img src="+good.ImgLink+"> ", null, "text/html");
					//Add image to HTML version
					message.Subject = "Sales on WSDP";
					message.AlternateViews.Add(htmlView);
                    message.IsBodyHtml = true;

                    client.Send(message);
                }

                return true;
            }

            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            return false;
        }
    }
}
