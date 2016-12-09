using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BAL.Interface;
using Common.Enum;
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

        public void DeleteByStatus(ExecuteStatus status)
        {
            foreach (var item in uOW.ExecuteRepo.All.Where(c => c.Status == ExecuteStatus.Executing).ToList())
            {
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

		public int Insert(ExecutingInfoDTO info) {
            lock (uOW)
            {
                if (info == null) return -1;
                //var ExInfo = Mapper.Map<ExecutingInfo>(info);
                var TaskToAdd = new ExecutingInfo()
                {
                    Date = info.Date,
                    GoodUrl = info.GoodUrl,
                    ParserTaskId = info.ParserTaskId,
                    Status = info.Status
                };

                uOW.ExecuteRepo.Insert(TaskToAdd);
                uOW.Save();
                return TaskToAdd.Id;
            }
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
