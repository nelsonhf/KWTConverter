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
                    var t = new Guid(data.Attribute("Type").Value);
                    OperTypes type;
                    if (Operation.Operations.ContainsKey(t))
                    {
                        type = Operation.Operations[t];
                    }
                    else
                    {
                        type = OperTypes.Unknown;
                    }

                    XElement opData = data.Element("Data");
                    XElement children = data.Element("Children");

                    switch (type)
                    {
                        case OperTypes.Comment:
                            return new Comment(opData, children);
                        // break;

                        case OperTypes.CallKWT:
                            return new CallKWT(opData, children);
                        // break;

                        case OperTypes.Try:
                            return new Try(opData, children);
                        // break;

                        case OperTypes.Catch:
                            return new Catch(opData, children);
                        // break;

                        case OperTypes.Finally:
                            return new Finally(opData, children);
                        // break;

                        case OperTypes.Group:
                            return new Group(opData, children);
                        // break;

                        case OperTypes.SetVariable:
                            return new SetVariable(opData, children);
                        // break;

                        case OperTypes.If:
                            return new If(opData, children);
                        // break;

                        case OperTypes.LogError:
                            return new LogError(opData, children);
                            // break;

                        case OperTypes.Return:
                            return new Return(opData, children);
                            // break;

                        case OperTypes.Else:
                            return new Else(opData, children);
                            // break;

                        case OperTypes.Delay:
                            return new Delay(opData, children);
                            // break;

                        case OperTypes.For:
                            return new For(opData, children);
                            // break;

                        case OperTypes.DataDrivenTest:
                            return new DataDrivenTest(opData, children);
                            // break;

                        case OperTypes.CallScript:
                            return new CallScript(opData, children);
                            // break;

                        case OperTypes.AppendLogFolder:
                            return new AppendLogFolder(opData, children);
                            // break;

                        case OperTypes.PopLogFolder:
                            return new PopLogFolder(opData, children);
                            // break;

                        default:
                            return new Operation("Unknown", type, opData, children);
                            // break;
                    }
                }
            }
        }
    }
}
