using System;
using System.Xml.Serialization;

namespace ChatterBox.Core.Infrastructure.Facts
{
    public class FactAttribute : XmlRootAttribute
    {
        public FactAttribute(string id)
        {
            Namespace = Guid.Parse(id).ToString();
        }
    }
}