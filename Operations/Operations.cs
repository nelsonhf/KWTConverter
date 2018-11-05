using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace TestComplete
{
    namespace Operations
    {
        public enum OperTypes
        {
            AppendLogFolder,
            CallKWT,
            CallLibrary,
            CallObjectMethod,
            CallScript,
            Catch,
            Comment,
            DataDrivenTest, 
            Delay,
            Else,
            Finally,
            FindObject,
            For,
            GotoLabel,
            Group,
            If,
            ImageBasedAction,
            Label,
            Log,
            MenuAction,
            OnScreenAction,
            PopLogFolder,
            ProcessAction,
            Return,
            RunCodeSnippet,
            RunTestedApp,
            RunTestFromProject,
            SetVariable,
            StopRunning,
            Try,
            While,
            Unknown,
        };

        public class Operation
        {
            public static readonly Dictionary<Guid, OperTypes> Operations = new Dictionary<Guid, OperTypes>
            {
                { new Guid("{16DC8186-4DEC-41AF-80A0-F8C655838299}"), OperTypes.RunTestFromProject },
                { new Guid("{180A2D9C-07CF-406C-A3AD-F5F88281A719}"), OperTypes.ProcessAction },
                { new Guid("{19D49D7D-E1B4-4986-9252-E04D3D66ABDE}"), OperTypes.ImageBasedAction },
                { new Guid("{1C967ED8-389B-4CAF-8C28-CF1F70256A95}"), OperTypes.FindObject },
                { new Guid("{206B0810-0B42-45AB-90F5-9F8DF0D669D2}"), OperTypes.CallScript },
                { new Guid("{28D3E73B-A718-4E67-96A3-4C49D5D4B340}"), OperTypes.AppendLogFolder },
                { new Guid("{457A3880-2713-4B54-A254-B2BDA06E7113}"), OperTypes.Try },
                { new Guid("{4B9A062D-45FB-439D-AF18-13015DA3B9FA}"), OperTypes.Finally },
                { new Guid("{56B7409E-809F-4569-B55F-35534DDD66FF}"), OperTypes.PopLogFolder },
                { new Guid("{575D0372-CCEB-4D0F-AFE8-D2004F108913}"), OperTypes.OnScreenAction },
                { new Guid("{5B065688-A7F9-41DE-BA45-BF23B8116C41}"), OperTypes.Comment },
                { new Guid("{60B8EBE8-9B0C-4A7B-A452-229144AEA05B}"), OperTypes.For },
                { new Guid("{66F32D5F-A5DB-420E-9CB4-3DB7CADE2692}"), OperTypes.RunTestedApp },
                { new Guid("{6A1B4CEE-4FB8-46E2-BFBD-403C84342301}"), OperTypes.Delay },
                { new Guid("{6F092AB6-AD45-4F55-ADE1-9E63E2D69763}"), OperTypes.GotoLabel },
                { new Guid("{6F20B0D8-E0CF-47FF-A68B-3E3672DD0CB0}"), OperTypes.SetVariable },
                { new Guid("{792A19F3-4764-463F-B326-ECE40D9596DB}"), OperTypes.StopRunning },
                { new Guid("{80DF6CF2-7793-4928-B7D9-A658325637B1}"), OperTypes.Return },
                { new Guid("{92F2155F-410E-4D93-B7B3-684BA934382B}"), OperTypes.Else },
                { new Guid("{94838C49-976F-4128-8B05-4E7C3E9C579D}"), OperTypes.DataDrivenTest },
                { new Guid("{BE98E2BD-F90C-4DA4-8914-6D6246ED3CAC}"), OperTypes.While },
                { new Guid("{BFB3C418-1303-46B3-9B24-3624AB5EB529}"), OperTypes.Log },
                { new Guid("{CAAA7522-FB54-4521-BF2F-29D327FC9341}"), OperTypes.CallObjectMethod },
                { new Guid("{CE294ABA-E13B-4B94-AFC8-FA8F4249123D}"), OperTypes.MenuAction },
                { new Guid("{D993D251-65A9-4BED-A2EC-C2AC83739BAD}"), OperTypes.Group },
                { new Guid("{DA0842B1-F222-4746-8498-4933E5527E0A}"), OperTypes.Catch },
                { new Guid("{DCEB53DA-FEB9-4BC3-93B2-33BCD89569F5}"), OperTypes.RunCodeSnippet },
                { new Guid("{E8089E1C-53BC-4D9C-A69D-7CDDB49422CB}"), OperTypes.CallKWT },
                { new Guid("{EEDC9229-5F2A-4450-980E-7EB218C67090}"), OperTypes.If },
                { new Guid("{F503A89E-FD8E-4922-B6E0-A5976461A68A}"), OperTypes.Label },
                //  { new Guid(), OperTypes. },
                //  { new Guid(), OperTypes. },
                //  { new Guid(), OperTypes. },
            };

            public const int OperationTypeColumn = 40;
            public const int ParametersColumn = 80;
            public const int CommentColumn = 120;

            public string OperationName { get; private set; }
            public OperTypes Type { get; private set; }
            public XElement Data { get; private set; }
            public IReadOnlyList<Parameter> Parameters => m_parameters.AsReadOnly();
            public IReadOnlyList<Operation> Children => m_children.AsReadOnly();

            protected List<Parameter> m_parameters;

            private List<Operation> m_children;

            public Operation(string opName, OperTypes type, XElement data, XElement children)
            {
                m_parameters = new List<Parameter>();
                foreach (var p in data.Element("Parameters")?.Elements("Parameter") ?? new List<XElement>())
                {
                    m_parameters.Add(new Parameter(p));
                }

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

            protected string GetDescription()
            {
                return Data.Attribute("DescriptionEdited")?.Value == "True"
                    ? Data.Attribute("Description")?.Value
                    : null;
            }

            protected Parameter GetParameter(string name)
            {
                return m_parameters.FirstOrDefault(n => n.Name == name);
            }

            // Return all parameters, in the order they are defined, separated by commas
            protected string AllParameters()
            {
                return string.Join(", ", m_parameters.Select(p => p.ToString()).ToArray());
            }
        }
    }
}
