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

            try
            {
                parsertaskDb.Category = uOW.CategoryRepo.GetByID(parsertaskDb.CategoryId);
                parsertaskDb.WebShop = uOW.WebShopRepo.GetByID(parsertaskDb.WebShopId);

                uOW.ParserRepo.Insert(parsertaskDb);
                uOW.Save();
                return parsertaskDb.Id;
            }
            catch(Exception ex)
            {
                logger.Error(ex.Message);
                return -1;
            }

        }

        /// <summary>
        /// Deletes parser task by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            try
            {
                var parsertask = uOW.ParserRepo.GetByID(id);
                if (parsertask == null)
                {
                    return false;
                }
                uOW.ParserRepo.Delete(parsertask);
                uOW.Save();
                return true;
            }
            catch(Exception ex)
            {
                logger.Error(ex.Message);
            }
            return false;
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
            if (parsertask.GrabberSettings != null)
            {
                temp.GrabberSettings = serializer.Serialize(parsertask.GrabberSettings);
            }
            if (parsertask.IteratorSettings != null)
            {
                temp.IteratorSettings = serializer.Serialize(parsertask.IteratorSettings);
            }

            temp.Priority = parsertask.Priority;
            temp.Status = parsertask.Status;
            temp.Description = parsertask.Description;
            uOW.Save();
            return Mapper.Map<ParserTaskDTO>(temp);
        }

        /// <summary>
        /// Returns all parser tasks
        /// </summary>
        /// <returns></returns>
        public List<ParserTaskDTO> GetAll()
        {
            return uOW.ParserRepo.All.ToList().Select(c => Mapper.Map<ParserTaskDTO>(c)).ToList();
        }
    }
}