using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using AarhusWebDevCoop.ViewModels;
using System.Net.Mail;
using Umbraco.Core.Models;

namespace AarhusWebDevCoop.Controller
{
    public class ContactFormSurfaceController : SurfaceController
    {
		// GET: Default 
		public ActionResult Index()
		{
			return PartialView("ContactForm", new ContactForm());
		}

		[HttpPost]
		public ActionResult HandleFormSubmit(ContactForm model)
		{
			//send email
			MailMessage message = new MailMessage();
			message.To.Add("chandnishawimps89@gmail.com");
			message.Subject = model.Subject;
			message.From = new System.Net.Mail.MailAddress(model.Email, model.Name);
			message.Body = model.Message;

			using (SmtpClient smtp = new SmtpClient())
			{
				smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
				smtp.UseDefaultCredentials = false;
				smtp.EnableSsl = true;
				smtp.Host = "smtp.gmail.com";
				smtp.Port = 587;
				smtp.Credentials = new System.Net.NetworkCredential("chandnishawimps89@gmail.com", "iyvieuljeovrhybc");
				smtp.EnableSsl = true;

				// send mail
				smtp.Send(message);
			}

			if (!ModelState.IsValid)
			{
				return CurrentUmbracoPage();
			}

			//create comment
			var comment = Services.ContentService.CreateContent(model.Subject, CurrentPage.Id, "message");
			comment.SetValue("name1", model.Name);
			comment.SetValue("email", model.Email);
			comment.SetValue("subject", model.Subject);
			comment.SetValue("message1", model.Message);

			//save
			Services.ContentService.SaveAndPublishWithStatus(comment);


			TempData["success"] = true;
			return RedirectToCurrentUmbracoPage();
		}
	}
}