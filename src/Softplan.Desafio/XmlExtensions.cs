using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace Softplan.Desafio
{
    public static class XmlExtensions
    {
        public static string SerializeObject<T>(this T toSerialize)
        {
            var xmlSerializer = new XmlSerializer(toSerialize.GetType());

            using (var textWriter = new StringWriterUtf8())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);

                var xmlDoc = new System.Xml.XmlDocument();

                xmlDoc.LoadXml(textWriter.ToString());
                xmlDoc.PreserveWhitespace = false;

                return xmlDoc.OuterXml;
            }
        }

        public static T DeserializeXMLFileToObject<T>(string XmlString)
        {
            T returnObject = default(T);
            if (string.IsNullOrEmpty(XmlString)) return default(T);

            try
            {
                var serializer = new XmlSerializer(typeof(T));
                returnObject = (T)serializer.Deserialize(GenerateStreamFromString(XmlString));
            }
            catch (Exception exp)
            {
                //ExceptionLogger.WriteExceptionToConsole(ex, DateTime.Now);
            }
            return returnObject;
        }

        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static XmlElement SerializeToXmlElement(object o)
        {
            var doc = new XmlDocument();

            using (XmlWriter writer = doc.CreateNavigator().AppendChild())
            {
                new XmlSerializer(o.GetType()).Serialize(writer, o);
            }

            return doc.DocumentElement;
        }

        public static string RemoverDadosXml(string xmlCte, string[] elementos)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlCte);

                for (int aux = 0; aux < elementos.Count(); aux++)
                {
                    XmlNodeList nodes = doc.GetElementsByTagName(elementos[aux]);
                    XmlNode node = nodes[0];
                    node.ParentNode.RemoveChild(node);
                }

                var retorno = doc.OuterXml;

                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
