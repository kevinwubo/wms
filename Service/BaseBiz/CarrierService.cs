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
    public class CarrierService : BaseService
    {
        public static CarrierEntity GetCarrierEntityById(long cid)
        {
            CarrierEntity result = new CarrierEntity();
            CarrierRepository mr = new CarrierRepository();            
            CarrierInfo info = mr.GetCarrierByKey(cid);
            if (info != null)
            {
                result = TranslateCarrierEntity(info);
                //获取联系人信息
                result.listContact = ContactService.GetContactByRule(UnionType.Carrier.ToString(), info.CarrierID);
            }
            return result;
        }

        private static CarrierInfo TranslateCarrierInfo(CarrierEntity entity)
        {
            CarrierInfo info = new CarrierInfo();
            if (info != null)
            {
                info.OperatorID = entity.OperatorID;
                info.CarrierName = entity.CarrierName;
                info.CarrierShortName = entity.CarrierShortName;
                info.CarrierNo = entity.CarrierNo;
                info.Type = entity.Type;
                info.Remark = entity.Remark;
                info.Status = entity.Status;
                info.CreateDate = entity.CreateDate;
                info.ChangeDate = entity.ChangeDate;
                info.CarrierID = entity.CarrierID;
            }


            return info;
        }

        private static CarrierEntity TranslateCarrierEntity(CarrierInfo info)
        {
            CarrierEntity entity = new CarrierEntity();
            if (info != null)
            {

                entity.OperatorID = info.OperatorID;
                entity.CarrierName = info.CarrierName;
                entity.CarrierShortName = info.CarrierShortName;
                entity.CarrierNo = info.CarrierNo;
                entity.Type = info.Type;
                entity.Remark = info.Remark;
                entity.Status = info.Status;
                entity.CreateDate = info.CreateDate;
                entity.ChangeDate = info.ChangeDate;
                entity.CarrierID = info.CarrierID;
            }

            return entity;
        }

        public static bool ModifyCarrier(CarrierEntity entity)
        {
            long result = 0;
            if (entity != null)
            {
                CarrierRepository mr = new CarrierRepository();

                CarrierInfo CarrierInfo = TranslateCarrierInfo(entity);

                ContactJsonEntity jsonlist = null;
                if (!string.IsNullOrEmpty(entity.ContactJson))
                {
                    try
                    {
                        jsonlist = (ContactJsonEntity)JsonHelper.FromJson(entity.ContactJson, typeof(ContactJsonEntity));

                    }
                    catch (Exception ex)
                    {
                        string str = ex.ToString();                        
                    }
                }

                if (entity.CarrierID > 0)
                {
                    CarrierInfo.CarrierID = entity.CarrierID;
                    CarrierInfo.ChangeDate = DateTime.Now;
                    result = mr.ModifyCarrier(CarrierInfo);
                }
                else
                {
                    CarrierInfo.ChangeDate = DateTime.Now;
                    CarrierInfo.CreateDate = DateTime.Now;
                    result = mr.CreateNew(CarrierInfo);               
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
                            contact.UnionType = UnionType.Carrier.ToString();//承运商
                            if (cc.ContactID > 0)
                            {
                                contact.ContactID = cc.ContactID;
                                contact.UnionID = entity.CarrierID;
                                contact.ChangeDate = DateTime.Now;
                                cr.ModifyContact(contact);
                            }
                            else
                            {
                                contact.UnionID = entity.CarrierID > 0 ? entity.CarrierID : result;
                                contact.CreateDate = DateTime.Now;
                                contact.ChangeDate = DateTime.Now;
                                cr.CreateNew(contact);
                            }
                        }
                    }
                }
                #endregion  

                List<CarrierInfo> miList = mr.GetAllCarrier();//刷新缓存
                Cache.Add("CarrierALL", miList);
            }
            return result > 0;
        }

        public static CarrierEntity GetCarrierById(long gid)
        {
            CarrierEntity result = new CarrierEntity();
            CarrierRepository mr = new CarrierRepository();
            CarrierInfo info = mr.GetCarrierByKey(gid);
            result = TranslateCarrierEntity(info);
            return result;
        }

        public static List<CarrierEntity> GetCarrierAll()
        {
            List<CarrierEntity> all = new List<CarrierEntity>();
            CarrierRepository mr = new CarrierRepository();
            List<CarrierInfo> miList = Cache.Get<List<CarrierInfo>>("CarrierALL");
            if (miList.IsEmpty())
            {
                miList = mr.GetAllCarrier();
                Cache.Add("CarrierALL", miList);
            }
            if (!miList.IsEmpty())
            {
                foreach (CarrierInfo mInfo in miList)
                {
                    CarrierEntity CarrierEntity = TranslateCarrierEntity(mInfo);
                    all.Add(CarrierEntity);
                }
            }

            return all;

        }

        public static List<CarrierEntity> GetCarrierByRule(string name, int status)
        {
            List<CarrierEntity> all = new List<CarrierEntity>();
            CarrierRepository mr = new CarrierRepository();
            List<CarrierInfo> miList = mr.GetCarrierByRule(name, status);

            if (!miList.IsEmpty())
            {
                foreach (CarrierInfo mInfo in miList)
                {
                    CarrierEntity CarrierEntity = TranslateCarrierEntity(mInfo);
                    all.Add(CarrierEntity);
                }
            }

            return all;

        }

        public static List<CarrierEntity> GetCarrierByKeys(string ids)
        {
            List<CarrierEntity> all = new List<CarrierEntity>();
            CarrierRepository mr = new CarrierRepository();
            List<CarrierInfo> miList = mr.GetCarrierByKeys(ids);

            if (!miList.IsEmpty())
            {
                foreach (CarrierInfo mInfo in miList)
                {
                    CarrierEntity entity = TranslateCarrierEntity(mInfo);
                    all.Add(entity);
                }
            }

            return all;
        }

        public static int Remove(long gid)
        {
            CarrierRepository mr = new CarrierRepository();
            int i= mr.Remove(gid);
            //List<CarrierInfo> miList = mr.GetAllCarrier();//刷新缓存
            //Cache.Add("CarrierALL", miList);
            return i;
        }

        #region 分页相关
        public static int GetCarrierCount(string name, string mcode, int status)
        {
            return new CarrierRepository().GetCarrierCount(name, mcode, status);
        }

        public static List<CarrierEntity> GetCarrierInfoPager(PagerInfo pager)
        {
            List<CarrierEntity> all = new List<CarrierEntity>();
            CarrierRepository mr = new CarrierRepository();
            List<CarrierInfo> miList = mr.GetAllCarrierInfoPager(pager);
            foreach (CarrierInfo mInfo in miList)
            {
                CarrierEntity carEntity = TranslateCarrierEntity(mInfo);
                all.Add(carEntity);
            }
            return all;
        }

        public static List<CarrierEntity> GetCarrierInfoByRule(string name, string mcode, int status, PagerInfo pager)
        {
            List<CarrierEntity> all = new List<CarrierEntity>();
            CarrierRepository mr = new CarrierRepository();
            List<CarrierInfo> miList = mr.GetCarrierInfoByRule(name, mcode, status, pager);

            if (!miList.IsEmpty())
            {
                foreach (CarrierInfo mInfo in miList)
                {
                    CarrierEntity storeEntity = TranslateCarrierEntity(mInfo);
                    all.Add(storeEntity);
                }
            }

            return all;
        }
        #endregion
    }
}
