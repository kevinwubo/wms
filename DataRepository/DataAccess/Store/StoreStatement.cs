using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.Store
{
    public class StoreStatement
    {
        public static string GetAllStorePager = @" DECLARE @UP INT
        
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
		                                                  WITH store AS
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY Status DESC) AS RowNumber FROM (SELECT * FROM StoreInfo WHERE 1=1 )as T ) 
		                                                  SELECT *  FROM store 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";
        public static string GetAllStoreInfoByRulePagerHeader = @"DECLARE @UP INT
        
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
		                                                  WITH store AS
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY Status DESC) AS RowNumber FROM (SELECT * FROM StoreInfo WHERE 1=1 ";
        public static string GetAllStoreInfoByRulePagerFooter = @")as T ) 
		                                                  SELECT *  FROM store 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";

        public static string GetStoreCount = @"SELECT COUNT(1) AS C FROM StoreInfo(NOLOCK) WHERE 1=1 ";

        public static string GetAllStores = @"SELECT * FROM [StoreInfo](NOLOCK)";

        public static string GetAllStoresByRule = @"SELECT * FROM StoreInfo(NOLOCK) WHERE 1=1 ";

        public static string GetStoreByKey = @"SELECT * FROM StoreInfo(NOLOCK) WHERE SupplierID=@SupplierID";

        public static string Remove = @"UPDATE StoreInfo SET Status=0 WHERE SupplierID=@SupplierID";

        public static string GetStoreByKeys = @"SELECT * FROM StoreInfo(NOLOCK) WHERE SupplierID IN (#ids#)";

        public static string CreateNewStore = @"INSERT INTO [StoreInfo]([SupplierName],[SupplierCode],[SupplierType],[CityID],[Address],[Telephone],[Mobile],[StartTime],[EndTime],[Coordinate],[Status],[CreateDate],[ModifyDate],[Operator],[AttachmentIDs]) 
                                                          VALUES (@SupplierName,@SupplierCode,@SupplierType,@CityID,@Address,@Telephone,@Mobile,@StartTime,@EndTime,@Coordinate,@Status,@CreateDate,@ModifyDate,@Operator,@AttachmentIDs)";

        public static string ModifyStore = @"UPDATE [StoreInfo]
                                               SET SupplierName=@SupplierName,SupplierCode=@SupplierCode,SupplierType=@SupplierType,CityID=@CityID,Address=@Address,
                                                   Telephone=@Telephone,Mobile=@Mobile,StartTime=@StartTime,EndTime=@EndTime,Coordinate=@Coordinate,Status=@Status,
                                                   ModifyDate=@ModifyDate,Operator=@Operator,AttachmentIDs=@AttachmentIDs
                                             WHERE [SupplierID]=@SupplierID";
    }
}
