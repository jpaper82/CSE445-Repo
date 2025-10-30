using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Runtime;
using System.Xml;
using System.Xml.Schema;



/**
 * This template file is created for ASU CSE445 Distributed SW Dev Assignment 4.
 * Please do not modify or delete any existing class/variable/method names. However, you can add more variables and functions.
 * Uploading this file directly will not pass the autograder's compilation check, resulting in a grade of 0.
 * **/


namespace ConsoleApp1
{


    public class Program
    {
        public static string xmlURL = "https://jpaper82.github.io/CSE445-Repo/Hotels.xml";
        public static string xmlErrorURL = "https://jpaper82.github.io/CSE445-Repo/HotelsErrors.xml";
        public static string xsdURL = "https://jpaper82.github.io/CSE445-Repo/Hotels.xsd";

        public static void Main(string[] args)
        {
            string result = Verification(xmlURL, xsdURL);
            Console.WriteLine(result);


            result = Verification(xmlErrorURL, xsdURL);
            Console.WriteLine(result);


            result = Xml2Json(xmlURL);
            Console.WriteLine(result);
        }

        // Q2.1
        public static string Verification(string xmlUrl, string xsdUrl)
        {
            XmlSchemaSet sc = new XmlSchemaSet();
            sc.Add(null, xsdUrl);
            XmlReaderSettings settings = new XmlReaderSettings();

            settings.ValidationType = ValidationType.Schema;
            settings.Schemas = sc;

            settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);

        //    XmlReader reader = XmlReader.Create(xmlUrl, settings);

       //     while (reader.Read()) ;
       //     Console.WriteLine("Finished validating!");


            try
            {
                using (XmlReader reader = XmlReader.Create(xmlUrl, settings))
                {
                    while (reader.Read()) ;
                }
            }
            catch (XmlException ex)
            {
                // Malformed XML
                return ex.Message;
            }

            //return "No Error" if XML is valid. Otherwise, return the desired exception message.

            return "No Error";
        }

        private static void ValidationCallBack(object sender, ValidationEventArgs e)
        {
           // Console.WriteLine("Validation Error:{0}", e.Message);
        }


        public static string Xml2Json(string xmlUrl)
        {
            string jsonText;


            using (var client = new WebClient())
            {
                string xmlString = client.DownloadString(xmlUrl).Trim(); // remove leading/trailing whitespace
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlString);
                jsonText = JsonConvert.SerializeXmlNode(doc);
            }



         //   XmlDocument doc = new XmlDocument();
        //    doc.LoadXml(xmlUrl);


            // The returned jsonText needs to be de-serializable by Newtonsoft.Json package. (JsonConvert.DeserializeXmlNode(jsonText))
            return jsonText;

        }
    }

}
