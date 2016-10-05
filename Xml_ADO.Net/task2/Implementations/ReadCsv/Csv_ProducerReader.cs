using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace task2.Implementations
{
	public class Csv_ProducerReader : IFileManager<List<Producer>>
	{
		public List<Producer> ReadFromFile(string path)
		{
			List<Producer> Producers = new List<Producer>();
			var lines = File.ReadAllLines(path);
			int counter = 0;

			foreach (var line in lines.Skip(1))
			{
				counter++;
				Producer producer = new Producer();

				char separator = ';';
				string[] items = line.Split(separator);

				if (line.Count(s => s == separator) == 2)
				{
					int _id;
					if (int.TryParse(items[0], out _id))
					{
						//if (!(Producers.Select(i => i.Id).Contains(_id)))
						if (!(Producers.Any(i => i.Id == _id)))
						{
							producer.Id = _id;


							string name = items[1];
							if (!String.IsNullOrWhiteSpace(name))
							{
								producer.Name = name;

								string country = items[2];
								if (!(String.IsNullOrWhiteSpace(country)) && char.IsUpper(country[0]))
								{
									if (!(items[2].Where(x => Char.IsDigit(x)).Any()))
									{
										producer.Country = items[2];

										if (producer.Id != 0)
										{
											Producers.Add(producer);
										}
									}
									else
									{
										Console.WriteLine("The field'Country' on {0} line have numbers.", counter);
									}
								}
								else
								{
									Console.WriteLine("The field 'Country' on {0} line not valid.", counter);
								}
							}
							else
							{
								Console.WriteLine("The id in Producers is exist.");
							}
						}
						else
						{
							Console.WriteLine("Element of 'Id' in Producers on {0} line not valid", counter);

						}
					}
					else
					{
						Console.WriteLine("The Country field in Producers on {0} line not valid.", counter);
					}
				}
			}
			return Producers;
		}


		public void WriteToFile(string path, List<Producer> Producers)
		{
			using (var sr = new StreamWriter(path))
			{
				sr.WriteLine("Id;Name;Country");
				foreach (var prod in Producers)
				{
					sr.WriteLine(prod.Id + ";" + prod.Name + ";" + prod.Country);
				}
			}
		}
	}
}
