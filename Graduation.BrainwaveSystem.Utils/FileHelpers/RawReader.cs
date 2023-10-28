using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Utils.FileHelpers
{
    public class RawReader
    {
        public List<int> GetListValueFromTextFileByLine(string filePath)
        {
            List<int> rawValues = new List<int>();

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    if (int.TryParse(line, out int value))
                    {
                        rawValues.Add(value);
                    }
                }
            }
            catch (IOException e)
            {
                // Xử lý lỗi đọc file
                Console.WriteLine("Lỗi đọc file: " + e.Message);
            }

            return rawValues;
        }
    }
}
