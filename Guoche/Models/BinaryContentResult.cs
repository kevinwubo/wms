using System.IO;
using System.Web;
using System.Web.Mvc;

namespace GuoChe.Models
{
    public class BinaryContentResult : ActionResult
    {
        private readonly string contentType;
        private readonly byte[] contentBytes;
        private readonly string filename;
        public BinaryContentResult(byte[] contentBytes, string contentType,string filename)
        {
            this.contentBytes = contentBytes;
            this.contentType = contentType;
            this.filename = filename;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.Clear();
            response.Cache.SetCacheability(HttpCacheability.Public);
            response.ContentType = this.contentType;
             //加上下面这行，则弹出下载 
            response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(filename + ".pdf", System.Text.Encoding.UTF8));
            using (var stream = new MemoryStream(this.contentBytes))
            {
                stream.WriteTo(response.OutputStream);
                stream.Flush();
            }
        }
    }
}