using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using SS_lab1.Model;
using System.Text.RegularExpressions;

namespace SS_lab1
{
    class GoodFileManager
    {
        private bool Validator(string source, out List<string> err)
        {
            List<string> errors = new List<string>();
            string[] line = source.Split(';');
            bool res = true;

            if (line.Length == 5)
            {
                int id;
                if (!Int32.TryParse(line[0], out id))
                {
                    errors.Add("Invalid formant of id");
                    res = false;
                }
                if (id < 1)
                {
                    errors.Add("Id is to small");
                    res = false;
                }

                if (String.IsNullOrWhiteSpace(line[1]))
                {
                    errors.Add("Name is empty");
                    res = false;
                }

                decimal price;
                if (!decimal.TryParse(line[2], out price) && price < 0)
                {
                    errors.Add("Uncorrect price");
                    res = false;
                }

                id = 0;
                if (!Int32.TryParse(line[3], out id))
                {
                    errors.Add("Invalid formant of id_cat");
                    res = false;
                }
                if (id < 1)
                {
                    errors.Add("Id_cat is to small");
                    res = false;
                }

                id = 0;
                if (!Int32.TryParse(line[4], out id))
                {
                    errors.Add("Invalid formant of id_cat");
                    res = false;
                }
                if (id < 1)
                {
                    errors.Add("Id_cat is to small");
                    res = false;
                }
            }
            else
            {
                errors.Add("Unсorrect format of line");
                res = false;
            }

            err = errors;
            return res;
        }



        public List<Good> ReadFile(string path, List<Category> categories, List<Producer> produsers)
        {  
            List<string> list = File.ReadAllLines(path).ToList<string>();
            list.RemoveAt(0);
            List<Good> res = new List<Good>();
            int counter = 1;

            foreach (var item in list)
            {
                List<string> errors = new List<string>();
                if (!Validator(item, out errors))
                {
                    Console.WriteLine("on " + counter + "line: ");
                    foreach (var item1 in errors)
                    {
                        Console.WriteLine(item1);
                    }
                }
                else
                {
                    
                    string[] prop = item.Split(';');
                    if (categories.Exists(x => x.Id == Int32.Parse(prop[3])) && produsers.Exists(x => x.Id == Int32.Parse(prop[4])))
                    {
                        res.Add(new Good(Int32.Parse(prop[0]),
                            prop[1],
                            decimal.Parse(prop[2]),
                            categories.FirstOrDefault(x => x.Id == Int32.Parse(prop[3])),
                            produsers.FirstOrDefault(x => x.Id == Int32.Parse(prop[4]))));
                    }
                    else continue;
                }
                counter++;
            }
            return res;
        }        

        public void WriteFile(string path, List<Good> goods)
        {
            StreamWriter file = new StreamWriter(path);
            StringBuilder res = new StringBuilder();

            foreach (var item in goods)
            {
                res.Append(item.Id);
                res.Append(";");
                res.Append(item.Name);
                res.Append(";");
                res.Append(item.Price);
                res.Append(";");
                res.Append(item.Category.Id);
                res.Append(";");
                res.Append(item.Producer.Id);
                file.WriteLine(res.ToString());
                res.Clear();
            }
            file.Close();            
        }
    }
}
