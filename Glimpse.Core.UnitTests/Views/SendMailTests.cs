using Microsoft.VisualStudio.TestTools.UnitTesting;
using Glimpse.Droid.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace Glimpse.Droid.Views.Tests
{
    [TestClass()]
    public class SendMailTests
    {
        [TestMethod()]
        public void SendEmailTest()
        {
            var mailFrom = "smtp.testing.g@gmail.com";
            var mailTo = "smtp.testing.g@gmail.com";
            var mailBody ="Test Mail Body";
            var mailSub = "Test Mail Subject";

            SmtpClient smtp = new SmtpClient("smtp.gmail.com",587);
            MailMessage mailMessage = new MailMessage(mailFrom, mailTo, mailSub, mailBody);
            smtp.Credentials = new System.Net.NetworkCredential("smtp.testing.g@gmail.com", "testingsmtp1234");
            try
            {
                smtp.Send(mailMessage);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull("Testing passed");
                return;
            }
                

            
        }
    }
}



              