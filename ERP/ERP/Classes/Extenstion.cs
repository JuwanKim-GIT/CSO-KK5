using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ERP
{
    static class Extenstion
    {

        //This method is to handle if element is missing
        public static string ElementValueNull(this XElement element)
        {
            if (element != null)
                return element.Value.Trim();

            return "";
        }
        public static string ElementValueNull0(this XElement element)
        {
            if (element != null)
                return element.Value.Trim();

            return "0";
        }
        public static XElement ElementElement(this XElement element)
        {
            if (element != null)
                return element;

            return new XElement("Empty", "");
        }

        //This method is to handle if attribute is missing
        public static string AttributeValueNull(this XElement element, string attributeName)
        {
            if (element == null)
                return "";
            else
            {
                XAttribute attr = element.Attribute(attributeName);
                return attr == null ? "" : attr.Value;
            }
        }

    }
}
