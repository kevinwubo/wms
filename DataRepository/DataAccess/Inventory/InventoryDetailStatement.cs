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
    public class InventoryDetailStatement
    {
        public static string GetCount = @"SELECT COUNT(1) AS C FROM wms_InventoryDetailInfo(NOLOCK) WHERE 1=1 ";

        public static string GetAllInventoryDetailInfoPager = @" DECLARE @UP INT
        
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
		                                                  WITH invent AS
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY InventoryDetailID Desc) AS RowNumber FROM (SELECT * FROM wms_InventoryDetailInfo WHERE 1=1 )as T ) 
		                                                  SELECT *  FROM invent 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";
        public static string GetAllInventoryDetailInfoByRulePagerHeader = @"DECLARE @UP INT
        
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
		                                                  WITH invent AS
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY InventoryDetailID Desc) AS RowNumber FROM (SELECT * FROM wms_InventoryDetailInfo WHERE 1=1 ";
        public static string GetAllInventoryDetailInfoByRulePagerFooter = @")as T ) 
		                                                  SELECT *  FROM invent 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";

        public static string GetAllInventoryDetail = @"SELECT * FROM wms_InventoryDetailInfo(NOLOCK)";

        public static string GetAllInventoryDetailByRule = @"SELECT * FROM wms_InventoryDetailInfo(NOLOCK) WHERE 1=1 ";

        public static string GetInventoryDetailByKey = @"SELECT * FROM wms_InventoryDetailInfo(NOLOCK) WHERE InventoryDetailID=@InventoryDetailID";
        

        public static string GetInventoryDetailByKeys = @"SELECT * FROM wms_InventoryDetailInfo(NOLOCK) WHERE InventoryDetailID IN (#ids#)";

        public static string CreateNewInventoryDetail = @"INSERT INTO [wms_InventoryDetailInfo]
                                                ([InventoryID],[GoodsID],[StorageID],[Quantity],[CustomerID],[InventoryType],[BatchNumber],[ProductDate]
                                                ,[InventoryDate],[UnitPrice],[Remark],[OperatorID],[OrderType],OrderNo,OrderID,[CreateDate],[ChangeDate])
                                                     VALUES(@InventoryID,@GoodsID,@StorageID,@Quantity,@CustomerID,@InventoryType,@BatchNumber,@ProductDate
                                                ,@InventoryDate,@UnitPrice,@Remark,@OperatorID,@OrderType,@OrderNo,@OrderID,@CreateDate,@ChangeDate) select @@IDENTITY";

    }
}
