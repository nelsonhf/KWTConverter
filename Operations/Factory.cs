using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace TestComplete
{
    namespace Operations
    {
        public class Factory
        {
            private static readonly Dictionary<OperTypes, Type> operationTypes = new Dictionary<OperTypes, Type>
            {
                { OperTypes.Comment,          typeof(Comment) },
                { OperTypes.CallKWT,          typeof(CallKWT) },
                { OperTypes.Try,              typeof(Try) },
                { OperTypes.Catch,            typeof(Catch) },
                { OperTypes.Finally,          typeof(Finally) },
                { OperTypes.Group,            typeof(Group) },
                { OperTypes.SetVariable,      typeof(SetVariable) },
                { OperTypes.If,               typeof(If) },
                { OperTypes.LogError,         typeof(LogError) },
                { OperTypes.Return,           typeof(Return) },
                { OperTypes.Else,             typeof(Else) },
                { OperTypes.Delay,            typeof(Delay) },
                { OperTypes.For,              typeof(For) },
                { OperTypes.DataDrivenTest,   typeof(DataDrivenTest) },
                { OperTypes.CallScript,       typeof(CallScript) },
                { OperTypes.AppendLogFolder,  typeof(AppendLogFolder) },
                { OperTypes.PopLogFolder,     typeof(PopLogFolder) },
            };

            public static Operation BuildOperation(XElement data)
            {
                if (data.Attribute("Moniker") != null)
                {
                    return CallLibrary.MakeLibrary(data, data.Element("Data"));
                }
                else
                {
                    var t = new Guid(data.Attribute("Type")?.Value);
                    var type = Operation.Operations.ContainsKey(t) ? Operation.Operations[t] : OperTypes.Unknown;

                    XElement opData = data.Element("Data");
                    XElement children = data.Element("Children");

                    return operationTypes.ContainsKey(type)
                        ? (Operation)Activator.CreateInstance(operationTypes[type], opData, children)
                        : new Operation("Unknown", type, opData, children);
                }
            }
        }
    }
}
