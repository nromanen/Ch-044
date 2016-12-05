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
			if (info == null)
				return;
			var ExInfo = uOW.ExecuteRepo.GetByID(info.Id);
			if (ExInfo == null)
				return;
			uOW.ExecuteRepo.Delete(ExInfo);
			uOW.Save();
		}

		public void DeleteAll() {
			foreach (var item in uOW.ExecuteRepo.All.ToList()) {
				uOW.ExecuteRepo.Delete(item);
			}
			uOW.Save();
		}

		public IEnumerable<ExecutingInfoDTO> GetAll() {
			List<ExecutingInfoDTO> executingsInfo = new List<ExecutingInfoDTO>();
			foreach (var info in uOW.ExecuteRepo.All.ToList()) {
				executingsInfo.Add(Mapper.Map<ExecutingInfoDTO>(info));
			}
			return executingsInfo;
		}

		public ExecutingInfoDTO GetById(int id) {
			var info = uOW.ExecuteRepo.GetByID(id);
			return info != null ? Mapper.Map<ExecutingInfoDTO>(info) : null;
		}

		public void Insert(ExecutingInfoDTO info) {
			if (info == null) return;
			var ExInfo = Mapper.Map<ExecutingInfo>(info);
			uOW.ExecuteRepo.Insert(ExInfo);
			uOW.Save();
		}

		public void Update(ExecutingInfoDTO info) {
			if (info == null)
				return;
			var ExInfo = uOW.ExecuteRepo.GetByID(info.Id);
			if (ExInfo == null)
				return;
			ExInfo.Status = info.Status;
			ExInfo.ParserTask = uOW.ParserRepo.GetByID(info.ParserTaskId);
			ExInfo.GoodUrl = info.GoodUrl;
			ExInfo.ErrorMessage = info.ErrorMessage;
			uOW.Save();
		}
	}
}
