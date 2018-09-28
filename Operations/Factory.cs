using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace TestComplete
{
    namespace Operations
    {
        public class Factory
        {
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

                    var operTypeToOperation = new Dictionary<OperTypes, Operation>()
                    {
                        { OperTypes.Comment,          new Comment(opData, children) },
                        { OperTypes.CallKWT,          new CallKWT(opData, children) },
                        { OperTypes.Try,              new Try(opData, children) },
                        { OperTypes.Catch,            new Catch(opData, children) },
                        { OperTypes.Finally,          new Finally(opData, children) },
                        { OperTypes.Group,            new Group(opData, children) },
                        { OperTypes.SetVariable,      new SetVariable(opData, children) },
                        { OperTypes.If,               new If(opData, children) },
                        { OperTypes.LogError,         new LogError(opData, children) },
                        { OperTypes.Return,           new Return(opData, children) },
                        { OperTypes.Else,             new Else(opData, children) },
                        { OperTypes.Delay,            new Delay(opData, children) },
                        { OperTypes.For,              new For(opData, children) },
                        { OperTypes.DataDrivenTest,   new DataDrivenTest(opData, children) },
                        { OperTypes.CallScript,       new CallScript(opData, children) },
                        { OperTypes.AppendLogFolder,  new AppendLogFolder(opData, children) },
                        { OperTypes.PopLogFolder,     new PopLogFolder(opData, children) },
                    };

                    return operTypeToOperation.ContainsKey(type) ? operTypeToOperation[type] : new Operation("Unknown", type, opData, children);
                }
            }
        }
    }
}
