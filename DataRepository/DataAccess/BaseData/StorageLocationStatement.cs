using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.BaseData
{
    /// <summary>
    /// 库位管理
    /// </summary>
    public class StorageLocationStatement
    {
        public static string GetCount = @"SELECT COUNT(1) AS C FROM wms_StorageLocationInfo(NOLOCK) WHERE 1=1 ";

        public static string GetAllStorageLocationInfoPager = @" DECLARE @UP INT
        
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
		                                                  WITH storageLoaction AS
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY Status DESC) AS RowNumber FROM (SELECT * FROM wms_StorageLocationInfo WHERE 1=1 )as T ) 
		                                                  SELECT *  FROM storageLoaction 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";
        public static string GetAllStorageLocationInfoByRulePagerHeader = @"DECLARE @UP INT
        
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
		                                                  WITH storageLoaction AS
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY Status DESC) AS RowNumber FROM (SELECT * FROM wms_StorageLocationInfo WHERE 1=1 ";
        public static string GetAllStorageLocationInfoByRulePagerFooter = @")as T ) 
		                                                  SELECT *  FROM storageLoaction 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";

        public static string GetAllStorageLocation = @"SELECT * FROM wms_StorageLocationInfo(NOLOCK)";

        public static string GetAllStorageLocationByRule = @"SELECT * FROM wms_StorageLocationInfo(NOLOCK) WHERE 1=1 ";

        public static string GetStorageLocationByKey = @"SELECT * FROM wms_StorageLocationInfo(NOLOCK) WHERE StorageLocationID=@StorageLocationID";

        public static string Remove = @"UPDATE wms_StorageLocationInfo SET Status=0 WHERE StorageLocationID=@StorageLocationID";

        public static string GetStorageLocationByKeys = @"SELECT * FROM wms_StorageLocationInfo(NOLOCK) WHERE StorageLocationID IN (#ids#)";

        public static string CreateNew = @"INSERT INTO [wms_StorageLocationInfo]([StorageID],[StorageAreaNo],[StorageSubAreaNo],[StorageLocationNo],[Remark],IsLock,[OperatorID],[Status],[CreateDate],[ChangeDate])
                                                VALUES(@StorageID,@StorageAreaNo,@StorageSubAreaNo,@StorageLocationNo,@Remark,@IsLock,@OperatorID,@Status,@CreateDate,@ChangeDate) select @@IDENTITY";

        public static string Modify = @"UPDATE [wms_StorageLocationInfo]  SET [StorageID] = @StorageID,[StorageAreaNo] = @StorageAreaNo,StorageSubAreaNo = @StorageSubAreaNo,[StorageLocationNo] = @StorageLocationNo
                                            ,[Remark] = @Remark,IsLock=@IsLock,[OperatorID] = @OperatorID,[Status] = @Status,[ChangeDate] = @ChangeDate
                                             WHERE StorageLocationID=@StorageLocationID";

        #region 自定义SQL
        public static string GetAreaNo = @"SELECT distinct StorageAreaNo FROM wms_StorageLocationInfo where 1=1 ";

        public static string GetSubAreaNo = @"SELECT distinct StorageSubAreaNo from wms_StorageLocationInfo where 1=1 ";

        public static string GetAutoAllocationLocation = @"select {0} StorageLocationID,StorageAreaNo,StorageSubAreaNo,StorageLocationNo from wms_StorageLocationInfo where StorageID=1 and Status=1 and IsLock!='T' ";

        public static string ModifyLock = @"UPDATE wms_StorageLocationInfo SET IsLock=@IsLock where StorageLocationID=StorageLocationID";

        #endregion
    }
}
