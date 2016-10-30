using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net.Mail;

namespace Glimpse.Droid.Views
{
    public class SendMail
    {

        //This method is used to create the email template to send the vendor after they signup
        //The name from the sign up form
        //The formatted mail body to inform and congradulate the vendor for signing up
        public string CreateMailBodyForVendor(string _firstname)
        {
            string mailBody = "<div> Hello " + _firstname + ", <br/>" +
                "Your account is created successfully! Pending verfication for your account. <br/><br/> Regards,<br/> The Glimpse Team</div>";
            return mailBody;
        }


        // This method is used to create a mail template to send the user after they signup
        // _firstname = The First name from the sign up form
        // _company = the company name from the sign up form
        //_phonenumber = The phone number from the sign up form
        //_email = The email address from the sign up form
        public string CreateMailBodyForAdmin(string _firstname, string _company, string _phoneNumber, string _email)
        {
            string mailBody = "<div> Hello Admin, <br/>" +
                "User <b>" + _firstname + ", " + _company + "</b> Account needs verification. Detailed Information are below: <br/><br/>" +
                "Name         : " + _firstname + "<br/>" +
                "Last Name    : " + _company + "<br/>" +
                "Phone Number : " + _phoneNumber + "<br/>" +
                "Email : " + _email + "<br/><br/> Regards,<br/> Glimpse Admin</div>";
            return mailBody;

        }


        //The method sends mail. Pass the parameters carefully when you call.
        // mailSub = Subject of the email.
        // mailbody = Mailbody of the email.
        // mailTo = The email address to send.

        public bool SendEmail(string mailSub, string mailbody, string mailTo)
        {

            MailMessage mail = new MailMessage("smtp.testing.g@gmail.com", mailTo);
            mail.Subject = mailSub;
            mail.Body = mailbody;
            mail.Priority = MailPriority.High;
            mail.IsBodyHtml = true;

            // Set the StmpServer name. 
            SmtpClient mailSmtp = new SmtpClient();

            // Smtp configuration
            //give the email address and password we will use to send (From) emails.
            mailSmtp.Credentials = new System.Net.NetworkCredential("smtp.testing.g@gmail.com", "testingsmtp1234");

            mailSmtp.Port = 587;
            mailSmtp.EnableSsl = true;
            mailSmtp.Host = "smtp.gmail.com";

            try
            {
                mailSmtp.Send(mail);
                return true;
            }
            catch (Exception ex) { }

            return false;
            
        }

    }
}
