using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestComplete.Operations;
using TestComplete.Variables;

namespace TestComplete
{
    public class Program
    {
        private const string m_usage =  "Usage:\n" +
                                        "KWTConverter <filename>\n" +
                                        "   where filename is the filename to convert, e.g. test.tcKDTest\n";
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine(m_usage);
                return;
            }

            var input = args[0];
            var dir = Path.GetDirectoryName(input);
            if (dir == string.Empty)
            {
                dir = "."; // If in "current directory", would have empty string. Use "." instead.
            }

            var filename = Path.GetFileNameWithoutExtension(input) + ".out";
            var slash = Path.DirectorySeparatorChar;
            var output = $"{dir}{slash}{filename}";

            File.Create(output).Close();
            var streamWriter = new StreamWriter(output);

            var doc = XDocument.Load(input);
            var variables = doc.Element("Root").Element("Variables").Element("Items").Elements();
            var operations = doc.Element("Root").Element("TestData").Element("Children").Elements();

            var allVars = new List<Variable>();
            foreach (var v in variables)
            {
                allVars.Add(Variables.Factory.BuildVariable(v));
            }

#if false
            allVars.Dump();
            variables.Dump();
#endif
            var program = new List<Operation>();
            string prog = string.Empty;
            foreach (var o in operations)
            {
                var operation = Operations.Factory.BuildOperation(o);
                program.Add(operation);
                prog += operation.Display(0);
                //// Console.WriteLine(operation.Display(0));
            }

            Console.WriteLine(prog);
            streamWriter.WriteLine(prog);
        }
    }
}
