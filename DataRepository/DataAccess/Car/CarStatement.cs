/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：CarStatement
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：5/7/2018 3:09:41 PM
 * 修改人：
 * 修改时间：
 * 修改备注：
 * 代码请保留相关关键处的注释
 * ==============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.Car
{
    public class CarStatement
    {
        public static string GetAllCarPager = @" DECLARE @UP INT
        
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
		                                                  WITH car AS
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY Status DESC) AS RowNumber FROM (SELECT * FROM CarInfo WHERE 1=1 )as T ) 
		                                                  SELECT *  FROM car 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";
        public static string GetAllCarInfoByRulePagerHeader = @"DECLARE @UP INT
        
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
		                                                  WITH car AS
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY Status DESC) AS RowNumber FROM (SELECT * FROM CarInfo WHERE 1=1 ";
        public static string GetAllCarInfoByRulePagerFooter = @")as T ) 
		                                                  SELECT *  FROM car 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";

        public static string GetCarCount = @"SELECT COUNT(1) AS C FROM CarInfo(NOLOCK) WHERE 1=1 ";

        public static string GetAllCarInfo = @"SELECT * FROM [CarInfo](NOLOCK) ORDER BY Status DESC ";

        public static string GetAllCarInfoByID = @"SELECT * FROM [CarInfo](NOLOCK) WHERE CarID=@CarID";

        public static string GetCarInfoBySupplierID = @"SELECT * FROM [CarInfo](NOLOCK) WHERE SupplierID=@SupplierID";

        public static string CreateNewCarInfo = @"INSERT INTO [CarInfo]([CarName],[ModelCode],[CarModel],[AttachmentIDs],[ContractCode],[AppearanceSize],[PlateSize],[Capacity],[Slope],[MaxWeight],[Wheelbase],[BatteryCapacity],[Quality],[Braking],[MaxSpeed],[SiteNum],[BatteryType],[SafeConfigure],[OuterConfigure],[Renewal],[SupplierID],[Status],[CarLicNumber],[SalePrice],[LeasePrice],[CreateDate],[ModifyDate],[Operator],[BrandID]) 
                                                     VALUES (@CarName,@ModelCode,@CarModel,@AttachmentIDs,@ContractCode,@AppearanceSize,@PlateSize,@Capacity,@Slope,@MaxWeight,@Wheelbase,@BatteryCapacity,@Quality,@Braking,@MaxSpeed,@SiteNum,@BatteryType,@SafeConfigure,@OuterConfigure,@Renewal,@SupplierID,@Status,@CarLicNumber,@SalePrice,@LeasePrice,@CreateDate,@ModifyDate,@Operator,@BrandID)";

        public static string ModifyCarInfo = @"UPDATE [CarInfo] SET CarName=@CarName,ModelCode=@ModelCode,CarModel=@CarModel,AttachmentIDs=@AttachmentIDs,ContractCode=@ContractCode,AppearanceSize=@AppearanceSize,PlateSize=@PlateSize,Capacity=@Capacity,Slope=@Slope,
                                                  MaxWeight=@MaxWeight,Wheelbase=@Wheelbase,BatteryCapacity=@BatteryCapacity,Quality=@Quality,Braking=@Braking,MaxSpeed=@MaxSpeed,SiteNum=@SiteNum,BatteryType=@BatteryType,SafeConfigure=@SafeConfigure,OuterConfigure=@OuterConfigure,Renewal=@Renewal,SupplierID=@SupplierID,Status=@Status,CarLicNumber=@CarLicNumber,SalePrice=@SalePrice,LeasePrice=@LeasePrice,ModifyDate=@ModifyDate,Operator=@Operator,BrandID=@BrandID
                                                   WHERE CarID=@CarID";

        public static string RemoveCarInfo = @"UPDATE [CarInfo] SET Status=0 WHERE CarID=@CarID";

        public static string GetAllCarInfoByRule = @"SELECT * FROM [CarInfo](NOLOCK) WHERE 1=1 ";

        //随机显示两条
        public static string GetHotCarInfo = @"select top 2 * from CarInfo  order by NEWID()";

        public static string GetCarInfoByBrandID = @"SELECT * FROM [CarInfo](NOLOCK) WHERE BrandID=@BrandID";

        #region 关联表
        public static string MyReservation = @"select a.CustomerID, b.CustomerName,b.Mobile,c.CarName,c.Renewal,c.CarLicNumber,c.CarModel,case a.Status when 0 then '未处理' else '已处理' end as Status,a.CreateDate from dbo.Reservations a 
                                    left join dbo.Customer b on a.CustomerID=b.CustomerID
                                    left join dbo.CarInfo c on a.CarID=c.CarID
                                    where a.CustomerID=@CustomerID and a.RType=@RType";

        public static string MyCarInfoByCityID = @"select * from CarInfo where SupplierID in(select SupplierID from StoreInfo where CityID=@CityID)";
        #endregion


    }
}
