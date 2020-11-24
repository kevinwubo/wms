using Common;
using DataRepository.DataAccess.BaseData;
using DataRepository.DataModel;
using Entity.ViewModel;
using Service.BaseBiz;
using Service.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ApiService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="GoodsID"></param>
        /// <param name="InventoryID"></param>
        /// <param name="StorageID"></param>
        /// <param name="BatchNumber"></param>
        /// <param name="Quantity"></param>
        public static void ApiInInventoty(int GoodsID, int InventoryID, int StorageID, string BatchNumber, int Quantity, long OperatorID)
        {
            // 商品信息
            GoodsEntity goodsEntity = GoodsService.GetGoodsEntityById(GoodsID);
            // 入库

            List<InventoryInfo> listInv = buildInventoryList(goodsEntity, StorageID, Quantity, BatchNumber, OperatorID);

            InventoryService.InventoryProcess(listInv, "", StorageID, DateTime.Now.ToShortDateString(), OperatorID);

        }


        /// <summary>
        /// 出库操作
        /// </summary>
        /// <param name="GoodsID"></param>
        /// <param name="InventoryID"></param>
        /// <param name="BatchNumber"></param>
        /// <param name="OutQuantity"></param>
        /// <param name="UserID"></param>
        public static void InventoryOut(int GoodsID, int InventoryID, string BatchNumber, int OutQuantity, long UserID)
        {
            // 当前库存信息
            InventoryEntity entity = InventoryService.GetInventoryEntityById(InventoryID);
            // 商品信息
            GoodsEntity goodsEntity = GoodsService.GetGoodsEntityById(GoodsID);
            if (entity != null && goodsEntity != null)
            {
                // 库存可用数量=库存数量-待出库数量
                int canQuantity = entity.Quantity - entity.WaitQuantity;
                if (canQuantity >= OutQuantity)
                {
                    entity.Quantity = canQuantity - OutQuantity;
                    // 商品库存扣减
                    OrderInventoryService.ModifyQuantity(entity);

                    //记录库存明细
                    createInventoryDetail(entity, goodsEntity, UserID);
                }
            }
        }

        /// <summary>
        /// 库存明细更新
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="orderEntity"></param>
        /// <param name="inventory"></param>
        private static void createInventoryDetail(InventoryEntity entity, GoodsEntity orderEntity, long UserID)
        {
            InventoryDetailRepository mrDetail = new InventoryDetailRepository();
            InventoryDetailInfo infoDetail = new InventoryDetailInfo();
            infoDetail.InventoryID = entity.InventoryID;
            infoDetail.OrderID = 0;
            infoDetail.OrderNo = "";
            infoDetail.OrderType = "";//订单类型
            infoDetail.GoodsID = entity.GoodsID;
            infoDetail.StorageID = entity.StorageID;
            infoDetail.Quantity = entity.Quantity;
            infoDetail.CustomerID = orderEntity.CustomerID;
            infoDetail.InventoryType = InventoryType.出库.ToString();//出库/入库
            infoDetail.BatchNumber = entity.BatchNumber;
            infoDetail.ProductDate = entity.ProductDate;
            infoDetail.InventoryDate = DateTime.Now;
            infoDetail.UnitPrice = 0;
            infoDetail.Remark = "PDA出库操作";
            infoDetail.OperatorID = UserID;
            infoDetail.CreateDate = DateTime.Now;
            infoDetail.ChangeDate = DateTime.Now;
            mrDetail.CreateNew(infoDetail);
        }

        private static List<InventoryInfo> buildInventoryList(GoodsEntity entity, int StorageID, int Quantity, string BatchNumber, long OperatorID)
        {
            List<InventoryInfo> listInv = new List<InventoryInfo>();
            InventoryInfo info = new InventoryInfo();
            if (entity != null)
            {
                info.GoodsID = entity.GoodsID;
                info.StorageID = StorageID;
                info.Quantity = Quantity;
                info.CustomerID = entity.CustomerID;
                info.InventoryType = Common.InventoryType.入库.ToString();
                info.BatchNumber = BatchNumber.Trim();
                info.ProductDate = convertDatetime(BatchNumber);
                info.InventoryDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                info.UnitPrice = 0;
                info.Remark = "PDA设备入库操作";
                info.OperatorID = OperatorID;
                info.CreateDate = DateTime.Now;
                info.ChangeDate = DateTime.Now;
                listInv.Add(info);
            }

            return listInv;
        }

        public static DateTime convertDatetime(string batchNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(batchNumber) || batchNumber.Length != 8)
                {
                    return DateTime.Now;
                }
                //20201024
                string year = batchNumber.Substring(0, 4);
                string yy = batchNumber.Substring(4, 2);
                string dd = batchNumber.Substring(6, 2);

               return DateTime.Parse(year + "-" + yy + "-" + dd);
            }
            catch (Exception ex)
            {
                return DateTime.Now;
            }
        }
    }
}
