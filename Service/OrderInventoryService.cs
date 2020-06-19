using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.ViewModel;
using Common;
using Service.Inventory;
using DataRepository.DataAccess.BaseData;
using DataRepository.DataModel;
namespace Service
{
    /// <summary>
    /// 订单库存扣减处理
    /// </summary>
    public class OrderInventoryService
    {
        /// <summary>
        ///   仓配订单 仓到店，涉及仓的库存扣减，无视门店库存。
        ///   调拨订单 仓到仓，涉及仓仓之间的库存扣减增加。
        ///   运输订单A 厂到仓，涉及仓的库存增加，无视厂的库存。
        ///   运输订单B 厂到店，均无视库存。
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public static bool OrderInventoryProcess(long orderid)
        {
            bool temp = false;

            try
            {
                OrderEntity orderInfo = OrderService.GetOrderByOrderID(orderid);
                if (orderInfo != null && !orderInfo.OrderOutStatus.Equals("T"))//订单不是已出库状态
                {
                    LogHelper.WriteTextLog("订单出库", "订单号：" + orderInfo.OrderNo);
                    //仓配订单 仓配订单 仓到店，涉及仓的库存扣减，无视门店库存。
                    if (orderInfo.OrderType.Equals(OrderType.CPDD.ToString()) || orderInfo.OrderType.Equals(OrderType.DBDD.ToString()))
                    {
                        //出库操作
                        OutInventoryProcess(orderInfo);

                        //更新订单出库状态为已出库
                        OrderService.UpdateOrderOutType(orderInfo.OrderID);
                    }

                    //运输订单A 厂到仓，涉及仓的库存增加，无视厂的库存。
                    //调拨订单 仓到仓，涉及仓仓之间的库存扣减增加。
                    if (orderInfo.OrderType.Equals(OrderType.YSDDA.ToString()) || orderInfo.OrderType.Equals(OrderType.DBDD.ToString()))
                    {
                        InInventoryProcess(orderInfo);

                        //更新订单出库状态为已出库
                        OrderService.UpdateOrderOutType(orderInfo.OrderID);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog("订单出库", "出库异常："+ex.ToString());
            }
            return temp;
        }

        #region 出库操作
        /// <summary>
        /// 库存处理
        /// </summary>
        /// <param name="orderInfo"></param>
        private static void OutInventoryProcess(OrderEntity orderInfo)
        {
            List<OrderDetailEntity> orderDetailList = orderInfo.orderDetailList;
            if (orderDetailList != null && orderDetailList.Count > 0)
            {
                foreach (OrderDetailEntity entity in orderDetailList)
                {
                    List<InventoryEntity> inventoryList = new List<InventoryEntity>();
                    if (entity.InventoryID > 0)
                    {
                        inventoryList.Add(InventoryService.GetInventoryEntityById(entity.InventoryID));
                    }
                    //同商品+批次号+客户 
                    else
                    {
                        inventoryList = InventoryService.GetInventoryByRule(entity.GoodsID, -1, entity.BatchNumber.Trim(), orderInfo.CustomerID, true);
                    }

                    inventoryInfoProcess(entity, inventoryList, orderInfo);
                }
            }
        }


        /// <summary>
        /// 库存计算处理
        /// </summary>
        /// <param name="orderDetail"></param>
        /// <param name="inventoryList"></param>
        private static void inventoryInfoProcess(OrderDetailEntity orderDetail, List<InventoryEntity> inventoryList, OrderEntity orderInfo)
        {
            int TotalGoodQuantity = orderDetail.Quantity;//当前明细出库数量
            if (inventoryList != null && inventoryList.Count > 0)
            {
                foreach (InventoryEntity inventory in inventoryList)
                {

                    //库存扣减                        
                    int newQuantity = inventory.Quantity - TotalGoodQuantity;
                    if (newQuantity >= 0)//库存数量大于订单出库数量  足够扣
                    {
                        //库存更新
                        deductionInventory(newQuantity, inventory);//直接扣除库存
                        //库存明细更新
                        createInventoryDetail(orderDetail, orderInfo, inventory.StorageID, TotalGoodQuantity);

                        break;//扣完跳出循环
                    }
                    else
                    {
                        //库存不足扣减
                        TotalGoodQuantity = TotalGoodQuantity - inventory.Quantity;//当前明细出库数量 减去库存数量
                        //库存更新
                        deductionInventory(0, inventory);//库存直接清0
                        //库存明细更新
                        createInventoryDetail(orderDetail, orderInfo, inventory.StorageID, inventory.Quantity);
                    }
                }
            }
        }

        /// <summary>
        /// 库存表数据更新
        /// /// </summary>
        private static void deductionInventory(int newQuantity, InventoryEntity inventory)
        {
            //库存更新
            InventoryRepository mr = new InventoryRepository();
            InventoryInfo inventinfo = new InventoryInfo();
            inventinfo.Quantity = newQuantity;//仓库库存减去订单明细中出库库存
            inventinfo.InventoryID = inventory.InventoryID;
            inventinfo.ChangeDate = DateTime.Now;
            mr.ModifyInventoryQuantity(inventinfo);
        }

        #endregion


        #region 入库操作
        /// <summary>
        /// 库存操作
        /// </summary>
        /// <param name="orderInfo"></param>
        private static void InInventoryProcess(OrderEntity orderInfo)
        {
            List<OrderDetailEntity> orderDetailList = orderInfo.orderDetailList;
            if (orderDetailList != null && orderDetailList.Count > 0)
            {
                foreach (OrderDetailEntity entity in orderDetailList)
                {
                    //同商品+批次号+客户 
                    List<InventoryEntity> inventoryList = InventoryService.GetInventoryByRule(entity.GoodsID, orderInfo.ReceiverStorageID, entity.BatchNumber, orderInfo.CustomerID, true);

                    
                }
            }
        }

        /// <summary>
        /// 执行库存操作
        /// </summary>
        /// <param name="orderDetail"></param>
        /// <param name="inventoryList"></param>
        /// <param name="orderInfo"></param>
        private static void InInventoryInfoProcess(OrderDetailEntity orderDetail, List<InventoryEntity> inventoryList, OrderEntity orderInfo)
        {
            //当前仓库已经存在同一个批次同一个商品信息
            if (inventoryList != null && inventoryList.Count > 0)
            {
                InventoryEntity inventory = inventoryList[0];
                inventory.Quantity = inventory.Quantity + orderDetail.Quantity;//当前库存数量+订单入库数量

                //库存更新
                deductionInventory(inventory.Quantity, inventory);//库存直接清0
                //库存明细更新
                createInventoryDetail(orderDetail, orderInfo, inventory.StorageID, inventory.Quantity);
            }
            else
            {
                InventoryRepository mr = new InventoryRepository();
                InventoryInfo info = new InventoryInfo();
                info.GoodsID = orderDetail.GoodsID;
                info.StorageID = orderInfo.ReceiverStorageID;
                info.Quantity = orderDetail.Quantity;
                info.CustomerID = orderInfo.CustomerID;
                info.InventoryType = Common.InventoryType.入库.ToString();
                info.BatchNumber = orderDetail.BatchNumber;
                info.ProductDate = orderDetail.ProductDate;
                info.InventoryDate = DateTime.Now;
                info.UnitPrice = 0;
                info.Remark = "订单类型：" + orderInfo.OrderType + "订单号：" + orderInfo.OrderNo + "库存入库";
                info.OperatorID = orderInfo.OperatorID;
                info.CreateDate = DateTime.Now;
                info.ChangeDate = DateTime.Now;
                //插入库存
                mr.CreateNew(info);
                //库存明细更新
                createInventoryDetail(orderDetail, orderInfo, info.StorageID, info.Quantity);
            }
        }



        #endregion

        

        /// <summary>
        /// 库存明细更新
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="orderEntity"></param>
        /// <param name="inventory"></param>
        private static void createInventoryDetail(OrderDetailEntity detail, OrderEntity orderEntity, int storageID, int Quantity)
        {
            InventoryDetailRepository mrDetail = new InventoryDetailRepository();
            InventoryDetailInfo infoDetail = new InventoryDetailInfo();
            infoDetail.OrderID = orderEntity.OrderID;
            infoDetail.OrderNo = orderEntity.OrderNo;
            infoDetail.OrderType = orderEntity.OrderType;//订单类型
            infoDetail.GoodsID = detail.GoodsID;
            infoDetail.StorageID = storageID;
            infoDetail.Quantity = Quantity;
            infoDetail.CustomerID = orderEntity.CustomerID;
            infoDetail.InventoryType = InventoryType.出库.ToString();//出库/入库
            infoDetail.BatchNumber = detail.BatchNumber;
            infoDetail.ProductDate = detail.ProductDate;
            infoDetail.InventoryDate = DateTime.Now;
            infoDetail.UnitPrice = 0;
            infoDetail.Remark = orderEntity.OrderType;
            infoDetail.OperatorID = orderEntity.OperatorID;
            infoDetail.CreateDate = DateTime.Now;
            infoDetail.ChangeDate = DateTime.Now;
            mrDetail.CreateNew(infoDetail);
        }
    }
}
