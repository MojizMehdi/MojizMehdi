using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HBLAutomationAPIs.XML.ElementFactory
{
    /* 
   Licensed under the Apache License, Version 2.0

   http://www.apache.org/licenses/LICENSE-2.0
   */

    [XmlRoot(ElementName = "Element")]
    public class Element
    {
        [XmlAttribute(AttributeName = "keyword")]
        public string Keyword { get; set; }
        [XmlAttribute(AttributeName = "locator")]
        public string Locator { get; set; }
        [XmlAttribute(AttributeName = "listlocator")]
        public string Listlocator { get; set; }
    }

    [XmlRoot(ElementName = "Module")]
    public class Module
    {
        [XmlElement(ElementName = "Element")]
        public List<Element> Element { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "ElementFactory")]
    public class ElementFactory
    {
        [XmlElement(ElementName = "Module")]
        public List<Module> Module { get; set; }
    }

}
