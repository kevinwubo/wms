using Entity.ApiBean;
using Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuoChe.ApiConvert
{
    public class ApiProcess
    {
        /// <summary>
        /// 用户登录接口--转换
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static UserBean convertUser(UserEntity entity)
        {
            UserBean bean = new UserBean();
            if(entity!=null)
            {
                bean.UserID = entity.UserID;
                bean.UserName = entity.UserName;
                bean.NickName = entity.NickName;
            }
            return bean;
        }


        /// <summary>
        /// 库存列表接口--转换
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<InventoryBean> convertInventoryList(List<InventoryEntity> list)
        {
            List<InventoryBean> beanList = new List<InventoryBean>();

            if (list != null && list.Count > 0)
            {
                foreach (InventoryEntity entity in list)
                {
                    InventoryBean bean = new InventoryBean();
                    bean.InventoryID = entity.InventoryID;
                    bean.StorageID = entity.StorageID;
                    bean.StorageName = entity.storages != null ? entity.storages.StorageName : "";
                    bean.GoodsID = entity.GoodsID;
                    bean.GoodsName = entity.goods != null ? entity.goods.GoodsName : "";
                    bean.BatchNumber = entity.BatchNumber;
                    bean.Quantity = entity.Quantity;
                    beanList.Add(bean);
                }
            }

            return beanList;
        }

        /// <summary>
        /// 库存明细接口--转换
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static InventoryDetailBean convertInventoryDetailInfo(InventoryEntity entity)
        {
            InventoryDetailBean bean = new InventoryDetailBean();
            if (entity != null)
            {
                bean.InventoryID = entity.InventoryID;
                bean.StorageID = entity.StorageID;
                bean.StorageName = entity.storages != null ? entity.storages.StorageName : "";
                bean.GoodsID = entity.GoodsID;

                bean.GoodsName = entity.goods != null ? entity.goods.GoodsName : "";
                bean.BatchNumber = entity.BatchNumber;
                bean.Quantity = entity.Quantity;
                bean.ProductDate = entity.ProductDate!=null?entity.ProductDate.ToString("yyyy-MM-dd"):"";
                bean.InventoryDate = entity.InventoryDate != null ? entity.InventoryDate.ToString("yyyy-MM-dd") : "";
                bean.ExpDate = entity.ExpDate != null ? entity.ExpDate.ToString("yyyy-MM-dd") : "";
                bean.InventoryLocationNo = "";
            }
            return bean;
        }

        /// <summary>
        /// 商品信息接口--转换
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static GoodsBean convertGoodsInfo(GoodsEntity entity)
        {
            GoodsBean bean = new GoodsBean();
            if (entity != null)
            {
                bean.GoodsID = entity.GoodsID;
                bean.CustomerID = entity.CustomerID;
                bean.CustomerName = entity.customer != null ? entity.customer.CustomerName : "";
                bean.GoodsNo = entity.GoodsNo;
                bean.GoodsName = entity.GoodsName;
                bean.GoodsModel = entity.GoodsModel;
                bean.Size = entity.Size;
                bean.exDate = entity.exDate + entity.exUnits;
            }
            return bean;
        }
    }
}