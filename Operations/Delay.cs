using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace TestComplete
{
    namespace Operations
    {
        public class Delay : Operation
        {
            private List<Parameter> m_parameters;

            public IReadOnlyList<Parameter> Parameters => m_parameters.AsReadOnly();
            public Delay(XElement data, XElement children) : base("Delay", OperTypes.Delay, data, children)
            {
                m_parameters = new List<Parameter>();
                foreach (var p in data.Element("Parameters")?.Elements("Parameter") ?? new List<XElement>())
                {
                    m_parameters.Add(new Parameter(p));
                }
            }

            public override string Display(int level)
            {
                var result = PaddedOperationName(level);
                result = PadToColumn(result, ParametersColumn);
                result += string.Join(", ", m_parameters.Select(p => p.ToString()).ToArray());
                return $"{result}\n";
            }
        }
    }
}