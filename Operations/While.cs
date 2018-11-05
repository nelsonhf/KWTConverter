using System.Xml.Linq;

namespace TestComplete
{
    namespace Operations
    {
        class While : Operation
        {
            public While(XElement data, XElement children) : base("While", OperTypes.Catch, data, children)
            {
            }
        }
    }
}