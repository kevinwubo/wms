using Common;
using DataRepository.DataAccess.BaseData;
using DataRepository.DataModel;
using Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Infrastructure.Helper;
using Service.BaseBiz;

namespace Service.Inventory
{
    /// <summary>
    /// 库存处理
    /// </summary>
    public class InventoryDetailService
    {

        private static InventoryDetailEntity TranslateInventoryDetailEntity(InventoryDetailInfo info)
        {
            InventoryDetailEntity entity = new InventoryDetailEntity();
            if (info != null)
            {

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

                entity.OrderNo = info.OrderNo;
                entity.OrderType = info.OrderType;
                entity.OrderTypeDesc = StringHelper.getOrderType(info.OrderType);
                entity.OperatorID = info.OperatorID;
                entity.CreateDate = info.CreateDate;
                entity.ChangeDate = info.ChangeDate;
                entity.InventoryDetailID = info.InventoryDetailID;
                entity.InventoryID = info.InventoryID;
                entity.goods = GoodsService.GetGoodsEntityById(info.GoodsID);
                entity.customer = CustomerService.GetCustomerEntityById(info.CustomerID);
                entity.storages = StorageService.GetStorageEntityById(info.StorageID);

            }

            return entity;
        }


        #region 分页相关
        public static int GetInventoryCount(string name, string inventoryType, int StorageID, int customerID, string inventoryDate,string orderType)
        {
            return new InventoryDetailRepository().GetInventoryDetailCount(name, inventoryType, StorageID, customerID, inventoryDate, orderType);
        }

        public static List<InventoryDetailEntity> GetInventoryDetailInfoPager(PagerInfo pager)
        {
            List<InventoryDetailEntity> all = new List<InventoryDetailEntity>();
            InventoryDetailRepository mr = new InventoryDetailRepository();
            List<InventoryDetailInfo> miList = mr.GetAllInventoryDetailInfoPager(pager);
            if (!miList.IsEmpty())
            {
                foreach (InventoryDetailInfo mInfo in miList)
                {
                    InventoryDetailEntity carEntity = TranslateInventoryDetailEntity(mInfo);
                    all.Add(carEntity);
                }
            }
            return all;
        }

        public static List<InventoryDetailEntity> GetInventoryDetailInfoByRule(string name, string inventoryType, int StorageID, int customerID, string inventoryDate, string orderType, PagerInfo pager)
        {
            List<InventoryDetailEntity> all = new List<InventoryDetailEntity>();
            InventoryDetailRepository mr = new InventoryDetailRepository();
            List<InventoryDetailInfo> miList = mr.GetInventoryDetailInfoByRule(name, inventoryType, StorageID, customerID, inventoryDate, orderType, pager);

            if (!miList.IsEmpty())
            {
                foreach (InventoryDetailInfo mInfo in miList)
                {
                    InventoryDetailEntity storeEntity = TranslateInventoryDetailEntity(mInfo);
                    all.Add(storeEntity);
                }
            }

            return all;
        }
        #endregion
    }
}
