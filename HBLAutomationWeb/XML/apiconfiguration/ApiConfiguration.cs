using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HBLAutomationWeb.XML.apiconfiguration
{
    [XmlRoot(ElementName = "param")]
    public class Param
    {
        [XmlAttribute(AttributeName = "key")]
        public string Key { get; set; }
        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }
    }

    [XmlRoot(ElementName = "header")]
    public class Header
    {
        [XmlElement(ElementName = "param")]
        public List<Param> Param { get; set; }
    }

    [XmlRoot(ElementName = "body")]
    public class Body
    {
        [XmlElement(ElementName = "param")]
        public List<Param> Param { get; set; }
    }

    [XmlRoot(ElementName = "request")]
    public class Request
    {
        [XmlElement(ElementName = "header")]
        public Header Header { get; set; }
        [XmlElement(ElementName = "body")]
        public Body Body { get; set; }
    }

    [XmlRoot(ElementName = "authentication")]
    public class Authentication
    {
        [XmlElement(ElementName = "request")]
        public Request Request { get; set; }
        [XmlAttribute(AttributeName = "alias")]
        public string Alias { get; set; }
        [XmlAttribute(AttributeName = "endPoint")]
        public string EndPoint { get; set; }
    }

    [XmlRoot(ElementName = "authorization")]
    public class Authorization
    {
        [XmlElement(ElementName = "request")]
        public Request Request { get; set; }
        [XmlAttribute(AttributeName = "alias")]
        public string Alias { get; set; }
    }

    [XmlRoot(ElementName = "restObject")]
    public class RestObject
    {
        [XmlElement(ElementName = "authentication")]
        public Authentication Authentication { get; set; }
        [XmlElement(ElementName = "authorization")]
        public Authorization Authorization { get; set; }
        [XmlAttribute(AttributeName = "keyword")]
        public string Keyword { get; set; }
        [XmlAttribute(AttributeName = "baseURI")]
        public string BaseURI { get; set; }
    }

    [XmlRoot(ElementName = "apiConfiguration")]
    public class ApiConfiguration
    {
        [XmlElement(ElementName = "restObject")]
        public List<RestObject> RestObject { get; set; }
    }
}
