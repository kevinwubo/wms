using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class StorageLocationEntity
    {
        /// <summary>
        /// 库位ID
        /// </summary>
        public int StorageLocationID { get; set; }
        /// <summary>
        /// 仓库ID
        /// </summary>
        public int StorageID { get; set; }

        /// <summary>
        /// 库位区域
        /// </summary>
        public string StorageAreaNo { get; set; }

        /// <summary>
        /// 库位子区域
        /// </summary>
        public string StorageSubAreaNo { get; set; }

        /// <summary>
        /// 库位编号
        /// </summary>
        public string StorageLocationNo { get; set; }

        /// <summary>
        /// 是否锁定  T/F
        /// </summary>
        public string IsLock { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public long OperatorID { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 状态   (1:使用中 0：已删除)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ChangeDate { get; set; }


        //关联对象
        /// <summary>
        /// 仓库信息
        /// </summary>
        public StorageEntity storages { get; set; }
    }
}
