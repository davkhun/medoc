using Medoc.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedocDto;
using Newtonsoft.Json;
using System.IO;

namespace Medoc.Controllers
{
    public class ContractController : BaseController
    {
        // GET: Harmonization
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetTable(bool showAll)
        {
            var result = _contractContext.GetTable(GetUser().Id, showAll);
            return Json(new { data = result }, JsonRequestBehavior.AllowGet); 
        }

        public JsonResult Update(ContractModel model)
        {
            model.UserId = GetUser().Id;
            var result = _contractContext.Update(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetContract(int contractId)
        {
            var result = _contractContext.GetContract(contractId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public void DeleteContract(int contractId)
        {
            _contractContext.DeleteContract(contractId);
        }

        [HttpPost]
        public JsonResult Upload(int fileType, int contractId)
        {
            var result = new Result();
            try
            {
                var file = Request.Files["file"];
                if (file != null)
                {

                    // Verify that the user selected a file
                    if (file != null && file.ContentLength > 0)
                    {
                        var buffer = new byte[file.InputStream.Length];
                        file.InputStream.Read(buffer, 0, buffer.Length);
                        var base64 = Convert.ToBase64String(buffer);
                        Console.WriteLine(base64);
                        var fileName = Path.GetFileName(file.FileName);
                        var fileModel = new UploadFileModel
                        {
                            ContractId = contractId,
                            UserId = GetUser().Id,
                            FileContent = base64,
                            FileName = fileName,
                            FileType = fileType
                        };
                        result = _fileContext.Upload(fileModel);
                    }
                }
            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.Message = ex.Message;
                throw;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFiles (int contractId)
        {
            var result = _fileContext.Get(contractId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public FileResult DownloadFile(int fileId, string fileName)
        {
            byte[] bt = _fileContext.Download(fileId);
            return File(bt, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}