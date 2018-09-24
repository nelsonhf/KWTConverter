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
        public class Comment : Operation
        {
            public string Value { get; private set; }
            public Comment(XElement data, XElement children) : base("Comment", OperTypes.Comment, data, children)
        	{
                Value = data?.Attribute("Comment")?.Value;
            }

            public override string Display(int level)
            {
                var prefix = string.Empty;
                if (!Value.StartsWith("/*"))
                {
                    prefix = "// ";
                }

                return $"{Indent(level)}{prefix}{Value}\n";
            }
        }
    }
}