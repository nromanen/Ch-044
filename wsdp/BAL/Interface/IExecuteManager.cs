using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DB;
using Model.DTO;

namespace BAL.Interface {
	public interface IExecuteManager {
		void Insert(ExecutingInfoDTO info);

		void Update(ExecutingInfoDTO info);

		IEnumerable<ExecutingInfoDTO> GetAll();

		ExecutingInfoDTO GetById(int id);

		void Delete(ExecutingInfoDTO info);

		void DeleteAll();
	}
}
