/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：StoreEntity
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：5/2/2018 2:19:55 PM
 * 修改人：
 * 修改时间：
 * 修改备注：
 * 代码请保留相关关键处的注释
 * ==============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class StoreEntity
    {
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string SupplierCode { get; set; }
        public int SupplierType { get; set; }
        public int CityID { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Coordinate { get; set; }
        public string imageUrl { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public long Operator { get; set; }
        public string AttachmentIDs { get; set; }
        public string SupplierTypeName
        {
            get
            {
                string result = string.Empty;
                switch (SupplierType)
                {
                    case 1: result = "租赁店"; break;
                    case 2: result = "销售店"; break;
                    case 3: result = "租售一体"; break;
                }

                return result;
            }
        }

        public City CityInfo { get; set; }

        public List<AttachmentEntity> Attachments { get; set; }
    }
}
