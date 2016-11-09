using Model.DTO;
using Model.Product;
using System.Collections.Generic;

namespace BAL.Interface
{
    public interface ITapeRecorderManager
    {
        List<TapeRecorderDTO> GetAll();

        TapeRecorder GetById(int id);
    }
}