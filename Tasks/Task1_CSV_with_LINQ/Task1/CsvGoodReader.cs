using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Task1_CSV_with_LINQ
{
    class CsvGoodReader
    {
        public IEnumerable<Good> ReadFromFile(string fileName, IEnumerable<Producer> producers, IEnumerable<Category> categories)
        {
            return File.ReadAllLines(fileName)
                .Skip(1)
                .Select(t => t.Split(';'))
                .Select(t => new Good()
                {
                    Id = Convert.ToInt32(t[0]),
                    Name = t[1],
                    Price = Convert.ToDecimal(t[2]),
                    Category = categories.SingleOrDefault(p => p.Id == Convert.ToInt32(t[3])),
                    Producer = producers.SingleOrDefault(p => p.Id == Convert.ToInt32(t[4]))
                }).ToList();
        }

        public bool WriteFromFile(IEnumerable<Good> items)
        {
            throw new NotImplementedException();
        }
    }
}
