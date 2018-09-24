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
        public class Return : Operation
        {
            private List<Parameter> m_parameters;

            public IReadOnlyList<Parameter> Parameters => m_parameters.AsReadOnly();
            public string Description { get; private set; }
            public string MessageType { get; private set; }
            public Return(XElement data, XElement children) : base("Return", OperTypes.Return, data, children)
            {
                m_parameters = new List<Parameter>();

                foreach (var p in data.Element("Parameters")?.Elements("Parameter") ?? new List<XElement>())
                {
                    m_parameters.Add(new Parameter(p));
                }
            }

            public override string Display(int level)
            {
                var returnValue = m_parameters.FirstOrDefault(n => n.Name == "Return Value")?.Value ?? string.Empty;
                var result = PaddedOperationName(level);
                result = PadToColumn(result, ParametersColumn);
                return $"{result}{returnValue}\n";
            }
        }
    }
}
