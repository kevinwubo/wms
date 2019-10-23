using Common;
using DataRepository.DataAccess.Order;
using Entity.ViewModel;
using Infrastructure.Cache;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
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
            //ViewBag.Carrier = CarrierService.GetCarrierByRule("", 1);//只显示使用中的数据
            //默认仓库
            //ViewBag.Storage = StorageService.GetStorageByRule("", 1);//只显示使用中的数据
            //门店
            //ViewBag.Goods = GoodsService.GetGoodsByRule("", 1);//只显示使用中的数据
            //客户信息
            ViewBag.Customer = CustomerService.GetCustomerByRule("", 1);//只显示使用中的数据
            //温度
            ViewBag.TemList = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "TM00" && t.Status == 1).ToList();
            //收货方类型
            ViewBag.ReceiverList = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "Reveiver00" && t.Status == 1).ToList();
            //收货方
            ViewBag.DeliverList = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "DeliverModel00" && t.Status == 1).ToList();
            //收货人信息
            //ViewBag.Receiver = ReceiverService.GetReceiverByRule("", "", "", 1);//只显示使用中的数据

            ViewBag.Order = new OrderEntity();
            ViewBag.ButtonName = "发布订单";
            if (orderid > 0)
            {
                ViewBag.ButtonName = "确认修改";
                ViewBag.Order = OrderService.GetOrderByOrderID(orderid);
            }
            ViewBag.TypeName = StringHelper.getOrderType(type);
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
        public ContentResult Modify(OrderEntity order)
        {
            string tips = "订单添加成功！";
            if (order != null)
            {
                if (order.OrderID > 0)
                {
                    tips = "订单修改成功！";
                }
                order.OperatorID = CurrentUser.UserID.ToString().ToInt(0);
            }
            OrderService.ModifyOrder(order);            
            return Content("<script>alert('" + tips + "');window.location.href='/Order/OrderSearch?orderno=" + order.OrderNo + "'</script>");
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
                    inventoryList = InventoryService.GetInventoryInfoByRule(name, "", storageID, customerid, pager);
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
            , string begindate = "", string enddate = "", int operatorid = -1, string ordertype = "", string ordersource = "", string subOrderType = "", int p = 1, int pageSize = 20)
        {
            // 默认当月
            if (string.IsNullOrEmpty(begindate) || string.IsNullOrEmpty(enddate))
            {
                DateTime dt = DateTime.Now;
                begindate = dt.Year + "-" + dt.Month + "-" + "01";
                enddate = DateTime.Now.ToString("yyyy-MM-dd");
            }

            List<OrderEntity> mList = null;

            int count = OrderService.GetOrderCount("", carrierid, storageid, customerid, orderstatus, -1, -1, ordertype, orderno, begindate, enddate, operatorid, ordersource, subOrderType);

            PagerInfo pager = new PagerInfo();
            pager.PageIndex = p;
            pager.PageSize = pageSize;
            pager.SumCount = count;
            pager.URL = "OrderSearch";

            if (orderstatus > -1 || carrierid > 0 || storageid > 0 || customerid > 0 || operatorid > 0 || !string.IsNullOrEmpty(ordertype) || !string.IsNullOrEmpty(subOrderType) || !string.IsNullOrEmpty(ordersource) || !string.IsNullOrEmpty(orderno) || !string.IsNullOrEmpty(begindate) || !string.IsNullOrEmpty(enddate))
            {
                mList = OrderService.GetOrderInfoByRule(pager, "", carrierid, storageid, customerid, orderstatus, -1, -1, ordertype, orderno, begindate, enddate, operatorid, ordersource, subOrderType);
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
            //订单类型
            List<BaseDataEntity> orderTypeList = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "OrderTypeList").ToList();
            //商超订单 导入
            List<BaseDataEntity> orderSourceList = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "MarketCode001").ToList();
            //操作人
            ViewBag.Users = UserService.GetUserAll();
            ViewBag.BeginDate = begindate;
            ViewBag.OrderNo = orderno;
            ViewBag.EndDate = enddate;
            ViewBag.PageType = type;
            ViewBag.OrderType = ordertype;
            ViewBag.OrderSource = ordersource;
            ViewBag.orderSourceList = orderSourceList;
            ViewBag.OperatorID = operatorid;
            ViewBag.OrderStatus = orderstatus;
            ViewBag.carrierid = carrierid;
            ViewBag.customerid = customerid;
            ViewBag.storageid = storageid;
            ViewBag.OrderList = mList;
            ViewBag.orderTypeList = orderTypeList;
            ViewBag.PageSize = pageSize;
            ViewBag.SubOrderType = subOrderType;
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
        public ActionResult OrderSearch_Carrier(string type, int uploadstatus = -1, string ordertype = "", string orderno = "", int p = 1, int pageSize = 20)
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
            pager.PageSize = pageSize;
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
            ViewBag.PageSize = pageSize;
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
        public ActionResult OrderImport(string source)
        {
            ViewBag.Token = Guid.NewGuid();
            //客户信息
            ViewBag.Customer = CustomerService.GetCustomerByRule("", 1);//只显示使用中的数据
            //订单类型
            List<BaseDataEntity> orderTypeList = BaseDataService.GetBaseDataAll().Where(t => t.PCode == "OrderTypeList").ToList();
            ViewBag.ImportSourceDesc = StringHelper.GetOrderSource(source);
            ViewBag.OrderSource = source;
            ViewBag.orderTypeList = orderTypeList;
            return View();
        }

        /// <summary>
        /// 订单导入
        /// </summary>
        /// <returns></returns>
        public JsonResult OrderImportData()
        {
            List<ImportOrderEntity> list = new List<ImportOrderEntity>();
            List<RegularOrderEntity> listRegular = new List<RegularOrderEntity>();
            DataSet ds = new DataSet();
            if (Request.Files.Count == 0)
            {
                throw new Exception("请选择导入文件！");
            }

            String token= Request["token"];
            Cache.Remove(token);
            string importSource = Request["importSource"];//导入类型
            // 保存文件到UploadFiles文件夹
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];
                var fileName = file.FileName;
                var filePath = Server.MapPath(string.Format("~/{0}", "UploadFiles"));
                string path = Path.Combine(filePath, fileName);
                file.SaveAs(path);
                if (importSource.Equals(OrderSource.Costa.ToString()))
                {
                    ds = ExcelHelper.ImportExcelXLSXtoDt(path);
                    list = OrderService.GetCostaImportList(ds);
                    //存入缓存
                    Cache.Add(token, list);
                }
                else if (importSource.Equals(OrderSource.Regular.ToString()))//常规导入
                {
                    ds = ExcelHelper.ImportBaseExceltoDt(path);
                    listRegular = OrderService.GetRegularImportList(ds);
                    //存入缓存
                    Cache.Add(token, listRegular);
                    return Json(listRegular);
                }
                else
                {
                    ds = ExcelHelper.ImportExceltoDt(path);
                    list = OrderService.GetImportList(ds);
                    //存入缓存
                    Cache.Add(token, list);
                }
                
            }
            return Json(list);
        }

        /// <summary>
        /// 订单生成
        /// </summary>
        /// <param name="token">缓存读取</param>
        /// <param name="ordersource">订单来源：大润发/家乐福/卜蜂莲花/上蔬永辉/Costa/常规订单</param>
        /// <param name="orderType">订单类型：仓配订单/调拨订单/入库单/运输订单A/运输订单B</param>
        public JsonResult GenerateOrder(string token, string ordersource,string orderType)
        {
            ImportIDSEntity idsEntity = new ImportIDSEntity();
            long operatorID = 0;
            if (CurrentUser != null)
            {
                operatorID = CurrentUser.UserID;
            }
            //常规订单
            if (ordersource.Equals(OrderSource.Regular.ToString()))
            {
                List<RegularOrderEntity> list = Cache.Get<List<RegularOrderEntity>>(token);
                if (list != null && list.Count > 0)
                {
                    LogHelper.WriteTextLog("常规订单导入", JsonHelper.ToJson(list));
                    idsEntity = OrderService.GenerateRegularOrder(list, ordersource, orderType, operatorID);
                }
            }
            else
            {
                //商超订单+Costa订单
                List<ImportOrderEntity> list = Cache.Get<List<ImportOrderEntity>>(token);
                if (list != null && list.Count > 0)
                {
                    LogHelper.WriteTextLog("商超订单导入", JsonHelper.ToJson(list));
                    idsEntity = OrderService.GenerateOrder(list, ordersource, orderType, operatorID);
                }
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
        public ActionResult OrderSearch_Modify(string type = "", int carrierid = 0, int storageid = 0, int customerid = 0, int status = -1, string orderno="",
            string begindate = "", string enddate = "", int p = 1, int pageSize = 20)
        {
            List<OrderEntity> mList = null;

            // 默认当月
            if (string.IsNullOrEmpty(begindate) || string.IsNullOrEmpty(enddate))
            {
                DateTime dt = DateTime.Now;
                begindate = dt.Year + "-" + dt.Month + "-" + "01";
                enddate = DateTime.Now.ToString("yyyy-MM-dd");
            }

            int count = OrderService.GetOrderCount("", carrierid, storageid, customerid, status, -1, -1, "", orderno,begindate,enddate);

            PagerInfo pager = new PagerInfo();
            pager.PageIndex = p;
            pager.PageSize = pageSize;
            pager.SumCount = count;
            pager.URL = "OrderSearch_Modify";

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

            if (status > -1 || carrierid > 0 || storageid > 0 || customerid > 0 || !string.IsNullOrEmpty(orderno) || !string.IsNullOrEmpty(begindate) || !string.IsNullOrEmpty(enddate))
            {
                mList = OrderService.GetOrderInfoByRule(pager, "", carrierid, storageid, customerid, status, -1, -1, "", orderno, begindate, enddate);
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

            ViewBag.UserID=CurrentUser!=null?CurrentUser.UserID:-1;

            ViewBag.TitleName = type.Equals("verify") ? "订单审核" : "订单修改";
            ViewBag.PageType = type;
            ViewBag.Status = status;
            ViewBag.carrierid = carrierid;
            ViewBag.customerid = customerid;
            ViewBag.storageid = storageid;
            ViewBag.OrderList = mList;
            ViewBag.PageSize = pageSize;
            ViewBag.BeginDate = begindate;
            ViewBag.OrderNo = orderno;
            ViewBag.EndDate = enddate;
            ViewBag.Pager = pager;
            return View();
        }

        /// <summary>
        /// 订单删除
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public void OrderDelete(int orderid, string type)
        {
            OrderService.Delete(orderid);
            Response.Redirect("/Order/OrderSearch_Modify?type=" + type);
        }
        #endregion


        #region 下载Excel
        public FileResult DownloadExcel(string ids)
        {
            List<OrderEntity> list = getOrderList(ids);
            //创建Excel文件的对象
            HSSFWorkbook book = new HSSFWorkbook();
            //添加一个sheet
            ISheet sheet1 = book.CreateSheet("Sheet1");
            sheet1.SetColumnWidth(0, 15 * 256);
            sheet1.SetColumnWidth(1, 15 * 256);
            sheet1.SetColumnWidth(2, 15 * 256);
            sheet1.SetColumnWidth(3, 15 * 256);
            sheet1.SetColumnWidth(4, 15 * 256);
            sheet1.SetColumnWidth(5, 15 * 256);
            sheet1.SetColumnWidth(6, 15 * 256);
            sheet1.SetColumnWidth(7, 15 * 256);
            sheet1.SetColumnWidth(8, 15 * 256);
            sheet1.SetColumnWidth(9, 20 * 256);
            sheet1.SetColumnWidth(10, 15 * 256);
            sheet1.SetColumnWidth(11, 15 * 256);
            sheet1.SetColumnWidth(12, 15 * 256);
            sheet1.SetColumnWidth(13, 15 * 256);
            sheet1.SetColumnWidth(14, 15 * 256);
            sheet1.SetColumnWidth(15, 15 * 256);
            sheet1.SetColumnWidth(16, 15 * 256);
            sheet1.SetColumnWidth(17, 15 * 256);
            sheet1.SetColumnWidth(18, 15 * 256);
            ////公共样式：加边框
            //NPOI.SS.UserModel.ICellStyle style = book.CreateCellStyle();
            //style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            //style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            //style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            //style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            //style.Alignment = HorizontalAlignment.Center;

            //NPOI.SS.UserModel.ICellStyle styleHeader = book.CreateCellStyle();
            //styleHeader.CloneStyleFrom(style);//克隆公共的样式
            //styleHeader.Alignment = HorizontalAlignment.Center;//单元格水平居中
            //IFont fontHeader = book.CreateFont();//新建一个字体样式对象
            //fontHeader.Boldweight = short.MaxValue;//设置字体加粗样式
            //styleHeader.SetFont(fontHeader);//使用SetFont方法将字体样式添加到单元格样式中
            //将数据逐步写入sheet1各个行

            //订单状态	订单属性	订单编号	下单时间	出库仓	承运商	客户编号	客户名称	送达方(门店/仓库)	送达地址(门店/仓库)	要求送达时间
            //确认送达时间	配送总数	总重量	备注	装卸应收	装卸应付	运费应收	运费应付
            //给sheet1添加第一行的头部标题
            int Row = 0;
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(Row);

            row1.CreateCell(0).SetCellValue("订单状态");
            row1.CreateCell(1).SetCellValue("订单属性");
            row1.CreateCell(2).SetCellValue("订单编号");
            row1.CreateCell(3).SetCellValue("下单时间");
            row1.CreateCell(4).SetCellValue("出库仓");
            row1.CreateCell(5).SetCellValue("承运商");
            row1.CreateCell(6).SetCellValue("客户编号");
            row1.CreateCell(7).SetCellValue("客户名称");
            row1.CreateCell(8).SetCellValue("送达方(门店/仓库)");
            row1.CreateCell(9).SetCellValue("送达地址(门店/仓库)");
            row1.CreateCell(10).SetCellValue("要求送达时间");
            row1.CreateCell(11).SetCellValue("确认送达时间");
            row1.CreateCell(12).SetCellValue("配送总数");
            row1.CreateCell(13).SetCellValue("总重量");
            row1.CreateCell(14).SetCellValue("备注");
            row1.CreateCell(15).SetCellValue("装卸应收");
            row1.CreateCell(16).SetCellValue("装卸应付");
            row1.CreateCell(17).SetCellValue("运费应收");
            row1.CreateCell(18).SetCellValue("运费应付");

            Row = Row + 1;
            for (int i = 0; i < list.Count; i++)
            {                
                //订单状态	订单属性	订单编号	下单时间	出库仓	承运商	客户编号	客户名称	送达方(门店/仓库)	送达地址(门店/仓库)	要求送达时间
                //确认送达时间	配送总数	总重量	备注	装卸应收	装卸应付	运费应收	运费应付
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(Row);
                
                rowtemp.CreateCell(0).SetCellValue(list[i].OrderStatusDesc);
                rowtemp.CreateCell(1).SetCellValue(list[i].OrderTypeDesc);
                rowtemp.CreateCell(2).SetCellValue(list[i].OrderNo);
                rowtemp.CreateCell(3).SetCellValue(list[i].OrderDate);
                rowtemp.CreateCell(4).SetCellValue(list[i].sendstorage != null ? list[i].sendstorage.StorageName : "");
                rowtemp.CreateCell(5).SetCellValue(list[i].carrier != null ? list[i].carrier.CarrierName : "");
                rowtemp.CreateCell(6).SetCellValue(list[i].customer != null ? list[i].customer.CustomerNo : "");
                rowtemp.CreateCell(7).SetCellValue(list[i].customer != null ? list[i].customer.CustomerName : "");
                rowtemp.CreateCell(8).SetCellValue(list[i].receiver != null ? list[i].receiver.ReceiverName : "");
                rowtemp.CreateCell(9).SetCellValue(list[i].receiver != null ? list[i].receiver.Address : "");
                rowtemp.CreateCell(10).SetCellValue(list[i].SendDate.ToShortDateString());
                rowtemp.CreateCell(11).SetCellValue(list[i].ArriverDate > DateTime.Parse("2001-01-01") ? list[i].ArriverDate.ToString() : "");

                int totalQutity = 0;
                int totalWeight = 0;
                List<OrderDetailEntity> listDetail= list[i].orderDetailList;
                if (listDetail != null && listDetail.Count > 0)
                {
                    foreach (OrderDetailEntity item in listDetail)
                    {
                        totalQutity += item.Quantity;
                        totalWeight += item.Quantity * item.Weight.ToInt(0);
                    }
                }

                rowtemp.CreateCell(12).SetCellValue(totalQutity);
                rowtemp.CreateCell(13).SetCellValue(totalWeight);
                rowtemp.CreateCell(14).SetCellValue(list[i].Remark);
                rowtemp.CreateCell(15).SetCellValue(list[i].configHandInAmt.ToString());
                rowtemp.CreateCell(16).SetCellValue(list[i].configHandOutAmt.ToString());
                rowtemp.CreateCell(17).SetCellValue(list[i].configPrice.ToString());
                rowtemp.CreateCell(18).SetCellValue(list[i].configCosting.ToString());


                //List<OrderDetailEntity> listDetail= list[i].orderDetailList;
                //if (listDetail != null && listDetail.Count > 0)
                //{
                //    Row = Row + 1;
                //    NPOI.SS.UserModel.IRow row2 = sheet1.CreateRow(Row);
                //    row2.CreateCell(0).SetCellValue("");
                //    row2.CreateCell(1).SetCellValue("");
                //    row2.CreateCell(2).SetCellValue("");
                //    row2.CreateCell(3).SetCellValue("");
                //    row2.CreateCell(4).SetCellValue("");
                //    row2.CreateCell(5).SetCellValue("");
                //    row2.CreateCell(6).SetCellValue("");                    
                //    row2.CreateCell(7).SetCellValue("项次");
                //    row2.CreateCell(8).SetCellValue("商品编号");
                //    row2.CreateCell(9).SetCellValue("名称");
                //    row2.CreateCell(10).SetCellValue("规格");
                //    row2.CreateCell(11).SetCellValue("数量");
                //    row2.CreateCell(12).SetCellValue("单位");
                //    row2.CreateCell(13).SetCellValue("重量");
                //    row2.CreateCell(14).SetCellValue("批次号");
                //    row2.CreateCell(15).SetCellValue("生产日期");
                //    row2.CreateCell(16).SetCellValue("到期日期");

                //    int jj = 1;
                //    for (int j = 0; j < listDetail.Count; j++)
                //    {
                //        Row = Row + 1;
                //        NPOI.SS.UserModel.IRow rowtemp2 = sheet1.CreateRow(Row);
                //        rowtemp2.CreateCell(0).SetCellValue("");
                //        rowtemp2.CreateCell(1).SetCellValue("");
                //        rowtemp2.CreateCell(2).SetCellValue("");
                //        rowtemp2.CreateCell(3).SetCellValue("");
                //        rowtemp2.CreateCell(4).SetCellValue("");
                //        rowtemp2.CreateCell(5).SetCellValue("");
                //        rowtemp2.CreateCell(6).SetCellValue("");
                //        rowtemp2.CreateCell(7).SetCellValue(jj++);
                //        rowtemp2.CreateCell(8).SetCellValue(listDetail[j].GoodsNo);
                //        rowtemp2.CreateCell(9).SetCellValue(listDetail[j].GoodsName);
                //        rowtemp2.CreateCell(10).SetCellValue(listDetail[j].GoodsModel);
                //        rowtemp2.CreateCell(11).SetCellValue(listDetail[j].Quantity);
                //        rowtemp2.CreateCell(12).SetCellValue(listDetail[j].Units);
                //        rowtemp2.CreateCell(13).SetCellValue(listDetail[j].TotalWeight);
                //        rowtemp2.CreateCell(14).SetCellValue(listDetail[j].BatchNumber);
                //        rowtemp2.CreateCell(15).SetCellValue(listDetail[j].ProductDate.ToShortDateString());
                //        rowtemp2.CreateCell(16).SetCellValue(listDetail[j].ExceedDate.ToShortDateString());
                //    }
                //}
                Row = Row + 1;

            }
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", DateTime.Now.ToString("yyyyMMddhhmmss") + "_Order.xls");
        }

        /// <summary>
        /// 获取传入订单号
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="typedesc">送货单、补损单</param>
        /// <returns></returns>
        private List<OrderEntity> getOrderList(string ids)
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
                        if (entity != null)
                        {
                            list.Add(entity);
                        }
                    }
                }
            }
            return list;
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
