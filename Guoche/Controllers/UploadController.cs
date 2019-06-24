using Common;
using Entity.ViewModel;
using Infrastructure.Cache;
using Service;
using Service.BaseBiz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuoChe.Controllers
{
    public class UploadController:BaseController
    {
        public JsonResult UploadFile()
        {
            List<AttachmentEntity> result = new List<AttachmentEntity>();
            // 文件数为0证明上传不成功
            if (Request.Files.Count == 0)
            {
                throw new Exception("请选择上传文件！");
            }
            string orderid=Request.Form["dataId"];
            string urlSufix = DateTime.Now.ToString("yyyyMMdd");
            string reuploadPath = "~/UploadFiles/" + urlSufix + "/";
            string uploadPath = Server.MapPath("../UploadFiles/" + urlSufix+"/");

            // 如果UploadFiles文件夹不存在则先创建
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            // 保存文件到UploadFiles文件夹
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];

                string filePath = uploadPath + Path.GetFileName(file.FileName);
                string fileName = file.FileName;

                // 获取文件扩展名
                string fileExtension = Path.GetExtension(filePath).ToLower();

                // 如果服务器上已经存在该文件则要修改文件名与其储存路径
                while (System.IO.File.Exists(filePath))
                {
                    Random rand = new Random();
                    fileName = rand.Next().ToString() + "-" + file.FileName;
                    filePath = uploadPath + Path.GetFileName(fileName);
                }
                // 把文件的存储路径保存起来
                // 保存文件到服务器
                file.SaveAs(filePath);

                AttachmentEntity attachment = new AttachmentEntity();
                attachment.FileName = fileName.Substring(0, fileName.IndexOf("."));
                attachment.FileExtendName = fileExtension;
                attachment.FilePath = reuploadPath + fileName;
                attachment.FileType = "产品文件上传";
                attachment.BusinessType = "产品文件上传";
                attachment.Remark = "";
                attachment.FileSize = "";
                attachment.Channel = "Offline";
                attachment.UploadDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                attachment.Operator = CurrentUser == null ? 0 : CurrentUser.UserID;
                attachment.AttachmentID = BaseDataService.CreateAttachment(attachment);
                result.Add(attachment);

                if(!string.IsNullOrEmpty(orderid))
                {
                    string attachments = "";
                    OrderEntity entity = OrderService.GetOrderByOrderID(orderid.ToInt(0), false);
                    if (entity != null)
                    {
                        attachments = entity.AttachmentIDs;
                    }
                    entity.AttachmentIDs = attachments + "," + attachment.AttachmentID;
                    OrderService.UpdateOrderAttachmentIDs(entity);

                    //更新订单状态 已回单
                    OrderService.UpdateOrderStatus(orderid.ToInt(0), 3);
                }
            }

            return new JsonResult
            {
                Data = "上传成功"
            };
        }
        
    }
}
