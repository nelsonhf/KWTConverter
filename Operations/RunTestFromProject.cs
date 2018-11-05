using System.Xml.Linq;

namespace TestComplete
{
    namespace Operations
    {
        class RunTestFromProject : Operation
        {
            public RunTestFromProject(XElement data, XElement children) : base("Run Test", OperTypes.Catch, data, children)
            {
            }
        }
    }
}