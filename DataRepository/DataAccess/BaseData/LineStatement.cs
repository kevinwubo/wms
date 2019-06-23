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
    public class LineStatement
    {

        public static string GetAllLine = @"SELECT * FROM wms_LineInfo(NOLOCK)";

        public static string GetAllLineByRule = @"SELECT * FROM wms_LineInfo(NOLOCK) WHERE 1=1 ";

        public static string GetLineByKey = @"SELECT * FROM wms_LineInfo(NOLOCK) WHERE ID=@ID";

        public static string Delete = @"Delete FROM wms_LineInfo WHERE ID=@ID";

        public static string CreateNewLine = @"INSERT INTO dbo.wms_LineInfo(LineID,ReceiverName,Address,OperatorID,Remark,CreateDate,ChangeDate)
                                            VALUES(@LineID,@ReceiverName,@Address,@OperatorID,@Remark,@CreateDate,@ChangeDate) select @@IDENTITY";

        public static string ModifyLine = @"UPDATE dbo.wms_LineInfo SET LineID = @LineID,ReceiverName = @ReceiverName,Address = @Address,OperatorID = @OperatorID,Remark = @Remark,ChangeDate = @ChangeDate WHERE ID=@ID";
        #region 分页相关
         public static string GetCount = @"SELECT COUNT(1) AS C FROM wms_LineInfo(NOLOCK) WHERE 1=1 ";

        public static string GetAllLineInfoPager = @" DECLARE @UP INT
        
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
		                                                  WITH lines AS
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY LineID ) AS RowNumber FROM (SELECT * FROM wms_LineInfo WHERE 1=1 )as T ) 
		                                                  SELECT *  FROM lines 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";
        public static string GetAllLineInfoByRulePagerHeader = @"DECLARE @UP INT
        
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
		                                                  WITH lines AS
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY LineID ) AS RowNumber FROM (SELECT * FROM wms_LineInfo WHERE 1=1 ";
        public static string GetAllLineInfoByRulePagerFooter = @")as T ) 
		                                                  SELECT *  FROM lines 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";
        #endregion

    }
}
