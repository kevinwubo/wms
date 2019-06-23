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
    public class PriceSetService : BaseService
    {
        public static PriceSetEntity GetPriceSetEntityById(long cid)
        {
            PriceSetEntity result = new PriceSetEntity();
            PriceSetRepository mr = new PriceSetRepository();            
            PriceSetInfo info = mr.GetPriceSetByKey(cid);
            result = TranslatePriceSetEntity(info);
            //获取联系人信息
            //result.listContact= ContactService.GetContactByRule(UnionType.PriceSet.ToString(), info.PriceSetID);
            return result;
        }

        private static PriceSetInfo TranslatePriceSetInfo(PriceSetEntity entity)
        {
            PriceSetInfo info = new PriceSetInfo();
            if (entity != null)
            {
                info.PriceSetID = entity.PriceSetID;
                info.CustomerID = entity.CustomerID;
                info.StorageID = entity.StorageID;
                info.CarrierID = entity.CarrierID;
                info.ReceivingType = entity.ReceivingType;
                info.ReceivingID = entity.ReceivingID;
                info.TemType = entity.TemType;

                info.configPrice = entity.configPrice;
                info.configHandInAmt = entity.configHandInAmt;
                info.configSortPrice = entity.configSortPrice;
                info.configCosting = entity.configCosting;
                info.configHandOutAmt = entity.configHandOutAmt;
                info.configSortCosting = entity.configSortCosting;

                info.DeliveryModel = entity.DeliveryModel;
                info.Remark = entity.Remark;
                info.Status = entity.Status;
                info.CreateDate = entity.CreateDate;
                info.ChangeDate = entity.ChangeDate;
            }


            return info;
        }

        private static PriceSetEntity TranslatePriceSetEntity(PriceSetInfo info)
        {
            PriceSetEntity entity = new PriceSetEntity();
            if (info != null)
            {
                entity.PriceSetID = info.PriceSetID;
                entity.CustomerID = info.CustomerID;
                entity.StorageID = info.StorageID;
                entity.CarrierID = info.CarrierID;
                entity.ReceivingType = info.ReceivingType;
                entity.ReceivingID = info.ReceivingID;
                entity.TemType = info.TemType;

                entity.configPrice = info.configPrice;
                entity.configHandInAmt = info.configHandInAmt;
                entity.configSortPrice = info.configSortPrice;
                entity.configCosting = info.configCosting;
                entity.configHandOutAmt = info.configHandOutAmt;
                entity.configSortCosting = info.configSortCosting;

                entity.DeliveryModel = info.DeliveryModel;
                entity.Remark = info.Remark;
                entity.Status = info.Status;
                entity.CreateDate = info.CreateDate;
                entity.ChangeDate = info.ChangeDate;

                entity.customer = CustomerService.GetCustomerById(info.CustomerID);
                entity.storage = StorageService.GetStorageEntityById(info.StorageID);
                entity.carrier = CarrierService.GetCarrierById(info.CarrierID);
                entity.receiver = ReceiverService.GetReceiverById(info.ReceivingID);
                //if (!string.IsNullOrEmpty(entity.ReceivingType))
                //{
                //    if ("仓库".Equals(entity.ReceivingType))
                //    {
                //        StorageEntity sentity= StorageService.GetStorageEntityById(entity.ReceivingID);
                //        if (sentity != null)
                //        {
                //            entity.ReceivingName = sentity.StorageName;
                //        }
                //    }
                //    if ("门店".Equals(entity.ReceivingType))
                //    {
                //        StorageEntity sentity = StorageService.GetStorageEntityById(entity.ReceivingID);
                //        if (sentity != null)
                //        {
                //            entity.ReceivingName = sentity.StorageName;
                //        }
                //    }
                //}
            }

            return entity;
        }

        public static bool ModifyPriceSet(PriceSetEntity entity)
        {
            long result = 0;
            if (entity != null)
            {
                PriceSetRepository mr = new PriceSetRepository();

                PriceSetInfo PriceSetInfo = TranslatePriceSetInfo(entity);

                //ContactJsonEntity jsonlist = null;
                //if (!string.IsNullOrEmpty(entity.ContactJson))
                //{
                //    try
                //    {
                //        jsonlist = (ContactJsonEntity)JsonHelper.FromJson(entity.ContactJson, typeof(ContactJsonEntity));

                //    }
                //    catch (Exception ex)
                //    {
                //        string str = ex.ToString();                        
                //    }
                //}

                if (entity.PriceSetID > 0)
                {
                    PriceSetInfo.PriceSetID = entity.PriceSetID;
                    PriceSetInfo.ChangeDate = DateTime.Now;
                    result = mr.ModifyPriceSet(PriceSetInfo);
                }
                else
                {
                    PriceSetInfo.ChangeDate = DateTime.Now;
                    PriceSetInfo.CreateDate = DateTime.Now;
                    result = mr.CreateNew(PriceSetInfo);               
                }

                #region 更新联系人信息
                //if (jsonlist != null)
                //{
                //    List<ContactJson> list = jsonlist.listContact;
                //    if (list != null && list.Count > 0)
                //    {
                //        foreach (ContactJson cc in list)
                //        {
                //            ContactRepository cr = new ContactRepository();
                //            ContactInfo contact = new ContactInfo();
                //            contact.ContactName = cc.ContactName;
                //            contact.Mobile = cc.Mobile;
                //            contact.Telephone = cc.Telephone;
                //            contact.Email = cc.Email;
                //            contact.Remark = cc.Remark;
                //            contact.UnionType = UnionType.PriceSet.ToString();//承运商
                //            if (cc.ContactID > 0)
                //            {
                //                contact.ContactID = cc.ContactID;
                //                contact.UnionID = entity.PriceSetID;
                //                contact.ChangeDate = DateTime.Now;
                //                cr.ModifyContact(contact);
                //            }
                //            else
                //            {
                //                contact.UnionID = entity.PriceSetID > 0 ? entity.PriceSetID : result;
                //                contact.CreateDate = DateTime.Now;
                //                contact.ChangeDate = DateTime.Now;
                //                cr.CreateNew(contact);
                //            }
                //        }
                //    }
                //}
                #endregion  

                List<PriceSetInfo> miList = mr.GetAllPriceSet();//刷新缓存
                Cache.Add("PriceSetALL", miList);
            }
            return result > 0;
        }

        public static PriceSetEntity GetPriceSetById(long gid)
        {
            PriceSetEntity result = new PriceSetEntity();
            PriceSetRepository mr = new PriceSetRepository();
            PriceSetInfo info = mr.GetPriceSetByKey(gid);
            result = TranslatePriceSetEntity(info);
            return result;
        }

        public static List<PriceSetEntity> GetPriceSetAll()
        {
            List<PriceSetEntity> all = new List<PriceSetEntity>();
            PriceSetRepository mr = new PriceSetRepository();
            List<PriceSetInfo> miList = mr.GetAllPriceSet();//Cache.Get<List<PriceSetInfo>>("PriceSetALL");
            //if (miList.IsEmpty())
            //{
            //    miList = mr.GetAllPriceSet();
            //    Cache.Add("PriceSetALL", miList);
            //}
            if (!miList.IsEmpty())
            {
                foreach (PriceSetInfo mInfo in miList)
                {
                    PriceSetEntity PriceSetEntity = TranslatePriceSetEntity(mInfo);
                    all.Add(PriceSetEntity);
                }
            }

            return all;

        }

        public static List<PriceSetEntity> GetPriceSetByRule(string name, int customerid, int receivingid, int sendstorageid, int carrierid, int status)
        {
            List<PriceSetEntity> all = new List<PriceSetEntity>();
            PriceSetRepository mr = new PriceSetRepository();
            List<PriceSetInfo> miList = mr.GetPriceSetByRule(name, customerid, receivingid, sendstorageid, carrierid, status);

            if (!miList.IsEmpty())
            {
                foreach (PriceSetInfo mInfo in miList)
                {
                    PriceSetEntity PriceSetEntity = TranslatePriceSetEntity(mInfo);
                    all.Add(PriceSetEntity);
                }
            }

            return all;

        }

        public static List<PriceSetEntity> GetPriceSetByKeys(string ids)
        {
            List<PriceSetEntity> all = new List<PriceSetEntity>();
            PriceSetRepository mr = new PriceSetRepository();
            List<PriceSetInfo> miList = mr.GetPriceSetByKeys(ids);

            if (!miList.IsEmpty())
            {
                foreach (PriceSetInfo mInfo in miList)
                {
                    PriceSetEntity entity = TranslatePriceSetEntity(mInfo);
                    all.Add(entity);
                }
            }

            return all;
        }

        public static int Remove(long gid)
        {
            PriceSetRepository mr = new PriceSetRepository();
            int i= mr.Remove(gid);
            //List<PriceSetInfo> miList = mr.GetAllPriceSet();//刷新缓存
            //Cache.Add("PriceSetALL", miList);
            return i;
        }

        #region 分页相关
        public static int GetPriceSetCount(string name, int carrierid, int storageid,int customerid, int status)
        {
            return new PriceSetRepository().GetPriceSetCount(name, carrierid,  storageid, customerid, status);
        }

        public static List<PriceSetEntity> GetPriceSetInfoPager(PagerInfo pager)
        {
            List<PriceSetEntity> all = new List<PriceSetEntity>();
            PriceSetRepository mr = new PriceSetRepository();
            List<PriceSetInfo> miList = mr.GetAllPriceSetInfoPager(pager);
            if (!miList.IsEmpty())
            {
                foreach (PriceSetInfo mInfo in miList)
                {
                    PriceSetEntity carEntity = TranslatePriceSetEntity(mInfo);
                    all.Add(carEntity);
                }
            }
            return all;
        }

        public static List<PriceSetEntity> GetPriceSetInfoByRule(string name, int carrierid, int storageid, int customerid, int status, PagerInfo pager)
        {
            List<PriceSetEntity> all = new List<PriceSetEntity>();
            PriceSetRepository mr = new PriceSetRepository();
            List<PriceSetInfo> miList = mr.GetPriceSetInfoByRule(name,  carrierid, storageid,  customerid, status, pager);

            if (!miList.IsEmpty())
            {
                foreach (PriceSetInfo mInfo in miList)
                {
                    PriceSetEntity storeEntity = TranslatePriceSetEntity(mInfo);
                    all.Add(storeEntity);
                }
            }

            return all;
        }
        #endregion
    }
}
