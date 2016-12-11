using AutoMapper;
using BAL.Interface;
using DAL.Interface;
using Model.DB;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Manager
{
    public class AppSettingsManager : BaseManager, IAppSettingsManager
    {
        public AppSettingsManager(IUnitOfWork uOW) : base(uOW)
        {

        }
        public void Insert(AppSettingsDTO model)
        {
            if (model.PullInterval == 0)
            {
                model.PullInterval = 1;
            }
            if(model.PushInterval==0)
            {
                model.PushInterval = 10;
            }

            uOW.AppSettingRepo.Insert(Mapper.Map<AppSetting>(model));
            uOW.Save();
        }

        public void Update(AppSettingsDTO model)
        {
            AppSetting appSetting = uOW.AppSettingRepo.Get().FirstOrDefault();
            if(appSetting==null)
            {
                this.Insert(model);
            }
            else
            {
                appSetting.PullInterval = model.PullInterval;
                appSetting.PushInterval = model.PushInterval;
                uOW.Save();
            }
        }
        public AppSetting Get()
        {
            var model = uOW.AppSettingRepo.Get().FirstOrDefault();
            if(model==null)
            {
                return new AppSetting();
            }
            return model;
        }
    }
}
