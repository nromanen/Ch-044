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
	public class CommentManager : BaseManager, ICommentManager {

		public CommentManager(IUnitOfWork uOW) : base(uOW) {
		}

		public IEnumerable<CommentDTO> CheckCommentsDependency(int userId, int goodId) {
			List<CommentDTO> comments = new List<CommentDTO>();
			foreach (var comment in uOW.CommentRepo.All.Where(t => t.GoodId == goodId).OrderByDescending(c => c.Date).ToList()) {
				comments.Add(Mapper.Map<CommentDTO>(comment));
			}
			foreach (var comment in comments) {
				if (comment.UserId == userId)
					comment.CheckComment = true;
			}
			return comments;
		}

		/// <summary>
		/// Delete comment by id
		/// </summary>
		/// <param name="comment"></param>
		/// <returns>Returns true if operation was succesfully and vice versa</returns>
		public bool Delete(CommentDTO comment) {
			if (comment == null)
				return false;
			var temp = uOW.CommentRepo.GetByID(comment.Id);
			if (temp == null)
				return false;
			uOW.CommentRepo.Delete(temp);
			return uOW.Save() > 0 ? true : false;
		}

		/// <summary>
		/// Delete all comments
		/// </summary>
		/// <returns>Returns true if operation was succesfully and vice versa</returns>
		public bool DeleteAll() {
			foreach (var comment in uOW.CommentRepo.All.ToList()) {
				uOW.CommentRepo.Delete(comment);
			}
			return uOW.Save() > 0 ? true : false;

		}

		/// <summary>
		/// Delete all comments by Good id
		/// </summary>
		/// <param name="Good id"></param>
		/// <returns>Returns true if operation was succesfully and vice versa</returns>
		public bool DeleteAllByGoodId(int id) {
			var comments = uOW.CommentRepo.All.Where(t => t.Id == id);
			foreach (var comment in comments) {
				uOW.CommentRepo.Delete(comment);
			}
			return uOW.Save() > 0 ? true : false;
		}

		/// <summary>
		/// Get all comments
		/// </summary>
		/// <returns>Returns IEnumerable of CommentDTO</returns>
		public IEnumerable<CommentDTO> GetAll() {
			List<CommentDTO> comments = new List<CommentDTO>();
			foreach (var comment in uOW.CommentRepo.All.ToList()) {
				comments.Add(Mapper.Map<CommentDTO>(comment));
			}
			return comments;
		}

		public IEnumerable<CommentDTO> GetAllCommentsByGoodId(int goodId) {
			List<CommentDTO> comments = new List<CommentDTO>();
			foreach (var comment in uOW.CommentRepo.All.Where(t => t.GoodId == goodId).ToList()) {
				comments.Add(Mapper.Map<CommentDTO>(comment));
			}
			return comments;
		}

		/// <summary>
		/// Get comment by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Returns CommentDTO entity</returns>
		public CommentDTO GetById(int id) {
			var comment = uOW.CommentRepo.GetByID(id);
			return comment != null ? Mapper.Map<CommentDTO>(comment) : null;
		}

		/// <summary>
		/// Insert comment in table Comments
		/// </summary>
		/// <param name="comment"></param>
		/// <returns>Returns true if operation was succesfully and vice versa</returns>
		public int Insert(CommentDTO comment) {
			if (comment == null)
				return 0;
			var temp = Mapper.Map<Comment>(comment);
			uOW.CommentRepo.Insert(temp);
            uOW.Save();
            return temp.Id;
		}

		/// <summary>
		/// Updates current comment in table Comments	
		/// </summary>
		/// <param name="comment"></param>
		/// <returns>Returns true if operation was succesfully and vice versa</returns>
		public bool Update(CommentDTO comment) {
			if (comment == null)
				return false;
			var temp = uOW.CommentRepo.GetByID(comment.Id);
			if (temp == null)
				return false;
            temp.Description = comment.Description;
			uOW.CommentRepo.Update(temp);
			return uOW.Save() > 0 ? true : false;
		}
	}
}

