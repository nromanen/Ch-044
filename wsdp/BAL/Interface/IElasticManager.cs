﻿using System.Collections.Generic;
using Model.DTO;

namespace BAL.Interface
{
    public interface IElasticManager
    {
        IList<GoodDTO> GetAll();
        GoodDTO GetByUrl(string url);
        void Update(GoodDTO good);
        void Delete(GoodDTO good);
        void Insert(GoodDTO good);
        IList<GoodDTO> GetByName(string name);
        IList<GoodDTO> Get(string value);
        IList<GoodDTO> GetByCategoryId(string category);
    }
}
