using System.Xml.Linq;

namespace TestComplete
{
    namespace Operations
    {
        class RunCodeSnippet : Operation
        {
            public RunCodeSnippet(XElement data, XElement children) : base("Run Code Snippet", OperTypes.Catch, data, children)
            {
            }
        }
    }
}