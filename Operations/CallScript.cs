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
        public class CallScript : Operation
        {
            public string TestName { get; private set; }
            public string Unit { get; private set; }
            public CallScript(XElement data, XElement children): base("Run Script Routine", OperTypes.CallScript, data, children)
            {
                TestName = data.Attribute("TestName")?.Value;
                Unit = data.Attribute("Unit")?.Value;
            }

            public override string Display(int level)
            {
                return $"{PaddedOperationName(level)}{Unit} - {TestName}\n";
            }
        }
    }
}