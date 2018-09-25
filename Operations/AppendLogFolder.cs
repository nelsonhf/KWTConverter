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
        public class AppendLogFolder : Operation
        {
            private List<Parameter> m_parameters;

            public IReadOnlyList<Parameter> Parameters => m_parameters.AsReadOnly();
            public string Description { get; private set; }

            public AppendLogFolder(XElement data, XElement children) : base ("Append Log Folder", OperTypes.AppendLogFolder, data, children)
            {
                m_parameters = new List<Parameter>();
                foreach (var p in data.Element("Parameters")?.Elements("Parameter") ?? new List<XElement>())
                {
                    m_parameters.Add(new Parameter(p));
                }

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
                var message = m_parameters.Where(p => p.Name == "MessageText").FirstOrDefault()?.ToString();
                var addInfo = m_parameters.Where(p => p.Name == "AdditionalInformation").FirstOrDefault()?.ToString();
                var result = PaddedOperationName(level);
                result = PadToColumn(result, ParametersColumn);
                result += $"\"{message}\", \"{addInfo}\"";

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