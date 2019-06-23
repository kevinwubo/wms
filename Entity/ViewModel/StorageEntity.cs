using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class StorageEntity
    {
        /// <summary>
        /// 仓库ID
        /// </summary>
        public int StorageID { get; set; }

        /// <summary>
        /// 仓库名称
        /// </summary>
        public string StorageName { get; set; }

        /// <summary>
        /// 仓库编号
        /// </summary>
        public string StorageNo { get; set; }

        /// <summary>
        /// 省份ID
        /// </summary>
        public int ProvinceID { get; set; }

        /// <summary>
        /// 城市ID
        /// </summary>
        public int CityID { get; set; }

        /// <summary>
        /// 仓库地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 状态 1：使用中 0：已删除
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public long OperatorID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ChangeDate { get; set; }

        public string ContactJson { get; set; }
        
        //关联对象
        /// <summary>
        /// 联系人信息
        /// </summary>
        public List<ContactEntity> listContact { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public Province province { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public City city { get; set; }
    }
}
