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
            public For(XElement data, XElement children) : base("For Loop", OperTypes.If, data, children)
            {
            }

            public override string Display(int level)
            {
                Parameter loopVariable = GetParameter("Loop Variable");
                Parameter from = GetParameter("Start Value");
                Parameter to = GetParameter("End Value");
                Parameter step = GetParameter("Step");

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