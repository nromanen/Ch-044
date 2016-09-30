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
    class ProducerFileManager 
    {
        private bool Validator(string source, out List<string> err)
        {
            List<string> errors = new List<string>();
            string[] line = source.Split(';');
            bool res = true;

            if (line.Length == 3)
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

                if (/*!(new Regex("^[а-яА-ЯёЁa-zA-Z]+$").IsMatch(line[1])*/ !new Regex(@"^[A-Z][a-zA-Z]+").IsMatch(line[1]))
                {
                    errors.Add("Unсorrect name");
                    res = false;
                }
                if (!(new Regex("^[а-яА-ЯёЁa-zA-Z]+$").IsMatch(line[2]) && new Regex(@"^[A-Z]").IsMatch(line[2])))
                {
                    errors.Add("Unсorrect country");
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
        public List<Producer> ReadFile(string path)
        {
            List<string> list = File.ReadAllLines(path).ToList<string>();
            list.RemoveAt(0);
            List<Producer> res = new List<Producer>();
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
                    res.Add(new Producer(Int32.Parse(prop[0]), prop[1], prop[2]));
                }
                counter++;
            }
            return res;


            #region 2
            //List<string> list = File.ReadAllLines(path).ToList<string>();
            //list.RemoveAt(0);
            //List<Producer> res = 
            //    list.Select(item => item.Split(';'))
            //    .Select(prod => new Producer(Int32.Parse(prod[0]), prod[1], prod[2]))
            //    .ToList();
            //return res;
            #endregion
            #region otherWay
            //List<string> list = File.ReadAllLines(path).ToList<string>();
            //List<Producer> res = new List<Producer>();

            //foreach (var item in list)
            //{
            //    string[] prod = item.Split(';');
            //    res.Add(new Producer() { Id = prod[0], Name = prod[1], Country = prod[2] });
            //}

            //return res;
            #endregion
        }

        public void WriteFile(string path, List<Producer> producers)
        {
            StreamWriter file = new StreamWriter(path);
            StringBuilder res = new StringBuilder();

            foreach (var item in producers)
            {
                res.Append(item.Id);
                res.Append(";");
                res.Append(item.Name);
                res.Append(";");
                res.Append(item.Country);
                file.WriteLine(res.ToString());
                res.Clear();
            }

            file.Close();
        }
    }
}
