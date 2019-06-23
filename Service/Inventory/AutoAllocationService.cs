using DataRepository.DataAccess.BaseData;
using DataRepository.DataModel;
using Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Inventory
{
    /// <summary>
    /// 仓库自动分配规则
    /// </summary>
    public class AutoAllocationService
    {
        /// <summary>
        /// 入库自动分配库位
        /// </summary>
        /// <param name="count"></param>
        public static void InInventoryAutoAllocation(int storageID,int count, List<InvGoodsDetailEntity> listGoods)
        {
            StorageLocationRepository sr = new StorageLocationRepository(); 
            if (count > 0 && listGoods != null && listGoods.Count > 0)
            {
                List<StorageLocationInfo> listSL = sr.GetAutoAllocationLocation(storageID, count);
            }
        }
    }
}
