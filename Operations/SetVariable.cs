using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestComplete.Variables;

namespace TestComplete
{
    namespace Operations
    {
        public class SetVariable : Operation
        {
            private List<Parameter> m_parameters;

            public IReadOnlyList<Parameter> Parameters => m_parameters.AsReadOnly();
            public string VariableName { get; private set; }
            public VarTypes VariableType { get; private set; }
            public SetVariable(XElement data, XElement children) : base("Set Variable Value", OperTypes.SetVariable, data, children)
            {
                m_parameters = new List<Parameter>();
                foreach (var p in data.Element("Parameters")?.Elements("Parameter") ?? new List<XElement>())
                {
                    m_parameters.Add(new Parameter(p));
                }

                VariableName = data.Attribute("VariableName").Value;
                var typenumber = int.Parse(data.Attribute("VariableType").Value);
                VariableType = Variable.TypeFromNumber[typenumber];
            }

            public override string Display(int level)
            {
                var result = $"{PaddedOperationName(level)}{VariableName}";
                result = PadToColumn(result, ParametersColumn);
                result += string.Join(", ", m_parameters.Select(p => p.ToString()).ToArray());
                return $"{result}\n";
            }
        }
    }
}
