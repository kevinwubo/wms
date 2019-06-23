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
    public class StorageLocationInfo
    {
        
        /// <summary>
        /// 库位ID
        /// </summary>
        [DataMapping("StorageLocationID", DbType.Int32)]
        public int StorageLocationID { get; set; }
        /// <summary>
        /// 仓库ID
        /// </summary>
        [DataMapping("StorageID", DbType.Int32)]
        public int StorageID { get; set; }

        /// <summary>
        /// 库位区域
        /// </summary>
        [DataMapping("StorageAreaNo", DbType.String)]
        public string StorageAreaNo { get; set; }

        /// <summary>
        /// 库位子区域
        /// </summary>
        [DataMapping("StorageSubAreaNo", DbType.String)]
        public string StorageSubAreaNo { get; set; }

        /// <summary>
        /// 库位编号
        /// </summary>
        [DataMapping("StorageLocationNo", DbType.String)]
        public string StorageLocationNo { get; set; }

        /// <summary>
        /// 是否锁定  T/F
        /// </summary>
        [DataMapping("IsLock", DbType.String)]
        public string IsLock { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        [DataMapping("OperatorID", DbType.Int64)]
        public long OperatorID { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMapping("Remark", DbType.String)]
        public string Remark { get; set; }

        /// <summary>
        /// 状态   (1:使用中 0：已删除)
        /// </summary>
        [DataMapping("Status", DbType.Int32)]
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMapping("CreateDate", DbType.DateTime)]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [DataMapping("ChangeDate", DbType.DateTime)]
        public DateTime ChangeDate { get; set; }
    }
}
