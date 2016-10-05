using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task2.Implementations
{
	public class Csv_GoodReader : IFileManager<List<Good>>
	{
		public List<Good> ReadFromFile(string path)
		{
			List<Good> Goods = new List<Good>();

			var lines = File.ReadAllLines(path);
			int counter = 0;

			foreach (var line in lines.Skip(1))
			{
				counter++;
				var good = new Good();
				good.Category = new Category();
				good.Producer = new Producer();

				char separator = ';';
				string[] items = line.Split(separator);

				//check if needed col in line
				if (line.Count(s => s == separator) == 4)
				{
					//check if goodId is valid
					int _id;
					if (int.TryParse(items[0], out _id))
					{
						if (!(Goods.Any(i => i.Id == _id)))
						{
							good.Id = _id;

							//check if name is valid
							string name = items[1];
							if (!(String.IsNullOrEmpty(name)) && char.IsUpper(name[0]))
							{
								good.Name = name;

								decimal price;
								if (decimal.TryParse(items[2], out price))
								{
									good.Price = price;

								}
								else
								{
									Console.WriteLine("The Price field in Goods on {0} line not valid", counter);
								}

								//check if category_id is valid
								int category_id;
								if (int.TryParse(items[3], out category_id))
								{
									good.Category.Id = category_id;

									//check if producer_id is valid
									int producer_id;
									if (int.TryParse(items[4], out producer_id))
									{
										good.Producer.Id = producer_id;

										if (good.Id != 0)
										{
											Goods.Add(good);
										}
									}

									else
									{
										Console.WriteLine("Element of 'ProducerId' in Goods on {0} line not valid", counter);
									}
								}

								else
								{
									Console.WriteLine("Element of 'CategoryId' in Goods  on {0} line not valid", counter);
								}
							}
							else
							{
								Console.WriteLine("The Name field in Goods is empty on {0} line.", counter);
							}
						}
						else
						{
							Console.WriteLine("The id in Goods is exist.");
						}
					}
					else
					{
						Console.WriteLine("Element of 'Id' in Goods on {0} line not valid", counter);

					}
				}
			}
			return Goods;
		}





		public void WriteToFile(string path, List<Good> Goods)
		{
			using (var sr = new StreamWriter(path))
			{
				sr.WriteLine("Id;Name;Price;CategoryId;ProducerId");
				foreach (var good in Goods)
				{
					sr.WriteLine(good.Id + ";" + good.Name + ";" + good.Price + ";" + good.Category.Id + ";" + good.Producer.Id);
				}
			}
		}
	}
}
