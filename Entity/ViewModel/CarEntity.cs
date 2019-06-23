/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：CarEntity
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：5/7/2018 3:30:00 PM
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
    public class CarEntity
    {
        public long CarID { get; set; }
        public string CarName { get; set; }
        public string ModelCode { get; set; }
        public string CarModel { get; set; }
        public string AttachmentIDs { get; set; }
        public string ContractCode { get; set; }
        public string AppearanceSize { get; set; }
        public string PlateSize { get; set; }
        public string Capacity { get; set; }
        public string Slope { get; set; }
        public string MaxWeight { get; set; }
        public string Wheelbase { get; set; }
        public string BatteryCapacity { get; set; }
        public string Quality { get; set; }
        public string Braking { get; set; }
        public string MaxSpeed { get; set; }
        public string SiteNum { get; set; }
        public string BatteryType { get; set; }
        public string SafeConfigure { get; set; }
        public string OuterConfigure { get; set; }
        public string Renewal { get; set; }
        public int SupplierID { get; set; }
        public int Status { get; set; }

        public long BrandID { get; set; }

        public string CarLicNumber { get; set; }
        public DateTime ModifyDate { get; set; }
        public long Operator { get; set; }
        public decimal CuidePrice { get; set; }

        public decimal SalePrice { get; set; }
        public decimal LeasePrice { get; set; }
        public List<AttachmentEntity> AttachmentsInfo { get; set; }

        public BrandEntity CarBrand { get; set; }

        public UserEntity OperatorInfo { get; set; }


        public StoreEntity Store { get; set; }

    }
}
