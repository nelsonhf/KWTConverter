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
                { OperTypes.AppendLogFolder,    typeof(AppendLogFolder) },
                { OperTypes.CallKWT,            typeof(CallKWT) },
                { OperTypes.CallObjectMethod,   typeof(CallObjectMethod) },
                { OperTypes.CallScript,         typeof(CallScript) },
                { OperTypes.Catch,              typeof(Catch) },
                { OperTypes.Comment,            typeof(Comment) },
                { OperTypes.DataDrivenTest,     typeof(DataDrivenTest) },
                { OperTypes.Delay,              typeof(Delay) },
                { OperTypes.Else,               typeof(Else) },
                { OperTypes.Finally,            typeof(Finally) },
                { OperTypes.FindObject,         typeof(FindObject) },
                { OperTypes.For,                typeof(For) },
                { OperTypes.GotoLabel,          typeof(GotoLabel) },
                { OperTypes.Group,              typeof(Group) },
                { OperTypes.If,                 typeof(If) },
                { OperTypes.ImageBasedAction,   typeof(ImageBasedAction) },
                { OperTypes.Label,              typeof(Label) },
                { OperTypes.Log,                typeof(Log) },
                { OperTypes.MenuAction,         typeof(MenuAction) },
                { OperTypes.OnScreenAction,     typeof(OnScreenAction) },
                { OperTypes.PopLogFolder,       typeof(PopLogFolder) },
                { OperTypes.ProcessAction,      typeof(ProcessAction) },
                { OperTypes.Return,             typeof(Return) },
                { OperTypes.RunCodeSnippet,     typeof(RunCodeSnippet) },
                { OperTypes.RunTestedApp,       typeof(RunTestedApp) },
                { OperTypes.RunTestFromProject, typeof(RunTestFromProject) },
                { OperTypes.SetVariable,        typeof(SetVariable) },
                { OperTypes.StopRunning,        typeof(StopRunning) },
                { OperTypes.Try,                typeof(Try) },
                { OperTypes.While,              typeof(While) },
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
