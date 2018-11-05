using System.Xml.Linq;

namespace TestComplete
{
    namespace Operations
    {
        class GotoLabel : Operation
        {
            public GotoLabel(XElement data, XElement children) : base("Go to Label", OperTypes.Catch, data, children)
            {
            }
        }
    }
}