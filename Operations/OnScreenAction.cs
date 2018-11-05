using System.Xml.Linq;

namespace TestComplete
{
    namespace Operations
    {
        class OnScreenAction : Operation
        {
            public OnScreenAction(XElement data, XElement children) : base("On-Screen Action", OperTypes.Catch, data, children)
            {
            }
        }
    }
}