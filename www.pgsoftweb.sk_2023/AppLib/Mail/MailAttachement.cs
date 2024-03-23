using System.Web;

namespace www.pgsoftweb.sk_2023.AppLib.Mail
{
    public class MailAttachement
    {
        private static string defaultPath = "\\App_Data\\Ebook";

        /// <summary>
        /// Gets the attachement file full path name
        /// </summary>
        /// <param name="filePath">Attachement file directory</param>
        /// <param name="fileName">Attachement file name</param>
        /// <returns>Returns file full path name</returns>
        public static string GetAttachementPath(string filePath, string fileName)
        {
            string fileFullName = string.Format("{0}\\{1}",
                string.IsNullOrEmpty(filePath) ? HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath) + defaultPath : filePath,
                fileName);


            return fileFullName;
        }
    }
}