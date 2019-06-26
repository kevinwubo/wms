using Common;
using DataRepository.DataAccess.Order;
using Entity.ViewModel;
using Infrastructure.Cache;
using Service;
using Service.ApiBiz;
using Service.BaseBiz;
using Service.Inventory;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace GuoChe.Controllers
{
    public class OrderController:BaseController
    {
        //
        // GET: /Order/
        public int PAGESIZE = 20;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ImportIndex()
        {
            return View();
        }

        #region 订单添加
        /// <summary>
        /// 仓配订单/调拨订单
        /// 运输订单A/运输订单B 添加
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult OrderAdd(string type, int orderid = 0)
        {
            //默认承运商
            ViewBag.Carrier = CarrierService.GetCarrierByRule("", 1);//只显示使用中的数据
            //默认仓库
            ViewBag.Storage = StorageService.GetStorageByRule("", 1);//只显示使用中的数据
            //门店
            ViewBag.Goods = GoodsService.GetGoodsByRule("", 1);//只显示使用中的数据
            //客户信息
            ViewBag.Customer = CustomerService.GetCustomerByRule("", 1);//只显示使用中的数据
            //温度
            ViewBag.TemList = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "TM00" && t.Status == 1).ToList();
            //收货方类型
            ViewBag.ReceiverList = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "Reveiver00" && t.Status == 1).ToList();
            //收货方
            ViewBag.DeliverList = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "DeliverModel00" && t.Status == 1).ToList();
            //收货人信息
            ViewBag.Receiver = ReceiverService.GetReceiverByRule("", "", "", 1);//只显示使用中的数据

            ViewBag.Order = new OrderEntity();
            if (orderid > 0)
            {
                ViewBag.Order = OrderService.GetOrderByOrderID(orderid);
            }
            ViewBag.TypeName = ConvertHelper.GetOrderType(type);
            ViewBag.Type = type;
            return View();
        }

        /// <summary>
        /// 计算价格
        /// </summary>
        /// <returns></returns>
        /// <param name="customerid">客户ID</param>
        /// <param name="receivingid">收货方ID（仓库 or 门店）</param>
        /// <param name="sendstorageid">发货/出库仓库ID</param>
        /// <param name="carrierid">承运商ID</param>
        /// <param name="ordertype">订单类型</param>
        /// <returns></returns>
        public JsonResult ComputePrice(int customerid,int receivingid,int sendstorageid,int carrierid,string ordertype)
        {
            List<PriceSetEntity> priceList = PriceSetService.GetPriceSetByRule("", customerid, receivingid, sendstorageid, carrierid, 1);
            return Json(priceList);
        }

        public JsonResult GetPriceSetInfoByID(int pricesetid)
        {
            PriceSetEntity entity = PriceSetService.GetPriceSetById(pricesetid);
            return Json(entity);
        }
        #endregion

        #region 订单保存
        public void Modify(OrderEntity order)
        {
            if (order != null)
            {
                order.OperatorID = CurrentUser.UserID.ToString().ToInt(0);
            }
            OrderService.ModifyOrder(order);
            Response.Redirect("/Order/");
        }
        #endregion

        #region 商品选择器
        /// <summary>
        /// 商品选择器
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mcode"></param>
        /// <param name="CustomerID"></param>
        /// <param name="status"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        /// 商品选择器 从商品中筛选
        public ActionResult OrderGoodsSelect(string type = "", string name = "", string goodsType = "", int carrierid = 0, int storageID = 0, int customerid = 0, int status = -1, int p = 1)
        {
            List<GoodsEntity> goodsList = null;
            List<InventoryEntity> inventoryList = null;
            PagerInfo pager = new PagerInfo();
            //运输订单A  运输订单B 从订单表中获取数据
            if (!string.IsNullOrEmpty(type) && type.Equals("YSDDA") || type.Equals("YSDDB"))
            {
                int count = GoodsService.GetGoodsCount(name, "", customerid, status);
                pager.PageIndex = p;
                pager.PageSize = PAGESIZE;
                pager.SumCount = count;
                pager.URL = "OrderGoodsSelect";
                //客户信息
                ViewBag.Customer = CustomerService.GetCustomerByRule("", 1);//只显示使用中的数据
                ViewBag.GoodsModel = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "GoodsCode" && t.Status == 1).ToList();
                if (!string.IsNullOrEmpty(name) || storageID > 0 || customerid > 0 || carrierid > 0)
                {
                    goodsList = GoodsService.GetGoodsInfoByRule(name, "", customerid, status, pager);
                }
                else
                {
                    goodsList = GoodsService.GetGoodsInfoPager(pager);
                }
            }
            else
            {
                //仓配订单 调拨订单从库存表中获取数据
                int count = InventoryService.GetInventoryCount(name, "", storageID, customerid);
                
                pager.PageIndex = p;
                pager.PageSize = PAGESIZE;
                pager.SumCount = count;
                pager.URL = "OrderGoodsSelect";

                //客户信息
                ViewBag.Customer = CustomerService.GetCustomerByRule("", 1);//只显示使用中的数据
                ViewBag.GoodsModel = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "GoodsCode" && t.Status == 1).ToList();
                if (!string.IsNullOrEmpty(name) || storageID > 0 )
                {
                    inventoryList = InventoryService.GetInventoryInfoByRule(name, "", storageID, 0, pager);
                }
                else
                {
                    inventoryList = InventoryService.GetInventoryInfoPager(pager);
                }
            }

            ViewBag.StorageID = storageID;
            ViewBag.Name = name ?? "";
            ViewBag.Status = status;
            ViewBag.GoodsType = goodsType;
            ViewBag.CustomerID = customerid;
            ViewBag.GoodsList = goodsList;
            ViewBag.InventoryList = inventoryList;
            ViewBag.Pager = pager;
            ViewBag.TYPE = type;//运输订单A:YSDDA  运输订单B:YSDDB  仓配订单:CPDD 调拨订单:DBDD
            return View();
        }

        /// <summary>
        /// 商品选择器
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mcode"></param>
        /// <param name="CustomerID"></param>
        /// <param name="status"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        /// 商品选择器 从库存中筛选
        public ActionResult OrderInventorySelect(string name = "", string bnumber = "", int storageID = 0, int CustomerID = 0, int status = -1, int p = 1)
        {
            List<InventoryEntity> mList = null;

            int count = InventoryService.GetInventoryCount(name, bnumber, storageID, CustomerID);

            PagerInfo pager = new PagerInfo();
            pager.PageIndex = p;
            pager.PageSize = PAGESIZE;
            pager.SumCount = count;
            pager.URL = "OrderInventorySelect";

            //客户信息
            ViewBag.Customer = CustomerService.GetCustomerByRule("", 1);//只显示使用中的数据
            ViewBag.GoodsModel = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "GoodsCode" && t.Status == 1).ToList();
            if (!string.IsNullOrEmpty(name) || storageID > 0 || CustomerID > 0 || !string.IsNullOrEmpty(bnumber))
            {
                mList = InventoryService.GetInventoryInfoByRule(name, bnumber, storageID, CustomerID, pager);
            }
            else
            {
                mList = InventoryService.GetInventoryInfoPager(pager);
            }
            ViewBag.StorageID = storageID;
            ViewBag.Name = name ?? "";
            ViewBag.Status = status;
            ViewBag.Description = bnumber;
            ViewBag.CustomerID = CustomerID;
            ViewBag.Goods = mList;
            ViewBag.Pager = pager;
            return View();
        }
        #endregion

        #region 订单查询
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">页面权限控制类型 check:通过、拒绝</param>
        /// <param name="carrierid"></param>
        /// <param name="storageid"></param>
        /// <param name="customerid"></param>
        /// <param name="orderstatus"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public ActionResult OrderSearch(string type = "", int carrierid = 0, int storageid = 0, int customerid = 0, int orderstatus = -1, string orderno = ""
            , string begindate = "", string enddate = "", int operatorid = -1, string ordertype = "", int p = 1)
        {
            List<OrderEntity> mList = null;

            int count = OrderService.GetOrderCount("", carrierid, storageid, customerid, orderstatus, -1, -1, ordertype, orderno, begindate, enddate, operatorid);

            PagerInfo pager = new PagerInfo();
            pager.PageIndex = p;
            pager.PageSize = PAGESIZE;
            pager.SumCount = count;
            pager.URL = "OrderSearch";

            if (orderstatus > -1 || carrierid > 0 || storageid > 0 || customerid > 0 || operatorid > 0 || !string.IsNullOrEmpty(ordertype) || !string.IsNullOrEmpty(orderno) || !string.IsNullOrEmpty(begindate) || !string.IsNullOrEmpty(enddate))
            {
                mList = OrderService.GetOrderInfoByRule(pager, "", carrierid, storageid, customerid, orderstatus, -1, -1, ordertype, orderno, begindate, enddate, operatorid);
            }
            else
            {
                mList = OrderService.GetOrderInfoPager(pager);
            }
            //默认承运商
            ViewBag.Carrier = CarrierService.GetCarrierByRule("", 1);//只显示使用中的数据
            //默认仓库
            ViewBag.Storage = StorageService.GetStorageByRule("", 1);//只显示使用中的数据
            //门店
            ViewBag.Goods = GoodsService.GetGoodsByRule("", 1);//只显示使用中的数据
            //客户信息
            ViewBag.Customer = CustomerService.GetCustomerByRule("", 1);//只显示使用中的数据
            //操作人
            ViewBag.Users = UserService.GetUserAll();
            ViewBag.BeginDate = begindate;
            ViewBag.OrderNo = orderno;
            ViewBag.EndDate = enddate;
            ViewBag.PageType = type;
            ViewBag.OrderType = ordertype;
            ViewBag.OperatorID = operatorid;
            ViewBag.OrderStatus = orderstatus;
            ViewBag.carrierid = carrierid;
            ViewBag.customerid = customerid;
            ViewBag.storageid = storageid;
            ViewBag.OrderList = mList;
            ViewBag.Pager = pager;
            return View();
        }
        #endregion
        
        #region 订单明细删除
        public JsonResult Remove(int oid)
        {
            int count = OrderDetailService.Delete(oid);
            return new JsonResult
            {
                Data = count > 0 ? true : false
            };
        }
        #endregion

        #region 订单通过
        public void OrderPass(int orderid,string type)
        {
            long OperatorID = 0;
            if (CurrentUser != null)
            {
                OperatorID = CurrentUser.UserID;
            }
            // 更新订单状态
            OrderService.UpdateOrderStatus(orderid, 4);            
        }
        #endregion

        #region 订单拒绝
        public void OrderBack(int orderid, string type)
        {
            long OperatorID = 0;
            if (CurrentUser != null)
            {
                OperatorID = CurrentUser.UserID;
            }
            OrderService.OrderBack(orderid, type, OperatorID);
        }
        #endregion

        //#region 删除
        ///// <summary>
        ///// DC订单查询下载确认
        ///// </summary>
        ///// <param name="type">dc_download:DC配送单下载  dc_confirm:DC订单确认 dc_search:DC订单查询</param>
        ///// <param name="uploadstatus"></param>
        ///// <param name="ordertype"></param>
        ///// <param name="orderno"></param>
        ///// <param name="p"></param>
        ///// <returns></returns>
        //public ActionResult OrderSearch_DC(string type, int uploadstatus = -1, string ordertype = "", string orderno = "", int p = 1)
        //{
        //    List<OrderEntity> mList = null;

        //    int count = OrderService.GetOrderCount("", -1, -1, -1, -1,uploadstatus, ordertype, orderno);

        //    PagerInfo pager = new PagerInfo();
        //    pager.PageIndex = p;
        //    pager.PageSize = PAGESIZE;
        //    pager.SumCount = count;
        //    pager.URL = "OrderSearch_DCS";

        //    ViewBag.OrderTypeList = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "OrderTypeList" && t.Status == 1).ToList();

        //    if (uploadstatus > -1 || !string.IsNullOrEmpty(ordertype) || !string.IsNullOrEmpty(orderno))
        //    {
        //        mList = OrderService.GetOrderInfoByRule(pager, "", -1, -1, -1, -1, uploadstatus, ordertype, orderno);
        //    }
        //    else
        //    {
        //        mList = OrderService.GetOrderInfoPager(pager);
        //    }

        //    ViewBag.PageType = type;
        //    ViewBag.uploadstatus = uploadstatus;
        //    ViewBag.ordertype = ordertype;
        //    ViewBag.orderno = orderno;            
        //    ViewBag.OrderList = mList;
        //    ViewBag.Pager = pager;
        //    return View();
        //}
        //#endregion

        #region 承运商订单查询下载确认
        /// <summary>
        /// 承运商订单查询下载确认
        /// </summary>
        /// <param name="type">tc_download:承运商配送单下载  tc_confirm:承运商订单确认 tc_search:承运商订单查询</param>
        /// <param name="uploadstatus"></param>
        /// <param name="ordertype"></param>
        /// <param name="orderno"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public ActionResult OrderSearch_Carrier(string type, int uploadstatus = -1, string ordertype = "", string orderno = "", int p = 1)
        {
            int orderstatus = -1;    //未配送 = 0,配送中 = 1, 已配送 = 2, 已回单 = 3, 订单完成 = 4, 已拒绝 = 5        
            if(type.Equals("tc_download"))
            {
                //orderstatus=0;
            }
            else if(type.Equals("tc_confirm"))
            {
                orderstatus = -1;//已配送
                uploadstatus = 1;//已下载
            }
            else if(type.Equals("tc_search"))
            {
                orderstatus = -1;//订单完成
                uploadstatus = 1;//已下载
            }

            List<OrderEntity> mList = null;

            int count = OrderService.GetOrderCount("", -1, -1, -1, -1, uploadstatus,orderstatus, ordertype, orderno);

            PagerInfo pager = new PagerInfo();
            pager.PageIndex = p;
            pager.PageSize = PAGESIZE;
            pager.SumCount = count;
            pager.URL = "OrderSearch_Carrier";

            ViewBag.OrderTypeList = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "OrderTypeList" && t.Status == 1).ToList();

            if (uploadstatus > -1 || !string.IsNullOrEmpty(ordertype) || !string.IsNullOrEmpty(orderno))
            {
                mList = OrderService.GetOrderInfoByRule(pager, "", -1, -1, -1, -1, uploadstatus,orderstatus, ordertype, orderno);
            }
            else
            {
                mList = OrderService.GetOrderInfoPager(pager);
            }

            string title="";
            if (type.Equals("tc_download"))
                title = "承运商配送单下载";
            else if (type.Equals("tc_confirm"))
                title = "承运商订单确认";
            else if (type.Equals("tc_search"))
                title = "承运商订单查询";

            ViewBag.TitleName = title;
            ViewBag.PageType = type;
            ViewBag.uploadstatus = uploadstatus;
            ViewBag.ordertype = ordertype;
            ViewBag.orderno = orderno;
            ViewBag.OrderList = mList;
            ViewBag.Pager = pager;
            return View();
        }

        /// <summary>
        /// 开始配送时间
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="arrivertime"></param>
        /// <returns></returns>
        public JsonResult OrderChangeArriverTime(string orderid, string arrivertime)
        {
            OrderEntity entity = new OrderEntity();
            entity.OrderID = orderid.ToInt(0);
            int i = -1;
            if (!string.IsNullOrEmpty(arrivertime))
            {
                entity.ArriverDate = DateTime.Parse(arrivertime);
                i = OrderService.UpdateOrderArriverDate(entity);

                //更新订单状态 已配送
                OrderService.UpdateOrderStatus(orderid.ToInt(0), 2);
            }
            return new JsonResult
            {
                Data = i > -1 ? "配送成功" : "配送失败，请稍后再试"
            };
        }

        #endregion

        #region 订单导入
        /// <summary>
        /// 订单导入
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult OrderImport(string type)
        {
            ViewBag.Token = Guid.NewGuid();
            //客户信息
            ViewBag.Customer = CustomerService.GetCustomerByRule("", 1);//只显示使用中的数据

            ViewBag.ImportTypeDesc = StringHelper.GetOrderSource(type);
            ViewBag.Type = type;
            return View();
        }

        /// <summary>
        /// 订单导入
        /// </summary>
        /// <returns></returns>
        public JsonResult OrderImportData()
        {
            List<ImportOrderEntity> list = new List<ImportOrderEntity>();
            DataSet ds = new DataSet();
            if (Request.Files.Count == 0)
            {
                throw new Exception("请选择导入文件！");
            }

            String token= Request["token"];
            string importType = Request["importType"];//导入类型
            // 保存文件到UploadFiles文件夹
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];
                var fileName = file.FileName;
                var filePath = Server.MapPath(string.Format("~/{0}", "UploadFiles"));
                string path = Path.Combine(filePath, fileName);
                file.SaveAs(path);
                if (importType.Equals(OrderSource.Costa.ToString()))
                {
                    ds = ExcelHelper.ImportExcelXLSXtoDt(path);
                    list = OrderService.GetCostaImportList(ds);
                }
                else
                {
                    ds = ExcelHelper.ImportExceltoDt(path);
                    list = OrderService.GetImportList(ds);
                }
                //存入缓存
                Cache.Add(token, list);
            }
            return Json(list);
        }

        /// <summary>
        /// 订单生成
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public JsonResult GenerateOrder(string token, string ordersource)
        {
            long operatorID = 0;
            if (CurrentUser != null)
            {
                operatorID = CurrentUser.UserID;
            }
            List<ImportOrderEntity> list = Cache.Get<List<ImportOrderEntity>>(token);
            ImportIDSEntity idsEntity=new ImportIDSEntity();
            if (list != null && list.Count > 0)
            {
                idsEntity = OrderService.GenerateOrder(list, ordersource, operatorID);
            }
            return new JsonResult
            {
                Data = idsEntity
            };
        }

        #endregion

        #region 订单审核修改列表
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">审核：verify[通过，拒绝]  修改：modify[修改，删除]</param>
        /// <param name="carrierid"></param>
        /// <param name="storageid"></param>
        /// <param name="customerid"></param>
        /// <param name="status"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public ActionResult OrderSearch_Modify(string type = "", int carrierid = 0, int storageid = 0, int customerid = 0, int status = -1, int p = 1)
        {
            List<OrderEntity> mList = null;

            int count = OrderService.GetOrderCount("", carrierid, storageid, customerid, status);

            PagerInfo pager = new PagerInfo();
            pager.PageIndex = p;
            pager.PageSize = PAGESIZE;
            pager.SumCount = count;
            pager.URL = "OrderSearch";

            int orderstatus = 0;
            int uploadstatus = 0;
            //订单修改只能读取 未配送 未接单订单状态
            if (type.Equals("modify"))
            {
                orderstatus = 0;// 未配送
                uploadstatus = 0;//未接单
            }
            else if (type.Equals("verify"))
            {
                orderstatus = 2;// 已回单
                uploadstatus = 1;//已接单
            }

            if (status > -1 || carrierid > 0 || storageid > 0 || customerid > 0)
            {
                mList = OrderService.GetOrderInfoByRule(pager,"", carrierid, storageid, customerid, status);
            }
            else
            {
                mList = OrderService.GetOrderInfoPager(pager);
            }
            //默认承运商
            ViewBag.Carrier = CarrierService.GetCarrierByRule("", 1);//只显示使用中的数据
            //默认仓库
            ViewBag.Storage = StorageService.GetStorageByRule("", 1);//只显示使用中的数据
            //门店
            ViewBag.Goods = GoodsService.GetGoodsByRule("", 1);//只显示使用中的数据
            //客户信息
            ViewBag.Customer = CustomerService.GetCustomerByRule("", 1);//只显示使用中的数据

            ViewBag.TitleName = type.Equals("verify") ? "订单审核" : "订单修改";
            ViewBag.PageType = type;
            ViewBag.Status = status;
            ViewBag.carrierid = carrierid;
            ViewBag.customerid = customerid;
            ViewBag.storageid = storageid;
            ViewBag.OrderList = mList;
            ViewBag.Pager = pager;
            return View();
        }

        /// <summary>
        /// 订单删除
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public void OrderDelete(int orderid)
        {
            OrderService.Delete(orderid);
            Response.Redirect("OrderSearch_Modify/");
        }
        #endregion

        public ActionResult OrderSearch_bak()
        {
            return View();
        }


        #region 查看回单

        public JsonResult searchAttachmentByIDs(string ids)
        {
            List<AttachmentEntity> listAtt = new List<AttachmentEntity>();
            if(!string.IsNullOrEmpty(ids))
            {
                listAtt = BaseDataService.GetAttachmentInfoByKyes(ids.TrimEnd(','));
            }            
            return Json(listAtt);
        }
        #endregion
    }
}
