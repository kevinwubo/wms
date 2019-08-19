﻿using Common;
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
                        InventoryRepository mr = new InventoryRepository();
                        InventoryInfo info = new InventoryInfo();
                        if (entity != null)
                        {
                            info.GoodsID = entity.GoodsID;
                            info.StorageID = StorageID;
                            info.Quantity = entity.Quantity;
                            info.CustomerID = entity.CustomerID;
                            info.InventoryType = Common.InventoryType.入库.ToString();
                            info.BatchNumber = entity.BatchNumber;
                            info.ProductDate = DateTime.Parse(entity.ProductDate);
                            info.InventoryDate = DateTime.Parse(inventoryDate);
                            info.UnitPrice = entity.UnitPrice;
                            info.Remark = entity.Remark;
                            info.OperatorID = OperatorID;
                            info.CreateDate = DateTime.Now;
                            info.ChangeDate = DateTime.Now;


                            //List<InventoryInfo> listInventory = mr.GetInventoryByRule(entity.GoodsID,-1, entity.BatchNumber);
                            //if (listInventory != null && listInventory.Count > 0)
                            //{
                            //    InventoryInfo oldInfo = listInventory[0];
                            //    if (type == OperatorType.IN)
                            //    {
                            //        info.Quantity += oldInfo.Quantity;//库存数量更新
                            //    }
                            //    else
                            //    {
                            //        info.Quantity = info.Quantity - oldInfo.Quantity;//库存数量更新
                            //    }
                            //    //更新库存数量
                            //    mr.ModifyInventoryQuantity(info);
                            //}
                            //else
                            //{
                                //插入库存
                                mr.CreateNew(info);
                            //}
                            listInv.Add(info);

                            #region 库存明细保存                            
                            CreateInventoryDetail(entity, StorageID, DateTime.Parse(inventoryDate), OperatorID);
                            #endregion
                        }
                    }
                    //生成入库单
                    OrderService.CreateOrderByInventory(listInv, tempType);
                }
            }
            catch (Exception)
            {
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
        public static void CreateInventoryDetail(InvGoodsDetailEntity entity, int StorageID, DateTime inventoryDate, long OperatorID)
        {
            InventoryDetailRepository mrDetail = new InventoryDetailRepository();
            InventoryDetailInfo infoDetail = new InventoryDetailInfo();
            if (entity != null)
            {
                infoDetail.GoodsID = entity.GoodsID;
                infoDetail.StorageID = StorageID;
                infoDetail.Quantity = entity.Quantity;
                infoDetail.CustomerID = entity.CustomerID;
                infoDetail.InventoryType = Common.InventoryType.入库.ToString();
                infoDetail.BatchNumber = entity.BatchNumber;
                infoDetail.ProductDate = DateTime.Parse(entity.ProductDate);
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

                entity.goods = GoodsService.GetGoodsEntityById(info.GoodsID);
                entity.customer = CustomerService.GetCustomerEntityById(info.CustomerID);
                entity.storages = StorageService.GetStorageEntityById(info.StorageID);
            }

            return entity;
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

        public static List<InventoryEntity> GetInventoryByRule(int goodsid, int storageID, string batchNumber)
        {
            List<InventoryEntity> all = new List<InventoryEntity>();
            InventoryRepository mr = new InventoryRepository();
            List<InventoryInfo> miList = mr.GetInventoryByRule(goodsid, storageID, batchNumber);

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
                                list.Add(entity);
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
                        info.Remark = "";
                        info.OperatorID = operatorID;
                        info.CreateDate = DateTime.Now;
                        info.ChangeDate = DateTime.Now;
                        mr.CreateNew(info);
                        listInv.Add(info);

                    }
                }
                //生成入库单
                OrderService.CreateOrderByInventory(listInv);
            }
            return count;

        }

    }
}
