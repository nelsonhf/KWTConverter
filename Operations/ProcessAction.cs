using System.Xml.Linq;

namespace TestComplete
{
    namespace Operations
    {
        class ProcessAction : Operation
        {
            public ProcessAction(XElement data, XElement children) : base("Process Action", OperTypes.Catch, data, children)
            {
            }
        }
    }
}