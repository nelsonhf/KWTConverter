using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestComplete
{
    namespace Variables
    {
        public class Table : Variable
        {
            public int Rows { get; private set; }
            public int Columns { get; private set; }
            public IReadOnlyList<string> RowNames => m_rowNames?.AsReadOnly();
            public IReadOnlyList<string> ColumnNames => m_columnNames.AsReadOnly();
            public IReadOnlyList<IReadOnlyList<string>> Data => DumpData();

            private List<string> m_rowNames;
            private List<string> m_columnNames;
            private string[,] m_data;

            public Table(string name, VarTypes type, string description, XElement def): base(name, type, description)
	        {
                m_rowNames = new List<string>();
                m_columnNames = new List<string>();

                Rows = int.Parse(def.Attribute("RowCount").Value);
                Columns = int.Parse(def.Attribute("ColumnCount").Value);
                var cols = def.Element("Columns");
                for (int i = 0; i < Columns; i++)
                {
                    m_columnNames.Add(cols.Attribute("ColumnName" + i).Value);
                }

                m_data = new string[Rows, Columns];
                var r = 0;
                foreach (var row in def.Elements("Row"))
                {
                    var thisRow = new Row(Columns, row);
                    var c = 0;
                    foreach (var col in thisRow.Data)
                    {
                        m_data[r, c++] = DecodeUnicodeString(col);
                    }

                    r++;
                }
            }

            public static string DecodeUnicodeString(string unicodeString)
            {
                if (string.IsNullOrEmpty(unicodeString))
                {
                    return string.Empty;
                }

                // We KNOW that the result will have exactly one character for each 4 in unicodeString.
                StringBuilder result = new StringBuilder(unicodeString.Length / 4);

                // Logic inside the loop assumes that unicodeString has multiple groups of 4 characters and process one group at a time
                while (unicodeString.Length >= 4)
                {
                    // Remove 4 characters (representing one Unicode character, e.g. "4D00" represents 'M') from unicodeString into oneUnicodeChar
                    var oneUnicodeChar = unicodeString.Substring(0, 4);
                    unicodeString = unicodeString.Substring(4);

                    // Convert into a little endian byte array and then to a one-character string, which is appended to the result
                    var number = int.Parse(oneUnicodeChar, System.Globalization.NumberStyles.HexNumber);    // "4D00" => 0x4D00
                    var liBytes = BitConverter.GetBytes(number);                                            // => { 0x4D, 0x00 }
                    byte[] biBytes = { liBytes[1], liBytes[0] };                                            // => { 0x00, 0x4D }
                    result.Append(Encoding.Unicode.GetChars(biBytes));                                      // => "M" +=> result
                }

                return result.ToString();
            }

            private IReadOnlyList<IReadOnlyList<string>> DumpData()
            {
                var result = new List<IReadOnlyList<string>>();
                for (int i = 0; i < Rows; i++)
                {
                    var oneRow = new List<string>();
                    for (int j = 0; j < Columns; j++)
                    {
                        oneRow.Add(m_data[i, j]);
                    }

                    result.Add(oneRow.AsReadOnly());
                }

                return result.AsReadOnly();
            }

            private class Row
            {
                public int Columns { get; private set; }
                public int Number { get; private set; }
                public IReadOnlyList<string> Data => m_data?.AsReadOnly();
                private List<string> m_data;
                public Row(int columns, XElement data)
                {
                    m_data = new List<string>();
                    Number = int.Parse(data.Attribute("No").Value);
                    for (int i = 0; i < columns; i++)
                    {
                        var cellName = "Cell" + i;
                        var d = data.Attribute(cellName)?.Value;
                        m_data.Add(d);
                    }
                }
            }
        }
    }
}
