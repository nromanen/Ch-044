using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task2
{
	public interface IFileManager<T> where T: IEnumerable
	{
		T ReadFromFile(string path);
		void WriteToFile(string path, T Entities);

	}
}
