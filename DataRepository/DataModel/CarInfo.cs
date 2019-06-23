/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：CarInfo
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：5/7/2018 3:29:19 PM
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
    public class CarInfo
    {
        [DataMapping("CarID", DbType.Int64)]
        public long CarID { get; set; }

        [DataMapping("CarName", DbType.String)]
        public string CarName { get; set; }

        [DataMapping("ModelCode", DbType.String)]
        public string ModelCode { get; set; }

        [DataMapping("CarModel", DbType.String)]
        public string CarModel { get; set; }

        [DataMapping("AttachmentIDs", DbType.String)]
        public string AttachmentIDs { get; set; }

        [DataMapping("ContractCode", DbType.String)]
        public string ContractCode { get; set; }

        [DataMapping("AppearanceSize", DbType.String)]
        public string AppearanceSize { get; set; }

        [DataMapping("PlateSize", DbType.String)]
        public string PlateSize { get; set; }

        [DataMapping("Capacity", DbType.String)]
        public string Capacity { get; set; }

        [DataMapping("Slope", DbType.String)]
        public string Slope { get; set; }

        [DataMapping("MaxWeight", DbType.String)]
        public string MaxWeight { get; set; }

        [DataMapping("Wheelbase", DbType.String)]
        public string Wheelbase { get; set; }

        [DataMapping("BatteryCapacity", DbType.String)]
        public string BatteryCapacity { get; set; }

        [DataMapping("Quality", DbType.String)]
        public string Quality { get; set; }

        [DataMapping("Braking", DbType.String)]
        public string Braking { get; set; }

        [DataMapping("MaxSpeed", DbType.String)]
        public string MaxSpeed { get; set; }

        [DataMapping("SiteNum", DbType.String)]
        public string SiteNum { get; set; }

        [DataMapping("BatteryType", DbType.String)]
        public string BatteryType { get; set; }

        [DataMapping("SafeConfigure", DbType.String)]
        public string SafeConfigure { get; set; }

        [DataMapping("OuterConfigure", DbType.String)]
        public string OuterConfigure { get; set; }

        [DataMapping("Renewal", DbType.String)]
        public string Renewal { get; set; }

        [DataMapping("SupplierID", DbType.Int32)]
        public int SupplierID { get; set; }

        [DataMapping("Status", DbType.Int32)]
        public int Status { get; set; }

        [DataMapping("CarLicNumber", DbType.String)]
        public string CarLicNumber { get; set; }

        [DataMapping("CuidePrice", DbType.Decimal)]
        public decimal CuidePrice { get; set; }

        [DataMapping("CreateDate", DbType.DateTime)]
        public DateTime CreateDate { get; set; }

        [DataMapping("ModifyDate", DbType.DateTime)]
        public DateTime ModifyDate { get; set; }

        [DataMapping("Operator", DbType.Int64)]
        public long Operator { get; set; }


        [DataMapping("SalePrice", DbType.Decimal)]
        public decimal SalePrice { get; set; }

        [DataMapping("LeasePrice", DbType.Decimal)]
        public decimal LeasePrice { get; set; }

        [DataMapping("BrandID", DbType.Int64)]
        public long BrandID { get; set; }
    }
}
