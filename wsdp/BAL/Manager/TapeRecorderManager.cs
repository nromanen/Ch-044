using AutoMapper;
using BAL.Interface;
using Common.Enum;
using DAL.Interface;
using ExtendedXmlSerialization;
using Model.DB;
using Model.DTO;
using Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BAL.Manager
{
    public class TapeRecorderManager : BaseManager, ITapeRecorderManager
    {
        public TapeRecorderManager(IUnitOfWork uOW) : base(uOW)
        {
        }

        public List<TapeRecorderDTO> GetAll()
        {
            List<TapeRecorderDTO> microwaves = new List<TapeRecorderDTO>();
            ExtendedXmlSerializer serializer = new ExtendedXmlSerializer();
            foreach (var MicrowaveDb in uOW.GoodRepo.All.Where(g => g.Category == GoodCategory.Microwave))
            {
                TapeRecorder wave = serializer.Deserialize(MicrowaveDb.XmlData, typeof(TapeRecorder)) as TapeRecorder;
                wave.Id = MicrowaveDb.Id;
                microwaves.Add(Mapper.Map<TapeRecorderDTO>(wave));
            }
            return microwaves;
        }

        public TapeRecorder GetById(int id)
        {
            Good good = null;
            ExtendedXmlSerializer serializer = new ExtendedXmlSerializer();
            try
            {
                good = uOW.GoodRepo.GetByID(id);
                if (good == null)
                    throw new Exception("Not Found");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            TapeRecorder wave = serializer.Deserialize(good.XmlData, typeof(TapeRecorder)) as TapeRecorder;
            wave.Id = id;
            return wave;
        }
    }
}