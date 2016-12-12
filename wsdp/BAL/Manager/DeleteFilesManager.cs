using BAL.Interface;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Manager
{
    public class DeleteFilesManager : BaseManager, IDeleteFilesManager
    {
        public DeleteFilesManager(IUnitOfWork uOW) : base(uOW)
        {
         
        }
        /// <summary>
        /// Delete files from WebSites folder
        /// </summary>
        public void DeleteFiles()
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "WebSites";

                string[] files = Directory.GetFiles(path, "*.html");
                foreach (var item in files)
                {
                    File.Delete(item);
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex.Message);
            }


        }
    }
}
