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
    public class CustomerService : BaseService
    {
        public static CustomerEntity GetCustomerEntityById(long cid)
        {
            CustomerEntity result = new CustomerEntity();
            CustomerRepository mr = new CustomerRepository();
            CustomerInfo info = mr.GetCustomerByKey(cid);
            if (info != null)
            {
                result = TranslateCustomerEntity(info);
                //获取联系人信息
                result.listContact = ContactService.GetContactByRule(UnionType.Customer.ToString(), info.CustomerID);
            }
            return result;
        }

        private static CustomerInfo TranslateCustomerInfo(CustomerEntity entity)
        {
            CustomerInfo info = new CustomerInfo();
            if (entity != null)
            {
                info.OperatorID = entity.OperatorID;
                info.CustomerName = entity.CustomerName;
                info.CustomerNo = entity.CustomerNo;
                info.Status = entity.Status;
                info.CreateDate = entity.CreateDate;
                info.ChangeDate = entity.ChangeDate;
                info.CustomerID = entity.CustomerID;
            }
            return info;
        }

        private static CustomerEntity TranslateCustomerEntity(CustomerInfo info)
        {
            CustomerEntity entity = new CustomerEntity();
            if (info != null)
            {
                entity.OperatorID = info.OperatorID;
                entity.CustomerName = info.CustomerName;
                entity.CustomerNo = info.CustomerNo;
                entity.Status = info.Status;
                entity.CreateDate = info.CreateDate;
                entity.ChangeDate = info.ChangeDate;
                entity.CustomerID = info.CustomerID;
            }

            return entity;
        }

        public static bool ModifyCustomer(CustomerEntity entity)
        {
            long result = 0;
            if (entity != null)
            {
                CustomerRepository mr = new CustomerRepository();

                CustomerInfo info = TranslateCustomerInfo(entity);

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

                if (entity.CustomerID > 0)
                {
                    info.CustomerID = entity.CustomerID;
                    info.ChangeDate = DateTime.Now;
                    result = mr.ModifyCustomer(info);
                }
                else
                {
                    info.ChangeDate = DateTime.Now;
                    info.CreateDate = DateTime.Now;
                    result = mr.CreateNew(info);               
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
                            contact.UnionType = UnionType.Customer.ToString();//客户
                            if (cc.ContactID > 0)
                            {
                                contact.ContactID = cc.ContactID;
                                contact.UnionID = entity.CustomerID;
                                contact.ChangeDate = DateTime.Now;
                                cr.ModifyContact(contact);
                            }
                            else
                            {
                                contact.UnionID = entity.CustomerID > 0 ? entity.CustomerID : result;
                                contact.CreateDate = DateTime.Now;
                                contact.ChangeDate = DateTime.Now;
                                cr.CreateNew(contact);
                            }
                        }
                    }
                }
                #endregion  

                List<CustomerInfo> miList = mr.GetAllCustomer();//刷新缓存
                Cache.Add("CustomerALL", miList);
            }
            return result > 0;
        }

        public static CustomerEntity GetCustomerById(long gid)
        {
            CustomerEntity result = new CustomerEntity();
            CustomerRepository mr = new CustomerRepository();
            CustomerInfo info = mr.GetCustomerByKey(gid);
            result = TranslateCustomerEntity(info);
            return result;
        }

        public static List<CustomerEntity> GetCustomerAll()
        {
            List<CustomerEntity> all = new List<CustomerEntity>();
            CustomerRepository mr = new CustomerRepository();
            List<CustomerInfo> miList = Cache.Get<List<CustomerInfo>>("CustomerALL");
            if (miList.IsEmpty())
            {
                miList = mr.GetAllCustomer();
                Cache.Add("CustomerALL", miList);
            }
            if (!miList.IsEmpty())
            {
                foreach (CustomerInfo mInfo in miList)
                {
                    CustomerEntity CustomerEntity = TranslateCustomerEntity(mInfo);
                    all.Add(CustomerEntity);
                }
            }

            return all;

        }

        public static List<CustomerEntity> GetCustomerByRule(string name, int status)
        {
            List<CustomerEntity> all = new List<CustomerEntity>();
            CustomerRepository mr = new CustomerRepository();
            List<CustomerInfo> miList = mr.GetCustomerByRule(name, status);

            if (!miList.IsEmpty())
            {
                foreach (CustomerInfo mInfo in miList)
                {
                    CustomerEntity CustomerEntity = TranslateCustomerEntity(mInfo);
                    all.Add(CustomerEntity);
                }
            }

            return all;

        }

        public static List<CustomerEntity> GetCustomerByKeys(string ids)
        {
            List<CustomerEntity> all = new List<CustomerEntity>();
            CustomerRepository mr = new CustomerRepository();
            List<CustomerInfo> miList = mr.GetCustomerByKeys(ids);

            if (!miList.IsEmpty())
            {
                foreach (CustomerInfo mInfo in miList)
                {
                    CustomerEntity entity = TranslateCustomerEntity(mInfo);
                    all.Add(entity);
                }
            }

            return all;
        }

        public static int Remove(long gid)
        {
            CustomerRepository mr = new CustomerRepository();
            int i= mr.Remove(gid);
            //List<info> miList = mr.GetAllCustomer();//刷新缓存
            //Cache.Add("CustomerALL", miList);
            return i;
        }

        #region 分页相关
        public static int GetCustomerCount(string name, string mcode, int status)
        {
            return new CustomerRepository().GetCustomerCount(name, mcode, status);
        }

        public static List<CustomerEntity> GetCustomerInfoPager(PagerInfo pager)
        {
            List<CustomerEntity> all = new List<CustomerEntity>();
            CustomerRepository mr = new CustomerRepository();
            List<CustomerInfo> miList = mr.GetAllCustomerInfoPager(pager);
            foreach (CustomerInfo mInfo in miList)
            {
                CustomerEntity carEntity = TranslateCustomerEntity(mInfo);
                all.Add(carEntity);
            }
            return all;
        }

        public static List<CustomerEntity> GetCustomerInfoByRule(string name, string mcode, int status, PagerInfo pager)
        {
            List<CustomerEntity> all = new List<CustomerEntity>();
            CustomerRepository mr = new CustomerRepository();
            List<CustomerInfo> miList = mr.GetCustomerInfoByRule(name, mcode, status, pager);

            if (!miList.IsEmpty())
            {
                foreach (CustomerInfo mInfo in miList)
                {
                    CustomerEntity storeEntity = TranslateCustomerEntity(mInfo);
                    all.Add(storeEntity);
                }
            }

            return all;
        }
        #endregion
    }
}
