using System.Xml.Linq;

namespace TestComplete
{
    namespace Operations
    {
        class CallObjectMethod : Operation
        {
            public CallObjectMethod(XElement data, XElement children) : base("Call Object Method", OperTypes.Catch, data, children)
            {
            }
        }
    }
}