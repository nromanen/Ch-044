using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SS_lab1.Model;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using System.Configuration;
using System.Data.SqlClient;
using SS_lab1.SqlManager;
using System.Threading;

namespace SS_lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 1
            //string path = "Goods1.xml";
            ////List<Good> goods = new List<Good>();

            ////XmlFileManager m = new XmlFileManager();
            ////m.GoodsFromXml(path, out goods);

            ////m.GoodsToXML("newGoogs.xml", goods);    

            //List<Sale> sales = QueryFromXml.Sales(path);
            //sales.Distinct();
            //foreach (var item in sales)
            //{
            //    Console.WriteLine(item);
            //}
            #endregion

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //Console.WriteLine(connectionString);


            #region SqlConnection 1
            //SqlConnection connection = new SqlConnection(connectionString);
            //try
            //{

            //    connection.Open();
            //    Console.WriteLine("Подключение открыто");
            //}
            //catch (SqlException ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            //finally
            //{

            //    connection.Close();
            //    Console.WriteLine("Подключение закрыто...");
            //}
            #endregion

            #region 2
            //GoodSqlManager a = new GoodSqlManager();
            //a.ConnectionOpen();
            ////Producer b = new Producer(1, "Samsung", "Korea");
            ////a.Create(new Producer(1, "Huawei", "Taiwan"));
            ////a.Create(new Producer(2, "Intel", "USA"));
            ////a.Create(new Producer(3, "Lumia", "USA"));


            ////List<Good> res = a.ReadAll();
            ////foreach (var item in res)
            ////{
            ////    Console.WriteLine(item);
            ////}

            //Good res = a.Read(1);
            //Console.WriteLine(res);

            //a.ConnectionClose();
            //Console.Read();
            #endregion


            //CategorySqlManager a = new CategorySqlManager();
            //a.ConnectionOpen();

            //a.CustomDelete(new Category(8, "Phone"));


            //a.ConnectionClose();

            List<Folder> list = XmlSize(Directory.GetDirectories(@"E:\New folder"));
            foreach (var item in list)
            {
                Console.WriteLine(item.folderPath + "\t" + item.commonXmlSize);
            }
          
            Console.Read();
            

        }

        public static List<Folder> XmlSize(params string[] folderPath)
        {
            List<Folder> folderList = new List<Folder>();
            for (int i = 0; i < folderPath.Length; i++)
            {
                Folder folder = new Folder { folderPath = folderPath[i] };
                Thread myThread = new Thread(new ParameterizedThreadStart(folder.Count));
                myThread.Start(i);
                folderList.Add(folder);
            }
            return folderList;
        }

        

        




    }
}
