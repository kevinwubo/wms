using DataRepository.DataAccess.BaseData;
using DataRepository.DataModel;
using Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Helper;

namespace Service.BaseBiz
{
    public class ContactService : BaseService
    {
        private static ContactInfo TranslateContactInfo(ContactEntity entity)
        {
            ContactInfo info = new ContactInfo();
            if (entity != null)
            {                
                info.ContactID = entity.ContactID;
                info.ContactName = entity.ContactName;
                info.Mobile = entity.Mobile;
                info.Telephone = entity.Telephone;
                info.Email = entity.Email;
                info.Remark = entity.Remark;
                info.Status = entity.Status;
                info.CreateDate = entity.CreateDate;
                info.ChangeDate = entity.ChangeDate;
            }


            return info;
        }

        private static ContactEntity TranslateContactEntity(ContactInfo info)
        {
            ContactEntity entity = new ContactEntity();
            if (info != null)
            {
                entity.ContactID = info.ContactID;
                entity.ContactName = info.ContactName;
                entity.Mobile = info.Mobile;
                entity.Telephone = info.Telephone;
                entity.Email = info.Email;
                entity.Remark = info.Remark;
                entity.Status = info.Status;
                entity.CreateDate = info.CreateDate;
                entity.ChangeDate = info.ChangeDate;
            }

            return entity;
        }

        public static bool ModifyContact(ContactEntity entity)
        {
            int result = 0;
            if (entity != null)
            {
                ContactRepository mr = new ContactRepository();

                ContactInfo info = TranslateContactInfo(entity);


                if (entity.ContactID > 0)
                {
                    result = mr.ModifyContact(info);
                }
                else
                {
                    info.CreateDate = DateTime.Now;
                    result = mr.CreateNew(info);
                }

                List<ContactInfo> miList = mr.GetAllContact();//刷新缓存
                Cache.Add("ContactALL", miList);
            }
            return result > 0;
        }

        public static ContactEntity GetContactById(long gid)
        {
            ContactEntity result = new ContactEntity();
            ContactRepository mr = new ContactRepository();
            ContactInfo info = mr.GetContactByKey(gid);
            result = TranslateContactEntity(info);
            return result;
        }

        public static List<ContactEntity> GetContactAll()
        {
            List<ContactEntity> all = new List<ContactEntity>();
            ContactRepository mr = new ContactRepository();
            List<ContactInfo> miList = Cache.Get<List<ContactInfo>>("ContactALL");
            if (miList.IsEmpty())
            {
                miList = mr.GetAllContact();
                Cache.Add("ContactALL", miList);
            }
            if (!miList.IsEmpty())
            {
                foreach (ContactInfo mInfo in miList)
                {
                    ContactEntity ContactEntity = TranslateContactEntity(mInfo);
                    all.Add(ContactEntity);
                }
            }

            return all;

        }

        public static List<ContactEntity> GetContactByRule(string UnionType, int UnionID)
        {
            List<ContactEntity> all = new List<ContactEntity>();
            ContactRepository mr = new ContactRepository();
            List<ContactInfo> miList = mr.GetContactsByRule(UnionType, UnionID);

            if (!miList.IsEmpty())
            {
                foreach (ContactInfo mInfo in miList)
                {
                    ContactEntity ContactEntity = TranslateContactEntity(mInfo);
                    all.Add(ContactEntity);
                }
            }

            return all;

        }

        public static List<ContactEntity> GetContactByKeys(string ids)
        {
            List<ContactEntity> all = new List<ContactEntity>();
            ContactRepository mr = new ContactRepository();
            List<ContactInfo> miList = mr.GetContactByKeys(ids);

            if (!miList.IsEmpty())
            {
                foreach (ContactInfo mInfo in miList)
                {
                    ContactEntity entity = TranslateContactEntity(mInfo);
                    all.Add(entity);
                }
            }

            return all;
        }

        public static int Remove(long gid)
        {
            ContactRepository mr = new ContactRepository();
            return mr.RemoveContact(gid);
            //List<ContactInfo> miList = mr.GetAllContact();//刷新缓存
            //Cache.Add("ContactALL", miList);
        }
    }
}
