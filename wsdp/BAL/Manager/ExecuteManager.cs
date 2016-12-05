using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BAL.Interface;
using DAL.Interface;
using Model.DB;
using Model.DTO;

namespace BAL.Manager {
	public class ExecuteManager : BaseManager, IExecuteManager {

		public ExecuteManager(IUnitOfWork uOW) : base(uOW) {

		}
		public void Delete(ExecutingInfoDTO info) {
			throw new NotImplementedException();
		}

		public void DeleteAll() {
			throw new NotImplementedException();
		}

		public IEnumerable<ExecutingInfoDTO> GetAll() {
			//List<ExecutingInfoDTO> executingsInfo = new List<ExecutingInfoDTO>();
			//foreach (var webShop in uOW.WebShopRepo.All.Where(x => x.Status)) {
			//	webShopDto.Add(Mapper.Map<WebShopDTO>(webShop));
			//}
			//return webShopDto;
			return null;
		}

		public ExecutingInfoDTO GetById(int id) {
			throw new NotImplementedException();
		}

		public void Insert(ExecutingInfoDTO info) {
			if (info == null) return;
			var ExInfo = Mapper.Map<ExecutingInfo>(info);
			uOW.ExecuteRepo.Insert(ExInfo);
			uOW.Save();
		}

		public void Update(ExecutingInfoDTO info) {
			throw new NotImplementedException();
		}
	}
}
