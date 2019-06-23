using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class EnumHelper
    {

    }
    //承运商：Carrier 仓库：Storage  客户：Customer  门店：Store
    public enum UnionType
    {
        Carrier,
        Storage,
        Customer,
        Store,
        Receiver
    }

    //库存类型
    public enum InventoryType
    {
        出库,
        入库
    }

    //订单类型：
    public enum OrderType
    {
        运输订单A,
        运输订单B,
        仓配订单,
        调拨订单, 
        报损订单
    }

    /// <summary>
    /// 库存状态
    /// </summary>
    public enum InventoryStatus
    {
        盘点中,
        盘点完成,
    }

    /// <summary>
    /// 库存操作
    /// </summary>
    public enum OperatorType
    {
        IN,
        OUT
    }

    /// <summary>
    /// 订单状态
    /// </summary>
    public enum OrderStatus
    {
        未配送 = 0,
        配送中 = 1,
        已配送 = 2,
        已回单 = 3,
        订单完成 = 4,
        已拒绝 = 5
    }

    /// <summary>
    /// 订单来源
    ///手工录入 = "Hand",大润发订单 = "RtMart",家乐福订单 = "Carrefour",卜蜂莲花订单 = "Lotus",上蔬永辉订单 = "YongHui",Costa订单 = "Costa",常规订单 = "Regular"
    /// </summary>
    public enum OrderSource
    {
        Hand,
        RtMart,
        Carrefour,
        Lotus,
        YongHui,
        Costa,
        Regular
    }

    public enum TypeDesc
    {
        送货单,
        补损单
    }
}
