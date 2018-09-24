using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestComplete
{
    public class Parameter
    {
        public readonly Guid LastOperation = new Guid("{D44DB91E-FD74-4F67-BE3D-951A1238A9AD}");
        public string Name { get; private set; }
        public string ParamType { get; private set; }
        public string Value { get; private set; }
        public string VarName { get; private set; }
        public string ValueType { get; private set; }
        public Parameter(XElement data)
        {
            Name = data.Attribute("Name").Value;
            ParamType = data.Attribute("ParamType")?.Value;
            Value = data.Attribute("ValueValue")?.Value;
            VarName = data.Attribute("VariableName")?.Value;
            ValueType = data.Attribute("ValueType")?.Value;
        }

        public override string ToString()
        {
            var debug = LastOperation.ToString();
            // ParamType is "{GUID}" and LastOperation.ToString is "GUID"! Must add '{' and '}' to compare.
            if (ParamType?.Equals($"{{{LastOperation.ToString()}}}",StringComparison.InvariantCultureIgnoreCase) ?? false)
            {
                return "LastResult";
            }
            else if (ValueType == "1" || ValueType =="7") // integer, boolean; 
            {
                return Value;
            }
            else if (ValueType == "6" && !Value.StartsWith("[KeywordTests")) // string; "[KeywordTests.." seems to indicate a reference to a variable or array
            {
                return $"\"{Value}\"";
            }
            else 
            {
                return ".";
            }
        }

        public static XElement ParameterNamed(XElement data, string name)
        {
            return data.Element("Parameters").Elements("Parameter").FirstOrDefault(e => e.Attribute("Name").Value == name);
        }
    }
}
