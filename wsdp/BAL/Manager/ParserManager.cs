using AutoMapper;
using BAL.Interface;
using DAL.Interface;
using ExtendedXmlSerialization;
using Model.DB;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BAL.Manager
{
    public class ParserTaskManager : BaseManager, IParserTaskManager
    {
        public ParserTaskManager(IUnitOfWork uOw) : base(uOw)
        {
        }

        public ParserTaskDTO Get(int id)
        {
            var parser = uOW.ParserRepo.GetByID(id);
            if (parser == null)
                return null;
            var result = Mapper.Map<ParserTaskDTO>(parser);

            return result;
        }

        /// <summary>
        /// Add new parsertask
        /// </summary>
        /// <param name="parsertask"></param>
        /// <returns>id of new parser</returns>
        public int Add(ParserTaskDTO parsertask)
        {
            if (parsertask == null) return -1;

            ParserTask parsertaskDb = Mapper.Map<ParserTask>(parsertask);

            parsertaskDb.Category = uOW.CategoryRepo.GetByID(parsertaskDb.CategoryId);
            parsertaskDb.WebShop = uOW.WebShopRepo.GetByID(parsertaskDb.WebShopId);

            uOW.ParserRepo.Insert(parsertaskDb);
            uOW.Save();
            return parsertaskDb.Id;
        }

        public bool Delete(int id)
        {
            /*TO DO*/
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update parser task
        /// </summary>
        /// <param name="parsertask"></param>
        /// <returns>instance of new parser dto</returns>
        public ParserTaskDTO Update(ParserTaskDTO parsertask)
        {
            var serializer = new ExtendedXmlSerializer();

            var temp = uOW.ParserRepo.Get(p => p.Id == parsertask.Id).FirstOrDefault();
            if (temp == null)
            {
                return null;
            }

            uOW.ParserRepo.SetStateModified(temp);
            temp.GrabberSettings = serializer.Serialize(parsertask.GrabberSettings);
            temp.IteratorSettings = serializer.Serialize(parsertask.IteratorSettings);
            temp.Priority = parsertask.Priority;
            temp.Status = parsertask.Status;
            temp.WebShop = uOW.WebShopRepo.GetByID(parsertask.WebShopId);
            temp.Category = uOW.CategoryRepo.GetByID(parsertask.CategoryId);
            return Mapper.Map<ParserTaskDTO>(temp);
        }

        public List<ParserTaskDTO> GetAll()
        {
            /*TO DO*/
            throw new NotImplementedException();
        }
    }
}