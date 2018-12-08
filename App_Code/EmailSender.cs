using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

/// <summary>
/// Summary description for EmailSender
/// </summary>
public class EmailSender
{
    public static void Send(string address, string title, string message)
    {
        SmtpClient client = new SmtpClient();
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.EnableSsl = true;
        client.Host = "smtp.gmail.com";
        client.Port = 587;

        // setup Smtp authentication
        System.Net.NetworkCredential credentials =
            new System.Net.NetworkCredential("TeamNKproject@gmail.com", "teamnk1!");
        client.UseDefaultCredentials = false;
        client.Credentials = credentials;
        client.EnableSsl = true;

        MailMessage msg = new MailMessage();
        msg.From = new MailAddress("TeamNKproject@gmail.com");
        msg.To.Add(new MailAddress(address));

        msg.Subject = title;
        msg.IsBodyHtml = true;
        msg.Body = string.Format("<html><head></head><body>{0}</body></html>", message);

        try
        {
            client.Send(msg);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}