using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_CSV_with_LINQ
{
    public class CsvProducerReader
    {

        public IEnumerable<Producer> ReadFromFile(string fileName)
        {
            return File.ReadAllLines(fileName)
                .Skip(1)
                .Select(t => t.Split(';'))
                .Select(t => new Producer() { Id = Convert.ToInt32(t[0]), Name = t[1], Country = t[2] }).ToList();
        }

        public bool WriteFromFile(IEnumerable<Producer> items)
        {
            throw new NotImplementedException();
        }
    }
}
