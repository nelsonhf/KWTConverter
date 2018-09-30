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
        /// Operation AppendLogFolder adds a group (causing indentation and allowing collapse) to
        /// the Test Run Report
        /// </summary>
        /// <example>
        /// &lt;Operation Type="{28D3E73B-A718-4E67-96A3-4C49D5D4B340}"&gt;
        ///   &lt;Data Description="Creates a log folder and makes it the current folder to which messages will be posted. This folder can contain messages of different types as well as subfolders." &gt;
        ///      &lt;Parameters LangId="{90F89436-9452-4F95-9882-6B5210079F13}"&gt;
        ///         &lt;Parameter Name="MessageText" DefVarType="12" VarType="8" ValueType="6" ValueValue="Text to display in log" /&gt;
        ///         &lt;Parameter Name="AdditionalInformation" DefVarType="12" VarType="8" ValueType="6" ValueValue="" /&gt;
        ///         &lt;Parameter Name="Priority" DefVarType="12" DefValueType="1" DefValueValue="300" VarType="3" ValueType="1" ValueValue="300"&gt;
        ///            &lt;Values IsEnum="True" &gt;
        ///               &lt;Value Type="1" Value="100" Text="pmLowest" /&gt;
        ///               &lt;Value Type="1" Value="200" Text="pmLower" /&gt;
        ///               &lt;Value Type="1" Value="300" Text="pmNormal" /&gt;
        ///               &lt;Value Type="1" Value="400" Text="pmHigher" /&gt;
        ///               &lt;Value Type="1" Value="500" Text="pmHighest" /&gt;
        ///            &lt;/Values&gt;
        ///         &lt;/Parameter&gt;
        ///         &lt;Parameter Name="Attrib" DefVarType="12" VarType="12" ValueType="0" ValueValue="0" /&gt;
        ///         &lt;Parameter Name="OwnerFolderId" DefVarType="3" DefValueType="1" DefValueValue="-1" VarType="3" ValueType="1" ValueValue="-1" /&gt;
        ///      &lt;/Parameters&gt;
        ///   &lt;/Data&gt;
        /// &lt;/Operation&gt;
        /// </example>
        public class AppendLogFolder : Operation
        {
            public string Description { get; private set; }

            public AppendLogFolder(XElement data, XElement children) : base ("Append Log Folder", OperTypes.AppendLogFolder, data, children)
            {
                Description = GetDescription();
            }

            public override string Display(int level)
            {
                Parameter message = GetParameter("MessageText");
                Parameter addInfo = GetParameter("AdditionalInformation");
                var result = PaddedOperationName(level);
                result = PadToColumn(result, ParametersColumn);
                result += $"{message}, {addInfo}";

                if (Description != null)
                {
                    result = PadToColumn(result, CommentColumn) + Description;
                }

                result += "\n";

                foreach (var c in Children)
                {
                    result += c.Display(level + 1);
                }

                return result;
            }
        }
    }
}