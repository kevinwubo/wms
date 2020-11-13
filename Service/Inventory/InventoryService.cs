using Common;
using DataRepository.DataAccess.BaseData;
using DataRepository.DataModel;
using Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Helper;
using Service.BaseBiz;
using System.Data;

namespace Service.Inventory
{
    /// <summary>
    /// 库存处理
    /// </summary>
    public class InventoryService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="StorageID">仓库ID</param>
        /// <param name="inventoryDate">出入库时间</param>
        /// <param name="json">出入库明细数据</param>
        /// <param name="OperatorID">操作员ID</param>
        /// <param name="type">In/Out</param>
        /// <returns></returns>
        public static bool ModifyInventory(int StorageID, string inventoryDate, int needCount, string json, long OperatorID, string tempType, OperatorType type)
        {

            try
            {
                InvGoodsEntity jsonInfo = null;
                if (!string.IsNullOrEmpty(json))
                {
                    try
                    {
                        jsonInfo = (InvGoodsEntity)JsonHelper.FromJson(json, typeof(InvGoodsEntity));
                    }
                    catch (Exception ex)
                    {
                        string str = ex.ToString();
                    }
                }
                List<InventoryInfo> listInv = new List<InventoryInfo>();
                if (jsonInfo != null && jsonInfo.listGoods != null && jsonInfo.listGoods.Count > 0)
                {
                    foreach (InvGoodsDetailEntity entity in jsonInfo.listGoods)
                    {
                        InventoryInfo info = new InventoryInfo();
                        if (entity != null)
                        {
                            info.GoodsID = entity.GoodsID;
                            info.StorageID = StorageID;
                            info.Quantity = entity.Quantity;
                            info.CustomerID = entity.CustomerID;
                            info.InventoryType = Common.InventoryType.入库.ToString();
                            info.BatchNumber = entity.BatchNumber.Trim();
                            info.ProductDate = DateTime.Parse(entity.ProductDate);
                            info.InventoryDate = DateTime.Parse(inventoryDate);
                            info.UnitPrice = entity.UnitPrice;
                            info.Remark = entity.Remark;
                            info.OperatorID = OperatorID;
                            info.CreateDate = DateTime.Now;
                            info.ChangeDate = DateTime.Now;           
                            listInv.Add(info);                            
                        }
                    }

                    //生成入库单
                    InventoryExecOrder orderExec= OrderService.CreateOrderByInventory(listInv, tempType);

                    if (listInv != null && listInv.Count > 0)
                    {
                        foreach (InventoryInfo info in listInv)
                        {
                            InventoryRepository mr = new InventoryRepository();
                            //插入库存
                            mr.CreateNew(info);

                            //库存明细保存
                            CreateInventoryDetail(info,orderExec, StorageID, DateTime.Parse(inventoryDate), OperatorID);
                            
                        }
                    }

   
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog("入库保存异常", ex.ToString());
                return false;
            }
            return true;
        }

        /// <summary>
        /// 库存明细入库
        /// </summary>
        /// <param name="entity">库存明细</param>
        /// <param name="StorageID"></param>
        /// <param name="inventoryDate"></param>
        /// <param name="OperatorID"></param>
        public static void CreateInventoryDetail(InventoryInfo entity, InventoryExecOrder orderExec, int StorageID, DateTime inventoryDate, long OperatorID)
        {
            InventoryDetailRepository mrDetail = new InventoryDetailRepository();
            InventoryDetailInfo infoDetail = new InventoryDetailInfo();
            if (entity != null)
            {
                infoDetail.OrderType = OrderType.RKD.ToString();
                infoDetail.OrderNo = orderExec.OrderNo;
                infoDetail.OrderID = orderExec.OrderID;
                infoDetail.GoodsID = entity.GoodsID;
                infoDetail.StorageID = StorageID;
                infoDetail.Quantity = entity.Quantity;
                infoDetail.CustomerID = entity.CustomerID;
                infoDetail.InventoryType = Common.InventoryType.入库.ToString();
                infoDetail.BatchNumber = entity.BatchNumber;
                infoDetail.ProductDate = entity.ProductDate;
                infoDetail.InventoryDate = inventoryDate;
                infoDetail.UnitPrice = entity.UnitPrice;
                infoDetail.Remark = entity.Remark;
                infoDetail.OperatorID = OperatorID;
                infoDetail.CreateDate = DateTime.Now;
                infoDetail.ChangeDate = DateTime.Now;
                mrDetail.CreateNew(infoDetail);
            }
        }

        public static InventoryEntity GetInventoryEntityById(long cid)
        {
            InventoryEntity result = new InventoryEntity();
            InventoryRepository mr = new InventoryRepository();
            InventoryInfo info = mr.GetInventoryByKey(cid);
            result = TranslateInventoryEntity(info);
            //获取联系人信息
            return result;
        }

        /// <summary>
        /// 盘点使用 更新库存状态
        /// </summary>
        /// <param name="InventoryID"></param>
        /// <param name="isLock">T/F</param>
        /// <param name="InventoryStatus">库存状态(盘点中)</param>
        /// <returns></returns>
        public static bool ModifyInventoryStatus(int InventoryID, string isLock, string InventoryStatus)
        {
            InventoryRepository mr = new InventoryRepository();
            InventoryInfo info=new InventoryInfo();
            info.InventoryID=InventoryID;
            info.IsLock = isLock;
            info.InventoryStatus = isLock;
            info.ChangeDate = DateTime.Now;
            return mr.ModifyInventoryStatus(info) > 0;
        }

        private static InventoryInfo TranslateInventoryInfo(InventoryEntity entity)
        {
            InventoryInfo info = new InventoryInfo();
            if (entity != null)
            {                
                info.InventoryID = entity.InventoryID;
                info.GoodsID = entity.GoodsID;
                info.StorageID = entity.StorageID;
                info.Quantity = entity.Quantity;
                info.CustomerID = entity.CustomerID;
                info.InventoryType = entity.InventoryType;

                info.BatchNumber = entity.BatchNumber;
                info.ProductDate = entity.ProductDate;
                info.InventoryDate = entity.InventoryDate;
                info.UnitPrice = entity.UnitPrice;
                info.Remark = entity.Remark;
                info.IsLock = entity.IsLock;
                info.InventoryStatus = entity.InventoryStatus;
                info.OperatorID = entity.OperatorID;
                info.CreateDate = entity.CreateDate;
                info.ChangeDate = entity.ChangeDate;
                info.InventoryID = entity.InventoryID;
            }


            return info;
        }

        private static InventoryEntity TranslateInventoryEntity(InventoryInfo info)
        {
            InventoryEntity entity = new InventoryEntity();
            if (info != null)
            {

                entity.InventoryID = info.InventoryID;
                entity.GoodsID = info.GoodsID;
                entity.StorageID = info.StorageID;
                entity.Quantity = info.Quantity;
                entity.CustomerID = info.CustomerID;
                entity.InventoryType = info.InventoryType;

                entity.BatchNumber = info.BatchNumber;
                entity.ProductDate = info.ProductDate;
                entity.InventoryDate = info.InventoryDate;
                entity.UnitPrice = info.UnitPrice;
                entity.Remark = info.Remark;

                entity.OperatorID = info.OperatorID;
                entity.CreateDate = info.CreateDate;
                entity.ChangeDate = info.ChangeDate;
                entity.InventoryID = info.InventoryID;
                entity.IsLock = info.IsLock;
                entity.InventoryStatus = info.InventoryStatus;
                //过期日期                
                entity.goods = GoodsService.GetGoodsEntityById(info.GoodsID);
                if (entity.goods!=null)
                {
                    entity.ExpDate = Datehelper.getDateTime(entity.ProductDate, entity.goods.exDate.ToInt(0), entity.goods.exUnits);
                }                
                entity.customer = CustomerService.GetCustomerEntityById(info.CustomerID);
                entity.storages = StorageService.GetStorageEntityById(info.StorageID);
                entity.WaitQuantity = OrderService.GetOutStockOrderByInentroyID(entity.InventoryID).Quantity;
                entity.CanUseQuantity = entity.Quantity - entity.WaitQuantity;
                entity.colorStyle = colorStyle(entity);
            
            }

            return entity;
        }

        /// <summary>
        /// 过期日期预警  3分之二 黄色 过期了红色
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private static string colorStyle(InventoryEntity entity)
        {
            DateTime startTime = Convert.ToDateTime(entity.ProductDate);
            DateTime endTime = Convert.ToDateTime(entity.ExpDate);
            TimeSpan ts = endTime - startTime;

            //过期了红色
            if (DateTime.Now > entity.ExpDate)
            {
                return "style=background-color:red";
            }

            DateTime startNowTime = DateTime.Now;
            TimeSpan tsNow = endTime - startNowTime;

            int days= ts.Days - tsNow.Days;
            if (entity.goods != null)
            {
                int totalDays = Datehelper.getDays(entity.goods.exDate.ToInt(0), entity.goods.exUnits);
                if (days / totalDays > 3)
                {
                    return "style=background-color:yellow";
                }
            }
            
            return "";
        }



        public static bool ModifyInventory(InventoryEntity entity)
        {
            long result = 0;
            if (entity != null)
            {
                InventoryRepository mr = new InventoryRepository();

                InventoryInfo InventoryInfo = TranslateInventoryInfo(entity);

                if (entity.InventoryID > 0)
                {
                    InventoryInfo.InventoryID = entity.InventoryID;
                    InventoryInfo.ChangeDate = DateTime.Now;
                    result = mr.ModifyInventory(InventoryInfo);
                }
                else
                {
                    InventoryInfo.ChangeDate = DateTime.Now;
                    InventoryInfo.CreateDate = DateTime.Now;
                    result = mr.CreateNew(InventoryInfo);
                }
            }
            return result > 0;
        }

        public static InventoryEntity GetInventoryById(long gid)
        {
            InventoryEntity result = new InventoryEntity();
            InventoryRepository mr = new InventoryRepository();
            InventoryInfo info = mr.GetInventoryByKey(gid);
            result = TranslateInventoryEntity(info);
            return result;
        }

        public static List<InventoryEntity> GetInventoryAll()
        {
            List<InventoryEntity> all = new List<InventoryEntity>();
            InventoryRepository mr = new InventoryRepository();
            List<InventoryInfo> miList = mr.GetAllInventory();//Cache.Get<List<InventoryInfo>>("InventoryALL");
            //if (miList.IsEmpty())
            //{
            //    miList = mr.GetAllInventory();
            //    Cache.Add("InventoryALL", miList);
            //}
            if (!miList.IsEmpty())
            {
                foreach (InventoryInfo mInfo in miList)
                {
                    InventoryEntity InventoryEntity = TranslateInventoryEntity(mInfo);
                    all.Add(InventoryEntity);
                }
            }

            return all;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="goodsid">商品编号</param>
        /// <param name="storageID"></param>
        /// <param name="batchNumber">批次号</param>
        /// <param name="CustomerID">客户ID</param>
        /// <returns></returns>
        public static List<InventoryEntity> GetInventoryByRule(int goodsid, int storageID, string batchNumber, int CustomerID = -1,bool DY0=false,string keywords="")
        {
            List<InventoryEntity> all = new List<InventoryEntity>();
            InventoryRepository mr = new InventoryRepository();
            List<InventoryInfo> miList = mr.GetInventoryByRule(goodsid, storageID, batchNumber, CustomerID, DY0,keywords);

            if (!miList.IsEmpty())
            {
                foreach (InventoryInfo mInfo in miList)
                {
                    InventoryEntity InventoryEntity = TranslateInventoryEntity(mInfo);
                    all.Add(InventoryEntity);
                }
            }

            return all;

        }

        public static List<InventoryEntity> GetInventoryByKeys(string ids)
        {
            List<InventoryEntity> all = new List<InventoryEntity>();
            InventoryRepository mr = new InventoryRepository();
            List<InventoryInfo> miList = mr.GetInventoryByKeys(ids);

            if (!miList.IsEmpty())
            {
                foreach (InventoryInfo mInfo in miList)
                {
                    InventoryEntity entity = TranslateInventoryEntity(mInfo);
                    all.Add(entity);
                }
            }

            return all;
        }

        public static int Remove(long gid)
        {
            InventoryRepository mr = new InventoryRepository();
            int i = mr.Remove(gid);
            //List<InventoryInfo> miList = mr.GetAllInventory();//刷新缓存
            //Cache.Add("InventoryALL", miList);
            return i;
        }

        #region 分页相关
        public static int GetInventoryCount(string name, string batchNumber, int StorageID, int customerID)
        {
            return new InventoryRepository().GetInventoryCount(name, batchNumber, StorageID, customerID);
        }

        public static List<InventoryEntity> GetInventoryInfoPager(PagerInfo pager)
        {
            List<InventoryEntity> all = new List<InventoryEntity>();
            InventoryRepository mr = new InventoryRepository();
            List<InventoryInfo> miList = mr.GetAllInventoryInfoPager(pager);
            if (!miList.IsEmpty())
            {
                foreach (InventoryInfo mInfo in miList)
                {
                    InventoryEntity carEntity = TranslateInventoryEntity(mInfo);
                    all.Add(carEntity);
                }
            }
            return all;
        }

        public static List<InventoryEntity> GetInventoryInfoByRule(string name, string batchNumber, int StorageID, int customerID, PagerInfo pager)
        {
            List<InventoryEntity> all = new List<InventoryEntity>();
            InventoryRepository mr = new InventoryRepository();
            List<InventoryInfo> miList = mr.GetInventoryInfoByRule(name, batchNumber, StorageID, customerID, pager);

            if (!miList.IsEmpty())
            {
                foreach (InventoryInfo mInfo in miList)
                {
                    InventoryEntity storeEntity = TranslateInventoryEntity(mInfo);
                    all.Add(storeEntity);
                }
            }

            return all;
        }
        #endregion

        public static List<ImportInventoryEntity> GetInventoryImportList(DataSet ds)
        {
            List<ImportInventoryEntity> list = new List<ImportInventoryEntity>();

            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[i]["所属客户"].ToString()))
                            {
                                //所属客户	仓库编号	仓库名称	商品编号	商品名称	规格型号	单位	批次号	生产日期	到期日期	余量
                                ImportInventoryEntity entity = new ImportInventoryEntity();
                                entity.CustomerName = dt.Rows[i]["所属客户"].ToString();
                                entity.StorageNo = dt.Rows[i]["仓库编号"].ToString();
                                entity.StorageName = dt.Rows[i]["仓库名称"].ToString();
                                entity.GoodsNo = dt.Rows[i]["商品编号"].ToString();
                                entity.GoodsName = dt.Rows[i]["商品名称"].ToString();
                                entity.Models = dt.Rows[i]["规格型号"].ToString();
                                entity.Units = dt.Rows[i]["单位"].ToString();
                                entity.BatchNumber = dt.Rows[i]["批次号"].ToString();
                                entity.ProductDate = dt.Rows[i]["生产日期"].ToString();
                                entity.ExitDate = dt.Rows[i]["到期日期"].ToString();
                                entity.Quantity = dt.Rows[i]["余量"].ToString();
                                GoodsEntity goodsEntity = OrderService.getGoodsModelByGoods("", entity.GoodsName, "", entity.Models);
                                entity.GoodsID = goodsEntity != null ? goodsEntity.GoodsID : 0;

                                if (!String.IsNullOrEmpty(entity.BatchNumber) && entity.BatchNumber.Length == 8)
                                {
                                    list.Add(entity);
                                }
                            }
                        }
                    }
                }
            }
            return list;
        }

        public static int insertInventory(List<ImportInventoryEntity> list, long operatorID)
        {
            int count = 0;
            InventoryRepository mr = new InventoryRepository();
            if(list!=null&&list.Count>0)
            {
                List<InventoryInfo> listInv = new List<InventoryInfo>();
                foreach (ImportInventoryEntity entity in list)
                {
                    InventoryInfo info = new InventoryInfo();
                    if (entity != null)
                    {
                        count++;
                        //List<GoodsEntity> listGoods = GoodsService.GetGoodsByRule(entity.GoodsNo, -1);
                        //GoodsEntity entityGood = listGoods != null && listGoods.Count > 0 ? listGoods[0] : null;
                        List<StorageEntity> listStorage = StorageService.GetStorageByRule(entity.StorageName, -1);
                        StorageEntity entityStorage = listStorage != null && listStorage.Count > 0 ? listStorage[0] : null;

                        List<CustomerEntity> listCustomer = CustomerService.GetCustomerByRule(entity.CustomerName, -1);
                        CustomerEntity entityCustomer = listCustomer != null && listCustomer.Count > 0 ? listCustomer[0] : null;
                        info.GoodsID = entity.GoodsID;
                        info.StorageID = entityStorage != null ? entityStorage.StorageID : 0;
                        info.Quantity = entity.Quantity.ToInt(0);
                        info.CustomerID = entityCustomer != null ? entityCustomer.CustomerID : 0;
                        info.InventoryType = Common.InventoryType.入库.ToString();
                        info.BatchNumber = entity.BatchNumber;
                        info.ProductDate = DateTime.Parse(entity.ProductDate);
                        info.InventoryDate = DateTime.Now;
                        info.UnitPrice = 0;
                        info.Remark = "库存导入";
                        info.OperatorID = operatorID;
                        info.CreateDate = DateTime.Now;
                        info.ChangeDate = DateTime.Now;
                        listInv.Add(info);

                    }
                }
                //生成入库单
                InventoryExecOrder orderExec = OrderService.CreateOrderByInventory(listInv);

                if (listInv != null && listInv.Count > 0)
                {
                    foreach (InventoryInfo info in listInv)
                    {
                        //插入库存
                        mr.CreateNew(info);
                        //库存明细保存
                        CreateInventoryDetail(info, orderExec, info.StorageID, info.InventoryDate, operatorID);

                    }
                }

            }
            return count;

        }


        #region 订单删除库存回位
        public static void updateInventoryByOrder(OrderEntity order)
        {
            if (order != null)
            {
                List<OrderDetailEntity> orderDetailList = order.orderDetailList;
                if (orderDetailList != null && orderDetailList.Count > 0)
                {
                    foreach (OrderDetailEntity orderDetail in orderDetailList)
                    {
                        //批次号+商品ID 确认唯一库存信息
                        List<InventoryEntity> inventoryList =null;
                        if(orderDetail.InventoryID>0)//
                        {
                            GetInventoryById(orderDetail.InventoryID);
                        }

                        //if (inventoryList == null || inventoryList.Count == 0)
                        //{
                        //    inventoryList = GetInventoryByRule(orderDetail.GoodsID, order.SendStorageID, orderDetail.BatchNumber);
                        //}

                        if (inventoryList != null && inventoryList.Count > 0)
                        {
                            foreach (InventoryEntity inventory in inventoryList)
                            {
                                LogHelper.WriteTextLog("订单删除", "库存编号：" + inventory.InventoryID + "当前库存数量：" + inventory.Quantity + "库存回位数量：" + orderDetail.Quantity);
                                InventoryRepository mr = new InventoryRepository();
                                InventoryInfo inventinfo = new InventoryInfo();
                                inventinfo.Quantity = inventory.Quantity + orderDetail.Quantity;//当前库存添加 订单中库存
                                inventinfo.InventoryID = inventory.InventoryID;
                                inventinfo.ChangeDate = DateTime.Now;
                                mr.ModifyInventoryQuantity(inventinfo);
                                LogHelper.WriteTextLog("订单删除", "库存编号：" + inventory.InventoryID + "更新后库存数量：" + inventinfo.Quantity);
                                #region 库存明细增加
                                CreateInventoryDetail(order, orderDetail);
                                #endregion
                            }
                        }
                    }
                }
            }
        }


        public static void CreateInventoryDetail(OrderEntity order, OrderDetailEntity orderDetail)
        {
            InventoryDetailRepository mrDetail = new InventoryDetailRepository();
            InventoryDetailInfo infoDetail = new InventoryDetailInfo();
            if (order != null)
            {
                infoDetail.OrderID = order.OrderID;
                infoDetail.OrderNo = order.OrderNo;
                infoDetail.OrderType = order.OrderType;
                infoDetail.GoodsID = orderDetail.GoodsID;
                infoDetail.StorageID = order.SendStorageID;
                infoDetail.Quantity = orderDetail.Quantity;
                infoDetail.CustomerID = order.CustomerID;
                infoDetail.InventoryType = Common.InventoryType.入库.ToString();
                infoDetail.BatchNumber = orderDetail.BatchNumber;
                infoDetail.ProductDate = orderDetail.ProductDate;
                infoDetail.InventoryDate = DateTime.Now;
                infoDetail.UnitPrice = 0;
                infoDetail.Remark = "订单删除-库存回位";
                infoDetail.OperatorID = order.OperatorID;
                infoDetail.CreateDate = DateTime.Now;
                infoDetail.ChangeDate = DateTime.Now;
                mrDetail.CreateNew(infoDetail);
            }
        }
        #endregion

    }
}
