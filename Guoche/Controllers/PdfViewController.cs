using Entity.ViewModel;
using GuoChe.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Common;

namespace GuoChe.Controllers
{
    public class PdfViewController : BasePdfController
    {
        //
        // GET: /PdfView/

        public ActionResult Index()
        {
            return View();
        }
        /// <sum
        /// mary>
        ///下載PDF檔案
        /// </summary>
        /// <returns></returns>
        public ActionResult DownloadPdf(string ids)
        {
            List<OrderEntity> orderlist = getOrderList(ids, "送货单");
            OrderInfo info = new OrderInfo(orderlist);
            string htmlText = this.ViewPdf("Preview", info);
            byte[] pdfFile = this.ConvertHtmlTextToPDF(htmlText);

            return new BinaryContentResult(pdfFile, "application/pdf", "送货单");//StringHelper.getOrderType(orderlist[0].OrderType)
        }

        /// <sum
        /// mary>
        ///导入模板下載PDF檔案
        /// </summary>
        /// <returns></returns>
        public ActionResult DownloadImportPdf(string ids, string type)
        {
            string typedesc = "SHD".Equals(type) ? "送货单" : "补损单";
            List<OrderEntity> orderlist = getOrderList(ids, typedesc);
            OrderInfo info = new OrderInfo(orderlist);
            string htmlText = this.ViewPdf("PreviewImport", info);
            byte[] pdfFile = this.ConvertHtmlTextToPDF(htmlText);

            return new BinaryContentResult(pdfFile, "application/pdf", typedesc);
        }

        /// <summary>
        /// 获取传入订单号
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="typedesc">送货单、补损单</param>
        /// <returns></returns>
        private List<OrderEntity> getOrderList(string ids,string typedesc)
        {
            List<OrderEntity> list = new List<OrderEntity>();
            if (!string.IsNullOrEmpty(ids))
            {
                string[] strs = ids.Split(',');
                foreach (string str in strs)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        OrderEntity entity = OrderService.GetOrderEntityById(str.ToInt(0));
                        entity.TypeDesc = typedesc;
                        if (entity != null)
                        {
                            list.Add(entity);
                        }

                        //更新订单接单(下载)状态
                        OrderService.UpdateUploadStatus(str.ToInt(0), 1);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 將Html文字 輸出到PDF檔裡
        /// </summary>
        /// <param name="htmlText"></param>
        /// <returns></returns>
        public byte[] ConvertHtmlTextToPDF(string htmlText)
        {
            if (string.IsNullOrEmpty(htmlText))
            {
                return null;
            }
            //避免當htmlText無任何html tag標籤的純文字時，轉PDF時會掛掉，所以一律加上<p>標籤
            htmlText = "<p>" + htmlText + "</p>";

            MemoryStream outputStream = new MemoryStream(); //要把PDF寫到哪個串流
            byte[] data = Encoding.UTF8.GetBytes(htmlText); //字串轉成byte[]
            MemoryStream msInput = new MemoryStream(data);

            Document doc = new Document(); //要寫PDF的文件，建構子沒填的話預設直式A4

            //横向
            Rectangle pageSize = new Rectangle(PageSize.A4.Height, PageSize.A4.Width);
            pageSize.Rotate();
            doc.SetPageSize(pageSize);

            PdfWriter writer = PdfWriter.GetInstance(doc, outputStream);

            //指定文件預設開檔時的縮放為100%
            PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1f);
            //開啟Document文件 
            doc.Open();
            //使用XMLWorkerHelper把Html parse到PDF檔裡
            XMLWorkerHelper.GetInstance()
                .ParseXHtml(writer, doc, msInput, null, Encoding.UTF8, new UnicodeFontFactory());
            //將pdfDest設定的資料寫到PDF檔
            PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
            writer.SetOpenAction(action);
            doc.Close();
            msInput.Close();
            outputStream.Close();
            //回傳PDF檔案 
            return outputStream.ToArray();

        }
    }
}
