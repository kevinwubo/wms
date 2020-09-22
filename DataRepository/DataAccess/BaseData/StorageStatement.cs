using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.Storage
{
    /// <summary>
    /// 仓库管理
    /// </summary>
    public class StorageStatement
    {
        public static string GetCount = @"SELECT COUNT(1) AS C FROM wms_StorageInfo(NOLOCK) WHERE 1=1 ";

        public static string GetAllStorageInfoPager = @" DECLARE @UP INT
        
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
		                                                  WITH Storage AS
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY Status DESC) AS RowNumber FROM (SELECT * FROM wms_StorageInfo WHERE 1=1 )as T ) 
		                                                  SELECT *  FROM Storage 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";
        public static string GetAllStorageInfoByRulePagerHeader = @"DECLARE @UP INT
        
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
		                                                  WITH Storage AS
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY Status DESC) AS RowNumber FROM (SELECT * FROM wms_StorageInfo WHERE 1=1 ";
        public static string GetAllStorageInfoByRulePagerFooter = @")as T ) 
		                                                  SELECT *  FROM Storage 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";

        public static string GetAllStorage = @"SELECT * FROM wms_StorageInfo(NOLOCK)";

        public static string GetAllStorageByRule = @"SELECT * FROM wms_StorageInfo(NOLOCK) WHERE 1=1 ";

        public static string GetStorageByKey = @"SELECT * FROM wms_StorageInfo(NOLOCK) WHERE StorageID=@StorageID";

        public static string Remove = @"UPDATE wms_StorageInfo SET Status=0 WHERE StorageID=@StorageID";


        public static string CreateNewStorage = @"INSERT INTO [wms_StorageInfo]([StorageName],[StorageNo],[ProvinceID],[CityID],[Address],[Status],[CreateDate],[ChangeDate],OperatorID)
                                                                        VALUES(@StorageName,@StorageNo,@ProvinceID,@CityID,@Address,@Status,@CreateDate,@ChangeDate,@OperatorID)SELECT @@IDENTITY";

        public static string ModifyStorage = @"UPDATE [wms_StorageInfo] SET [StorageName] = @StorageName,[StorageNo] = @StorageNo,[ProvinceID] = @ProvinceID,[CityID] = @CityID,
                                            [Address] = @Address,[ChangeDate] = @ChangeDate  ,OperatorID=@OperatorID ,Status=@Status
                                            WHERE StorageID=@StorageID";
    }
}
