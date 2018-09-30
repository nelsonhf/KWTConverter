using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace TestComplete
{
    namespace Operations
    {
        public class Delay : Operation
        {
            public Delay(XElement data, XElement children) : base("Delay", OperTypes.Delay, data, children)
            {
            }

            public override string Display(int level)
            {
                var result = PaddedOperationName(level);
                result = PadToColumn(result, ParametersColumn);
                result += AllParameters();
                return $"{result}\n";
            }
        }
    }
}