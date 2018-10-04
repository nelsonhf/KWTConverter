using System;
using System.Linq;
using System.Xml.Linq;
using TestComplete.NamedData;

namespace TestComplete
{
    public class Parameter
    {
        private const string UnknownValue = ".";
        public string ColumnName { get; private set; }
        public string Expression { get; private set; }
        public string Name { get; private set; }
        public string ParamType { get; private set; }
        public Guid? ParamTypeGuid { get; private set; }
        public string Value { get; private set; }
        public string VariableName { get; private set; }
        public int? VarType { get; private set; }
        public int? ValueType { get; private set; }

        public Parameter(XElement data)
        {
            Expression = data.Attribute("Expression")?.Value;
            ColumnName = data.Attribute("ColumnName")?.Value;
            Name = data.Attribute("Name")?.Value;
            ParamType = data.Attribute("ParamType")?.Value;
            ParamTypeGuid = ParamType == null ? (Guid?)null : new Guid(ParamType);
            Value = data.Attribute("ValueValue")?.Value;
            VariableName = data.Attribute("VariableName")?.Value;
            var vt = data.Attribute("VarType")?.Value;
            VarType = vt == null ? (int?)null : int.Parse(vt);
            vt = data.Attribute("ValueType")?.Value;
            ValueType = vt == null ? (int?)null : int.Parse(vt);
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
                            return VariableName;

                        case VarTypes.TableData:
                            return $"NamedData.{VariableName}(\"{ColumnName}\")";

                        default:
                            return UnknownValue;
                    }
                }
                else
                {
                    return UnknownValue;
                }
            }
            else if (ValueType == (int)NamedData.ValueType.Null)
            {
                return "null";
            }
            else if (ValueType == (int)NamedData.ValueType.Integer || ValueType == (int)NamedData.ValueType.Boolean)
            {
                return Value;
            }
            else if (ValueType == (int)NamedData.ValueType.UnicodeString)
            {
                return $"\"{Table.DecodeUnicodeString(Value)}\"";
            }
            else if (ValueType == (int)NamedData.ValueType.String && !Value.StartsWith("[KeywordTests")) // "[KeywordTests.." seems to indicate a reference to a variable or array
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
