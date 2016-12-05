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

        /// <summary>
        /// get parser task by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
				parsertaskDb.Status = Common.Enum.Status.NotFinished;
                parsertaskDb.LastChange = DateTime.Now;
				uOW.ParserRepo.Insert(parsertaskDb);
				uOW.Save();
				return parsertaskDb.Id;
			}
			catch (Exception ex)
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
			catch (Exception ex)
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
			//temp vars for checking fillness additional settings
			bool IsIteratorSettingsAreFilled = false;
			bool IsGrabebrSettingsAreFilled = false;


			//Filling and checking additional settings
			if (parsertask.GrabberSettings != null)
			{
				temp.GrabberSettings = serializer.Serialize(parsertask.GrabberSettings);
				IsIteratorSettingsAreFilled = true;
			}
			if (parsertask.IteratorSettings != null)
			{
				temp.IteratorSettings = serializer.Serialize(parsertask.IteratorSettings);
				IsGrabebrSettingsAreFilled = true;
			}

			if (IsGrabebrSettingsAreFilled && IsIteratorSettingsAreFilled)
			{
				if (parsertask.EndDate == null)
					temp.Status = Common.Enum.Status.Infinite;
				else
					temp.Status = Common.Enum.Status.Coming;
			}
			else
			{
				temp.Status = Common.Enum.Status.NotFinished;
			}

            if (parsertask.Status == Common.Enum.Status.InQuery)
            {
                temp.Status = Common.Enum.Status.InQuery;
            }

			temp.Priority = parsertask.Priority;

			temp.Description = parsertask.Description;
			temp.EndDate = parsertask.EndDate;

			temp.CategoryId = parsertask.CategoryId;
			temp.WebShopId = parsertask.WebShopId;

			temp.Category = uOW.CategoryRepo.GetByID(temp.CategoryId);
			temp.WebShop = uOW.WebShopRepo.GetByID(temp.WebShopId);

			uOW.ParserRepo.SetStateModified(temp);
			uOW.Save();
			return Mapper.Map<ParserTaskDTO>(temp);
		}

		/// <summary>
		/// Returns all parser tasks
		/// </summary>
		/// <returns></returns>
		public List<ParserTaskDTO> GetAll()
		{
			List<ParserTaskDTO> resultList = new List<ParserTaskDTO>();
			foreach (var parsertask in uOW.ParserRepo.All.ToList())
			{
				resultList.Add(Mapper.Map<ParserTaskDTO>(parsertask));
			}
			return resultList;
		}
	}
}