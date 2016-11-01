using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BAL.Interface;
using Common.Enum;
using DAL.Interface;
using ExtendedXmlSerialization;
using log4net.Repository.Hierarchy;
using Model.DB;
using Model.DTO;
using Model.Product;

namespace BAL.Manager {
	public class MicrowaveManager : BaseManager, IMicrowaveManager {
		
		public MicrowaveManager(IUnitOfWork uOW) : base(uOW) {}
	
		public List<MicrowaveDTO> GetAll() {
			List<MicrowaveDTO> microwaves = new List<MicrowaveDTO>();
			ExtendedXmlSerializer serializer = new ExtendedXmlSerializer();
			foreach (var MicrowaveDb in uOW.GoodRepo.All.Where(g => g.Category == GoodCategory.Microwave)) {
				Microwave wave = serializer.Deserialize(MicrowaveDb.XmlData, typeof(Microwave)) as Microwave;
				wave.Id = MicrowaveDb.Id;
				microwaves.Add(Mapper.Map<MicrowaveDTO>(wave));
			}
			return microwaves;
		}

		public Microwave GetById(int id) {
			Good good = null;
			ExtendedXmlSerializer serializer = new ExtendedXmlSerializer();
			try {
				good = uOW.GoodRepo.GetByID(id);
				if (good == null)
					throw new Exception("Not Found");
			} catch (Exception ex) {
				logger.Error(ex.Message);
			}

			Microwave wave = serializer.Deserialize(good.XmlData, typeof(Microwave)) as Microwave;
			wave.Id = id;
			return wave;
		}
	}
}
