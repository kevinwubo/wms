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
    public class BlackListStatement
    {

        public static string GetAllBlackList = @"SELECT * FROM wms_BlackListInfo(NOLOCK)";

        public static string GetAllBlackListByRule = @"SELECT * FROM wms_BlackListInfo(NOLOCK) WHERE 1=1 ";

        public static string GetBlackListByKey = @"SELECT * FROM wms_BlackListInfo(NOLOCK) WHERE BlackID=@BlackID";

        public static string Delete = @"Delete FROM wms_BlackListInfo WHERE BlackID=@BlackID";

        public static string CreateNewBlackList = @"INSERT INTO wms_BlackListInfo(BlackType,UnionID,UnionName,Status,SubStatus,CardCode,Remark
                                                    ,OperatorID,CreateDate,ChangeDate)
                                                    VALUES(@BlackType,@UnionID,@UnionName,@Status,@SubStatus,@CardCode,@Remark,@OperatorID,@CreateDate,@ChangeDate) select @@IDENTITY";

        public static string ModifyBlackList = @"UPDATE wms_BlackListInfo   SET BlackType = @BlackType,UnionID = @UnionID,UnionName = @UnionName,Status = @Status
                                                ,SubStatus = @SubStatus,CardCode = @CardCode,Remark = @Remark,OperatorID = @OperatorID,ChangeDate = @ChangeDate
                                                 WHERE BlackID=@BlackID";
        #region 分页相关
         public static string GetCount = @"SELECT COUNT(1) AS C FROM wms_BlackListInfo(NOLOCK) WHERE 1=1 ";

        public static string GetAllBlackListInfoPager = @" DECLARE @UP INT
        
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
		                                                  WITH BlackLists AS
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY CreateDate ) AS RowNumber FROM (SELECT * FROM wms_BlackListInfo WHERE 1=1 )as T ) 
		                                                  SELECT *  FROM BlackLists 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";
        public static string GetAllBlackListInfoByRulePagerHeader = @"DECLARE @UP INT
        
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
		                                                  WITH BlackLists AS
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY CreateDate ) AS RowNumber FROM (SELECT * FROM wms_BlackListInfo WHERE 1=1 ";
        public static string GetAllBlackListInfoByRulePagerFooter = @")as T ) 
		                                                  SELECT *  FROM BlackLists 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";
        #endregion

    }
}
