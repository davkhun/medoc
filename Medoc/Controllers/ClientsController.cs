using Medoc.App_Start;
using MedocDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medoc.Controllers
{
    public class ClientsController : BaseController
    {
        // GET: Clients
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Update(ClientModel model)
        {
            model.UserId = GetUser().Id;
            var result = _clientContext.Update(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetItem(int clientId)
        {
            var result = _clientContext.Get(clientId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get(ClientSearchModel model)
        {
            var result = _clientContext.GetTable(model);
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int clientId)
        {
            _clientContext.Delete(clientId, GetUser().Id);
            return Json("Ok", JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchTypeahead(string name)
        {
            var result = _clientContext.SearchTypeahead(name);
            return Json(result.Select(x => new { id = x.Id, name = x.Name }), JsonRequestBehavior.AllowGet);
        }
    }
}