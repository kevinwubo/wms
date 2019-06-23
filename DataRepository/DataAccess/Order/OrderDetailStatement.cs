using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.Order
{
    public class OrderDetailStatement
    {

        public static string GetCount = @"SELECT COUNT(1) AS C FROM wms_OrderDetailInfo(NOLOCK) WHERE 1=1 ";

        public static string GetAllOrderDetailInfoPager = @" DECLARE @UP INT
        
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
		                                                  WITH order AS
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY Status DESC) AS RowNumber FROM (SELECT * FROM wms_OrderDetailInfo WHERE 1=1 )as T ) 
		                                                  SELECT *  FROM order 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";
        public static string GetAllOrderDetailInfoByRulePagerHeader = @"DECLARE @UP INT
        
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
		                                                  WITH order AS
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY Status DESC) AS RowNumber FROM (SELECT * FROM wms_OrderDetailInfo WHERE 1=1 ";
        public static string GetAllOrderDetailInfoByRulePagerFooter = @")as T ) 
		                                                  SELECT *  FROM order 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";

        public static string GetAllOrderDetail = @"SELECT * FROM wms_OrderDetailInfo(NOLOCK)";

        public static string GetAllOrderDetailByRule = @"SELECT * FROM wms_OrderDetailInfo(NOLOCK) WHERE 1=1 ";

        public static string GetAllOrderDetailByOrderID = @"SELECT * FROM wms_OrderDetailInfo(NOLOCK) WHERE OrderID=@OrderID ";

        public static string GetOrderDetailByKey = @"SELECT * FROM wms_OrderDetailInfo(NOLOCK) WHERE ID=@ID";

        //public static string Remove = @"UPDATE wms_OrderDetailInfo SET Status=0 WHERE OrderID=@OrderID";
        public static string Delete = @"DELETE FROM wms_OrderDetailInfo WHERE ID=@ID";


        public static string GetOrderDetailByKeys = @"SELECT * FROM wms_OrderDetailInfo(NOLOCK) WHERE OrderID IN (#ids#)";

        public static string CreateNewOrderDetail = @"INSERT INTO wms_OrderDetailInfo(OrderID,GoodsID,InventoryID,GoodsNo,GoodsName,GoodsModel,Quantity,Units,Weight,TotalWeight
                                                    ,BatchNumber,ProductDate,ExceedDate,CreateDate,ChangeDate)     VALUES
                                                    (@OrderID,@GoodsID,@InventoryID,@GoodsNo,@GoodsName,@GoodsModel,@Quantity,@Units,@Weight,@TotalWeight
                                                    ,@BatchNumber,@ProductDate,@ExceedDate,@CreateDate,@ChangeDate) select @@IDENTITY";

        public static string ModifyOrderDetail = @"UPDATE dbo.wms_OrderDetailInfo   SET OrderID = @OrderID,GoodsID = @GoodsID,InventoryID = @InventoryID,GoodsNo = @GoodsNo,GoodsName = @GoodsName
                                                    ,GoodsModel = @GoodsModel,Quantity = @Quantity,Units = @Units,Weight = @Weight,TotalWeight = @TotalWeight,BatchNumber = @BatchNumber,ProductDate = @ProductDate
                                                    ,ExceedDate = @ExceedDate,ChangeDate = @ChangeDate
                                                     WHERE ID = @ID";
    }
}
