using DataRepository.DataAccess.Storage;
using DataRepository.DataModel;
using Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Infrastructure.Helper;
using DataRepository.DataAccess.BaseData;

namespace Service.BaseBiz
{
    public class StorageService : BaseService
    {
        public static StorageEntity GetStorageEntityById(long cid)
        {
            StorageEntity result = new StorageEntity();
            StorageRepository mr = new StorageRepository();
            StorageInfo info = mr.GetStorageByKey(cid);
            if (info != null)
            {
                result = TranslateStorageEntity(info);
                //获取联系人信息
                result.listContact = ContactService.GetContactByRule(UnionType.Storage.ToString(), info.StorageID);
            }
            return result;
        }


        public static List<StorageEntity> GetStorageByRule(string name, int status)
        {
            List<StorageEntity> all = new List<StorageEntity>();
            StorageRepository mr = new StorageRepository();
            List<StorageInfo> miList = mr.GetStorageByRule(name, status);

            if (!miList.IsEmpty())
            {
                foreach (StorageInfo mInfo in miList)
                {
                    StorageEntity StorageEntity = TranslateStorageEntity(mInfo);
                    all.Add(StorageEntity);
                }
            }

            return all;

        }


        public static List<StorageEntity> GetStorageAll()
        {
            List<StorageEntity> all = new List<StorageEntity>();
            StorageRepository mr = new StorageRepository();
            List<StorageInfo> miList = Cache.Get<List<StorageInfo>>("StorageALL");
            if (miList.IsEmpty())
            {
                miList = mr.GetAllstorage();
                Cache.Add("StorageALL", miList);
            }
            if (!miList.IsEmpty())
            {
                foreach (StorageInfo mInfo in miList)
                {
                    StorageEntity storageEntity = TranslateStorageEntity(mInfo);
                    all.Add(storageEntity);
                }
            }

            return all;

        }

        public static void RemoveStorage(string StorageID)
        {
            StorageRepository mr = new StorageRepository();
            mr.RemoveStorage(long.Parse(StorageID));
        }

        public static bool ModifyStorage(StorageEntity StorageEntity)
        {
            long result = 0;
            StorageRepository mr = new StorageRepository();
            if (StorageEntity != null)
            {                
                StorageInfo StorageInfo = TranslateStorageInfo(StorageEntity);

                ContactJsonEntity jsonlist = null;
                if (!string.IsNullOrEmpty(StorageEntity.ContactJson))
                {
                    try
                    {
                        jsonlist = (ContactJsonEntity)JsonHelper.FromJson(StorageEntity.ContactJson, typeof(ContactJsonEntity));

                    }
                    catch (Exception ex)
                    {
                        string str = ex.ToString();
                    }
                }

                if (StorageEntity.StorageID > 0)
                {
                    StorageInfo.ChangeDate = DateTime.Now;
                    result = mr.ModifyStorage(StorageInfo);
                }
                else
                {
                    StorageInfo.CreateDate = DateTime.Now;
                    StorageInfo.ChangeDate = DateTime.Now;
                    result = mr.CreateNew(StorageInfo);
                }

                #region 更新联系人信息
                if (jsonlist != null)
                {
                    List<ContactJson> list = jsonlist.listContact;
                    if (list != null && list.Count > 0)
                    {
                        foreach (ContactJson cc in list)
                        {
                            ContactRepository cr = new ContactRepository();
                            ContactInfo contact = new ContactInfo();
                            contact.ContactName = cc.ContactName;
                            contact.Mobile = cc.Mobile;
                            contact.Telephone = cc.Telephone;
                            contact.Email = cc.Email;
                            contact.Remark = cc.Remark;
                            contact.UnionType = UnionType.Storage.ToString();//仓库
                            if (cc.ContactID > 0)
                            {
                                contact.ContactID = cc.ContactID;
                                contact.UnionID = StorageEntity.StorageID;
                                contact.ChangeDate = DateTime.Now;
                                cr.ModifyContact(contact);
                            }
                            else
                            {
                                contact.UnionID = StorageEntity.StorageID > 0 ? StorageEntity.StorageID : result;
                                contact.CreateDate = DateTime.Now;
                                contact.ChangeDate = DateTime.Now;
                                cr.CreateNew(contact);
                            }
                        }
                    }
                }
                #endregion  
            }
            List<StorageInfo> miList = mr.GetAllstorage();
            Cache.Add("StorageALL", miList);
            return result > 0;
        }

        /// <summary>
        /// 从info到Entity
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private static StorageEntity TranslateStorageEntity(StorageInfo info)
        {
            StorageEntity entity = new StorageEntity();
            if (info != null)
            {
                entity.StorageID = info.StorageID;
                entity.StorageName = info.StorageName;
                entity.StorageNo = info.StorageNo;
                entity.ProvinceID = info.ProvinceID;
                entity.CityID = info.CityID;
                entity.Address = info.Address;
                entity.OperatorID = info.OperatorID;
                entity.Status = info.Status;
                entity.CreateDate = info.CreateDate;
                entity.ChangeDate = info.ChangeDate;
                City city = BaseDataService.GetAllCity().FirstOrDefault(t => t.CityID == info.CityID) ?? new City();
                Province province = BaseDataService.GetAllProvince().FirstOrDefault(t => t.ProvinceID == info.ProvinceID) ?? new Province();
                entity.province = province;
                entity.city = city;
            }
            return entity;
        }



        /// <summary>
        /// 从Entity到info
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private static StorageInfo TranslateStorageInfo(StorageEntity entity)
        {
            StorageInfo info = new StorageInfo();
            if (entity != null)
            {
                info.StorageID = entity.StorageID;
                info.StorageName = entity.StorageName;
                info.StorageNo = entity.StorageNo;
                info.ProvinceID = entity.ProvinceID;
                info.CityID = entity.CityID;
                info.Address = entity.Address;
                info.OperatorID = entity.OperatorID;
                info.Status = entity.Status;
                info.CreateDate = entity.CreateDate;
                info.ChangeDate = entity.ChangeDate;
            }
            return info;
        }


        #region 分页相关
        public static int GetStorageCount(string name, string mcode, int status)
        {
            return new StorageRepository().GetStorageCount(name,mcode,status);
        }

        public static List<StorageEntity> GetStorageInfoPager(PagerInfo pager)
        {
            List<StorageEntity> all = new List<StorageEntity>();
            StorageRepository mr = new StorageRepository();
            List<StorageInfo> miList = mr.GetAllStorageInfoPager(pager);
            if (!miList.IsEmpty())
            {
                foreach (StorageInfo mInfo in miList)
                {
                    StorageEntity carEntity = TranslateStorageEntity(mInfo);
                    all.Add(carEntity);
                }
            }
            return all;
        }

        public static List<StorageEntity> GetStorageInfoByRule(string name, string mcode, int status, PagerInfo pager)
        {
            List<StorageEntity> all = new List<StorageEntity>();
            StorageRepository mr = new StorageRepository();
            List<StorageInfo> miList = mr.GetStorageInfoByRule(name, mcode, status, pager);

            if (!miList.IsEmpty())
            {
                foreach (StorageInfo mInfo in miList)
                {
                    StorageEntity storeEntity = TranslateStorageEntity(mInfo);
                    all.Add(storeEntity);
                }
            }

            return all;
        }
        #endregion
    }
}
