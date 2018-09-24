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
            public Parameter LoopVariable { get; private set; }
            public string From { get; private set; }
            public string To { get; private set; }
            public string Step { get; private set; }
            public For(XElement data, XElement children) : base("For Loop", OperTypes.If, data, children)
            {
                LoopVariable = new Parameter(Parameter.ParameterNamed(data, "Loop Variable"));
                From = Parameter.ParameterNamed(data, "Start Value")?.Attribute("ValueValue")?.Value;
                To = Parameter.ParameterNamed(data, "End Value")?.Attribute("ValueValue")?.Value;
                Step = Parameter.ParameterNamed(data, "Step")?.Attribute("ValueValue")?.Value;
            }

            public override string Display(int level)
            {
                var name = LoopVariable.VarName;
                if (string.IsNullOrEmpty(name))
                {
                    name = "[n/a]";
                }

                var result = PaddedOperationName(level);
                result = PadToColumn(result, ParametersColumn);
                result += $"{Indent(level)}{OperationName} {name}, {From}, {To}, {Step}\n";
                foreach (var c in Children)
                {
                    result += c.Display(level + 1);
                }

                return result;
            }
        }
    }
}