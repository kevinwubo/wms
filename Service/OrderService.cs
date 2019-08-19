using DataRepository.DataAccess.BaseData;
using DataRepository.DataModel;
using Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Helper;
using Common;
using DataRepository.DataAccess.Order;
using Service.BaseBiz;
using System.Data;
using Service.Inventory;

namespace Service
{
    public class OrderService : BaseService
    {
        /// <summary>
        /// 订单号生成
        /// </summary>
        /// <returns></returns>
        public static string GetOrderNo()
        {
            string orderNo = DateTime.Now.ToString("yyyyMMddhhfff");
            OrderEntity entity = OrderService.GetOrderByOrderNo(orderNo);
            if (entity != null)
            {
                orderNo = DateTime.Now.ToString("yyyyMMddhhfff");
            }
            return orderNo;
        }
        public static OrderEntity GetOrderEntityById(long oid)
        {
            OrderEntity result = new OrderEntity();
            OrderRepository mr = new OrderRepository();
            OrderInfo info = mr.GetOrderByOrderID(oid);
            result = TranslateOrderEntity(info);
            //获取联系人信息
            //result.listContact= ContactService.GetContactByRule(UnionType.Order.ToString(), info.OrderID);
            return result;
        }


        public static OrderEntity GetOrderByOrderNo(string OrderNo)
        {
            OrderEntity result = new OrderEntity();
            OrderRepository mr = new OrderRepository();
            OrderInfo info = mr.GetOrderByOrderNo(OrderNo);
            result = TranslateOrderEntity(info);
            //获取联系人信息
            //result.listContact= ContactService.GetContactByRule(UnionType.Order.ToString(), info.OrderID);
            return result;
        }


        private static OrderInfo TranslateOrderInfo(OrderEntity entity)
        {
            OrderInfo info = new OrderInfo();
            if (entity != null)
            {
                info.OrderID = entity.OrderID;
                info.OrderNo = entity.OrderNo;
                info.MergeNo = entity.MergeNo;
                info.OrderType = entity.OrderType;
                info.SubOrderType = entity.SubOrderType;
                info.ReceiverID = entity.ReceiverID;
                info.CustomerID = entity.CustomerID;
                info.SendStorageID = entity.SendStorageID;
                info.ReceiverStorageID = entity.ReceiverStorageID;
                info.CarrierID = entity.CarrierID;
                info.OrderDate = Convert.ToDateTime(entity.OrderDate);
                info.SendDate =Convert.ToDateTime( entity.SendDate);

                info.configPrice = entity.configPrice;
                info.configHandInAmt = entity.configHandInAmt;
                info.configSortPrice = entity.configSortPrice;
                info.configCosting = entity.configCosting;
                info.configHandOutAmt = entity.configHandOutAmt;
                info.configSortCosting = entity.configSortCosting;

                info.TempType = entity.TempType;
                info.OrderStatus = entity.OrderStatus;
                info.UploadStatus = entity.UploadStatus;
                info.Status = entity.Status;
                info.Remark = entity.Remark;
                info.OperatorID = entity.OperatorID;

                info.IsImport = entity.IsImport;
                info.OrderSource = entity.OrderSource;
                info.SalesMan = entity.SalesMan;
                info.PromotionMan = entity.PromotionMan;

                info.CreateDate = entity.CreateDate;
                info.ChangeDate = entity.ChangeDate;
            }


            return info;
        }

        public static OrderEntity TranslateOrderEntity(OrderInfo info, bool isContact = true)
        {
            OrderEntity entity = new OrderEntity();
            if (info != null)
            {
                entity.OrderID = info.OrderID;
                entity.OrderNo = info.OrderNo;
                entity.MergeNo = info.MergeNo;
                entity.OrderType = info.OrderType;
                entity.SubOrderType = info.SubOrderType;
                entity.OrderTypeDesc = StringHelper.getOrderType(info.OrderType) + "-" + StringHelper.getSubOrderType(info.SubOrderType);
                entity.ReceiverID = info.ReceiverID;
                entity.CustomerID = info.CustomerID;
                entity.SendStorageID = info.SendStorageID;
                entity.ReceiverStorageID = info.ReceiverStorageID;
                entity.CarrierID = info.CarrierID;
                entity.OrderDate = info.OrderDate.ToShortDateString();
                entity.SendDate = info.SendDate;
                entity.ArriverDate = info.ArriverDate;
                entity.configPrice = info.configPrice;
                entity.configHandInAmt = info.configHandInAmt;
                entity.configSortPrice = info.configSortPrice;
                entity.configCosting = info.configCosting;
                entity.configHandOutAmt = info.configHandOutAmt;
                entity.configSortCosting = info.configSortCosting;

                entity.TempType = info.TempType;
                entity.OrderStatus = info.OrderStatus;
                entity.OrderStatusDesc = StringHelper.GetOrderStatusDesc(info.OrderStatus);
                entity.UploadStatus = info.UploadStatus;
                entity.UploadStatusDesc = StringHelper.GetUploadStatusDesc(info.UploadStatus);
                entity.Status = info.Status;
                entity.Remark = info.Remark;
                entity.OrderSource = info.OrderSource;
                entity.SalesMan = info.SalesMan;
                entity.PromotionMan = info.PromotionMan;
                entity.IsImport = info.IsImport;
                entity.OperatorID = info.OperatorID;
                entity.CreateDate = info.CreateDate;
                entity.ChangeDate = info.ChangeDate;
                entity.orderDetailList = OrderDetailService.GetOrderDetailByOrderID(entity.OrderID);

                entity.receiver = ReceiverService.GetReceiverById(entity.ReceiverID);
                entity.carrier = CarrierService.GetCarrierById(entity.CarrierID);
                entity.customer = CustomerService.GetCustomerById(entity.CustomerID);
                entity.sendstorage = StorageService.GetStorageEntityById(entity.SendStorageID);
                entity.contact = new OrderContactEntity();
                entity.AttachmentIDs = info.AttachmentIDs;
                if (isContact)
                {
                    OrderContactEntity item = new OrderContactEntity();
                    if (entity.ReceiverID > 0)
                    {
                        ReceiverEntity receiver = ReceiverService.GetReceiverEntityById(entity.ReceiverID);
                        if (receiver != null)
                        {
                            item.name = receiver.ReceiverName;
                            List<ContactEntity> listCon = receiver.listContact;
                            if (listCon != null && listCon.Count > 0)
                            {
                                item.contact = listCon[0].ContactName;
                                item.telephone = string.IsNullOrEmpty(listCon[0].Mobile) ? listCon[0].Telephone : listCon[0].Mobile;
                            }
                            item.address = receiver.Address;
                        }

                    }
                    if (entity.ReceiverStorageID > 0)
                    {
                        StorageEntity storage = StorageService.GetStorageEntityById(entity.ReceiverStorageID);
                        if (storage != null)
                        {
                            item.name = storage.StorageName;
                            List<ContactEntity> listCon = storage.listContact;
                            if (listCon != null && listCon.Count > 0)
                            {
                                item.contact = listCon[0].ContactName;
                                item.telephone = string.IsNullOrEmpty(listCon[0].Mobile) ? listCon[0].Telephone : listCon[0].Mobile;
                            }
                            item.address = storage.Address;
                        }
                        //entity.receiverstorage = 
                    }
                    entity.contact = item;
                    entity.user = UserService.GetUserById(info.OperatorID);
                }

            }

            return entity;
        }

        /// <summary>
        /// 更新附件信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int UpdateOrderAttachmentIDs(OrderEntity entity)
        {
            OrderRepository mr = new OrderRepository();
            OrderInfo info=new OrderInfo();
            info.OrderID=entity.OrderID;
            info.AttachmentIDs=entity.AttachmentIDs;
            return mr.UpdateOrderAttachmentIDs(info);
        }

        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="orderstatus">未配送 = 0,配送中 = 1, 已配送 = 2, 已回单 = 3, 订单完成 = 4, 已拒绝 = 5</param>
        /// <returns></returns>
        public static int UpdateOrderStatus(int orderid,int orderstatus)
        {
            OrderRepository mr = new OrderRepository();
            OrderInfo info = new OrderInfo();
            info.OrderID = orderid;
            info.OrderStatus = orderstatus;
            return mr.UpdateOrderStatus(info);
        }

        /// <summary>
        /// 更新下载(接单)状态
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="uploadstatus">接单状态 0：(未接单) 1：(已接单)</param>
        /// <returns></returns>
        public static int UpdateUploadStatus(int orderid, int uploadstatus)
        {
            OrderRepository mr = new OrderRepository();
            OrderInfo info = new OrderInfo();
            info.OrderID = orderid;
            info.UploadStatus = uploadstatus;
            return mr.UpdateUploadStatus(info);
        }
        public static int UpdateOrderArriverDate(OrderEntity entity)
        {
            OrderRepository mr = new OrderRepository();
            OrderInfo info = new OrderInfo();
            info.OrderID = entity.OrderID;
            info.ArriverDate = entity.ArriverDate;
            return mr.UpdateOrderArriverDate(info);
        }


        public static bool ModifyOrder(OrderEntity entity)
        {
            long OrderID = 0;
            long result = 0;
            if (entity != null)
            {
                OrderRepository mr = new OrderRepository();

                OrderInfo orderInfo = TranslateOrderInfo(entity);
                OrderJsonEntity jsonlist = null;
                if (!string.IsNullOrEmpty(entity.orderDetailJson))
                {
                    try
                    {
                        jsonlist = (OrderJsonEntity)JsonHelper.FromJson(entity.orderDetailJson, typeof(OrderJsonEntity));

                    }
                    catch (Exception ex)
                    {
                        string str = ex.ToString();
                    }
                }

                //订单信息更新
                if (entity.OrderID > 0)
                {
                    orderInfo.OrderID = entity.OrderID;
                    orderInfo.ChangeDate = DateTime.Now;
                    result = mr.ModifyOrder(orderInfo);
                    OrderID = entity.OrderID;
                }
                else
                {
                    orderInfo.ChangeDate = DateTime.Now;
                    orderInfo.CreateDate = DateTime.Now;
                    if (!string.IsNullOrEmpty(orderInfo.OrderNo))
                    {
                        OrderEntity oEntity = OrderService.GetOrderByOrderNo(orderInfo.OrderNo);
                        if (oEntity != null)
                        {
                            oEntity.OrderNo = GetOrderNo();
                        }
                    }
                    result = mr.CreateNew(orderInfo);
                    OrderID = result;
                }

                #region 订单明细更新

                if (jsonlist != null)
                {
                    List<OrderDetailInfo> oldOrderDetail = new List<OrderDetailInfo>();//修改 涉及到库存数量更改
                    List<OrderDetailJsonEntity> list = jsonlist.listOrderDetail;
                    if (list != null && list.Count > 0)
                    {
                        OrderDetailRepository mrdetail = new OrderDetailRepository();
                        foreach(OrderDetailJsonEntity item in list)
                        {
                            OrderDetailInfo info = new OrderDetailInfo();
                            info.ID = item.ID;
                            info.OrderID = entity.OrderID > 0 ? item.OrderID : result.ToString().ToInt(0);
                            int goodsID = item.GoodsID.ToInt(0);
                            GoodsEntity goodsEntity = new GoodsEntity(); ;
                            if (goodsID < 1)
                            {
                                goodsEntity = getGoodsModelByGoods("", item.GoodsName, item.GoodsNo, item.GoodsModel);
                            }
                            else
                            {
                                goodsEntity = GoodsService.GetGoodsEntityById(goodsID);
                            }
                            info.GoodsID = goodsEntity.GoodsID;

                            

                            info.InventoryID = item.InventoryID.ToInt(0);
                            info.GoodsNo = goodsEntity != null ? goodsEntity.GoodsNo : item.GoodsNo;
                            info.GoodsName = goodsEntity != null ? goodsEntity.GoodsName : item.GoodsName;
                            info.GoodsModel = goodsEntity != null ? goodsEntity.GoodsModel : item.GoodsModel;
                            info.Quantity = item.Quantity.ToInt(0);
                            info.Units = goodsEntity != null ? goodsEntity.Units: item.Units;
                            info.Weight = goodsEntity != null ? goodsEntity.Weight : item.Weight;
                            info.TotalWeight = item.TotalWeight;
                            info.BatchNumber = item.BatchNumber;
                            info.ProductDate = string.IsNullOrEmpty(item.ProductDate) ? DateTime.Now : Convert.ToDateTime(item.ProductDate);
                            if (!string.IsNullOrEmpty(item.ExceedDate))
                            {
                                if (info.GoodsID > 0)
                                {
                                    if (goodsEntity != null)
                                    {
                                        info.ExceedDate = Datehelper.getDateTime(info.ProductDate, goodsEntity.exDate.ToInt(0), goodsEntity.exUnits);
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        info.ExceedDate = string.IsNullOrEmpty(item.ExceedDate) ? DateTime.Now : Convert.ToDateTime(item.ExceedDate);
                                    }
                                    catch (Exception ex)
                                    {
                                                                               
                                    }
                                }
                            }
                            
     
                            if (info.ID > 0)
                            {
                                oldOrderDetail.Add(info);
                                info.ChangeDate = DateTime.Now;
                                mrdetail.ModifyOrderDetail(info);
                            }
                            else
                            {
                                info.CreateDate = DateTime.Now;
                                info.ChangeDate = DateTime.Now;
                                mrdetail.CreateNew(info);
                            }                            
                        }
                    }
                    //库存更新
                    OrderInventoryProcess(OrderID, orderInfo.OrderType, orderInfo.OperatorID, oldOrderDetail);
                }

                #endregion

                //List<OrderInfo> miList = mr.GetAllOrder();//刷新缓存
                //Cache.Add("OrderALL", miList);
            }
            return result > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oid">订单号</param>
        /// <param name="isContact">true:返回订单所有相关信息 false:只返回订单数据</param>
        /// <returns></returns>
        public static OrderEntity GetOrderByOrderID(long oid, bool isContact = true)
        {
            OrderEntity result = new OrderEntity();
            OrderRepository mr = new OrderRepository();
            OrderInfo info = mr.GetOrderByOrderID(oid);
            result = TranslateOrderEntity(info,isContact);
            return result;
        }

        public static List<OrderEntity> GetOrderAll()
        {
            List<OrderEntity> all = new List<OrderEntity>();
            OrderRepository mr = new OrderRepository();
            List<OrderInfo> miList = mr.GetAllOrder();//Cache.Get<List<OrderInfo>>("OrderALL");
            //if (miList.IsEmpty())
            //{
            //    miList = mr.GetAllOrder();
            //    Cache.Add("OrderALL", miList);
            //}
            if (!miList.IsEmpty())
            {
                foreach (OrderInfo mInfo in miList)
                {
                    OrderEntity OrderEntity = TranslateOrderEntity(mInfo);
                    all.Add(OrderEntity);
                }
            }

            return all;

        }

        public static List<OrderEntity> GetOrderByRule(string name, int status)
        {
            List<OrderEntity> all = new List<OrderEntity>();
            OrderRepository mr = new OrderRepository();
            List<OrderInfo> miList = mr.GetOrderByRule(name, status);

            if (!miList.IsEmpty())
            {
                foreach (OrderInfo mInfo in miList)
                {
                    OrderEntity OrderEntity = TranslateOrderEntity(mInfo);
                    all.Add(OrderEntity);
                }
            }

            return all;

        }

        public static List<OrderEntity> GetOrderByKeys(string ids)
        {
            List<OrderEntity> all = new List<OrderEntity>();
            OrderRepository mr = new OrderRepository();
            List<OrderInfo> miList = mr.GetOrderByKeys(ids);

            if (!miList.IsEmpty())
            {
                foreach (OrderInfo mInfo in miList)
                {
                    OrderEntity entity = TranslateOrderEntity(mInfo);
                    all.Add(entity);
                }
            }

            return all;
        }

        public static int Remove(long gid)
        {
            OrderRepository mr = new OrderRepository();
            int i = mr.Remove(gid);
            //List<OrderInfo> miList = mr.GetAllOrder();//刷新缓存
            //Cache.Add("OrderALL", miList);
            return i;
        }

        public static int Delete(long oid)
        {
            OrderRepository mr = new OrderRepository();
            int i = mr.Delete(oid);
            //List<OrderInfo> miList = mr.GetAllOrder();//刷新缓存
            //Cache.Add("OrderALL", miList);
            return i;
        }

        #region 分页相关
        public static int GetOrderCount(string name = "", int carrierid = -1, int storageid = -1, int customerid = -1, int status = -1, int uploadstatus = -1,
            int orderstatus = -1, string ordertype = "", string orderno = "", string begindate = "", string enddate = "", int operatorid = -1, string ordersource = "", string subOrderType="")
        {
            return new OrderRepository().GetOrderCount(name, carrierid, storageid, customerid, status, uploadstatus, orderstatus, ordertype, orderno, begindate, enddate, operatorid, ordersource, subOrderType);
        }

        public static List<OrderEntity> GetOrderInfoPager(PagerInfo pager)
        {
            List<OrderEntity> all = new List<OrderEntity>();
            OrderRepository mr = new OrderRepository();
            List<OrderInfo> miList = mr.GetAllOrderInfoPager(pager);
            foreach (OrderInfo mInfo in miList)
            {
                OrderEntity carEntity = TranslateOrderEntity(mInfo);
                all.Add(carEntity);
            }
            return all;
        }

        public static List<OrderEntity> GetOrderInfoByRule(PagerInfo pager, string name = "", int carrierid = -1, int storageid = -1, int customerid = -1, int status = -1,
            int uploadstatus = -1, int orderstatus = -1, string ordertype = "", string orderno = "", string begindate = "", string enddate = "", int operatorid = -1, string ordersource = "", string subOrderType = "")
        {
            List<OrderEntity> all = new List<OrderEntity>();
            OrderRepository mr = new OrderRepository();
            List<OrderInfo> miList = mr.GetOrderInfoByRule(name, carrierid, storageid, customerid, status, uploadstatus, orderstatus, ordertype, orderno, begindate, enddate, operatorid, ordersource, subOrderType, pager);

            if (!miList.IsEmpty())
            {
                foreach (OrderInfo mInfo in miList)
                {
                    OrderEntity storeEntity = TranslateOrderEntity(mInfo);
                    all.Add(storeEntity);
                }
            }

            return all;
        }
        #endregion


        #region 订单库存处理
        /// <summary>
        /// 仓配订单 仓到店，涉及仓的库存扣减，无视门店库存。
        /// 调拨订单 仓到仓，涉及仓仓之间的库存扣减增加。
        /// 运输订单A 厂到仓，涉及仓的库存增加，无视厂的库存。
        /// 运输订单B 厂到店，均无视库存。
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="orderType">订单类型：仓配订单/调拨订单/入库单/运输订单A/运输订单B</param>
        /// <returns></returns>
        public static bool OrderInventoryProcess(long orderid, string orderType, long operatorID, List<OrderDetailInfo> oldOrderDetail = null)
        {
            InventoryRepository mr = new InventoryRepository();
            OrderEntity orderinfo = GetOrderByOrderID(orderid);
            if (orderinfo != null)
            {
                if ("CPDD".Equals(orderType))
                {
                    #region 仓配订单 仓到店，涉及仓的库存扣减，无视门店库存。
                    List<OrderDetailEntity> orderDetailList = orderinfo.orderDetailList;
                    if (orderDetailList != null && orderDetailList.Count > 0)
                    {
                        foreach (OrderDetailEntity detail in orderDetailList)
                        {
                            InventoryEntity inventory = detail.inventory;

                            if (inventory != null)
                            {
                                //#region 库存扣减
                                //InventoryInfo inventinfo = new InventoryInfo();
                                //inventinfo.Quantity = inventory.Quantity - UpdateQuantity(detail, oldOrderDetail);//仓库库存减去订单明细中出库库存
                                //inventinfo.InventoryID = inventory.InventoryID;
                                //inventinfo.ChangeDate = DateTime.Now;
                                //mr.ModifyInventoryQuantity(inventinfo);
                                //#endregion

                                //#region 库存明细增加
                                //CreateInventoryDetail(detail, orderinfo.SendStorageID, operatorID, OrderType.CPDD.ToString(), Common.InventoryType.出库.ToString());
                                //#endregion
                                deductionInventory(detail, inventory, oldOrderDetail, orderinfo, operatorID, OrderType.CPDD.ToString(), Common.InventoryType.出库.ToString());
                            }
                        }
                    }
                    #endregion                    
                }
                else if ("DBDD".Equals(orderType))
                {
                    #region 调拨订单 仓到仓，涉及仓仓之间的库存扣减增加。
                    List<OrderDetailEntity> orderDetailList = orderinfo.orderDetailList;
                    if (orderDetailList != null && orderDetailList.Count > 0)
                    {
                        foreach (OrderDetailEntity detail in orderDetailList)
                        {
                            InventoryEntity inventory = detail.inventory;

                            if (inventory != null)
                            {
                                #region 发货仓库库存扣减
                                InventoryInfo inventinfo = new InventoryInfo();
                                inventinfo.Quantity = inventory.Quantity - UpdateQuantity(detail,oldOrderDetail);//仓库库存减去订单明细中出库库存
                                inventinfo.InventoryID = inventory.InventoryID;
                                inventinfo.ChangeDate = DateTime.Now;
                                mr.ModifyInventoryQuantity(inventinfo);

                                //库存明细增加
                                CreateInventoryDetail(detail, orderinfo.SendStorageID, operatorID, OrderType.DBDD.ToString(), Common.InventoryType.出库.ToString());
                                #endregion

                                #region 收货仓库库存增加
                                //库存增加
                                CreateInventory(detail, orderinfo.ReceiverStorageID, operatorID, OrderType.DBDD.ToString());

                                //库存明细增加
                                CreateInventoryDetail(detail, orderinfo.ReceiverStorageID, operatorID, OrderType.DBDD.ToString(), Common.InventoryType.入库.ToString());
                                #endregion
                            }
                        }
                    }
                    #endregion
                    
                }
                else if ("YSDDA".Equals(orderType))
                {
                    #region 运输订单A 厂到仓，涉及仓的库存增加，无视厂的库存。
                    List<OrderDetailEntity> orderDetailList = orderinfo.orderDetailList;
                    if (orderDetailList != null && orderDetailList.Count > 0)
                    {
                        foreach (OrderDetailEntity detail in orderDetailList)
                        {
                            #region 收货仓库库存增加
                            //库存增加
                            CreateInventory(detail, orderinfo.ReceiverStorageID, operatorID, OrderType.YSDDA.ToString());
                            //库存明细增加
                            CreateInventoryDetail(detail, orderinfo.ReceiverStorageID, operatorID, OrderType.YSDDA.ToString(), Common.InventoryType.入库.ToString());
                            #endregion
                        }
                    }
                    #endregion
                }
                else if ("YSDDB".Equals(orderType))
                {
                    //运输订单B 厂到店，均无视库存。
                }
            }
            return true;
        }

        #region 库存扣减逻辑

        /// <summary>
        /// 库存扣减 查看是否当前库存是否足够
        /// </summary>
        /// <param name="detail">订单商品明细</param>
        /// <param name="inventory">订单商品对应的库存明细</param>
        public static void deductionInventory(OrderDetailEntity detail, InventoryEntity inventory, List<OrderDetailInfo> oldOrderDetail,
            OrderEntity orderinfo, long operatorID, string typedesc, string inventoryType)
        {
            if (detail != null && inventory != null)
            {
                int currentGoodsQuantity = detail.Quantity;
                int goodsQuantity = detail.Quantity;// 当前商品需要出库数量
                int inventQuantity = inventory.Quantity;//当前商品库存数量
                int quantity = inventQuantity - goodsQuantity;//库存数量-商品出库数量
                //库存数量足够
                if (quantity >= 0)
                {
                    ExecuteInventory(detail, inventory, oldOrderDetail, orderinfo, operatorID, typedesc, inventoryType);
                }
                else
                {
                    //库存不够的情况
                    List<GoodsEntity> goodsList = GoodsService.GetGoodsByRule("", -1, detail.GoodsName, detail.GoodsModel, "");
                    if (goodsList != null && goodsList.Count > 0)
                    {
                        GoodsEntity goodsEntity = goodsList[0];
                        List<InventoryEntity> inventList = InventoryService.GetInventoryByRule(goodsEntity.GoodsID, -1, "");////
                        if (inventList != null && inventList.Count > 0)
                        {
                            foreach (InventoryEntity entity in inventList)
                            {
                                //库存数量-商品数量
                                goodsQuantity = goodsQuantity - entity.Quantity;                                

                                if (goodsQuantity > 0)
                                {
                                    detail.Quantity = entity.Quantity;//优先减去库存中已有数量
                                }
                                else
                                {
                                    if (entity.Quantity > currentGoodsQuantity)
                                    {
                                        detail.Quantity = currentGoodsQuantity;
                                    }
                                    else
                                    {
                                        detail.Quantity = entity.Quantity;
                                    }                                    
                                }
                                currentGoodsQuantity = currentGoodsQuantity - detail.Quantity;//总商品数量 减去扣除库存数量
                                ExecuteInventory(detail, entity, oldOrderDetail, orderinfo, operatorID, typedesc, inventoryType);

                                ////如果商品数量足够扣除 小于0  退出循环
                                if (goodsQuantity < 0)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }

            }
        }

        /// <summary>
        /// 执行库存处理
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="inventory"></param>
        /// <param name="oldOrderDetail"></param>
        /// <param name="orderinfo"></param>
        /// <param name="operatorID"></param>
        /// <param name="typedesc"></param>
        /// <param name="inventoryType"></param>
        public static void ExecuteInventory(OrderDetailEntity detail, InventoryEntity inventory, List<OrderDetailInfo> oldOrderDetail,
            OrderEntity orderinfo, long operatorID, string typedesc, string inventoryType)
        {
            #region 库存扣减
            InventoryRepository mr = new InventoryRepository();
            InventoryInfo inventinfo = new InventoryInfo();
            inventinfo.Quantity = inventory.Quantity - UpdateQuantity(detail, oldOrderDetail);//仓库库存减去订单明细中出库库存
            inventinfo.InventoryID = inventory.InventoryID;
            inventinfo.ChangeDate = DateTime.Now;
            mr.ModifyInventoryQuantity(inventinfo);
            #endregion

            #region 库存明细增加
            CreateInventoryDetail(detail, orderinfo.SendStorageID, operatorID, typedesc, inventoryType);
            #endregion
        }

        /// <summary>
        /// 修改 如果修改出库数量
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="oldOrderDetail"></param>
        /// <returns></returns>
        public static int UpdateQuantity(OrderDetailEntity detail, List<OrderDetailInfo> oldOrderDetail)
        {
            int quantity = detail.Quantity;
            if (oldOrderDetail != null && oldOrderDetail.Count > 0)
            {
                OrderDetailInfo olddetail = oldOrderDetail.Find(p => p.ID == detail.ID);
                if (olddetail != null)
                {
                    quantity = detail.Quantity - olddetail.Quantity;
                }
            }
            return quantity;
        }

        #endregion

        

        /// <summary>
        /// 库存更新
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="StorageID"></param>
        /// <param name="operatorID"></param>
        /// <param name="typedesc"></param>
        private static void CreateInventory(OrderDetailEntity detail, int StorageID, long operatorID,string typedesc)
        {
            InventoryRepository mr = new InventoryRepository();
            InventoryInfo info = new InventoryInfo();
            info.GoodsID = detail.GoodsID;
            info.StorageID = StorageID;
            info.Quantity = detail.Quantity;
            if (detail.GoodsID > 0)
            {
                info.CustomerID = detail.goods != null ? detail.goods.CustomerID : 0;
            }
            if (detail.InventoryID > 0)
            {
                info.CustomerID = detail.inventory != null ? detail.inventory.CustomerID : 0;
            }
            info.InventoryType = Common.InventoryType.入库.ToString();
            info.BatchNumber = detail.BatchNumber;
            info.ProductDate = detail.ProductDate;
            info.InventoryDate = DateTime.Now;
            info.UnitPrice = 0;
            info.Remark = typedesc;
            info.OperatorID = operatorID;
            info.CreateDate = DateTime.Now;
            info.ChangeDate = DateTime.Now;
            info.Quantity = detail.Quantity;
            //插入库存
            mr.CreateNew(info);

        }

        /// <summary>
        /// 库存明细更新
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="StorageID"></param>
        /// <param name="OperatorID"></param>
        /// <param name="typedesc"></param>
        /// <param name="inventoryType"></param>
        private static void CreateInventoryDetail(OrderDetailEntity detail, int StorageID, long OperatorID,string typedesc,string inventoryType)
        {
            InventoryDetailRepository mrDetail = new InventoryDetailRepository();
            InventoryDetailInfo infoDetail = new InventoryDetailInfo();
            infoDetail.GoodsID = detail.GoodsID;
            infoDetail.StorageID = StorageID;
            infoDetail.Quantity = detail.Quantity;
            if (detail.GoodsID > 0)
            {
                infoDetail.CustomerID = detail.goods != null ? detail.goods.CustomerID : 0;
            }
            if(detail.InventoryID>0)
            {
                infoDetail.CustomerID = detail.inventory != null ? detail.inventory.CustomerID : 0;    
            }
            infoDetail.InventoryType = inventoryType;
            infoDetail.BatchNumber = detail.BatchNumber;
            infoDetail.ProductDate = detail.ProductDate;
            infoDetail.InventoryDate = DateTime.Now;
            infoDetail.UnitPrice =  0;
            infoDetail.Remark = typedesc;
            infoDetail.OperatorID = OperatorID;
            infoDetail.CreateDate = DateTime.Now;
            infoDetail.ChangeDate = DateTime.Now;
            mrDetail.CreateNew(infoDetail);
        }
        #endregion

        #region 订单审核拒绝
        public static bool OrderBack(int orderid, string type, long operatorID)
        {
            // 更新订单状态
            OrderService.UpdateOrderStatus(orderid,5);
            return true;
        }
        #endregion

        #region Excel导入

        /// <summary>
        /// Costa订单导入
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static List<ImportOrderEntity> GetCostaImportList(DataSet ds)
        {
             List<ImportOrderEntity> list = new List<ImportOrderEntity>();

             if (ds != null && ds.Tables.Count > 0)
             {
                 foreach (DataTable dt in ds.Tables)
                 {
                     if (dt != null && dt.Rows.Count > 0)
                     {
                         for (int i = 0; i < dt.Rows.Count; i++)
                         {
                             if (!dt.Rows[i]["单位"].ToString().Equals("合计") && !string.IsNullOrEmpty(dt.Rows[i]["品名"].ToString()))
                             {
                                 //品号	品名	包装规格	单位	数量	单位号	单位名称	订单编号	采购单日	应到货日 备注	订单备注
                                 ImportOrderEntity entity = new ImportOrderEntity();
                                 GoodsEntity goodsEntity = getGoodsModelByGoods("", dt.Rows[i]["品名"].ToString(), "", dt.Rows[i]["包装规格"].ToString());
                                 entity.GoodsID = goodsEntity == null ? "-1" : goodsEntity.GoodsID.ToString();
                                 entity.GoodsNo = dt.Rows[i]["品号"].ToString();
                                 entity.GoodsName = dt.Rows[i]["品名"].ToString();
                                 entity.GoodsModel = dt.Rows[i]["包装规格"].ToString();
                                 entity.Units = dt.Rows[i]["单位"].ToString();
                                 entity.Quantity = dt.Rows[i]["数量"].ToString();
                                 entity.ShopNo = dt.Rows[i]["单位号"].ToString();
                                 entity.ShopName = dt.Rows[i]["单位名称"].ToString();
                                 entity.OrderNo = dt.Rows[i]["订单编号"].ToString();
                                 entity.OrderDate = dt.Rows[i]["采购单日"].ToString();
                                 entity.YyDate = dt.Rows[i]["应到货日"].ToString();
                                 entity.Remark = dt.Rows[i]["备注"].ToString() + dt.Rows[i]["订单备注"].ToString();
                                 List<ReceiverEntity> receiver = new List<ReceiverEntity>();
                                 entity.receiver = ReceiverService.GetReceiverByRule(entity.ShopName, "", "", 1);
                                 entity.CustomerID = -1;
                                 if (entity.receiver != null && entity.receiver.Count > 0)
                                 {
                                     entity.CustomerID = entity.receiver[0].CustomerID;
                                 }
                                 list.Add(entity);
                             }
                         }
                     }
                 }
             }
             return list;
        }
        /// <summary>
        ///  商超订单导入
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static List<ImportOrderEntity> GetImportList(DataSet ds)
        {
            List<ImportOrderEntity> list = new List<ImportOrderEntity>();

            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[i]["品名"].ToString()))
                            {
                                ImportOrderEntity entity = new ImportOrderEntity();              
                                entity.ImportType = dt.TableName;
                                entity.OrderDate = dt.Rows[i]["日期"].ToString();
                                entity.CustomerName = dt.Rows[i]["客户名称"].ToString();
                                entity.ShopName = dt.Rows[i]["门店"].ToString();
                                entity.OrderNo = dt.Rows[i]["订单号"].ToString();
                                entity.GoodsName = dt.Rows[i]["品名"].ToString();
                                entity.Units = dt.Rows[i]["单位"].ToString();
                                entity.Quantity = dt.Rows[i]["数量"].ToString();
                                entity.Address = dt.Rows[i]["地址"].ToString();
                                entity.BarCode = dt.Rows[i]["条码"].ToString();
                                //entity.SalesMan = dt.Rows[i]["销售人员"].ToString();
                                entity.PromotionMan = dt.Rows[i]["促销人员"].ToString();
                                entity.YyDate = dt.Rows[i]["预约时间"].ToString();
                                entity.Remark = dt.Rows[i]["备注"].ToString();
                                GoodsEntity goodsEntity = getGoodsModelByGoods(entity.BarCode, entity.GoodsName, entity.GoodsNo, entity.GoodsModel);
                                entity.GoodsID = goodsEntity == null ? "-1" : goodsEntity.GoodsID.ToString();

                                List<ReceiverEntity> receiver = new List<ReceiverEntity>();
                                entity.receiver = ReceiverService.GetReceiverByRule(entity.CustomerName + entity.ShopName, "", "", 1);
                                entity.CustomerID = -1;
                                if (entity.receiver != null && entity.receiver.Count > 0)
                                {
                                    entity.CustomerID = entity.receiver[0].CustomerID;
                                }                                
                                list.Add(entity);
                            }
                        }
                    }
                }
            }
            return list;
        }
        #endregion

        #region 订单生成
        /// <summary>
        /// 根据门店信息获取线路
        /// </summary>
        /// <param name="receiverName"></param>
        /// <returns></returns>
        private static int GetLineID(string receiverName)
        {
            int lineID=0;
            List<LineEntity> list = LineService.GetLineAll();
            if (list != null && list.Count > 0)
            {
                LineEntity entity = list.Find(p => p.ReceiverName.Equals(receiverName));
                if (entity != null)
                {
                    lineID = entity.LineID;
                }
            }
            return lineID;
        }


        /// <summary>
        /// 订单导入后生成订单
        /// </summary>
        /// <param name="list"></param>
        /// <param name="ordersource">订单来源：大润发/家乐福/卜蜂莲花/上蔬永辉/Costa/常规订单</param>
        /// <param name="orderType">订单类型：仓配订单/调拨订单/入库单/运输订单A/运输订单B</param>
        /// <param name="OperatorID"></param>
        /// <returns></returns>
        public static ImportIDSEntity GenerateOrder(List<ImportOrderEntity> list, string ordersource, string orderType, long OperatorID)
        {
            ImportIDSEntity idsEntity = new ImportIDSEntity();
            string ids = "";

            List<String> listGroup = groupByName(list);//分组处理

            foreach (String str in listGroup)
            {
                List<ImportOrderEntity> listNew = list.FindAll(p => str.Equals(p.CustomerName + p.ShopName + p.ImportType));

                if (listNew != null && listNew.Count > 0)
                {
                    ImportOrderEntity orderEntity = listNew[0];
                    OrderInfo info = new OrderInfo();
                    #region 订单信息
                    List<ReceiverEntity> receiver = orderEntity.receiver;
                    ////Costa特殊处理
                    //if (ordersource.Equals(OrderSource.Costa.ToString()))
                    //{
                    //    receiver = ReceiverService.GetReceiverByRule("", orderEntity.ShopNo, "", 1);
                    //}
                    //else
                    //{
                    //    receiver = ReceiverService.GetReceiverByRule(orderEntity.CustomerName + orderEntity.ShopName, "", "", 1);
                    //}

                    if (receiver != null && receiver.Count > 0)
                    {
                        info.CustomerID = receiver[0].CustomerID;
                        info.ReceiverID = receiver[0].ReceiverID;
                        info.SendStorageID = receiver[0].DefaultStorageID;
                        info.CarrierID = receiver[0].DefaultCarrierID;
                    }
                    info.OrderNo = GetOrderNo();//entity.OrderNo;
                    info.MergeNo = "";
                    info.OrderType = orderType;//商超订单、COSTA订单都为仓配订单
                    info.ReceiverStorageID = 0;
                    info.OrderDate = string.IsNullOrEmpty(orderEntity.OrderDate) ? DefaultDateTime : DateTime.Parse(orderEntity.OrderDate);
                    info.SendDate = string.IsNullOrEmpty(orderEntity.YyDate) ? DefaultDateTime : DateTime.Parse(orderEntity.YyDate);
                    info.configPrice = 0;
                    info.configHandInAmt = 0;
                    info.configSortPrice = 0;
                    info.configCosting = 0;
                    info.configHandOutAmt = 0;
                    info.configSortCosting = 0;
                    info.TempType = "";
                    info.OrderStatus = 0;
                    info.UploadStatus = 0;
                    info.Status = 1;
                    info.Remark = orderEntity.Remark;
                    info.OperatorID = OperatorID.ToString().ToInt(0);
                    info.CreateDate = DateTime.Now;
                    info.ChangeDate = DateTime.Now;
                    info.IsImport = "T";
                    info.OrderSource = ordersource;

                    if (!String.IsNullOrEmpty(orderEntity.ImportType))
                    {
                        if (orderEntity.ImportType.Contains(TypeDesc.送货单.ToString()))
                        {
                            info.SubOrderType = SubOrderType.SHD.ToString();
                        }
                        if (orderEntity.ImportType.Contains(TypeDesc.补损单.ToString()))
                        {
                            info.SubOrderType = SubOrderType.BSD.ToString();
                        }
                    }
                    else //Costa订单导入 只有送货单
                    {
                        info.SubOrderType = SubOrderType.SHD.ToString();
                    }
                    info.SalesMan = orderEntity.SalesMan;
                    info.PromotionMan = orderEntity.PromotionMan;

                    info.LineID = GetLineID(orderEntity.CustomerName + orderEntity.ShopName);//线路
                    OrderRepository or = new OrderRepository();
                    long orderid = or.CreateNew(info);

                    #endregion
                    if (!String.IsNullOrEmpty(orderEntity.ImportType))
                    {
                        if (orderEntity.ImportType.Contains(TypeDesc.送货单.ToString()))
                        {
                            idsEntity.SHDIds += orderid + ",";
                        }
                        if (orderEntity.ImportType.Contains(TypeDesc.补损单.ToString()))
                        {
                            idsEntity.BSDIds += orderid + ",";
                        }
                    }
                    else
                    {
                        idsEntity.SHDIds += orderid + ",";
                    }
                    
                    ids += orderid + ",";

                    foreach (ImportOrderEntity entity in listNew)
                    {
                        if (orderid > 0)
                        {
                            #region 订单明细信息
                            OrderDetailInfo infodetail = new OrderDetailInfo();
                            GoodsEntity goods = GoodsService.GetGoodsEntityById(long.Parse(entity.GoodsID));
                            //List<GoodsEntity> listGoods = GoodsService.GetGoodsByRule("", 1, entity.BarCode);
                            //if (listGoods != null && listGoods.Count > 0)
                            //{
                            //    goods = listGoods[0];
                            //}
                            infodetail.OrderID = orderid.ToString().ToInt(0);
                            infodetail.GoodsID = goods.GoodsID;

                            List<InventoryEntity> listInventory = InventoryService.GetInventoryByRule(goods.GoodsID, info.SendStorageID, "");
                            InventoryEntity entityInv = null;
                            if (listInventory != null && listInventory.Count > 0)
                            {
                                entityInv = listInventory[0];
                            }

                            infodetail.InventoryID = entityInv != null ? entityInv.InventoryID : -1;//info.SendStorageID ;
                            infodetail.GoodsNo = string.IsNullOrEmpty(entity.GoodsNo) ? goods.GoodsNo : entity.GoodsNo;
                            infodetail.GoodsName = string.IsNullOrEmpty(entity.GoodsName) ? goods.GoodsName : entity.GoodsName;
                            infodetail.GoodsModel = string.IsNullOrEmpty(entity.GoodsModel) ? goods.GoodsModel : entity.GoodsModel;
                            infodetail.Quantity = entity.Quantity.ToInt(0);
                            infodetail.Units = string.IsNullOrEmpty(entity.Units) ? goods.Units : entity.Units;
                            infodetail.Weight = goods.Weight;
                            infodetail.TotalWeight = "";


                            infodetail.BatchNumber = entityInv != null ? entityInv.BatchNumber : "";
                            infodetail.ProductDate = string.IsNullOrEmpty(entity.OrderDate) ? DefaultDateTime : DateTime.Parse(entity.OrderDate);//DefaultDateTime;
                            infodetail.ExceedDate = Datehelper.getDateTime(DateTime.Parse(entity.OrderDate), goods.exDate.ToInt(0), goods.exUnits);
                            infodetail.CreateDate = DateTime.Now;
                            infodetail.ChangeDate = DateTime.Now;
                            OrderDetailRepository ordetail = new OrderDetailRepository();
                            long id = ordetail.CreateNew(infodetail);
                            #endregion
                        }
                        //订单库存扣减处理
                        OrderInventoryProcess(orderid.ToString().ToInt(0), orderType, OperatorID);
                    }
                }
            }
            return idsEntity;
        }

        /// <summary>
        /// 订单导入 按照门店信息分组
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private static List<String> groupByName(List<ImportOrderEntity> list)
        {
            List<String> listGroup = new List<string>();

            List<ImportGroup> strs = new List<ImportGroup>();
            foreach (ImportOrderEntity entity in list)
            {
                ImportGroup group = new ImportGroup();
                group.name = entity.CustomerName + entity.ShopName + entity.ImportType;
                strs.Add(group);
            }

            var GroupList = strs.GroupBy(x => x.name).OrderBy(x => x.Key);
            foreach (var u in GroupList)
            {
                if (!string.IsNullOrEmpty(u.Key))
                {
                    listGroup.Add(u.Key);
                }
            }
            return listGroup;
        }

        /// <summary>
        /// 根据商品信息 获取数据中已有商品信息
        /// </summary>
        /// <param name="barCode"></param>
        /// <param name="goodsName"></param>
        /// <param name="goodsNo"></param>
        /// <param name="goodsModel"></param>
        /// <returns></returns>
        public static GoodsEntity getGoodsModelByGoods(string barCode, string goodsName, string goodsNo, string goodsModel)
        {
            GoodsEntity entity = null;

            List<GoodsEntity> listGoods = string.IsNullOrEmpty(barCode) ? null : GoodsService.GetGoodsByRule("", 1, "", "", barCode);
            if (listGoods != null && listGoods.Count > 0)
            {
                entity = listGoods[0];
            }
            if (entity == null)
            {
                List<GoodsEntity> listg = GoodsService.GetGoodsByRule("", 1, goodsName, goodsModel, "");
                if (listg != null && listg.Count > 0)
                {
                    entity = listg[0];
                }
            }
            if (entity == null)
            {
                List<GoodsEntity> listg = GoodsService.GetGoodsByRule("", 1, goodsName, "", "");
                if (listg != null && listg.Count > 0)
                {
                    entity = listg[0];
                }
            }
            return entity;
        }


        /// <summary>
        /// 从Excel中读取常规订单数据
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static List<RegularOrderEntity> GetRegularImportList(DataSet ds)
        {
            List<RegularOrderEntity> list = new List<RegularOrderEntity>();

            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            RegularOrderEntity entity = new RegularOrderEntity();
                            if (!string.IsNullOrEmpty(dt.Rows[i]["商品名称"].ToString()) && !string.IsNullOrEmpty(dt.Rows[i]["发货仓库"].ToString()))
                            {
                                entity.orderDate = dt.Rows[i]["下单时间"].ToString();
                                entity.sendDate = dt.Rows[i]["要求送达时间"].ToString();
                                entity.sendStorageName = dt.Rows[i]["发货仓库"].ToString();
                                entity.Temp = dt.Rows[i]["温区"].ToString();
                                entity.CarrierName = dt.Rows[i]["承运商"].ToString();
                                entity.CustomerName = dt.Rows[i]["客户名称"].ToString();
                                entity.ReceiverName = dt.Rows[i]["送达方(门店/仓库)"].ToString();
                                entity.GoodsName = dt.Rows[i]["商品名称"].ToString();
                                entity.Units = dt.Rows[i]["单位"].ToString();
                                entity.Quantity = dt.Rows[i]["数量"].ToString();
                                entity.Remark = dt.Rows[i]["备注"].ToString();
                                GoodsEntity goodsEntity = getGoodsModelByGoods("", entity.GoodsName, "", "");
                                entity.GoodsID = goodsEntity == null ? "-1" : goodsEntity.GoodsID.ToString();
                                list.Add(entity);
                            }
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 常规订单生成
        /// </summary>
        /// <param name="list"></param>
        /// <param name="ordersource">订单来源：大润发/家乐福/卜蜂莲花/上蔬永辉/Costa/常规订单</param>
        /// <param name="orderType">订单类型：仓配订单/调拨订单/入库单/运输订单A/运输订单B</param>
        /// <param name="OperatorID"></param>
        /// <returns></returns>
        public static ImportIDSEntity GenerateRegularOrder(List<RegularOrderEntity> list, string ordersource, string OrderType, long OperatorID)
        {
            ImportIDSEntity idsEntity = new ImportIDSEntity();
            string ids = "";
            if (list != null && list.Count > 0)
            {
                foreach (RegularOrderEntity entity in list)
                {
                    OrderInfo info = new OrderInfo();
                    //							
                    //发货仓库
                    List<StorageEntity> listStorage = StorageService.GetStorageByRule(entity.sendStorageName, -1);
                    StorageEntity storageEntity = listStorage != null && listStorage.Count > 0 ? listStorage[0] : null;
                    info.SendStorageID = storageEntity != null ? storageEntity.StorageID : 0;//发货仓库  

                    //承运商
                    List<CarrierEntity> listCarrier = CarrierService.GetCarrierByRule(entity.CarrierName, -1);
                    CarrierEntity carrierEntity = listCarrier != null && listCarrier.Count > 0 ? listCarrier[0] : null;
                    info.CarrierID = carrierEntity != null ? carrierEntity.CarrierID : 0;//承运商

                    //客户名称
                    List<CustomerEntity> listCustomer = CustomerService.GetCustomerByRule(entity.CustomerName, -1);
                    CustomerEntity customerEntity = listCustomer != null && listCustomer.Count > 0 ? listCustomer[0] : null;
                    info.CustomerID = customerEntity != null ? customerEntity.CustomerID : 0;

                    //送达方(门店/仓库)
                    List<ReceiverEntity> listReceiver = ReceiverService.GetReceiverByRule(entity.ReceiverName, "", "", -1);//门店信息
                    ReceiverEntity receiverEntity = listReceiver != null && listReceiver.Count > 0 ? listReceiver[0] : null;
                    info.ReceiverID = receiverEntity != null ? receiverEntity.ReceiverID : 0;

                    List<StorageEntity> listReceiverStorage = StorageService.GetStorageByRule(entity.ReceiverName, -1);//仓库信息
                    StorageEntity receiverStorageEntity = listReceiverStorage != null && listReceiverStorage.Count > 0 ? listReceiverStorage[0] : null;
                    info.ReceiverStorageID = receiverStorageEntity != null ? receiverStorageEntity.StorageID : 0;

                    info.OrderNo = GetOrderNo();
                    info.MergeNo = "";
                    info.OrderType = OrderType;
                    info.SubOrderType = SubOrderType.SHD.ToString();
                    info.OrderDate =DateTime.Parse( entity.orderDate);//下单时间
                    info.SendDate = DateTime.Parse(entity.orderDate);//要求送达时间
                    info.configPrice = 0;
                    info.configHandInAmt = 0;
                    info.configSortPrice = 0;
                    info.configCosting = 0;
                    info.configHandOutAmt = 0;
                    info.configSortCosting = 0;
                    info.TempType = entity.Temp;//温区
                    info.OrderStatus = 0;
                    info.UploadStatus = 0;
                    info.Status = 1;
                    info.Remark = entity.Remark;
                    info.OperatorID = OperatorID.ToString().ToInt(0);
                    info.CreateDate = DateTime.Now;
                    info.ChangeDate = DateTime.Now;

                    info.IsImport = "F";//常规订单走普通订单模版
                    info.OrderSource = ordersource;
                    info.SalesMan = "";
                    info.PromotionMan = "";
                    info.LineID = GetLineID(entity.CustomerName + (receiverEntity != null ? receiverEntity.ReceiverName : ""));//线路
                    OrderRepository or = new OrderRepository();
                    long orderid = or.CreateNew(info);

                    idsEntity.SHDIds += orderid + ",";
                    if (orderid > 0)
                    {
                        #region 订单明细信息
                        OrderDetailInfo infodetail = new OrderDetailInfo();
                        GoodsEntity goods = GoodsService.GetGoodsEntityById(long.Parse(entity.GoodsID));
                        //List<GoodsEntity> listGoods = GoodsService.GetGoodsByRule("", 1, entity.GoodsName);//商品名称	数量	单位	备注
                        //if (listGoods != null && listGoods.Count > 0)
                        //{
                        //    goods = listGoods[0];
                        //}
                        List<InventoryEntity> listInventory = InventoryService.GetInventoryByRule(goods.GoodsID, info.SendStorageID, "");
                        InventoryEntity inventoryEntity = null;
                        if (listInventory != null && listInventory.Count > 0)
                        {
                            inventoryEntity = listInventory[0];
                        }
                        infodetail.OrderID = orderid.ToString().ToInt(0);
                        infodetail.GoodsID = goods.GoodsID;
                        infodetail.InventoryID = inventoryEntity != null ? inventoryEntity.InventoryID : -1;
                        infodetail.GoodsNo = string.IsNullOrEmpty(goods.GoodsNo) ? goods.GoodsNo : goods.GoodsNo;
                        infodetail.GoodsName = string.IsNullOrEmpty(goods.GoodsName) ? goods.GoodsName : goods.GoodsName;
                        infodetail.GoodsModel = string.IsNullOrEmpty(goods.GoodsModel) ? goods.GoodsModel : goods.GoodsModel;
                        infodetail.Quantity = entity.Quantity.ToInt(0);
                        infodetail.Units = string.IsNullOrEmpty(entity.Units) ? goods.Units : entity.Units;
                        infodetail.Weight = goods.Weight;
                        
            
                        infodetail.TotalWeight = "";
                        if (inventoryEntity != null)
                        {
                            infodetail.BatchNumber = inventoryEntity.BatchNumber;
                            infodetail.ProductDate = inventoryEntity.ProductDate;//DefaultDateTime;
                        }
                        else
                        {
                            infodetail.BatchNumber = "";
                            infodetail.ProductDate = DefaultDateTime;
                        }
                        infodetail.ExceedDate = Datehelper.getDateTime(inventoryEntity.ProductDate, goods.exDate.ToInt(0), goods.exUnits);

                        infodetail.CreateDate = DateTime.Now;
                        infodetail.ChangeDate = DateTime.Now;
                        OrderDetailRepository ordetail = new OrderDetailRepository();
                        long id = ordetail.CreateNew(infodetail);
                        #endregion
                    }
                    //订单库存扣减处理
                    OrderInventoryProcess(orderid.ToString().ToInt(0), OrderType, OperatorID);
                }
            }
            return idsEntity;
        }

        #endregion

        #region 基础数据导入
        /// <summary>
        ///  商超订单导入
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static void GetBaseImportList(DataSet ds)
        {
            List<ImportOrderEntity> list = new List<ImportOrderEntity>();

            if (ds != null && ds.Tables.Count > 0)
            {
                List<GoodsEntity> listG = GoodsService.GetAllGoods();
                foreach (DataTable dt in ds.Tables)
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //所属客户    仓库编号    仓库名称    商品编号    商品名称    规格型号    单位    批次号    生产日期    到期日期    
                            //余量    破损    总库存    成本    售价    重量    总总量    最后更新时间
                            //GoodsEntity entity = new GoodsEntity();
                            //entity.TypeCode = "商品";
                            //entity.CustomerID = 14;
                            //entity.GoodsNo = dt.Rows[i]["商品编号"].ToString();
                            //entity.GoodsName = dt.Rows[i]["商品名称"].ToString();

                            //entity.GoodsModel = dt.Rows[i]["规格型号"].ToString();
                            //entity.Weight = dt.Rows[i]["重量"].ToString();
                            //entity.Size = "";
                            //entity.Units = dt.Rows[i]["单位"].ToString();
                            //entity.Costing = decimal.Parse(dt.Rows[i]["成本"].ToString());
                            //entity.SalePrice = decimal.Parse(dt.Rows[i]["售价"].ToString());
                            //entity.Torr = "";
                            //entity.exDate = "2";
                            //entity.exUnits = "年";
                            //entity.Status = 1;
                            //GoodsService.ModifyGoods(entity);

                            GoodsEntity gEntity = listG.Find(p => p.GoodsNo.Equals(dt.Rows[i]["商品编号"].ToString()));

                            InventoryInfo info = new InventoryInfo();
                            info.BatchNumber = dt.Rows[i]["批次号"].ToString();
                            info.CreateDate = DateTime.Now;
                            info.ChangeDate = DateTime.Now;
                            info.CustomerID = 14;
                            info.GoodsID = gEntity != null ? gEntity.GoodsID : 0;
                            info.InventoryDate = DateTime.Now;
                            info.InventoryStatus = "";
                            info.InventoryType = InventoryType.入库.ToString();
                            info.OperatorID = 1;
                            info.ProductDate = DateTime.Parse(dt.Rows[i]["生产日期"].ToString());
                            info.Quantity = int.Parse(dt.Rows[i]["总库存"].ToString());
                            info.StorageID = 13;
                            info.UnitPrice = 0;
                            InventoryRepository mr = new InventoryRepository();
                            long inventID = mr.CreateNew(info);

                            InventoryDetailInfo dinfo = new InventoryDetailInfo();
                            dinfo.BatchNumber = dt.Rows[i]["批次号"].ToString();
                            dinfo.CreateDate = DateTime.Now;
                            dinfo.ChangeDate = DateTime.Now;
                            dinfo.CustomerID = 14;
                            dinfo.GoodsID = gEntity != null ? gEntity.GoodsID : 0;
                            dinfo.InventoryDate = DateTime.Now;
                            dinfo.InventoryType = InventoryType.入库.ToString();
                            dinfo.OrderNo = "";
                            dinfo.OrderType = OrderType.RKD.ToString();;
                            dinfo.ProductDate = DateTime.Parse(dt.Rows[i]["生产日期"].ToString());
                            dinfo.Quantity = int.Parse(dt.Rows[i]["总库存"].ToString());
                            dinfo.StorageID = 13;
                            dinfo.UnitPrice = 0;
                            InventoryDetailRepository dmr = new InventoryDetailRepository();
                            dmr.CreateNew(dinfo);
                        }
                    }
                }
            }
        }
        #endregion

        #region 出入库 生成订单

        public static void CreateOrderByInventory(List<InventoryInfo> List, string tempType = "")
        {
            InventoryInfo entity = null;
            if (List != null && List.Count > 0)
            {
                entity = List[0];
                OrderRepository mr = new OrderRepository();
                OrderInfo info = new OrderInfo();
                info.OrderNo = GetOrderNo();
                info.MergeNo = "";
                info.OrderType = OrderType.RKD.ToString();
                info.ReceiverID = -1;
                info.CustomerID = entity.CustomerID;
                info.SendStorageID = entity.StorageID;
                info.ReceiverStorageID = entity.StorageID;
                info.CarrierID = -1;
                info.OrderDate = Convert.ToDateTime(entity.InventoryDate);
                info.SendDate = DateTime.Now;

                info.configPrice = 0;
                info.configHandInAmt = 0;
                info.configSortPrice = 0;
                info.configCosting = 0;
                info.configHandOutAmt = 0;
                info.configSortCosting = 0;

                info.TempType = tempType;
                info.OrderStatus = 0;
                info.UploadStatus = 0;
                info.Status = 0;
                info.Remark = entity.Remark;
                info.OperatorID = entity.OperatorID.ToString().ToInt(0);

                info.OrderSource = OrderType.RKD.ToString();
                info.SalesMan = "";
                info.PromotionMan = "";
                info.CreateDate = entity.CreateDate;
                info.ChangeDate = entity.ChangeDate;
                long orderid = mr.CreateNew(info);

                foreach (InventoryInfo item in List)
                {
                    OrderDetailInfo dInfo = new OrderDetailInfo();
                    dInfo.OrderID = orderid.ToString().ToInt(0);
                    dInfo.GoodsID = item.GoodsID;
                    dInfo.InventoryID = item.InventoryID;
                    GoodsEntity gEntity = GoodsService.GetGoodsEntityById(item.GoodsID);
                    dInfo.GoodsNo = gEntity.GoodsNo;
                    dInfo.GoodsName = gEntity.GoodsName;
                    dInfo.GoodsModel = gEntity.GoodsModel;
                    dInfo.Quantity = item.Quantity;
                    dInfo.Units = gEntity.Units;
                    dInfo.Weight = gEntity.Weight;
                    dInfo.TotalWeight = "";
                    dInfo.BatchNumber = item.BatchNumber;
                    dInfo.ProductDate = Convert.ToDateTime(item.ProductDate);
                    dInfo.ExceedDate = Datehelper.getDateTime(info.OrderDate, gEntity.exDate.ToInt(0), gEntity.exUnits);
                    dInfo.CreateDate = DateTime.Now;
                    dInfo.ChangeDate = DateTime.Now;
                    OrderDetailRepository mrd = new OrderDetailRepository();
                    mrd.CreateNew(dInfo);
                }
            }
            
        }
        #endregion
    }
}
