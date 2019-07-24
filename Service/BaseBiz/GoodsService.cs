using DataRepository.DataAccess.Goods;
using DataRepository.DataModel;
using Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Infrastructure.Helper;
namespace Service.BaseBiz
{
    public class GoodsService
    {
        public static List<GoodsEntity> GetAllGoods()
        {
            List<GoodsEntity> all = new List<GoodsEntity>();
            GoodsRepository mr = new GoodsRepository();
            List<GoodsInfo> miList = mr.GetAllGoods();

            if (!miList.IsEmpty())
            {
                foreach (GoodsInfo mInfo in miList)
                {
                    GoodsEntity StorageEntity = TranslateGoodsEntity(mInfo);
                    all.Add(StorageEntity);
                }
            }

            return all;
        }

        public static GoodsEntity GetGoodsEntityById(long cid)
        {
            GoodsEntity result = new GoodsEntity();
            GoodsRepository mr = new GoodsRepository();
            GoodsInfo info = mr.GetGoodsByKey(cid);
            if (info != null)
            {
                result = TranslateGoodsEntity(info);
            }
            return result;
        }

        public static List<GoodsEntity> GetGoodsByRule(string goodsNo, int status, string goodsName = "", string goodsModel = "", string BarCode = "")
        {
            List<GoodsEntity> all = new List<GoodsEntity>();
            GoodsRepository mr = new GoodsRepository();
            List<GoodsInfo> miList = mr.GetGoodsByRule(goodsNo, status, goodsName, goodsModel, BarCode);

            if (!miList.IsEmpty())
            {
                foreach (GoodsInfo mInfo in miList)
                {
                    GoodsEntity StorageEntity = TranslateGoodsEntity(mInfo);
                    all.Add(StorageEntity);
                }
            }

            return all;

        }

        public static void RemoveGoods(string GoodsID)
        {
            GoodsRepository mr = new GoodsRepository();
            mr.RemoveGoods(long.Parse(GoodsID));
        }

        public static bool ModifyGoods(GoodsEntity GoodsEntity)
        {
            int result = 0;
            if (GoodsEntity != null)
            {
                GoodsRepository mr = new GoodsRepository();
                GoodsInfo goodsInfo = TranslateGoodsInfo(GoodsEntity);

                if (GoodsEntity.GoodsID > 0)
                {
                    goodsInfo.ChangeDate = DateTime.Now;
                    result = mr.ModifyGroup(goodsInfo);
                }
                else
                {
                    goodsInfo.CreateDate = DateTime.Now;
                    goodsInfo.ChangeDate = DateTime.Now;
                    result = mr.CreateNew(goodsInfo);
                }
            }
            return result > 0;
        }

        /// <summary>
        /// 从info到Entity
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static GoodsEntity TranslateGoodsEntity(GoodsInfo info)
        {
            GoodsEntity entity = new GoodsEntity();
            if (info != null)
            {
                entity.GoodsID = info.GoodsID;
                entity.TypeCode = info.TypeCode;
                entity.CustomerID = info.CustomerID;
                entity.GoodsNo = info.GoodsNo;
                entity.GoodsName = info.GoodsName;
                entity.GoodsModel = info.GoodsModel;
                entity.Weight = info.Weight;
                entity.Size = info.Size;
                entity.Units = info.Units;
                entity.Costing = info.Costing;
                entity.SalePrice = info.SalePrice;
                entity.Torr = info.Torr;
                entity.exDate = info.exDate;
                entity.exUnits = info.exUnits;
                entity.AnotherNO = info.AnotherNO;
                entity.AnotherName = info.AnotherName;
                entity.Remark = info.Remark;
                entity.BarCode = info.BarCode;
                entity.OperatorID = info.OperatorID;
                entity.Status = info.Status;
                entity.CreateDate = info.CreateDate;
                entity.ChangeDate = info.ChangeDate;

                entity.customer = new CustomerEntity();
                entity.customer = CustomerService.GetCustomerById(info.CustomerID);
            }
            return entity;
        }



        /// <summary>
        /// 从Entity到info
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static GoodsInfo TranslateGoodsInfo(GoodsEntity entity)
        {
            GoodsInfo info = new GoodsInfo();
            if (entity != null)
            {
                info.GoodsID = entity.GoodsID;
                info.TypeCode = entity.TypeCode;
                info.CustomerID = entity.CustomerID;
                info.GoodsNo = entity.GoodsNo;
                info.GoodsName = entity.GoodsName;
                info.GoodsModel = entity.GoodsModel;
                info.Weight = entity.Weight;
                info.Size = entity.Size;
                info.Units = entity.Units;
                info.Costing = entity.Costing;
                info.SalePrice = entity.SalePrice;
                info.Torr = entity.Torr;
                info.exDate = entity.exDate;
                info.exUnits = entity.exUnits;
                info.AnotherNO = entity.AnotherNO;
                info.AnotherName = entity.AnotherName;
                info.Remark = entity.Remark;
                info.OperatorID = entity.OperatorID;
                info.Status = entity.Status;
                info.BarCode = entity.BarCode;
                info.CreateDate = entity.CreateDate;
                info.ChangeDate = entity.ChangeDate;
            }
            return info;
        }


        #region 分页相关
        public static int GetGoodsCount(string name, string mcode,int customerID, int status)
        {
            return new GoodsRepository().GetGoodsCount(name, mcode, customerID, status);
        }

        public static List<GoodsEntity> GetGoodsInfoPager(PagerInfo pager)
        {
            List<GoodsEntity> all = new List<GoodsEntity>();
            GoodsRepository mr = new GoodsRepository();
            List<GoodsInfo> miList = mr.GetAllGoodsInfoPager(pager);
            if (!miList.IsEmpty())
            {
                foreach (GoodsInfo mInfo in miList)
                {
                    GoodsEntity carEntity = TranslateGoodsEntity(mInfo);
                    all.Add(carEntity);
                }
            }
            return all;
        }

        public static List<GoodsEntity> GetGoodsInfoByRule(string name, string mcode, int customerID, int status, PagerInfo pager)
        {
            List<GoodsEntity> all = new List<GoodsEntity>();
            GoodsRepository mr = new GoodsRepository();
            List<GoodsInfo> miList = mr.GetGoodsInfoByRule(name, mcode, customerID, status, pager);

            if (!miList.IsEmpty())
            {
                foreach (GoodsInfo mInfo in miList)
                {
                    GoodsEntity storeEntity = TranslateGoodsEntity(mInfo);
                    all.Add(storeEntity);
                }
            }

            return all;
        }
        #endregion
    }
}
