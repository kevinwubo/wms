/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：StoreInfo
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：5/2/2018 1:17:24 PM
 * 修改人：
 * 修改时间：
 * 修改备注：
 * 代码请保留相关关键处的注释
 * ==============================================================================*/

using Infrastructure.EntityFactory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataModel
{
    [Serializable]
    public class StoreInfo
    {
        [DataMapping("SupplierID", DbType.Int32)]
        public int SupplierID { get; set; }
        [DataMapping("SupplierName", DbType.String)]
        public string SupplierName { get; set; }
        [DataMapping("SupplierCode", DbType.String)]
        public string SupplierCode { get; set; }
        [DataMapping("SupplierType", DbType.Int32)]
        public int SupplierType { get; set; }
        [DataMapping("CityID", DbType.Int32)]
        public int CityID { get; set; }
        [DataMapping("Address", DbType.String)]
        public string Address { get; set; }
        [DataMapping("Telephone", DbType.String)]
        public string Telephone { get; set; }
        [DataMapping("Mobile", DbType.String)]
        public string Mobile { get; set; }
        [DataMapping("StartTime", DbType.String)]
        public string StartTime { get; set; }
        [DataMapping("EndTime", DbType.String)]
        public string EndTime { get; set; }
        [DataMapping("Coordinate", DbType.String)]
        public string Coordinate { get; set; }
        [DataMapping("Status", DbType.Int32)]
        public int Status { get; set; }
        [DataMapping("CreateDate", DbType.DateTime)]
        public DateTime CreateDate { get; set; }
        [DataMapping("ModifyDate", DbType.DateTime)]
        public DateTime ModifyDate { get; set; }
        [DataMapping("Operator", DbType.Int64)]
        public long Operator { get; set; }
        [DataMapping("AttachmentIDs", DbType.String)]
        public string AttachmentIDs { get; set; }
    }
}
