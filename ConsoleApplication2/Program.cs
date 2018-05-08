using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace ConsoleApplication2
{
    class Program
    {
        private class IgnoreBadCert : ICertificatePolicy
        {
            public bool CheckValidationResult(ServicePoint sp,
            X509Certificate cert, WebRequest request, int err)
            {
                return true;
            }
        }
        static string username = "waifutrique@gmail.com";
        static string pass = "triquevkl123";
        static void Main(string[] args)
        {
            ServicePointManager.CertificatePolicy = new IgnoreBadCert();
            NetworkCredential cred = new NetworkCredential();
            cred.UserName = username;
            cred.Password = pass;

            WebRequest webr = WebRequest.Create("https://mail.google.com/mail/feed/atom");
            webr.Credentials = cred;
            Stream stream = webr.GetResponse().GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string s = sr.ReadToEnd();
            s = s.Replace("<feed version=\"0.3\" xmlns=\"http://purl.org/atom/ns#\">", @"<feed>");
            StreamWriter sw = new StreamWriter("emaildata.txt");
            sw.Write(s);
            sr.Close();
            sw.Close();
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load("emaildata.txt");
            String xml = s.ToString();


            
            string nr = xmldoc.SelectSingleNode(@"/feed/fullcount").InnerText;
            Console.WriteLine("Count: {0}", nr);



            foreach (XmlNode node in xmldoc.SelectNodes(@"/feed/entry"))
            {
                string title = node.SelectSingleNode("title").InnerText;
                string id = node.SelectSingleNode("id").InnerText;
                XElement element = XElement.Parse(xml)
                .Descendants()
                .FirstOrDefault(i => i.Name.LocalName == id);

                Console.WriteLine("" + element);


                //Console.WriteLine("{0} \n{1}\n{2}\n{3}\n", title, summary, recieve, modified);



            }
        }
    }
}
