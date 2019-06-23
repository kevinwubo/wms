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
    public class PriceSetStatement
    {
        public static string GetCount = @"SELECT COUNT(1) AS C FROM wms_PriceSetInfo(NOLOCK) WHERE 1=1 ";

        public static string GetAllPriceSetInfoPager = @" DECLARE @UP INT
        
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
		                                                  WITH priceset AS
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY Status DESC) AS RowNumber FROM (SELECT * FROM wms_PriceSetInfo WHERE 1=1 )as T ) 
		                                                  SELECT *  FROM priceset 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";
        public static string GetAllPriceSetInfoByRulePagerHeader = @"DECLARE @UP INT
        
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
		                                                  WITH priceset AS
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY Status DESC) AS RowNumber FROM (SELECT * FROM wms_PriceSetInfo WHERE 1=1 ";
        public static string GetAllPriceSetInfoByRulePagerFooter = @")as T ) 
		                                                  SELECT *  FROM priceset 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";

        public static string GetAllPriceSet = @"SELECT * FROM wms_PriceSetInfo(NOLOCK)";

        public static string GetAllPriceSetByRule = @"SELECT * FROM wms_PriceSetInfo(NOLOCK) WHERE 1=1 ";

        public static string GetPriceSetByKey = @"SELECT * FROM wms_PriceSetInfo(NOLOCK) WHERE PriceSetID=@PriceSetID";

        public static string Remove = @"UPDATE wms_PriceSetInfo SET Status=0 WHERE PriceSetID=@PriceSetID";

        public static string GetPriceSetByKeys = @"SELECT * FROM wms_PriceSetInfo(NOLOCK) WHERE PriceSetID IN (#ids#)";

        public static string CreateNewPriceSet = @"INSERT INTO [wms_PriceSetInfo]([CustomerID],[StorageID],[CarrierID],[ReceivingType],[ReceivingID],[TemType],[configPrice],[configHandInAmt]
                                                    ,[configSortPrice],[configCosting],[configHandOutAmt],[configSortCosting],[Remark],[Status],[OperatorID],[DeliveryModel],[CreateDate],[ChangeDate])
                                                         VALUES(@CustomerID,@StorageID,@CarrierID,@ReceivingType,@ReceivingID,@TemType,@configPrice,@configHandInAmt,@configSortPrice,@configCosting
                                                    ,@configHandOutAmt,@configSortCosting,@Remark,@Status,@OperatorID,@DeliveryModel,@CreateDate,@ChangeDate) select @@IDENTITY";

        public static string ModifyPriceSet = @"UPDATE [wms_PriceSetInfo]   SET [CustomerID] = @CustomerID,[StorageID] = @StorageID,[CarrierID] = @CarrierID,[ReceivingType] = @ReceivingType
                                                    ,[ReceivingID] = @ReceivingID,[TemType] = @TemType,[configPrice] = @configPrice,[configHandInAmt] = @configHandInAmt
                                                    ,[configSortPrice] = @configSortPrice,[configCosting] = @configCosting,[configHandOutAmt] = @configHandOutAmt,[configSortCosting] = @configSortCosting
                                                    ,[Remark] = @Remark,[Status] = @Status,[OperatorID] = @OperatorID,[ChangeDate] = @ChangeDate,DeliveryModel=@DeliveryModel WHERE PriceSetID = @PriceSetID";
    }
}
