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
        /// <summary>
        /// Operation PopLogFolder closes a group opened with AppendLogFolder
        /// </summary>
        /// <example>
        /// &lt;Operation Type="{56B7409E-809F-4569-B55F-35534DDD66FF}"&gt;
        ///    &lt;Data Description="Pops the folder that is currently at the top of the folder stack out of the stack. This makes the folder that will become the top of the stack the default folder of the test log." /&gt;
        /// &lt;/Operation&gt;
        /// </example>
        public class PopLogFolder : Operation
        {
            public PopLogFolder(XElement data, XElement children) : base("Pop Log Folder", OperTypes.PopLogFolder, data, children)
            {
            }
        }
    }
}