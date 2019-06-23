using Infrastructure.EntityFactory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataModel
{
    public class StorageInfo
    {
        /// <summary>
        /// 仓库ID
        /// </summary>
        [DataMapping("StorageID", DbType.Int32)]
        public int StorageID { get; set; }

        /// <summary>
        /// 仓库名称
        /// </summary>
        [DataMapping("StorageName", DbType.String)]
        public string StorageName { get; set; }

        /// <summary>
        /// 仓库编号
        /// </summary>
        [DataMapping("StorageNo", DbType.String)]
        public string StorageNo { get; set; }

        /// <summary>
        /// 省份ID
        /// </summary>
        [DataMapping("ProvinceID", DbType.Int32)]
        public int ProvinceID { get; set; }

        /// <summary>
        /// 城市ID
        /// </summary>
        [DataMapping("CityID", DbType.Int32)]
        public int CityID { get; set; }

        /// <summary>
        /// 仓库编号
        /// </summary>
        [DataMapping("Address", DbType.String)]
        public string Address { get; set; }

        /// <summary>
        /// 状态 1：使用中 0：已删除
        /// </summary>
        [DataMapping("Status", DbType.Int32)]
        public int Status { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        [DataMapping("OperatorID", DbType.Int64)]
        public long OperatorID { get; set; }
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
