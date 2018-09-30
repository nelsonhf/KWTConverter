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
            public string Description { get; private set; }
            public string MessageType { get; private set; }
            public Return(XElement data, XElement children) : base("Return", OperTypes.Return, data, children)
            {
            }

            public override string Display(int level)
            {
                var returnValue = GetParameter("Return Value");
                var result = PaddedOperationName(level);
                result = PadToColumn(result, ParametersColumn);
                return $"{result}{returnValue}\n";
            }
        }
    }
}
