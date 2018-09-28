using System;
using System.Xml.Linq;

namespace TestComplete
{
    namespace Variables
    {
        public class Factory
        {
            public static Variable BuildVariable(XElement data)
            {
                string name = data.Attribute("Name")?.ToString();
                VarTypes type = Variable.TypeFromGuid[new Guid(data.Attribute("Type").Value)];
                string description = data.Attribute("Descr")?.Value;
                XElement def = data.Element("DefValue");

                switch (type)
                {
                    case VarTypes.String:
                        return new String(name, type, description, def);
                    // break;

                    case VarTypes.Integer:
                        return new Integer(name, type, description, def);
                    // break;

                    case VarTypes.Array:
                        return new Variable(name, type, description);
                    // break;

                    case VarTypes.Table:
                        return new Table(name, type, description, def);
                    // break;

                    default:
                        return null;
                        // break;
                }
            }
        }
    }
}

