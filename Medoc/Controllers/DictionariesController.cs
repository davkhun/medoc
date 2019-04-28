using Medoc.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedocDto;
using MedocData;

namespace Medoc.Controllers
{
    public class DictionariesController : BaseController
    {
        // GET: Dictionaries
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Get(DictionaryContext.DictionaryType dictType, bool showAll)
        {
            var result = showAll ? _dictionaryContext.Get(dictType) : _dictionaryContext.Get(dictType).Where(x => x.Active);
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetItem(DictionaryContext.DictionaryType dictType, int id)
        {
            var result = _dictionaryContext.Get(dictType).First(x => x.Id == id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public void Update(DictionaryContext.DictionaryType dictType, DictionaryModel model)
        {
            _dictionaryContext.Update(dictType, model);
        }
    }
}