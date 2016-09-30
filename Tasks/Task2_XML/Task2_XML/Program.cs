using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Xml.Serialization;

namespace Task2_XML {
	
	class Program {
		static void Main(string[] args) {
			var key = ConfigurationManager.AppSettings["ParserFormat"].ToLower();			//Get value from App.config
			string path1 = "Goods1.xml"; 
			string path2 = "Goods2.xml";
			IXmlManager xmlSerializer = new XmlSerializer();				
			IXmlManager xmlLinqSerializer = new XmlSerializerByLINQ();		
			#region Initialize list 1
			List<Good> defaultGoods1 = new List<Good>();			
			Good good1 = new Good() {
				Id = 10,
				Name = "Mon1",
				Price = 2.76m,
				Category = new Category { Id = 1, Name = "Technique" },
				Producer = new Producer { Id = 1, Name = "Sony", Country = "Korea" }

			};
			Good good2 = new Good() {
				Id = 11,
				Name = "Heidy",
				Price = 12.08m,
				Category = new Category { Id = 2, Name = "Toy" },
				Producer = new Producer { Id = 1, Name = "Sony", Country = "Korea" }
			};
			Good good3 = new Good() {
				Id = 12,
				Name = "Monkey",
				Price = 12.76m,
				Category = new Category { Id = 2, Name = "Toy" },
				Producer = new Producer { Id = 2, Name = "NonSony", Country = "Korea" }
			};
			defaultGoods1.Add(good1);	 
			defaultGoods1.Add(good2);	
			defaultGoods1.Add(good3);	
			#endregion 
			#region Initialize list 2
			List<Good> defaultGoods2 = new List<Good>();				
			Good good2_1 = new Good() {
				Id = 1,
				Name = "BMW",
				Price = 22222.76m,
				Category = new Category { Id = 1, Name = "Auto" },
				Producer = new Producer { Id = 3, Name = "Hamann", Country = "Germany" }

			};
			Good good2_2 = new Good() {
				Id = 2,
				Name = "Iphone",
				Price = 1200.08m,
				Category = new Category { Id = 2, Name = "Phone" },
				Producer = new Producer { Id = 4, Name = "Apple", Country = "USA" }
			};
			defaultGoods2.Add(good2_1);				
			defaultGoods2.Add(good2_2);				
			#endregion
			#region KeyValid - true
			if (key == "xmlnolinq") {
				Console.WriteLine("We use XmlSerializer class");        
				xmlSerializer.Serializer("Goods1.xml", defaultGoods1); 	
				xmlSerializer.Serializer("Goods2.xml", defaultGoods2); 	
												   
				var good = xmlSerializer.Deserializer(path1);			
				var peace = xmlSerializer.Deserializer(path2);

				good.AddRange(peace);                                    
				Console.WriteLine("Group by list");						
				Console.WriteLine(new string('-', 30));                 
				DataWriter.GroupByCount(good);							
				Console.WriteLine(new string('-', 30));					
				DataWriter.GroupByPrice(peace);							
				Console.WriteLine(new string('-', 30));					
				Console.WriteLine("Group by Path");						
				Console.WriteLine(new string('-', 30));                 
				DataWriter.GroupByCount(path2);							
				Console.WriteLine(new string('-', 30));					
				DataWriter.GroupByPrice(path2);                         
			}                                                           							  
			#endregion
			#region KeyValid - false
			 if(key == "xmlwithlinq") {
				Console.WriteLine("We use LINQ Serializing / Deserializing");  
				xmlLinqSerializer.Serializer(path1, defaultGoods1);			   
				xmlLinqSerializer.Serializer(path2, defaultGoods2);			   

				var goods = xmlLinqSerializer.Deserializer(path1);             	
				var peace = xmlLinqSerializer.Deserializer(path2);
				
				goods.AddRange(peace);                                  
				Console.WriteLine("Group by list");						
				Console.WriteLine(new string('-', 30));					
				DataWriter.GroupByCount(goods);							
				Console.WriteLine(new string('-', 30));					
				DataWriter.GroupByPrice(goods);							
				Console.WriteLine(new string('-', 30));					
				Console.WriteLine("Group by Path");						
				Console.WriteLine(new string('-', 30));					
				DataWriter.GroupByCount(path2);							
				Console.WriteLine(new string('-', 30));					
				DataWriter.GroupByPrice(path2);							
			}																						  
			#endregion
			Console.ReadKey();
		}
	}
}
