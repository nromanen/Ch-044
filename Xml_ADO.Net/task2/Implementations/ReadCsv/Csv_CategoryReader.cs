using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace task2.Implementations
{
	public class Csv_CategoryReader : IFileManager<List<Category>>
	{
		public List<Category> ReadFromFile(string path)
		{
			List<Category> Categories = new List<Category>();

			var lines = File.ReadAllLines(path);
			int counter = 0;
			foreach (var line in lines.Skip(1))
			{
				Category category = new Category();
				counter++;
				char separator = ';';
				string[] items = line.Split(separator);

				if (line.Count(s => s == separator) == 1)
				{
					int _id;
					if (int.TryParse(items[0], out _id))
					{
						//if (!(Categories.Select(i => i.Id).Contains(_id)))
						if (!(Categories.Any(i => i.Id == _id)))
						{

							category.Id = _id;

							string name = items[1];

							if (!(String.IsNullOrWhiteSpace(name)) && char.IsUpper(name[0]))
							{
								category.Name = name;

								if (category.Id != 0)
								{
									Categories.Add(category);
								}
							}
							else
							{
								Console.WriteLine("The Name field in Categories on {0} line not valid.", counter);
							}

						}
						else
						{
							Console.WriteLine("The Id in Categories on {0} is exist.", counter);
						}
					}
					else
					{
						Console.WriteLine("Element of 'Id' in Categories on {0} line not valid", counter);

					}
				}

				else
				{
					Console.WriteLine("Error on the {0} line", counter);
				}

			}
			return Categories;
		}


		public void WriteToFile(string path, List<Category> Categories)
		{
			using (var sr = new StreamWriter(path))
			{
				sr.WriteLine("Id;Name");
				foreach (var cat in Categories)
				{
					sr.WriteLine(cat.Id + ";" + cat.Name);
				}
			}
		}
	}
}
