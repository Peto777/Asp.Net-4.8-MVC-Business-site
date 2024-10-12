using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using www.gloziksoft.sk_2023.AppLib.Template;

namespace www.gloziksoft.sk_2023.AppLib.Mail
{
    /// <summary>
    /// Enables to sendig emails
    /// </summary>
    public class MailProvider
    {
        string mailerID;
        /// <summary>
        /// Gets the mailer identification
        /// </summary>
        public string MailerID
        {
            get
            {
                return mailerID;
            }
        }

        string smtpHost;
        /// <summary>
        /// Gets the smtp server host for sending emails
        /// </summary>
        public string SmtpHost
        {
            get
            {
                return smtpHost;
            }
        }
        /// <summary>
        /// Gets the smtp port for sending emails
        /// </summary>
        public int SmtpPort { get; private set; }
        /// <summary>
        /// Gets whether to use SSL for sending emails
        /// </summary>
        public bool SmtpUseSsl { get; private set; }

        string smtpUser;
        /// <summary>
        /// Gets the user name for sending emails
        /// </summary>
        public string SmtpUser
        {
            get
            {
                return smtpUser;
            }
        }

        string smtpPassword;
        /// <summary>
        /// Gets the password for sending emails
        /// </summary>
        public string SmtpPassword
        {
            get
            {
                return smtpPassword;
            }
        }

        string sendToAdmin;
        /// <summary>
        /// Admin Email address to send email to admin
        /// </summary>
        public string SendToAdmin
        {
            get
            {
                return sendToAdmin;
            }
        }

        string sendFromMail;
        /// <summary>
        /// Email address to send email from
        /// </summary>
        public string SendFromMail
        {
            get
            {
                return sendFromMail;
            }
        }

        string sendFromName;
        /// <summary>
        /// Email address to send email from
        /// </summary>
        public string SendFromName
        {
            get
            {
                return sendFromName;
            }
        }

        string sendToBcc;
        /// <summary>
        /// Email address to send email to as blind copy
        /// </summary>
        public string SendToBcc
        {
            get
            {
                return sendToBcc;
            }
        }

        string sendToBccServiceId;
        /// <summary>
        /// Internal identifier for Email address to send email to as blind copy for a specific mail service
        /// </summary>
        public string SendToBccServiceId
        {
            get
            {
                return sendToBccServiceId;
            }
        }
        string sendToBccService;
        /// <summary>
        /// Email address to send email to as blind copy for a specific mail service
        /// </summary>
        public string SendToBccService
        {
            get
            {
                return sendToBccService;
            }
        }

        bool isBodyHtml;
        /// <summary>
        /// Whether email body is HTML
        /// </summary>
        public bool IsBodyHtml
        {
            get
            {
                return isBodyHtml;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MailProvider"/> class.
        /// </summary>
        /// <param name="bccServiceId">Blind copy email service ID</param>
        public MailProvider(string bccServiceId)
            : this(bccServiceId, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MailProvider"/> class.
        /// </summary>
        /// <param name="aIsBodyHtml">Whether email body is HTML</param>
        public MailProvider(string bccServiceId, bool aIsBodyHtml)
        {
            sendToBccServiceId = bccServiceId;
            isBodyHtml = aIsBodyHtml;
            LoadCfg();
        }

        /// <summary>
        /// Clears the configuration settings
        /// </summary>
        public void ClearCfg()
        {
            mailerID = string.Empty;
            smtpHost = string.Empty;
            this.SmtpPort = -1;
            this.SmtpUseSsl = false;
            smtpUser = string.Empty;
            smtpPassword = string.Empty;
            sendToAdmin = string.Empty;
            sendFromMail = string.Empty;
            sendFromName = string.Empty;
            sendToBcc = string.Empty;
            sendToBccService = string.Empty;
        }

        /// <summary>
        /// Loads configuration settings
        /// </summary>
        public void LoadCfg()
        {
            ClearCfg();

            // Mailer ID
            mailerID = ConfigurationManager.AppSettings["mailerID"];
            // SMTP host
            smtpHost = ConfigurationManager.AppSettings["smtpHost"];
            if (ConfigurationManager.AppSettings["smtpPort"] != null)
            {
                this.SmtpPort = int.Parse(ConfigurationManager.AppSettings["smtpPort"]);
            }
            if (ConfigurationManager.AppSettings["smtpUseSsl"] != null)
            {
                this.SmtpUseSsl = ConfigurationManager.AppSettings["smtpUseSsl"] == "true";
            }
            // SMTP user
            smtpUser = ConfigurationManager.AppSettings["smtpUser"];
            // SMTP password
            smtpPassword = ConfigurationManager.AppSettings["smtpPassword"];
            // Send to admin mail address
            sendToAdmin = ConfigurationManager.AppSettings["sendToAdmin"];
            // Send from
            sendFromMail = ConfigurationManager.AppSettings["sendFrom"];
            sendFromName = ConfigurationManager.AppSettings["sendFromName"];
            // Send to Bcc
            sendToBcc = ConfigurationManager.AppSettings["sendToBcc"];
            // Send to Bcc
            if (!string.IsNullOrEmpty(sendToBccServiceId))
            {
                sendToBccService = ConfigurationManager.AppSettings[sendToBccServiceId];
            }
        }

        /// <summary>
        /// Send an email
        /// </summary>
        /// <param name="mailSubject">Mail subject</param>
        /// <param name="mailBody">Mail body</param>
        /// <param name="sendTo">Send to email address</param>
        public void SendMail(string mailSubject, string mailBody, string sendTo, List<string> attachementList)
        {
            List<string> sendToList = new List<string>();
            sendToList.Add(sendTo);
            SendMail(mailSubject, mailBody, sendToList, false, false, attachementList);
        }

        /// <summary>
        /// Send an email to the specified recepients
        /// </summary>
        /// <param name="mailSubject">Mail subject</param>
        /// <param name="mailBody">Mail body</param>
        /// <param name="sendToList">The list of recepients</param>
        /// <param name="asBcc">Whether the parameter <paramref name="sendToList"/> should be treated as BCC list</param>
        /// <param name="sendSeparately">Whether to send email separately to each recepient</param>
        /// <param name="attachementList">The list of attachements</param>
        public void SendMail(string mailSubject, string mailBody, List<string> sendToList, bool asBcc, bool sendSeparately, List<string> attachementList)
        {
            if (string.IsNullOrEmpty(this.SmtpHost))
                // No mail support
                return;

            if (sendToList == null || (sendToList != null && sendToList.Count == 0))
                // Empty receivers list
                return;

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(this.SendFromMail, this.SendFromName);

            if (!sendSeparately)
            {
                // Send as one message to all recepients
                AddToMailAddressCollection(mail.Bcc, this.sendToBcc);
                if (!string.IsNullOrEmpty(this.sendToBccService))
                {
                    AddToMailAddressCollection(mail.Bcc, this.sendToBccService);
                }
                foreach (string sender in sendToList)
                {
                    if (!string.IsNullOrEmpty(sender))
                    {
                        if (asBcc)
                            AddToMailAddressCollection(mail.Bcc, sender);
                        else
                            AddToMailAddressCollection(mail.To, sender);
                    }
                }
            }

            mail.Headers["X-Mailer"] = this.mailerID;// "madosoft.sk mailer system";
            mail.Subject = mailSubject;
            mail.IsBodyHtml = this.isBodyHtml;
            mail.SubjectEncoding = Encoding.UTF8;
            mail.BodyEncoding = Encoding.UTF8;
            mail.Body = mailBody;

            if (attachementList != null)
            {
                foreach (string attPath in attachementList)
                {
                    Attachment attachment = new Attachment(attPath);
                    mail.Attachments.Add(attachment);
                }
            }

            try
            {
                SmtpClient c = GetSmtpClient();

                if (!sendSeparately)
                {
                    // Send as one message to all recepients
                    c.Send(mail);
                }
                else
                {
                    // Send individual message to each recepient
                    foreach (string sender in sendToList)
                    {
                        mail.Bcc.Clear();
                        mail.To.Clear();
                        if (!string.IsNullOrEmpty(sender))
                        {
                            if (asBcc)
                                AddToMailAddressCollection(mail.Bcc, sender);
                            else
                                AddToMailAddressCollection(mail.To, sender);
                            c.Send(mail);
                        }
                    }
                    // Send message to system bcc
                    mail.Bcc.Clear();
                    mail.To.Clear();
                    if (!string.IsNullOrEmpty(this.sendToBcc))
                    {
                        if (asBcc)
                            AddToMailAddressCollection(mail.Bcc, this.sendToBcc);
                        else
                            AddToMailAddressCollection(mail.To, this.sendToBcc);
                        c.Send(mail);
                    }
                    // Send message to service bcc
                    mail.Bcc.Clear();
                    mail.To.Clear();
                    if (!string.IsNullOrEmpty(this.sendToBccService))
                    {
                        if (asBcc)
                            AddToMailAddressCollection(mail.Bcc, this.sendToBccService);
                        else
                            AddToMailAddressCollection(mail.To, this.sendToBccService);
                        c.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new SendMailException("Chyba pri odosielaní e-mailu", ex);
            }
        }

        /// <summary>
        /// Send an email to the specified recepients
        /// </summary>
        /// <param name="mailSubject">Mail subject</param>
        /// <param name="mailBody">Mail body</param>
        /// <param name="sendToList">The list of recepients</param>
        /// <param name="asBcc">Whether the parameter <paramref name="sendToList"/> should be treated as BCC list</param>
        /// <param name="sendSeparately">Whether to send email separately to each recepient</param>
        public void SendAdminMail(string mailSubject, string mailBody)
        {
            if (string.IsNullOrEmpty(this.SmtpHost))
                // No mail support
                return;

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(this.SendFromMail, this.SendFromName);

            mail.Headers["X-Mailer"] = this.mailerID;// "madosoft.sk mailer system";
            mail.Subject = mailSubject;
            mail.IsBodyHtml = this.isBodyHtml;
            mail.SubjectEncoding = Encoding.UTF8;
            mail.BodyEncoding = Encoding.UTF8;
            mail.Body = mailBody;
            AddToMailAddressCollection(mail.To, this.SendToAdmin);

            try
            {
                SmtpClient c = GetSmtpClient();

                c.Send(mail);
            }
            catch (Exception ex)
            {
                throw new SendMailException("Error sending e-mail.", ex);
            }
        }

        SmtpClient GetSmtpClient()
        {
            SmtpClient client = new SmtpClient(this.SmtpHost);
            if (this.SmtpUseSsl)
            {
                client.EnableSsl = true;
            }
            if (this.SmtpPort > 0)
            {
                client.Port = this.SmtpPort;
            }
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(this.SmtpUser, this.SmtpPassword);

            return client;
        }


        private void AddToMailAddressCollection(MailAddressCollection list, string mailAddress)
        {
            string[] mailItems = mailAddress.Split(';');
            foreach (string mailItem in mailItems)
            {
                list.Add(mailItem);
            }
        }
    }

    public class SendMailException : Exception
    {
        public string Subject { get; set; }
        public string Body { get; set; }

        // Summary:
        //     Initializes a new instance of the System.Exception class.
        public SendMailException()
            : base()
        {
        }

        //
        // Summary:
        //     Initializes a new instance of the System.Exception class with a specified
        //     error message.
        //
        // Parameters:
        //   message:
        //     The message that describes the error.
        public SendMailException(string message)
            : base(message)
        {
        }

        //
        // Summary:
        //     Initializes a new instance of the System.Exception class with a specified
        //     error message and a reference to the inner exception that is the cause of
        //     this exception.
        //
        // Parameters:
        //   message:
        //     The error message that explains the reason for the exception.
        //
        //   innerException:
        //     The exception that is the cause of the current exception, or a null reference
        //     (Nothing in Visual Basic) if no inner exception is specified.
        public SendMailException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }


    /// <summary>
    /// Mailer class
    /// </summary>
    public abstract class Mailer
    {
        /// <summary>
        /// Send an email
        /// </summary>
        /// <param name="mailSubject">Mail subject</param>
        /// <param name="mailBody">Mail body</param>
        /// <param name="sendTo">Send to email address</param>
        public static void SendMail(string mailSubject, string mailBody, string sendTo)
        {
            MailProvider mailProvider = new MailProvider(null);
            mailProvider.SendMail(GetFullTitle(mailSubject, mailProvider), mailBody, sendTo, null);
        }

        /// <summary>
        /// Send an email to the specified recepients
        /// </summary>
        /// <param name="mailSubject">Mail subject</param>
        /// <param name="mailBody">Mail body</param>
        /// <param name="sendToList">The list of recepients</param>
        /// <param name="asBcc">Whether the parameter <paramref name="sendToList"/> should be treated as BCC list</param>
        public static void SendMail(string mailSubject, string mailBody, List<string> sendToList, bool asBcc)
        {
            MailProvider mailProvider = new MailProvider(null);
            mailProvider.SendMail(GetFullTitle(mailSubject, mailProvider), mailBody, sendToList, asBcc, !asBcc, null);
        }

        /// <summary>
        /// Send an email using HTML template
        /// </summary>
        /// <param name="mailSubject">Mail subject</param>
        /// <param name="mailBody">Mail body</param>
        /// <param name="sendTo">Send to email address</param>
        public static void SendMailTemplate(string mailSubject, string mailBody, string sendTo, string cultureId, List<string> attachementList)
        {
            SendMailTemplate(mailSubject, mailBody, sendTo, null, cultureId, attachementList);
        }

        /// <summary>
        /// Send an email using HTML template
        /// </summary>
        /// <param name="mailSubject">Mail subject</param>
        /// <param name="mailBody">Mail body</param>
        /// <param name="sendTo">Send to email address</param>
        /// <param name="bccServiceId">Blind copy email service ID</param>
        public static void SendMailTemplate(string mailSubject, string mailBody, string sendTo, string bccServiceId, string cultureId, List<string> attachementList)
        {
            List<TextTemplateParam> paramList = new List<TextTemplateParam>();
            paramList.Add(new TextTemplateParam("EMAIL_SUBJ", mailSubject));
            paramList.Add(new TextTemplateParam("EMAIL_MSG", mailBody));
            paramList.Add(new TextTemplateParam("EMAIL_TO", sendTo));

            MailProvider mailProvider = new MailProvider(bccServiceId);
            mailProvider.SendMail(GetFullTitle(mailSubject, mailProvider), TextTemplate.GetTemplateText("EmailOne" + cultureId, paramList), sendTo, attachementList);
        }

        /// <summary>
        /// Send an email to the specified recepients using HTML template
        /// </summary>
        /// <param name="mailSubject">Mail subject</param>
        /// <param name="mailBody">Mail body</param>
        /// <param name="sendToList">The list of recepients</param>
        /// <param name="asBcc">Whether the parameter <paramref name="sendToList"/> should be treated as BCC list</param>
        public static void SendMailTemplate(string mailSubject, string mailBody, List<string> sendToList, bool asBcc, string cultureId)
        {
            List<TextTemplateParam> paramList = new List<TextTemplateParam>();
            paramList.Add(new TextTemplateParam("EMAIL_SUBJ", mailSubject));
            paramList.Add(new TextTemplateParam("EMAIL_MSG", mailBody));

            MailProvider mailProvider = new MailProvider(null);
            mailProvider.SendMail(GetFullTitle(mailSubject, mailProvider), TextTemplate.GetTemplateText("EmailMore" + cultureId, paramList), sendToList, asBcc, !asBcc, null);
        }

        private static string GetFullTitle(string mailSubject, MailProvider mailProvider)
        {
            if (string.IsNullOrEmpty(mailProvider.SendFromName))
            {
                return string.Format("DUFEKSOFT.com: {0}", mailSubject);
            }
            else
            {
                return mailSubject;
            }
        }

        /// <summary>
        /// Send an email to admin
        /// </summary>
        /// <param name="mailSubject">Mail subject</param>
        /// <param name="mailBody">Mail body</param>
        /// <param name="sendTo">Send to email address</param>
        public static void SendAdminMail(string mailSubject, string mailBody)
        {
            MailProvider mailProvider = new MailProvider(null, false);
            mailProvider.SendAdminMail(GetFullTitle(mailSubject, mailProvider), mailBody);
        }
    }
}