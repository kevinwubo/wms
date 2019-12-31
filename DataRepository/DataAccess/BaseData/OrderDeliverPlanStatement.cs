using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.BaseData
{
    /// <summary>
    /// 承运商管理
    /// </summary>
    public class OrderDeliverPlanStatement
    {

        public static string GetAllOrderDeliverPlan = @"SELECT * FROM wms_OrderDeliverPlan(NOLOCK)";

        public static string GetAllOrderDeliverPlanByRule = @"SELECT * FROM wms_OrderDeliverPlan(NOLOCK) WHERE 1=1 ";

        public static string GetOrderDeliverPlanByKey = @"SELECT * FROM wms_OrderDeliverPlan(NOLOCK) WHERE PlanID=@PlanID";

        public static string Delete = @"Delete FROM wms_OrderDeliverPlan WHERE ID=@ID";

        public static string CreateNewOrderDeliverPlan = @"INSERT INTO wms_OrderDeliverPlan(OrderIDS,CarrierName,CarrierID,Temp,DeliveryType,DriverName,DriverTelephone,CarModel,CarNo,DeliverDate,Remark,OperatorID,CreateDate,ChangeDate,OilCardNo,OilCardBalance,GPSNo,NeedTicket,DeliveryNo)
                                                VALUES(@OrderIDS,@CarrierName,@CarrierID,@Temp,@DeliveryType,@DriverName,@DriverTelephone,@CarModel,@CarNo,@DeliverDate,@Remark,@OperatorID,@CreateDate,@ChangeDate,@OilCardNo,@OilCardBalance,@GPSNo,@NeedTicket,@DeliveryNo) select @@IDENTITY";

        public static string ModifyOrderDeliverPlan = @"UPDATE wms_OrderDeliverPlan   SET OrderIDS=@OrderIDS,CarrierName = @CarrierName,CarrierID=@CarrierID,Temp = @Temp,DeliveryType = @DeliveryType,DriverName = @DriverName
                                        ,DriverTelephone = @DriverTelephone,CarModel = @CarModel,CarNo = @CarNo,DeliverDate = @DeliverDate,Remark = @Remark,OperatorID = @OperatorID,ChangeDate = @ChangeDate
                                        ,OilCardNo = @OilCardNo,OilCardBalance = @OilCardBalance,GPSNo = @GPSNo,NeedTicket = @NeedTicket
                                         WHERE PlanID=@PlanID";
        #region 分页相关
         public static string GetCount = @"SELECT COUNT(1) AS C FROM wms_OrderDeliverPlan(NOLOCK) WHERE 1=1 ";

        public static string GetAllOrderDeliverPlanInfoPager = @" DECLARE @UP INT
        
	                                                  ---------分页区间计算-------------最大页数
                                                      IF(@recordCount<@PageSize*(@PageIndex-1)) 
                                                      BEGIN
                                                        SET @PageIndex= @recordCount/@PageSize+1
                                                      END
                                                      --最小页数
	                                                  IF(@PageIndex<1)
	                                                     SET @PageIndex=1
                                                      --当前页起始游标值
	                                                  IF(@recordCount>@PageSize*(@PageIndex-1))
	                                                  BEGIN
		                                                  SET @UP=@PageSize*(@PageIndex-1);
		                                                  ---------分页查询-----------
		                                                  WITH OrderDeliverPlans AS
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY DeliverDate DESC ) AS RowNumber FROM (SELECT * FROM wms_OrderDeliverPlan WHERE 1=1 )as T ) 
		                                                  SELECT *  FROM OrderDeliverPlans 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";
        public static string GetAllOrderDeliverPlanInfoByRulePagerHeader = @"DECLARE @UP INT
        
	                                                  ---------分页区间计算-------------最大页数
                                                      IF(@recordCount<@PageSize*(@PageIndex-1)) 
                                                      BEGIN
                                                        SET @PageIndex= @recordCount/@PageSize+1
                                                      END
                                                      --最小页数
	                                                  IF(@PageIndex<1)
	                                                     SET @PageIndex=1
                                                      --当前页起始游标值
	                                                  IF(@recordCount>@PageSize*(@PageIndex-1))
	                                                  BEGIN
		                                                  SET @UP=@PageSize*(@PageIndex-1);
		                                                  ---------分页查询-----------
		                                                  WITH OrderDeliverPlans AS
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY DeliverDate DESC ) AS RowNumber FROM (SELECT * FROM wms_OrderDeliverPlan WHERE 1=1 ";
        public static string GetAllOrderDeliverPlanInfoByRulePagerFooter = @")as T ) 
		                                                  SELECT *  FROM OrderDeliverPlans 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";
        #endregion

    }
}
