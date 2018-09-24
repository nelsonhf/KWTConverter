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
        public class If : Operation
        {
            public Condition Test { get; private set; }
            public If(XElement data, XElement children) : base("If", OperTypes.If, data, children)
            {
                Test = new Condition(data.Element("Root"));
            }

            public override string Display(int level)
            {
                var result = PaddedOperationName(level);
                result = PadToColumn(result, ParametersColumn);
                result += Test.Display() + "\n";

                foreach (var c in Children)
                {
                    result += c.Display(level + 1);
                }

                return result;
            }
        }
    }
}
