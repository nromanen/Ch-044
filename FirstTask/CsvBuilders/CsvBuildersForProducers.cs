using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTask
{
    class CsvBuilderForProducers
    {
        static private char _delimeter = ';';

        public static void ChangeDelimeter(char del)
        {
            _delimeter = del;
        }

        public static List<Producer> ReadFromCsv(string path)
        {
            var reader = new StreamReader(File.OpenRead(path));
            List<Producer> resultList = new List<Producer>();

            reader.ReadLine();
            int iterator = 1;
            while (!reader.EndOfStream)
            {
                int id;
                string name;
                string country;
                var line = reader.ReadLine();
                var values = line.Split(_delimeter);
                try
                {
                    id = Int32.Parse(values[0]);
                    if (id < 1)
                    {
                        throw new Exception("Error on parsing field ID on " + iterator.ToString() + "string.Id must be greater than 0 -- ProducersBuilder");
                    }

                    var query = from producer in resultList
                                where id == producer.Id
                                select producer;

                    if (query.Count() != 0)
                    {
                        throw new Exception("Error on parsing field ID on " + iterator.ToString() + "string.Id must be unique -- ProducersBuilder");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error on parsing field ID on " + iterator.ToString() + "string.Can't parse number -- ProducersBuilder\n" + ex.Message);
                }

                try
                {
                    name = values[1];
                }
                catch
                {
                    throw new Exception("Error on parsing field Name on " + iterator.ToString() + "string -- ProducersBuilder");
                }

                try
                {
                    country = values[2];
                    if (!char.IsUpper(country[0]))
                    {
                        throw new Exception("Error on parsing field Country on " + iterator.ToString() + "string. First char of field Country must be uppercase -- ProducersBuilder");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error on parsing field Country on " + iterator.ToString() + "string -- ProducersBuilder\n" + ex.Message);
                }


                resultList.Add(new Producer(id, name, country));
                iterator++;
            }
            return resultList;

        }
        
        static public void WriteToCsv(List<Producer> list, string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine("Id;Name;Country");
                    foreach (var producer in list)
                    {
                        sw.WriteLine(producer.Id.ToString() + _delimeter +
                                     producer.Name          + _delimeter +
                                     producer.Country);
                    }


                }
            }
        }
    }
}
