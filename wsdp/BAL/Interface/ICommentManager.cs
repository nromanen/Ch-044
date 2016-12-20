using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DTO;

namespace BAL.Interface {
	public interface ICommentManager {
		bool Insert(CommentDTO webShop);

		bool Update(CommentDTO webShop);

		IEnumerable<CommentDTO> GetAll();

		CommentDTO GetById(int id);

		bool Delete(CommentDTO webShop);

		bool DeleteAllByGoodId(int id);

		IEnumerable<CommentDTO> GetAllCommentsByGoodId(int goodId);

		IEnumerable<CommentDTO> CheckCommentsDependency(int userId, int goodId);
	}
}
