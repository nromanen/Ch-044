using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTask
{
    static class CsvBuilderForGoods
    {
        static private char _delimeter = ';';

        public static void ChangeDelimeter(char del)
        {
            _delimeter = del;
        }

        public static List<Good> ReadFromCsv(string path)
        {
            var reader = new StreamReader(File.OpenRead(path));
            List<Good> resultList = new List<Good>();

            reader.ReadLine();
            int iterator = 1;
            while (!reader.EndOfStream)
            {
                int id;
                string name;
                decimal price;
                int categoryId;
                int producerId;

                var line = reader.ReadLine();
                var values = line.Split(_delimeter);

                try
                {
                    id = Int32.Parse(values[0]);
                    if (id < 1)
                    {
                        throw new Exception("Error on parsing field ID on " + iterator.ToString() + "string.Id must be greater than 0 -- GoodBuilder");
                    }

                    var query = from good in resultList
                                where id == good.Id
                                select good;

                    if (query.Count() != 0)
                    {
                        throw new Exception("Error on parsing field ID on " + iterator.ToString() + "string.Id must be unique -- GoodBuilder");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error on parsing field ID on " + iterator.ToString() + "string -- GoodBuilder\n" + ex.Message);
                }

                try
                {
                    name = values[1];
                }
                catch
                {
                    throw new Exception("Error on parsing field Name on " + iterator.ToString() + "string -- GoodBuilder");
                }

                try
                {
                    price = Decimal.Parse(values[2]);
                }
                catch
                {
                    throw new Exception("Error on parsing field Price on " + iterator.ToString() + "string -- GoodBuilder");
                }

                try
                {
                    categoryId = Int32.Parse(values[3]);

                    if (categoryId < 1)
                    {
                        throw new Exception("Error on parsing field CategoryId on " + iterator.ToString() + "string.CategoryId must be greater than 0 -- GoodBuilder");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error on parsing field CategoryId on " + iterator.ToString() + "string -- GoodBuilder\n" + ex.Message);
                }

                try
                {
                    producerId = Int32.Parse(values[4]);

                    if (producerId < 1)
                    {
                        throw new Exception("Error on parsing field ProducerId on " + iterator.ToString() + "string.ProducerId must be greater than 0 -- GoodBuilder");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error on parsing field ProducerId on " + iterator.ToString() + "string -- GoodBuilder\n" + ex.Message);
                }

                resultList.Add(new Good(id, name, price,
                                        new Category() { Id = categoryId },
                                        new Producer() { Id = producerId }
                               ));
                iterator++;
            }
            return resultList;
        }

        static public void WriteToCsv(List<Good> list, string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine("Id;Name;Price;CategoryId;ProducerId");
                    foreach (var good in list)
                    {
                        sw.WriteLine(good.Id.ToString()          + _delimeter +
                                     good.Name                   + _delimeter +
                                     good.Price.ToString()       + _delimeter +
                                     good.Category.Id.ToString() + _delimeter +
                                     good.Producer.Id.ToString() + _delimeter);
                    }


                }
            }
        }
    }
}
