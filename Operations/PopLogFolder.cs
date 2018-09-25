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
        public class PopLogFolder : Operation
        {
            public PopLogFolder(XElement data, XElement children) : base("Pop Log Folder", OperTypes.PopLogFolder, data, children)
            {
            }
        }
    }
}