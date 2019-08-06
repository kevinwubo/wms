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
    public class InventoryStatement
    {
        public static string GetCount = @"SELECT COUNT(1) AS C FROM wms_InventoryInfo(NOLOCK) WHERE 1=1 ";

        public static string GetAllInventoryInfoPager = @" DECLARE @UP INT
        
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
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY Status DESC) AS RowNumber FROM (SELECT * FROM wms_InventoryInfo WHERE 1=1 )as T ) 
		                                                  SELECT *  FROM invent 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";
        public static string GetAllInventoryInfoByRulePagerHeader = @"DECLARE @UP INT
        
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
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY Status DESC) AS RowNumber FROM (SELECT * FROM wms_InventoryInfo WHERE 1=1 ";
        public static string GetAllInventoryInfoByRulePagerFooter = @")as T ) 
		                                                  SELECT *  FROM invent 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";

        public static string GetAllInventory = @"SELECT * FROM wms_InventoryInfo(NOLOCK)";

        public static string GetAllInventoryByRule = @"SELECT * FROM wms_InventoryInfo(NOLOCK) WHERE 1=1 ";

        public static string GetInventoryByKey = @"SELECT * FROM wms_InventoryInfo(NOLOCK) WHERE InventoryID=@InventoryID";

        public static string Remove = @"UPDATE wms_InventoryInfo SET Status=0 WHERE InventoryID=@InventoryID  ";

        public static string Delete = @"DELETE FROM wms_InventoryInfo WHERE InventoryID=@InventoryID ";

        public static string GetInventoryByKeys = @"SELECT * FROM wms_InventoryInfo(NOLOCK) WHERE InventoryID IN (#ids#)";

        public static string CreateNewInventory = @"INSERT INTO [wms_InventoryInfo]
                                                ([GoodsID],[StorageID],[Quantity],[CustomerID],[InventoryType],[BatchNumber],[ProductDate]
                                                ,[InventoryDate],[UnitPrice],[Remark],[OperatorID],[CreateDate],[ChangeDate])
                                                     VALUES(@GoodsID,@StorageID,@Quantity,@CustomerID,@InventoryType,@BatchNumber,@ProductDate
                                                ,@InventoryDate,@UnitPrice,@Remark,@OperatorID,@CreateDate,@ChangeDate) select @@IDENTITY";

        public static string ModifyInventory = @"UPDATE [wms_InventoryInfo]   SET [GoodsID] = @GoodsID,[StorageID] = @StorageID,[Quantity] = @Quantity,[CustomerID] = @CustomerID
                                                ,[InventoryType] = @InventoryType,[BatchNumber] = @BatchNumber,[ProductDate] = @ProductDate,[InventoryDate] = @InventoryDate
                                                ,[UnitPrice] = @UnitPrice,[Remark] = @Remark,[OperatorID] = @OperatorID,[ChangeDate] = @ChangeDate
                                                 WHERE InventoryID=@InventoryID";
        public static string ModifyInventoryQuantity = "UPDATE [wms_InventoryInfo]   SET [Quantity] = @Quantity ,[ChangeDate] = @ChangeDate WHERE InventoryID=@InventoryID";
        /// <summary>
        /// 盘点 更改库存状态 为锁定 盘点中
        /// </summary>
        public static string ModifyInventoryStatus = @"UPDATE wms_InventoryInfo SET IsLock=@IsLock,InventoryStatus=@InventoryStatus WHERE InventoryID=@InventoryID";
    }
}
