﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DB;
using Model.DTO;
using Model.Product;

namespace BAL.Interface {
	public interface ITapeRecorderManager {
		List<TapeRecorderDTO> GetAll();
		TapeRecorder GetById(int id);
	}
}