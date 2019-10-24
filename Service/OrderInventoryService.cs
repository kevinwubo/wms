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
        public static bool OrderInventoryProcess(long orderid)
        {
            bool temp = false;

            try
            {
                OrderEntity orderInfo = OrderService.GetOrderByOrderID(orderid);
                if (orderInfo != null)
                {
                    //仓配订单 仓配订单 仓到店，涉及仓的库存扣减，无视门店库存。
                    if (orderInfo.OrderType.Equals(OrderType.CPDD.ToString()))
                    {
                        inventoryProcess(orderInfo);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return temp;
        }


        /// <summary>
        /// 库存处理
        /// </summary>
        /// <param name="orderInfo"></param>
        private static void inventoryProcess(OrderEntity orderInfo)
        {
            List<OrderDetailEntity> orderDetailList = orderInfo.orderDetailList;
            if (orderDetailList != null && orderDetailList.Count > 0)
            {
                foreach (OrderDetailEntity entity in orderDetailList)
                {
                    //同商品+批次号+客户 
                    List<InventoryEntity> inventoryList = InventoryService.GetInventoryByRule(entity.GoodsID, -1, entity.BatchNumber, orderInfo.CustomerID, true);

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
                        createInventoryDetail(orderDetail, orderInfo, inventory, TotalGoodQuantity);

                        break;//扣完跳出循环
                    }
                    else
                    {
                        //库存不足扣减
                        TotalGoodQuantity = TotalGoodQuantity - inventory.Quantity;//当前明细出库数量 减去库存数量
                        //if (TotalGoodQuantity > 0)
                        //{
                        //库存更新
                        deductionInventory(0, inventory);//库存直接清0
                        //库存明细更新
                        createInventoryDetail(orderDetail, orderInfo, inventory, inventory.Quantity);
                        //}
                        //else
                        //{
                        //    int quantity = inventory.Quantity - TotalGoodQuantity;
                        //    //库存更新
                        //    deductionInventory(quantity, inventory);//直接扣除库存

                        //    //库存明细更新
                        //    createInventoryDetail(orderDetail, orderInfo, inventory, TotalGoodQuantity);
                        //}


                        //if (TotalGoodQuantity == 0)
                        //{
                        //    break;
                        //}
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


        /// <summary>
        /// 库存明细更新
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="orderEntity"></param>
        /// <param name="inventory"></param>
        private static void createInventoryDetail(OrderDetailEntity detail, OrderEntity orderEntity, InventoryEntity inventory, int Quantity)
        {
            InventoryDetailRepository mrDetail = new InventoryDetailRepository();
            InventoryDetailInfo infoDetail = new InventoryDetailInfo();
            infoDetail.OrderID = orderEntity.OrderID;
            infoDetail.OrderNo = orderEntity.OrderNo;
            infoDetail.OrderType = orderEntity.OrderType;//订单类型
            infoDetail.GoodsID = detail.GoodsID;
            infoDetail.StorageID = inventory.StorageID;
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
