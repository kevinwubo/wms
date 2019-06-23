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

namespace Service.BaseBiz
{
    public class StorageLocationService : BaseService
    {
        public static StorageLocationEntity GetStorageLocationEntityById(long cid)
        {
            StorageLocationEntity result = new StorageLocationEntity();
            StorageLocationRepository mr = new StorageLocationRepository();            
            StorageLocationInfo info = mr.GetStorageLocationByKey(cid);
            result = TranslateStorageLocationEntity(info);
        
            return result;
        }

        private static StorageLocationInfo TranslateStorageLocationInfo(StorageLocationEntity entity)
        {
            StorageLocationInfo info = new StorageLocationInfo();
            if (info != null)
            {
                info.StorageID = entity.StorageID;
                info.StorageAreaNo = entity.StorageAreaNo;
                info.StorageSubAreaNo = entity.StorageSubAreaNo;            
                info.StorageLocationNo = entity.StorageLocationNo;
                info.Remark = entity.Remark;
                info.OperatorID = entity.OperatorID;
                info.Status = entity.Status;
                info.IsLock = entity.IsLock;
                info.CreateDate = entity.CreateDate;
                info.ChangeDate = entity.ChangeDate;
                info.StorageLocationID = entity.StorageLocationID;
            }


            return info;
        }

        private static StorageLocationEntity TranslateStorageLocationEntity(StorageLocationInfo info)
        {
            StorageLocationEntity entity = new StorageLocationEntity();
            if (info != null)
            {
                entity.StorageID = info.StorageID;
                entity.StorageAreaNo = info.StorageAreaNo;
                entity.StorageSubAreaNo = info.StorageSubAreaNo;
                entity.StorageLocationNo = info.StorageLocationNo;
                entity.Remark = info.Remark;
                entity.IsLock = info.IsLock;
                entity.OperatorID = info.OperatorID;
                entity.Status = info.Status;
                entity.CreateDate = info.CreateDate;
                entity.ChangeDate = info.ChangeDate;
                entity.StorageLocationID = info.StorageLocationID;

                // 关联的仓库信息
                entity.storages = StorageService.GetStorageEntityById(entity.StorageID);    
            }

            return entity;
        }

        public static bool ModifyStorageLocation(StorageLocationEntity entity)
        {
            long result = 0;
            if (entity != null)
            {
                StorageLocationRepository mr = new StorageLocationRepository();

                StorageLocationInfo StorageLocationInfo = TranslateStorageLocationInfo(entity);

                if (entity.StorageLocationID > 0)
                {
                    StorageLocationInfo.StorageLocationID = entity.StorageLocationID;
                    StorageLocationInfo.ChangeDate = DateTime.Now;
                    result = mr.ModifyStorageLocation(StorageLocationInfo);
                }
                else
                {
                    StorageLocationInfo.ChangeDate = DateTime.Now;
                    StorageLocationInfo.CreateDate = DateTime.Now;
                    result = mr.CreateNew(StorageLocationInfo);               
                }               

                List<StorageLocationInfo> miList = mr.GetAllStorageLocation();//刷新缓存
                Cache.Add("StorageLocationALL", miList);
            }
            return result > 0;
        }

        public static StorageLocationEntity GetStorageLocationById(long gid)
        {
            StorageLocationEntity result = new StorageLocationEntity();
            StorageLocationRepository mr = new StorageLocationRepository();
            StorageLocationInfo info = mr.GetStorageLocationByKey(gid);
            result = TranslateStorageLocationEntity(info);
            return result;
        }

        public static List<StorageLocationEntity> GetStorageLocationAll()
        {
            List<StorageLocationEntity> all = new List<StorageLocationEntity>();
            StorageLocationRepository mr = new StorageLocationRepository();
            List<StorageLocationInfo> miList = mr.GetAllStorageLocation();//Cache.Get<List<StorageLocationInfo>>("StorageLocationALL");
            //if (miList.IsEmpty())
            //{
            //    miList = mr.GetAllStorageLocation();
            //    Cache.Add("StorageLocationALL", miList);
            //}
            if (!miList.IsEmpty())
            {
                foreach (StorageLocationInfo mInfo in miList)
                {
                    StorageLocationEntity StorageLocationEntity = TranslateStorageLocationEntity(mInfo);
                    all.Add(StorageLocationEntity);
                }
            }

            return all;

        }

        public static List<StorageLocationEntity> GetStorageLocationByRule(string name,int storageID, int status)
        {
            List<StorageLocationEntity> all = new List<StorageLocationEntity>();
            StorageLocationRepository mr = new StorageLocationRepository();
            List<StorageLocationInfo> miList = mr.GetStorageLocationByRule(name,storageID, status);

            if (!miList.IsEmpty())
            {
                foreach (StorageLocationInfo mInfo in miList)
                {
                    StorageLocationEntity StorageLocationEntity = TranslateStorageLocationEntity(mInfo);
                    all.Add(StorageLocationEntity);
                }
            }

            return all;

        }

        public static List<StorageLocationEntity> GetStorageLocationByKeys(string ids)
        {
            List<StorageLocationEntity> all = new List<StorageLocationEntity>();
            StorageLocationRepository mr = new StorageLocationRepository();
            List<StorageLocationInfo> miList = mr.GetStorageLocationByKeys(ids);

            if (!miList.IsEmpty())
            {
                foreach (StorageLocationInfo mInfo in miList)
                {
                    StorageLocationEntity entity = TranslateStorageLocationEntity(mInfo);
                    all.Add(entity);
                }
            }

            return all;
        }

        public static int Remove(long gid)
        {
            StorageLocationRepository mr = new StorageLocationRepository();
            int i= mr.Remove(gid);
            //List<StorageLocationInfo> miList = mr.GetAllStorageLocation();//刷新缓存
            //Cache.Add("StorageLocationALL", miList);
            return i;
        }

        #region 分页相关
        public static int GetStorageLocationCount(int StorageID, string storageAreaNo, string storagesubareano, int status)
        {
            return new StorageLocationRepository().GetStorageLocationCount(StorageID, storageAreaNo, storagesubareano, status);
        }

        public static List<StorageLocationEntity> GetStorageLocationInfoPager(PagerInfo pager)
        {
            List<StorageLocationEntity> all = new List<StorageLocationEntity>();
            StorageLocationRepository mr = new StorageLocationRepository();
            List<StorageLocationInfo> miList = mr.GetAllStorageLocationInfoPager(pager);
            if (!miList.IsEmpty())
            {
                foreach (StorageLocationInfo mInfo in miList)
                {
                    StorageLocationEntity carEntity = TranslateStorageLocationEntity(mInfo);
                    all.Add(carEntity);
                }
            }
            return all;
        }

        public static List<StorageLocationEntity> GetStorageLocationInfoByRule(int StorageID, string storageAreaNo, string storagesubareano, int status, PagerInfo pager)
        {
            List<StorageLocationEntity> all = new List<StorageLocationEntity>();
            StorageLocationRepository mr = new StorageLocationRepository();
            List<StorageLocationInfo> miList = mr.GetStorageLocationInfoByRule(StorageID, storageAreaNo, storagesubareano, status, pager);

            if (!miList.IsEmpty())
            {
                foreach (StorageLocationInfo mInfo in miList)
                {
                    StorageLocationEntity storeEntity = TranslateStorageLocationEntity(mInfo);
                    all.Add(storeEntity);
                }
            }

            return all;
        }
        #endregion


        #region 自定义SQL
        public static List<StorageLocationEntity> GetAreaNoByStorageID(int storageID, int status)
        {
            List<StorageLocationEntity> all = new List<StorageLocationEntity>();
            StorageLocationRepository mr = new StorageLocationRepository();
            List<StorageLocationInfo> miList = mr.GetAreaNoByStorageID( storageID, status);

            if (!miList.IsEmpty())
            {
                foreach (StorageLocationInfo mInfo in miList)
                {
                    StorageLocationEntity entity = new StorageLocationEntity();
                    entity.StorageAreaNo = mInfo.StorageAreaNo;
                    all.Add(entity);
                }
            }
            return all;
        }


        public static List<StorageLocationEntity> GetSubAreaNoByStorageAreaNo(string StorageAreaNo, int status)
        {
            List<StorageLocationEntity> all = new List<StorageLocationEntity>();
            StorageLocationRepository mr = new StorageLocationRepository();
            List<StorageLocationInfo> miList = mr.GetSubAreaNoByStorageAreaNo(StorageAreaNo, status);

            if (!miList.IsEmpty())
            {
                foreach (StorageLocationInfo mInfo in miList)
                {
                    StorageLocationEntity entity = new StorageLocationEntity();
                    entity.StorageSubAreaNo = mInfo.StorageSubAreaNo;
                    all.Add(entity);
                }
            }
            return all;
        }
        #endregion
    }
}
