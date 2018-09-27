using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestComplete
{
    namespace Operations
    {
        public class For : Operation
        {
            private List<Parameter> m_parameters;

            public IReadOnlyList<Parameter> Parameters => m_parameters.AsReadOnly();
            public For(XElement data, XElement children) : base("For Loop", OperTypes.If, data, children)
            {
                m_parameters = new List<Parameter>();
                foreach (var p in data.Element("Parameters")?.Elements("Parameter") ?? new List<XElement>())
                {
                    m_parameters.Add(new Parameter(p));
                }
            }

            public override string Display(int level)
            {
                Parameter loopVariable = m_parameters.FirstOrDefault(n => n.Name == "Loop Variable");
                Parameter from = m_parameters.FirstOrDefault(n => n.Name == "Start Value");
                Parameter to = m_parameters.FirstOrDefault(n => n.Name == "End Value");
                Parameter step = m_parameters.FirstOrDefault(n => n.Name == "Step");

                var name = loopVariable.VarName;
                if (string.IsNullOrEmpty(name))
                {
                    name = "[n/a]";
                }

                var result = PaddedOperationName(level);
                result = PadToColumn(result, ParametersColumn);
                result += $"{name}, {from}, {to}, {step}\n";
                foreach (var c in Children)
                {
                    result += c.Display(level + 1);
                }

                return result;
            }
        }
    }
}