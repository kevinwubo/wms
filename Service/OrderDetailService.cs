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
using Service.Inventory;

namespace Service
{
    public class OrderDetailService : BaseService
    {
        public static OrderDetailEntity GetOrderDetailEntityById(long cid)
        {
            OrderDetailEntity result = new OrderDetailEntity();
            OrderDetailRepository mr = new OrderDetailRepository();
            OrderDetailInfo info = mr.GetOrderDetailByKey(cid);
            result = TranslateOrderDetailEntity(info);
            return result;
        }

        private static OrderDetailInfo TranslateOrderDetailInfo(OrderDetailEntity entity)
        {
            OrderDetailInfo info = new OrderDetailInfo();
            if (entity != null)
            {
                info.ID = entity.ID;
                info.OrderID = entity.OrderID;
                info.GoodsID = entity.GoodsID;
                info.InventoryID = entity.InventoryID;
                info.GoodsNo = entity.GoodsNo;
                info.GoodsName = entity.GoodsName;
                info.GoodsModel = entity.GoodsModel;
                info.Quantity = entity.Quantity;
                info.Units = entity.Units;
                info.Weight = entity.Weight;
                info.TotalWeight = entity.TotalWeight;
                info.BatchNumber = entity.BatchNumber;
                info.ProductDate = entity.ProductDate;
                info.ExceedDate = entity.ExceedDate;
                info.CreateDate = entity.CreateDate;
                info.ChangeDate = entity.ChangeDate;
            }


            return info;
        }

        private static OrderDetailEntity TranslateOrderDetailEntity(OrderDetailInfo info)
        {
            OrderDetailEntity entity = new OrderDetailEntity();
            if (info != null)
            {
                entity.ID = info.ID;
                entity.OrderID = info.OrderID;
                entity.GoodsID = info.GoodsID;
                entity.InventoryID = info.InventoryID;
                entity.GoodsNo = info.GoodsNo;
                entity.GoodsName = info.GoodsName;
                entity.GoodsModel = info.GoodsModel;
                entity.Quantity = info.Quantity;
                entity.Units = info.Units;
                entity.Weight = info.Weight;
                entity.TotalWeight = info.TotalWeight;
                entity.BatchNumber = info.BatchNumber;
                entity.ProductDate = info.ProductDate;
                entity.ExceedDate = info.ExceedDate;
                entity.CreateDate = info.CreateDate;
                entity.ChangeDate = info.ChangeDate;
                entity.CreateDate = info.CreateDate;
                entity.ChangeDate = info.ChangeDate;

                entity.goods = new GoodsEntity();
                if (entity.GoodsID > 0)
                {
                    entity.goods = GoodsService.GetGoodsEntityById(entity.GoodsID);
                }

                entity.inventory = new InventoryEntity();
                if (entity.InventoryID > 0)
                {
                    entity.inventory = InventoryService.GetInventoryById(entity.InventoryID);
                }
                
            }

            return entity;
        }

        public static bool ModifyOrderDetail(OrderDetailEntity entity)
        {
            long result = 0;
            if (entity != null)
            {
                OrderDetailRepository mr = new OrderDetailRepository();

                OrderDetailInfo OrderDetailInfo = TranslateOrderDetailInfo(entity);

                if (entity.ID > 0)
                {
                    OrderDetailInfo.ID = entity.ID;
                    OrderDetailInfo.ChangeDate = DateTime.Now;
                    result = mr.ModifyOrderDetail(OrderDetailInfo);
                }
                else
                {
                    OrderDetailInfo.ChangeDate = DateTime.Now;
                    OrderDetailInfo.CreateDate = DateTime.Now;
                    result = mr.CreateNew(OrderDetailInfo);
                }


                List<OrderDetailInfo> miList = mr.GetAllOrderDetail();//刷新缓存
                Cache.Add("OrderDetailALL", miList);
            }
            return result > 0;
        }

        public static OrderDetailEntity GetOrderDetailById(long gid)
        {
            OrderDetailEntity result = new OrderDetailEntity();
            OrderDetailRepository mr = new OrderDetailRepository();
            OrderDetailInfo info = mr.GetOrderDetailByKey(gid);
            result = TranslateOrderDetailEntity(info);
            return result;
        }

        public static List<OrderDetailEntity> GetOrderDetailAll()
        {
            List<OrderDetailEntity> all = new List<OrderDetailEntity>();
            OrderDetailRepository mr = new OrderDetailRepository();
            List<OrderDetailInfo> miList = mr.GetAllOrderDetail();//Cache.Get<List<OrderDetailInfo>>("OrderDetailALL");
            //if (miList.IsEmpty())
            //{
            //    miList = mr.GetAllOrderDetail();
            //    Cache.Add("OrderDetailALL", miList);
            //}
            if (!miList.IsEmpty())
            {
                foreach (OrderDetailInfo mInfo in miList)
                {
                    OrderDetailEntity OrderDetailEntity = TranslateOrderDetailEntity(mInfo);
                    all.Add(OrderDetailEntity);
                }
            }

            return all;

        }

        public static List<OrderDetailEntity> GetOrderDetailByOrderID(int OrderID)
        {
            List<OrderDetailEntity> all = new List<OrderDetailEntity>();
            OrderDetailRepository mr = new OrderDetailRepository();
            List<OrderDetailInfo> miList = mr.GetAllOrderDetailByOrderID(OrderID);
            if (!miList.IsEmpty())
            {
                foreach (OrderDetailInfo mInfo in miList)
                {
                    OrderDetailEntity OrderDetailEntity = TranslateOrderDetailEntity(mInfo);
                    all.Add(OrderDetailEntity);
                }
            }

            return all;
        }

        public static List<OrderDetailEntity> GetOrderDetailByRule(string name, int status)
        {
            List<OrderDetailEntity> all = new List<OrderDetailEntity>();
            OrderDetailRepository mr = new OrderDetailRepository();
            List<OrderDetailInfo> miList = mr.GetOrderDetailByRule(name, status);

            if (!miList.IsEmpty())
            {
                foreach (OrderDetailInfo mInfo in miList)
                {
                    OrderDetailEntity OrderDetailEntity = TranslateOrderDetailEntity(mInfo);
                    all.Add(OrderDetailEntity);
                }
            }

            return all;
        }

        public static List<OrderDetailEntity> GetOrderDetailByKeys(string ids)
        {
            List<OrderDetailEntity> all = new List<OrderDetailEntity>();
            OrderDetailRepository mr = new OrderDetailRepository();
            List<OrderDetailInfo> miList = mr.GetOrderDetailByKeys(ids);

            if (!miList.IsEmpty())
            {
                foreach (OrderDetailInfo mInfo in miList)
                {
                    OrderDetailEntity entity = TranslateOrderDetailEntity(mInfo);
                    all.Add(entity);
                }
            }

            return all;
        }

        public static int Delete(long gid)
        {
            OrderDetailRepository mr = new OrderDetailRepository();
            int i = mr.Delete(gid);
            //List<OrderDetailInfo> miList = mr.GetAllOrderDetail();//刷新缓存
            //Cache.Add("OrderDetailALL", miList);
            return i;
        }

        #region 分页相关
        public static int GetOrderDetailCount(string name, int carrierid, int storageid, int customerid, int status)
        {
            return new OrderDetailRepository().GetOrderDetailCount(name, carrierid, storageid, customerid, status);
        }

        public static List<OrderDetailEntity> GetOrderDetailInfoPager(PagerInfo pager)
        {
            List<OrderDetailEntity> all = new List<OrderDetailEntity>();
            OrderDetailRepository mr = new OrderDetailRepository();
            List<OrderDetailInfo> miList = mr.GetAllOrderDetailInfoPager(pager);
            foreach (OrderDetailInfo mInfo in miList)
            {
                OrderDetailEntity carEntity = TranslateOrderDetailEntity(mInfo);
                all.Add(carEntity);
            }
            return all;
        }

        public static List<OrderDetailEntity> GetOrderDetailInfoByRule(string name, int carrierid, int storageid, int customerid, int status, PagerInfo pager)
        {
            List<OrderDetailEntity> all = new List<OrderDetailEntity>();
            OrderDetailRepository mr = new OrderDetailRepository();
            List<OrderDetailInfo> miList = mr.GetOrderDetailInfoByRule(name, carrierid, storageid, customerid, status, pager);

            if (!miList.IsEmpty())
            {
                foreach (OrderDetailInfo mInfo in miList)
                {
                    OrderDetailEntity storeEntity = TranslateOrderDetailEntity(mInfo);
                    all.Add(storeEntity);
                }
            }

            return all;
        }
        #endregion
    }
}
