/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：MenuRepository
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：4/28/2018 9:56:46 AM
 * 修改人：
 * 修改时间：
 * 修改备注：
 * 代码请保留相关关键处的注释
 * ==============================================================================*/

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
    public class BaseDataRepository : DataAccessBase
    {

        public List<CityInfo> GetAllCity()
        {
            List<CityInfo> result = new List<CityInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(BaseDataStatement.GetAllCity, "Text"));
            result = command.ExecuteEntityList<CityInfo>();
            return result;
        }

        public List<CityInfo> GetAllHasCity()
        {
            List<CityInfo> result = new List<CityInfo>();
            string sqlText = BaseDataStatement.GetAllHasCity;
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(BaseDataStatement.GetAllHasCity, "Text"));
            result = command.ExecuteEntityList<CityInfo>();
            return result;
        }

        public List<ProvinceInfo> GetAllProvince()
        {
            List<ProvinceInfo> result = new List<ProvinceInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(BaseDataStatement.GetAllProvince, "Text"));
            result = command.ExecuteEntityList<ProvinceInfo>();
            return result;
        }

        public List<AttachmentInfo> GetAttachments(string keys)
        {
            List<AttachmentInfo> result = new List<AttachmentInfo>();
            if (!string.IsNullOrEmpty(keys))
            {
                string sqlText = BaseDataStatement.GetAttachmentByKey;
                sqlText = sqlText.Replace("#ids#", keys.Replace(",,",","));
                DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
                result = command.ExecuteEntityList<AttachmentInfo>();
            }
            return result;
        }

        public long CreateNewAttachment(AttachmentInfo attachment)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(BaseDataStatement.CreateAttachment, "Text"));
            command.AddInputParameter("@FileName", DbType.String, attachment.FileName);
            command.AddInputParameter("@FileExtendName", DbType.String, attachment.FileExtendName);
            command.AddInputParameter("@FilePath", DbType.String, attachment.FilePath);
            command.AddInputParameter("@UploadDate", DbType.String, attachment.UploadDate);
            command.AddInputParameter("@FileType", DbType.String, attachment.FileType);
            command.AddInputParameter("@BusinessType", DbType.String, attachment.BusinessType);
            command.AddInputParameter("@Channel", DbType.String, attachment.Channel);
            command.AddInputParameter("@FileSize", DbType.String, attachment.FileSize);
            command.AddInputParameter("@Remark", DbType.String, attachment.Remark);
            command.AddInputParameter("@Operator", DbType.Int64, attachment.Operator);
            command.AddInputParameter("@CreateDate", DbType.DateTime, attachment.CreateDate);

            var o=command.ExecuteScalar<object>();
            return Convert.ToInt64(o);
        }


        public List<BaseDataInfo> GetAllData()
        {
            List<BaseDataInfo> result = new List<BaseDataInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(BaseDataStatement.GetAllBaseData, "Text"));
            result = command.ExecuteEntityList<BaseDataInfo>();
            return result;
        }

        public List<BaseDataInfo> GetAllDataByPCode(string pcode)
        {
            List<BaseDataInfo> result = new List<BaseDataInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(BaseDataStatement.GetBaseDataByPCode, "Text"));
            command.AddInputParameter("@PCode", DbType.String, pcode);
            result = command.ExecuteEntityList<BaseDataInfo>();
            return result;
        }

        public List<BaseDataInfo> GetDataByType(string typeCode)
        {
            List<BaseDataInfo> result = new List<BaseDataInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(BaseDataStatement.GetBaseDataByType, "Text"));
            command.AddInputParameter("@TypeCode", DbType.String, typeCode);
            result = command.ExecuteEntityList<BaseDataInfo>();
            return result;
        }

        public List<BaseDataInfo> GetBaseDataByRule(string desc)
        {
            List<BaseDataInfo> result = new List<BaseDataInfo>();
            string sqlText = BaseDataStatement.GetAllBaseDataByRule;
            if (!string.IsNullOrEmpty(desc))
            {
                sqlText += " AND Description LIKE '%'+@key+'%'";
            }
            sqlText += " ORDER BY TypeCode";

            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
            if (!string.IsNullOrEmpty(desc))
            {
                command.AddInputParameter("@key", DbType.String, desc);
            }

            result = command.ExecuteEntityList<BaseDataInfo>();
            return result;
        }

        public BaseDataInfo GetBaseDataByKey(int id)
        {
            BaseDataInfo result = new BaseDataInfo();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(BaseDataStatement.GetBaseDataByKey, "Text"));
            command.AddInputParameter("@ID", DbType.Int32, id);
            result = command.ExecuteEntity<BaseDataInfo>();
            return result;
        }

        public int CreateNew(BaseDataInfo data)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(BaseDataStatement.CreateNewData, "Text"));
           command.AddInputParameter("@TypeCode",DbType.String,data.TypeCode);
		   command.AddInputParameter("@PCode",DbType.String,data.PCode);
		   command.AddInputParameter("@ValueInfo",DbType.String,data.ValueInfo);
		   command.AddInputParameter("@Description",DbType.String,data.Description);
		   command.AddInputParameter("@Status",DbType.Int32,data.Status);
           command.AddInputParameter("@CreateDate", DbType.DateTime, data.CreateDate);

            return command.ExecuteNonQuery();
        }

        public int ModifyData(BaseDataInfo data)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(BaseDataStatement.ModifyBaseData, "Text"));
            command.AddInputParameter("@ID", DbType.Int32, data.ID);
            command.AddInputParameter("@TypeCode", DbType.String, data.TypeCode);
            command.AddInputParameter("@PCode", DbType.String, data.PCode);
            command.AddInputParameter("@ValueInfo", DbType.String, data.ValueInfo);
            command.AddInputParameter("@Description", DbType.String, data.Description);
            command.AddInputParameter("@Status", DbType.Int32, data.Status);

            return command.ExecuteNonQuery();
        }

        public int Remove(int id)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(BaseDataStatement.Remove, "Text"));
            command.AddInputParameter("@ID", DbType.Int32, id);
            int result=command.ExecuteNonQuery();
            return result;
        }


        /// <summary>
        /// 短信验证码信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int AddVerificationCode(VerificationCodeInfo info)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(BaseDataStatement.InsertVerificationCodeSql, "Text"));
            command.AddInputParameter("@ID", DbType.Int32, info.ID);
            command.AddInputParameter("@Mobile", DbType.String, info.Mobile);
            command.AddInputParameter("@Email", DbType.String, info.Email);
            command.AddInputParameter("@VCode", DbType.String, info.VCode);
            command.AddInputParameter("@Status", DbType.Int32, info.Status);
            command.AddInputParameter("@DeadLine", DbType.DateTime, info.DeadLine);
            command.AddInputParameter("@CreateDate", DbType.DateTime, DateTime.Now);
            return command.ExecuteNonQuery();
        }

        /// <summary>
        /// 判断手机号 VCODE 是否有效
        /// </summary>
        /// <param name="telephone"></param>
        /// <param name="vcode"></param>
        /// <returns></returns>
        public VerificationCodeInfo CheckVerificationCode(string telephone, string vcode)
        {
            VerificationCodeInfo result = new VerificationCodeInfo();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(BaseDataStatement.CheckVerificationCodeSql, "Text"));
            command.AddInputParameter("@Mobile", DbType.String, telephone);
            command.AddInputParameter("@VCode", DbType.String, vcode);
            result = command.ExecuteEntity<VerificationCodeInfo>();
            return result;
        }

    }
}
