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
        public enum OperTypes
        {
            Comment,
            Try,
            Catch,
            Group,
            CallKWT,
            SetVariable,
            CallLibrary,
            If,
            LogError,
            Return,
            Else,
            Delay,
            For,
            CallScript,
            DataDrivenTest, 
            Finally,
            AppendLogFolder,
            PopLogFolder,
        };

        public class Operation
        {
            public static readonly Dictionary<Guid, OperTypes> Operations = new Dictionary<Guid, OperTypes>
            {
                { new Guid("{5B065688-A7F9-41DE-BA45-BF23B8116C41}"), OperTypes.Comment },
                { new Guid("{457A3880-2713-4B54-A254-B2BDA06E7113}"), OperTypes.Try },
                { new Guid("{D993D251-65A9-4BED-A2EC-C2AC83739BAD}"), OperTypes.Group },
                { new Guid("{E8089E1C-53BC-4D9C-A69D-7CDDB49422CB}"), OperTypes.CallKWT },
                { new Guid("{6F20B0D8-E0CF-47FF-A68B-3E3672DD0CB0}"), OperTypes.SetVariable },
	            { new Guid("{EEDC9229-5F2A-4450-980E-7EB218C67090}"), OperTypes.If },
	            { new Guid("{BFB3C418-1303-46B3-9B24-3624AB5EB529}"), OperTypes.LogError },
	            { new Guid("{80DF6CF2-7793-4928-B7D9-A658325637B1}"), OperTypes.Return },
	            { new Guid("{92F2155F-410E-4D93-B7B3-684BA934382B}"), OperTypes.Else },
	            { new Guid("{6A1B4CEE-4FB8-46E2-BFBD-403C84342301}"), OperTypes.Delay },
	            { new Guid("{94838C49-976F-4128-8B05-4E7C3E9C579D}"), OperTypes.DataDrivenTest },
	            { new Guid("{206B0810-0B42-45AB-90F5-9F8DF0D669D2}"), OperTypes.CallScript },
	            { new Guid("{60B8EBE8-9B0C-4A7B-A452-229144AEA05B}"), OperTypes.For },
                { new Guid("{DA0842B1-F222-4746-8498-4933E5527E0A}"), OperTypes.Catch },
	            { new Guid("{4B9A062D-45FB-439D-AF18-13015DA3B9FA}"), OperTypes.Finally },
	            { new Guid("{90F89436-9452-4F95-9882-6B5210079F13}"), OperTypes.AppendLogFolder },
	            { new Guid("{56B7409E-809F-4569-B55F-35534DDD66FF}"), OperTypes.PopLogFolder },
	            //	{ new Guid(), OperTypes. },
	            //	{ new Guid(), OperTypes. },
            };

            public const int OperationTypeColumn = 40;
            public const int ParametersColumn = 80;
            public const int CommentColumn = 120;

            public string OperationName { get; private set; }
            public OperTypes Type { get; private set; }
            public XElement Data { get; private set; }
            public IReadOnlyList<Operation> Children => m_children.AsReadOnly();

            private List<Operation> m_children;
            public Operation(string opName, OperTypes type, XElement data, XElement children)
            {
                m_children = new List<Operation>();

                OperationName = opName;
                Type = type;
                Data = data;

                if (children?.Elements("Operation") != null)
                {
                    foreach (var c in children.Elements("Operation"))
                    {
                        m_children.Add(Factory.BuildOperation(c));
                    }
                }
            }

            public static string Indent(int level)
            {
                return new string(' ', level * 4);
            }

            public static string PadToColumn(string data, int column)
            {
                int length = data.Length;
                if (length > column)
                {
                    return data.Substring(0, column - 3) + "...";
                }
                else
                {
                    string pad = new string(' ', column - length);
                    return data + pad;
                }
            }

            public string PaddedOperationName(int level)
            {
                var result = $"{Indent(level)}{OperationName}";
                return PadToColumn(result, OperationTypeColumn);
            }

            public virtual string Display(int level)
            {
                var result = $"{Indent(level)}{OperationName}\n";
                foreach (var c in Children)
                {
                    result += c.Display(level + 1);
                }

                return result;
            }
        }
    }
}
