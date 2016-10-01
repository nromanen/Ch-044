using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTask
{
    static class CsvBuilderForCategories
    {
        static private char _delimeter = ';';

        public static void ChangeDelimeter(char del)
        {
            _delimeter = del;
        }

        static public List<Category> ReadFromCsv(string path)
        {
            var reader = new StreamReader(File.OpenRead(path));
            List<Category> resultList = new List<Category>();
            int iterator = 1;
            reader.ReadLine();

            while (!reader.EndOfStream)
            {
                int id;
                string name;
                var line = reader.ReadLine();
                var values = line.Split(_delimeter);
                try
                {
                    id = Int32.Parse(values[0]);
                    if (id < 1)
                    {
                        throw new Exception("Error on parsing field ID on " + iterator.ToString() + "string.Id must be greater than 0 -- CategoryBuilder");
                    }

                    var query = from category in resultList
                                where id == category.Id
                                select category;

                    if (query.Count() != 0)
                    {
                        throw new Exception("Error on parsing field ID on " + iterator.ToString() + "string.Id must be unique -- CategoryBuilder");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error on parsing field ID on " + iterator.ToString() + "string -- CategoryBuilder\n" + ex.Message);
                }

                try
                {
                    name = values[1];
                }
                catch
                {
                    throw new Exception("Error on parsing field Name on " + iterator.ToString() + "string -- CategoryBuilder");
                }


                resultList.Add(new Category(id, name));
                iterator++;
            }
            return resultList;
        }

        static public void WriteToCsv(List<Category> list,string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine("Id;Name");
                    foreach (var category in list)
                    {
                        sw.WriteLine(category.Id.ToString() + _delimeter +
                                     category.Name);
                    }


                }
            }
        }
    }
}
