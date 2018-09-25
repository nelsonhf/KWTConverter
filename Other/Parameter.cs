using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestComplete.Variables;

namespace TestComplete
{
    public class Parameter
    {
        private const string UnknownValue = ".";
        public string Expression { get; private set; }
        public string Name { get; private set; }
        public string ParamType { get; private set; }
        public Guid? ParamTypeGuid { get; private set; }
        public string Value { get; private set; }
        public string VarName { get; private set; }
        public string ValueType { get; private set; }

        public Parameter(XElement data)
        {
            Expression = data.Attribute("Expression")?.Value;
            Name = data.Attribute("Name").Value;
            ParamType = data.Attribute("ParamType")?.Value;
            ParamTypeGuid = ParamType == null ? (Guid?)null : new Guid(ParamType);
            Value = data.Attribute("ValueValue")?.Value;
            VarName = data.Attribute("VariableName")?.Value;
            ValueType = data.Attribute("ValueType")?.Value;
        }

        public override string ToString()
        {
            if (ParamTypeGuid != null)
            {
                if (Variable.TypeFromGuid.ContainsKey((Guid)ParamTypeGuid))
                {
                    switch (Variable.TypeFromGuid[(Guid)ParamTypeGuid])
                    {
                        case VarTypes.Expression:
                            return Expression;

                        case VarTypes.LastResult:
                            return "LastResult";

                        case VarTypes.Variable:
                            return VarName;

                        default:
                            return UnknownValue;
                    }
                }
                else
                {
                    return UnknownValue;
                }
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
                return UnknownValue;
            }
        }

        public static XElement ParameterNamed(XElement data, string name)
        {
            return data.Element("Parameters").Elements("Parameter").FirstOrDefault(e => e.Attribute("Name").Value == name);
        }
    }
}
