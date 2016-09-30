using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Task1_CSV_with_LINQ
{
    class CsvCategoryReader
    {
        public IEnumerable<Category> ReadFromFile(string fileName)
        {
            return File.ReadAllLines(fileName)
                .Skip(1)
                .Select(t => t.Split(';'))
                .Select(t => new Category() { Id = Convert.ToInt32(t[0]), Name = t[1] }).ToList();
}
        public bool WriteFromFile(IEnumerable<Category> items)
        {
            throw new NotImplementedException();
        }
    }
}
