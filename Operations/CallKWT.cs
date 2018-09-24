using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestComplete;

namespace TestComplete
{
    namespace Operations
    {
        public class CallKWT : Operation
        {
            private List<Parameter> m_parameters;

            public IReadOnlyList<Parameter> Parameters => m_parameters.AsReadOnly();
            public string Name { get; private set; }
            public string Description { get; private set; }

            public CallKWT(XElement data, XElement children) : base("Run KWT", OperTypes.CallKWT, data, children)
            {
                m_parameters = new List<Parameter>();
                foreach (var p in data.Element("Parameters")?.Elements("Parameter") ?? new List<XElement>())
                {
                    m_parameters.Add(new Parameter(p));
                }

                Name = data.Attribute("TestName").Value;
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
                var result = $"{PaddedOperationName(level)}{Name}";
                result = PadToColumn(result, ParametersColumn);
                result += string.Join(", ", m_parameters.Select(p => p.ToString()).ToArray());

                if (Description != null)
                {
                    result = PadToColumn(result, CommentColumn) + Description;
                }

                result += "\n";

                foreach (var c in Children)
                {
                    result += c.Display(level + 1);
                }

                return result;
            }
        }
    }
}