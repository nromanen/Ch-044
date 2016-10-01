using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Configuration;

namespace FirstTask
{

    class ProducersBuilder : IFileManager<List<Producer>>
    {      
        public List<Producer> ReadFromCsv(string path)
        {
            return CsvBuilderForProducers.ReadFromCsv(path);
        }
        public void WriteToCsv(List<Producer> list, string path)
        {
            CsvBuilderForProducers.WriteToCsv(list, path);
        }
        public void WriteToXml(List<Producer> list, string path)
        {
            XmlBuilderForProducers.WriteToXml(list, path);
        }
        
        public List<Producer> ReadFromXml(string path)
        {
            return XmlBuilderForProducers.ReadFromXml(path);
        } 
    }

    class CategoryBuilder : IFileManager<List<Category>>
    {
        public List<Category> ReadFromCsv(string path)
        {
            return CsvBuilderForCategories.ReadFromCsv(path);
        }
        public void WriteToCsv(List<Category> list, string path)
        {
            CsvBuilderForCategories.WriteToCsv(list, path);
        }
        public void WriteToXml(List<Category> list, string path)
        {
            XmlBuilderForCategories.WriteToXml(list, path);
        }
        public List<Category> ReadFromXml(string path)
        {
            return XmlBuilderForCategories.ReadFromXml(path);
        }

    }

    class GoodsBuilder : IFileManager<List<Good>>
    {
        public List<Good> ReadFromCsv(string path)
        {
            return CsvBuilderForGoods.ReadFromCsv(path);
        }
        public void WriteToCsv(List<Good> list, string path)
        {
            CsvBuilderForGoods.WriteToCsv(list, path);
        }

        public void WriteToXml(List<Good> list, string path)
        {
            var MyReader = new System.Configuration.AppSettingsReader();
            string IsLinqWriteKey = MyReader.GetValue("IsLinqWrite", typeof(string)).ToString();
            if (IsLinqWriteKey == "false")
                XmlBuilderForGoods.WriteToXmlNotLinq(list, path);
            else
                XmlBuilderForGoods.WriteToXmlLinq(list, path);
        }

        public List<Good> ReadFromXml(string path)
        {
            var MyReader = new System.Configuration.AppSettingsReader();
            string IsLinqWriteKey = MyReader.GetValue("IsLinqWrite", typeof(string)).ToString();
            if (IsLinqWriteKey == "false")
                return XmlBuilderForGoods.ReadFromXmlNotLinq(path);
            else
                return XmlBuilderForGoods.ReadFromXmlLinq(path);
        }


    }




    static class FactoryOfBuilders
    {
        static public ProducersBuilder CreateProducersBuilder()
        {
            return new ProducersBuilder();
        }

        static public CategoryBuilder CreateCategoriesBuilder()
        {
            return new CategoryBuilder();
        }

        static public GoodsBuilder CreateGoodsBuilder()
        {
            return new GoodsBuilder();
        }
    }
}
