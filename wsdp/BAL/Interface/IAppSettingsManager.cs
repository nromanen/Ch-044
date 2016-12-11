using Model.DB;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface
{
    public interface IAppSettingsManager
    {
        void Insert(AppSettingsDTO model);
        void Update(AppSettingsDTO model);
        AppSetting Get();
    }
}
