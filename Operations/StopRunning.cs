using System.Xml.Linq;

namespace TestComplete
{
    namespace Operations
    {
        class StopRunning : Operation
        {
            public StopRunning(XElement data, XElement children) : base("Stop", OperTypes.Catch, data, children)
            {
            }
        }
    }
}