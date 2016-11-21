﻿using AutoMapper;
using BAL.Interface;
using DAL.Interface;
using Model.DB;
using Model.DTO;
using System.Collections.Generic;
using System.Linq;

namespace BAL.Manager
{
    public class GoodManager : BaseManager, IGoodManager
    {
        public GoodManager(IUnitOfWork uOW) : base(uOW)
        {
        }

        public void InsertGood(GoodDTO good)
        {
            uOW.GoodRepo.Insert(Mapper.Map<Good>(good));
            uOW.Save();
        }

        public void InsertGood(Good good)
        {
            uOW.GoodRepo.Insert(good);
            uOW.Save();
        }

        public List<GoodDTO> GetAll()
        {
            var goodList = new List<GoodDTO>();
            foreach (var good in uOW.GoodRepo.All.ToList())
            {
                var good_temp = uOW.GoodRepo.GetByID(good.Id);
                goodList.Add(Mapper.Map<GoodDTO>(good_temp));
            }
            return goodList;
        }
    }
}