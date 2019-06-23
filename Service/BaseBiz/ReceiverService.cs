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
    public class ReceiverService : BaseService
    {
        public static ReceiverEntity GetReceiverEntityById(long cid)
        {
            ReceiverEntity result = new ReceiverEntity();
            ReceiverRepository mr = new ReceiverRepository();            
            ReceiverInfo info = mr.GetReceiverByKey(cid);
            if (info != null)
            {
                result = TranslateReceiverEntity(info);
                //获取联系人信息
                result.listContact = ContactService.GetContactByRule(UnionType.Receiver.ToString(), info.ReceiverID);
            }
            return result;
        }

        private static ReceiverInfo TranslateReceiverInfo(ReceiverEntity entity)
        {
            ReceiverInfo info = new ReceiverInfo();
            if (entity != null)
            {
                info.CustomerID = entity.CustomerID;
                info.ReceiverType = entity.ReceiverType;
                info.DefaultCarrierID = entity.DefaultCarrierID;
                info.DefaultStorageID = entity.DefaultStorageID;
                info.ProvinceID = entity.ProvinceID;
                info.CityID = entity.CityID;
                info.Address = entity.Address;
                info.Remark = entity.Remark;

                info.OperatorID = entity.OperatorID;
                info.ReceiverName = entity.ReceiverName;
                info.ReceiverNo = entity.ReceiverNo;
                info.Status = entity.Status;
                info.CreateDate = entity.CreateDate;
                info.ChangeDate = entity.ChangeDate;
                info.ReceiverID = entity.ReceiverID;
            }
            return info;
        }

        private static ReceiverEntity TranslateReceiverEntity(ReceiverInfo info)
        {
            ReceiverEntity entity = new ReceiverEntity();
            if (info != null)
            {
                entity.CustomerID = info.CustomerID;
                entity.ReceiverType = info.ReceiverType;
                entity.DefaultCarrierID = info.DefaultCarrierID;
                entity.DefaultStorageID = info.DefaultStorageID;
                entity.ProvinceID = info.ProvinceID;
                entity.CityID = info.CityID;
                entity.Address = info.Address;
                entity.Remark = info.Remark;

                entity.OperatorID = info.OperatorID;
                entity.ReceiverName = info.ReceiverName;
                entity.ReceiverNo = info.ReceiverNo;
                entity.Status = info.Status;
                entity.CreateDate = info.CreateDate;
                entity.ChangeDate = info.ChangeDate;
                entity.ReceiverID = info.ReceiverID;

                City city = BaseDataService.GetAllCity().FirstOrDefault(t => t.CityID == info.CityID) ?? new City();
                Province province = BaseDataService.GetAllProvince().FirstOrDefault(t => t.ProvinceID == info.ProvinceID) ?? new Province();
                entity.province = province;
                entity.city = city;
                entity.customer = new CustomerEntity();
                entity.customer = CustomerService.GetCustomerById(info.CustomerID);
            }

            return entity;
        }

        public static bool ModifyReceiver(ReceiverEntity entity)
        {
            long result = 0;
            if (entity != null)
            {
                ReceiverRepository mr = new ReceiverRepository();

                ReceiverInfo ReceiverInfo = TranslateReceiverInfo(entity);

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

                if (entity.ReceiverID > 0)
                {
                    ReceiverInfo.ReceiverID = entity.ReceiverID;
                    ReceiverInfo.ChangeDate = DateTime.Now;
                    result = mr.ModifyReceiver(ReceiverInfo);
                }
                else
                {
                    ReceiverInfo.ChangeDate = DateTime.Now;
                    ReceiverInfo.CreateDate = DateTime.Now;
                    result = mr.CreateNew(ReceiverInfo);               
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
                            contact.UnionType = UnionType.Receiver.ToString();//客户
                            if (cc.ContactID > 0)
                            {
                                contact.ContactID = cc.ContactID;
                                contact.UnionID = entity.ReceiverID;
                                contact.ChangeDate = DateTime.Now;
                                cr.ModifyContact(contact);
                            }
                            else
                            {
                                contact.UnionID = entity.ReceiverID > 0 ? entity.ReceiverID : result;
                                contact.CreateDate = DateTime.Now;
                                contact.ChangeDate = DateTime.Now;
                                cr.CreateNew(contact);
                            }
                        }
                    }
                }
                #endregion  

                List<ReceiverInfo> miList = mr.GetAllReceiver();//刷新缓存
                Cache.Add("ReceiverALL", miList);
            }
            return result > 0;
        }

        public static ReceiverEntity GetReceiverById(long gid)
        {
            ReceiverEntity result = new ReceiverEntity();
            ReceiverRepository mr = new ReceiverRepository();
            ReceiverInfo info = mr.GetReceiverByKey(gid);
            result = TranslateReceiverEntity(info);
            return result;
        }

        public static List<ReceiverEntity> GetReceiverAll()
        {
            List<ReceiverEntity> all = new List<ReceiverEntity>();
            ReceiverRepository mr = new ReceiverRepository();
            List<ReceiverInfo> miList = Cache.Get<List<ReceiverInfo>>("ReceiverALL");
            if (miList.IsEmpty())
            {
                miList = mr.GetAllReceiver();
                Cache.Add("ReceiverALL", miList);
            }
            if (!miList.IsEmpty())
            {
                foreach (ReceiverInfo mInfo in miList)
                {
                    ReceiverEntity ReceiverEntity = TranslateReceiverEntity(mInfo);
                    all.Add(ReceiverEntity);
                }
            }

            return all;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="no">编号</param>
        /// <param name="receiverType">收货类型</param>
        /// <param name="status">状态 1：使用中 0：已删除</param>
        /// <returns></returns>
        public static List<ReceiverEntity> GetReceiverByRule(string name,string no,string receiverType, int status)
        {
            List<ReceiverEntity> all = new List<ReceiverEntity>();
            ReceiverRepository mr = new ReceiverRepository();
            List<ReceiverInfo> miList = mr.GetReceiverByRule(name,no, receiverType, status);

            if (!miList.IsEmpty())
            {
                foreach (ReceiverInfo mInfo in miList)
                {
                    ReceiverEntity ReceiverEntity = TranslateReceiverEntity(mInfo);
                    all.Add(ReceiverEntity);
                }
            }

            return all;

        }

        public static List<ReceiverEntity> GetReceiverByKeys(string ids)
        {
            List<ReceiverEntity> all = new List<ReceiverEntity>();
            ReceiverRepository mr = new ReceiverRepository();
            List<ReceiverInfo> miList = mr.GetReceiverByKeys(ids);

            if (!miList.IsEmpty())
            {
                foreach (ReceiverInfo mInfo in miList)
                {
                    ReceiverEntity entity = TranslateReceiverEntity(mInfo);
                    all.Add(entity);
                }
            }

            return all;
        }

        public static int Remove(long gid)
        {
            ReceiverRepository mr = new ReceiverRepository();
            int i= mr.Remove(gid);
            //List<ReceiverInfo> miList = mr.GetAllReceiver();//刷新缓存
            //Cache.Add("ReceiverALL", miList);
            return i;
        }

        #region 分页相关
        public static int GetReceiverCount(string name, string receiverType, int customerID, int status)
        {
            return new ReceiverRepository().GetReceiverCount(name, receiverType, customerID, status);
        }

        public static List<ReceiverEntity> GetReceiverInfoPager(PagerInfo pager)
        {
            List<ReceiverEntity> all = new List<ReceiverEntity>();
            ReceiverRepository mr = new ReceiverRepository();
            List<ReceiverInfo> miList = mr.GetAllReceiverInfoPager(pager);
            if (!miList.IsEmpty())
            {
                foreach (ReceiverInfo mInfo in miList)
                {
                    ReceiverEntity carEntity = TranslateReceiverEntity(mInfo);
                    all.Add(carEntity);
                }
            }
            return all;
        }

        public static List<ReceiverEntity> GetReceiverInfoByRule(string name, string receiverType, int customerID, int status, PagerInfo pager)
        {
            List<ReceiverEntity> all = new List<ReceiverEntity>();
            ReceiverRepository mr = new ReceiverRepository();
            List<ReceiverInfo> miList = mr.GetReceiverInfoByRule(name, receiverType, customerID, status, pager);

            if (!miList.IsEmpty())
            {
                foreach (ReceiverInfo mInfo in miList)
                {
                    ReceiverEntity storeEntity = TranslateReceiverEntity(mInfo);
                    all.Add(storeEntity);
                }
            }

            return all;
        }
        #endregion
    }
}
