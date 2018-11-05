using System.Xml.Linq;

namespace TestComplete
{
    namespace Operations
    {
        class RunTestedApp : Operation
        {
            public RunTestedApp(XElement data, XElement children) : base("Run TestedApp", OperTypes.Catch, data, children)
            {
            }
        }
    }
}