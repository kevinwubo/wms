using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.BaseData
{
    /// <summary>
    /// 客户管理
    /// </summary>
    public class ReceiverStatement
    {
        public static string GetCount = @"SELECT COUNT(1) AS C FROM wms_ReceiverInfo(NOLOCK) WHERE 1=1 ";

        public static string GetAllReceiverInfoPager = @" DECLARE @UP INT
        
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
		                                                  WITH receiver AS
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY Status DESC) AS RowNumber FROM (SELECT * FROM wms_ReceiverInfo WHERE 1=1 )as T ) 
		                                                  SELECT *  FROM receiver 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";
        public static string GetAllReceiverInfoByRulePagerHeader = @"DECLARE @UP INT
        
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
		                                                  WITH receiver AS
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY Status DESC) AS RowNumber FROM (SELECT * FROM wms_ReceiverInfo WHERE 1=1 ";
        public static string GetAllReceiverInfoByRulePagerFooter = @")as T ) 
		                                                  SELECT *  FROM receiver 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";

        public static string GetAllReceiver = @"SELECT * FROM wms_ReceiverInfo(NOLOCK)";

        public static string GetAllReceiverByRule = @"SELECT * FROM wms_ReceiverInfo(NOLOCK) WHERE 1=1 ";

        public static string GetReceiverByKey = @"SELECT * FROM wms_ReceiverInfo(NOLOCK) WHERE ReceiverID=@ReceiverID";

        public static string GetReceiverByCustomerID = @"SELECT * FROM wms_ReceiverInfo(NOLOCK) WHERE CustomerID=@CustomerID";

        public static string Remove = @"UPDATE wms_ReceiverInfo SET Status=0 WHERE ReceiverID=@ReceiverID";

        public static string GetReceiverByKeys = @"SELECT * FROM wms_ReceiverInfo(NOLOCK) WHERE ReceiverID IN (#ids#)";

        public static string CreateNewReceiver = @"INSERT INTO [wms_ReceiverInfo]([CustomerID],[ReceiverNo],[ReceiverName],[ProvinceID],[ReceiverType],[CityID],[Address],[OperatorID],[Remark],[DefaultCarrierID],[DefaultStorageID],[Status],[CreateDate],[ChangeDate])     
                                                    VALUES(@CustomerID,@ReceiverNo,@ReceiverName,@ProvinceID,@ReceiverType,@CityID,@Address,@OperatorID,@Remark,@DefaultCarrierID,@DefaultStorageID,@Status,@CreateDate,@ChangeDate) SELECT @@IDENTITY";

        public static string ModifyReceiver = @"UPDATE [wms_ReceiverInfo] SET [CustomerID] = @CustomerID,[ReceiverNo] = @ReceiverNo,[ReceiverName] = @ReceiverName,[ProvinceID] = @ProvinceID
                                                ,[ReceiverType] = @ReceiverType,[CityID] = @CityID,[Address] = @Address,[OperatorID] = @OperatorID,[Remark] = @Remark
                                                ,[DefaultCarrierID] = @DefaultCarrierID,[Status] = @Status,[ChangeDate] = @ChangeDate,DefaultStorageID=@DefaultStorageID
                                                 WHERE ReceiverID = @ReceiverID";
    }
}
