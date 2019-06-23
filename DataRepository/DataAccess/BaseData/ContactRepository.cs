using DataRepository.DataModel;
using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.BaseData
{
    public class ContactRepository : DataAccessBase
    {
        public List<ContactInfo> GetAllContact()
        {
            List<ContactInfo> result = new List<ContactInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(ContactStatement.GetAllContact, "Text"));
            result = command.ExecuteEntityList<ContactInfo>();
            return result;
        }

        public List<ContactInfo> GetContactByKeys(string keys)
        {
            List<ContactInfo> result = new List<ContactInfo>();
            if (!string.IsNullOrEmpty(keys))
            {
                string sqlText = ContactStatement.GetContactByKeys;
                sqlText = sqlText.Replace("#ids#", keys);
                DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
                result = command.ExecuteEntityList<ContactInfo>();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UnionType"></param>
        /// <param name="UnionID"></param>
        /// <returns></returns>
        public List<ContactInfo> GetContactsByRule(string UnionType, int UnionID)
        {
            List<ContactInfo> result = new List<ContactInfo>();
            string sqlText = ContactStatement.GetAllContactByRule;
            if (!string.IsNullOrEmpty(UnionType))
            {
                sqlText += " AND UnionType =@UnionType";
            }
            if (UnionID > -1)
            {
                sqlText += " AND UnionID=@UnionID";
            }
            sqlText += " AND Status=@Status";

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
            if (!string.IsNullOrEmpty(UnionType))
            {
                command.AddInputParameter("@UnionType", DbType.String, UnionType);
            }
            if (UnionID > -1)
            {
                command.AddInputParameter("@UnionID", DbType.Int32, UnionID);
            }
            command.AddInputParameter("@Status", DbType.Int32, 1);

            result = command.ExecuteEntityList<ContactInfo>();
            return result;
        }

        public ContactInfo GetContactByKey(long gid)
        {
            ContactInfo result = new ContactInfo();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(ContactStatement.GetContactByKey, "Text"));
            command.AddInputParameter("@ContactID", DbType.String, gid);
            result = command.ExecuteEntity<ContactInfo>();
            return result;
        }

        public int CreateNew(ContactInfo contact)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(ContactStatement.CreateNewContact, "Text"));
            command.AddInputParameter("@ContactName", DbType.String, contact.ContactName);
            command.AddInputParameter("@Mobile", DbType.String, contact.Mobile);
            command.AddInputParameter("@Telephone", DbType.String, contact.Telephone);
            command.AddInputParameter("@Email", DbType.String, contact.Email);
            command.AddInputParameter("@Remark", DbType.String, contact.Remark);
            command.AddInputParameter("@UnionType", DbType.String, contact.UnionType);
            command.AddInputParameter("@UnionID", DbType.Int32, contact.UnionID);
            command.AddInputParameter("@CreateDate", DbType.DateTime, contact.CreateDate);
            command.AddInputParameter("@ChangeDate", DbType.DateTime, contact.ChangeDate);

            return command.ExecuteNonQuery();
        }

        public int ModifyContact(ContactInfo contact)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(ContactStatement.ModifyContact, "Text"));
            command.AddInputParameter("@ContactName", DbType.String, contact.ContactName);
            command.AddInputParameter("@Mobile", DbType.String, contact.Mobile);
            command.AddInputParameter("@Telephone", DbType.String, contact.Telephone);
            command.AddInputParameter("@Email", DbType.String, contact.Email);
            command.AddInputParameter("@Remark", DbType.String, contact.Remark);
            command.AddInputParameter("@UnionType", DbType.String, contact.UnionType);
            command.AddInputParameter("@UnionID", DbType.Int32, contact.UnionID);
            command.AddInputParameter("@ChangeDate", DbType.DateTime, contact.ChangeDate);
            command.AddInputParameter("@ContactID", DbType.Int32, contact.ContactID);
            return command.ExecuteNonQuery();
        }

        public int RemoveContact(long gid)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(ContactStatement.Remove, "Text"));
            command.AddInputParameter("@ContactID", DbType.Int64, gid);
            int result = command.ExecuteNonQuery();
            return result;
        }
    }
}
