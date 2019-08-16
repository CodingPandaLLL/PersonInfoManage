using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonInfoManage.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using PersonInfoManage.Utils;
using System.Text;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Drawing;
using System.Net.Http;
using System.Net;

namespace ImagesController.Controllers
{
    public class ImagesController : Controller
    {
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="fileBase"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadImage()
        {
            string path11 = "E:\\codeSpace\\c#\\PersonInfoManage\\PersonInfoManage\\Files\\Images\\SlideConfig\\";
            //接受参数
            HttpPostedFileBase fileBase = Request.Files["image"];
            string imgurl = string.Empty;
            string imgPath = System.IO.Path.GetFileName(fileBase.FileName);
            int index = imgPath.LastIndexOf('.');
            string suffix = imgPath.Substring(index).ToLower();
            string[] imageTypeArr = imgPath.Split('.');
            string imageType = imgPath.Split('.')[imgPath.Split('.').Length - 1];
            if (suffix == ".jpg" || suffix == ".jpeg" || suffix == ".png" || suffix == ".gif" || suffix == ".bmp")
            {
                string pictureName = DateTime.Now.Ticks.ToString() + suffix; //图片名称
                string savePath = path11;//幻灯片文件夹
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                //保存图片信息到数据库表
                studentsEntities studentsEntities = new studentsEntities();
                syspic pic = new syspic();
                pic.IMG_NAME = pictureName;
                pic.IMG_PATH = savePath;
                pic.IMG_TYPE = imageType;

                studentsEntities.syspic.Add(pic);
                studentsEntities.Entry<syspic>(pic).State = EntityState.Added;
                studentsEntities.SaveChanges();

                imgurl = "https://" + Request.Url.Authority + "/Files/Images/SlideConfig/" + pictureName;
                fileBase.SaveAs(savePath + pictureName);
            }
            else
            {
                imgurl = "0";
            }
            var result = new
            {
                imgurl = imgurl
            };
            return Content(JsonConvert.SerializeObject(result));
        }


        public FileResult GetImg()
        {
            //获取参数
            string url = Request.RawUrl;
            string[] paraArr = url.Split('/');
            int imgId = Convert.ToInt32(paraArr[paraArr.Length - 1]);

            //查询语句
            studentsEntities studentsEntities = new studentsEntities();
            syspic syspic = (from c in studentsEntities.syspic where c.ID == imgId select c).FirstOrDefault();
            if (syspic == null)
            {
                return new FilePathResult("~/Files/Images/SlideConfig/1.jpg", "image/jpg");

            }
            else
            {
                string path = syspic.IMG_PATH + syspic.IMG_NAME;
                return new FilePathResult(path, "image/" + syspic.IMG_TYPE);
            }
        }
    }
}
