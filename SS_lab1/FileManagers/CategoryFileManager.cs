using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SS_lab1.Model;
using System.Text.RegularExpressions;

namespace SS_lab1
{
    //delegate IEnumerable<string> TemplateValidator(string sourse, out IEnumerable<string> err);
    class CategoryFileManager
    {

        private bool Validator(string source, out List<string> err)
        {
            List<string> errors = new List<string>();
            string[] line = source.Split(';');
            bool res = true;

            if (line.Length == 2)
            {
                int id;
                if (!Int32.TryParse(line[0], out id))
                {
                    errors.Add("Unсorrect formant of id");
                    res = false;
                }
                if (id < 1)
                {
                    errors.Add("Id is to small");
                    res = false;
                }

                if (!(new Regex("^[а-яА-ЯёЁa-zA-Z]+$").IsMatch(line[1])))
                {
                    errors.Add("Unсorrect name");
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


        public List<Category> ReadFile(string path)
        {
            List<string> list = File.ReadAllLines(path).ToList<string>();
            list.RemoveAt(0);
            List<Category> res = new List<Category>();
            int counter = 1;

            foreach (var item in list)
            {
                List<string> errors = new List<string>();
                if (!Validator(item, out errors))
                {
                    Console.WriteLine("on " + counter + " line: ");
                    foreach (var item1 in errors)
                    {
                        Console.WriteLine(item1);                                       
                    }
                } 
                else
                {
                    string[] prop = item.Split(';');
                    res.Add(new Category(Int32.Parse(prop[0]), prop[1]));
                }
                counter++;                              
            }
            return res;     

            
            //    list.Select(item => item.Split(';'))
            //    .Select(prod => new Category(Int32.Parse(prod[0]), prod[1]))
            //    .ToList();
            //return res;
        }
        public void WriteFile(string path, List<Category> categories)
        {
            StreamWriter file = new StreamWriter(path);
            StringBuilder res = new StringBuilder();
            
            foreach (var item in categories)
            {
                res.Append(item.Id);
                res.Append(";");
                res.Append(item.Name);
                file.WriteLine(res.ToString());
                res.Clear();
            }

            file.Close();                                                            
        }


    }
}
