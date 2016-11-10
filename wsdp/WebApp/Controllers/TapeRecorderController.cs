using BAL.Interface;
using Model.DTO;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    [Authorize]
    public class TapeRecorderController : BaseController
    {
        // GET: Tape Recorder
        private ITapeRecorderManager TapeRecorderManager;

        private ITapeRecorderParseManager TapeRecorderParseManager;

        public TapeRecorderController(ITapeRecorderManager TapeRecorderManager, ITapeRecorderParseManager TapeRecorderParseManager)
        {
            this.TapeRecorderParseManager = TapeRecorderParseManager;
            this.TapeRecorderManager = TapeRecorderManager;
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult All()
        {
            List<TapeRecorderDTO> tapeRecorders = TapeRecorderManager.GetAll();
            return View(tapeRecorders);
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult GetTapeRecorder(int id)
        {
            var tapeRecorder = TapeRecorderManager.GetById(id);
            return View(tapeRecorder);
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult Load()
        {
            TapeRecorderParseManager.GetAllWaves("http://pulsepad.com.ua/catalog/g4619581-avtomagnitoly?");
            return RedirectToAction("All");
        }
    }
}