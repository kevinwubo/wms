using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.Goods
{
    /// <summary>
    /// 商品管理
    /// </summary>
    public class GoodsStatement
    {
        public static string GetCount = @"SELECT COUNT(1) AS C FROM wms_GoodsInfo(NOLOCK) WHERE 1=1 ";

        public static string GetAllGoodsInfoPager = @" DECLARE @UP INT
        
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
		                                                  WITH goods AS
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY Status DESC) AS RowNumber FROM (SELECT * FROM wms_GoodsInfo WHERE 1=1 )as T ) 
		                                                  SELECT *  FROM goods 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";
        public static string GetAllGoodsInfoByRulePagerHeader = @"DECLARE @UP INT
        
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
		                                                  WITH goods AS
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY Status DESC) AS RowNumber FROM (SELECT * FROM wms_GoodsInfo WHERE 1=1 ";
        public static string GetAllGoodsInfoByRulePagerFooter = @")as T ) 
		                                                  SELECT *  FROM goods 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";

        public static string GetAllGoods = @"SELECT * FROM wms_GoodsInfo(NOLOCK)";

        public static string GetAllGoodsByRule = @"SELECT * FROM wms_GoodsInfo(NOLOCK) WHERE 1=1 ";

        public static string GetGoodsByKey = @"SELECT * FROM wms_GoodsInfo(NOLOCK) WHERE GoodsID=@GoodsID";

        public static string Remove = @"UPDATE wms_GoodsInfo SET Status=0 WHERE GoodsID=@GoodsID";


        public static string CreateNewGoods = @"INSERT INTO [wms_GoodsInfo]([TypeCode],[CustomerID],[GoodsNo],[GoodsName],[GoodsModel],[Weight],[Size],[Units],[Costing],[SalePrice],[Torr],[exDate],[exUnits],[AnotherNO],[AnotherName],[Remark],[OperatorID],[Status],BarCode)
                                                VALUES(@TypeCode,@CustomerID,@GoodsNo,@GoodsName,@GoodsModel,@Weight,@Size,@Units,@Costing,@SalePrice,@Torr,@exDate,@exUnits,@AnotherNO,@AnotherName,@Remark,@OperatorID,@Status,@BarCode)";

        public static string ModifyGoods = @"UPDATE [wms_GoodsInfo] SET [TypeCode] = @TypeCode,[CustomerID] = @CustomerID,[GoodsNo] = @GoodsNo,[GoodsName] = @GoodsName,[GoodsModel] = @GoodsModel,[Weight] = @Weight,[Size] = @Size,[Units] = @Units,
		                                    [Costing] = @Costing,[SalePrice] = @SalePrice,[Torr] = @Torr,[exDate] = @exDate,[exUnits] = @exUnits,[AnotherNO] = @AnotherNO,[AnotherName] = @AnotherName,[Remark] = @Remark,
		                                    [OperatorID] = @OperatorID,[ChangeDate] = @ChangeDate,Status=@Status,BarCode=@BarCode
                                            WHERE GoodsID=@GoodsID";
    }
}
