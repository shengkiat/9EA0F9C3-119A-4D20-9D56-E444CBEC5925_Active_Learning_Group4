﻿using ActiveLearning.Business.Implementation;
using ActiveLearning.Business.Interface;
using ActiveLearning.DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActiveLearning.Web.Controllers
{
    public class UploadController : BaseController
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string guid = Guid.NewGuid().ToString();

                    var fileName = Path.GetFileName(file.FileName);
                    Content fileDetail = new Content()
                    {
                        //TODO set the courseSID
                        CourseSid = 2,
                        Path = "~/App_Data/Upload/",
                        OriginalFileName = fileName,
                        FileName = guid + Path.GetExtension(fileName),
                        Type = "",
                        CreateDT = DateTime.Now
                    };

                    // Write to Database
                    string message = string.Empty;
                    IContentManager fileManager = new ContentManager();
                    fileManager.AddContent(fileDetail, 1, out message);


                    var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), guid + Path.GetExtension(fileName));
                    file.SaveAs(path);
                }
                ViewBag.Message = "Upload successful";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Upload failed. " + ex.Message;
                return View();
            }
        }
    }
}