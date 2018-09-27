using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace TestComplete
{
    namespace Operations
    {
        public class LogError : Operation
        {
            private List<Parameter> m_parameters;

            public IReadOnlyList<Parameter> Parameters => m_parameters.AsReadOnly();
            public string Description { get; private set; }
            public string MessageType { get; private set; }
            public LogError(XElement data, XElement children) : base("Log", OperTypes.LogError, data, children)
            {
                m_parameters = new List<Parameter>();
                foreach (var p in data.Element("Parameters")?.Elements("Parameter") ?? new List<XElement>())
                {
                    m_parameters.Add(new Parameter(p));
                }

                MessageType = data.Attribute("MessageType")?.Value;
                if (data.Attribute("DescriptionEdited")?.Value == "True")
                {
                    Description = data.Attribute("Description")?.Value;
                }
                else
                {
                    Description = null;
                }
            }

            public override string Display(int level)
            {
                Parameter description = m_parameters.FirstOrDefault(n => n.Name == "MessageText");
                Parameter additInfo = m_parameters.FirstOrDefault(n => n.Name == "AdditionalInformation");
                var result = PaddedOperationName(level);
                result = PadToColumn(result, ParametersColumn);
                result += $"{description}, {additInfo}\n";
                return result;
            }
        }
    }
}