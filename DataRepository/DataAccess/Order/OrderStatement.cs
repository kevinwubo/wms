﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.Order
{
    public class OrderStatement
    {

        public static string GetCount = @"SELECT COUNT(1) AS C FROM wms_OrderInfo(NOLOCK) WHERE 1=1 ";

        public static string GetAllOrderInfoPager = @" DECLARE @UP INT
        
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
		                                                  WITH ordera AS
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY CreateDate DESC) AS RowNumber FROM (SELECT * FROM wms_OrderInfo WHERE 1=1 )as T ) 
		                                                  SELECT *  FROM ordera 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";
        public static string GetAllOrderInfoByRulePagerHeader = @"DECLARE @UP INT
        
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
		                                                  WITH ordera AS
		                                                  (SELECT *,ROW_NUMBER() OVER (ORDER BY CreateDate DESC) AS RowNumber FROM (SELECT * FROM wms_OrderInfo WHERE 1=1 ";
        public static string GetAllOrderInfoByRulePagerFooter = @")as T ) 
		                                                  SELECT *  FROM ordera 
		                                                  WHERE RowNumber BETWEEN @UP+1 AND @UP+@PageSize
	                                                  END";

        public static string GetAllOrder = @"SELECT * FROM wms_OrderInfo(NOLOCK)";

        public static string GetAllOrderByRule = @"SELECT * FROM wms_OrderInfo(NOLOCK) WHERE 1=1 ";

        public static string GetOrderByOrderID = @"SELECT * FROM wms_OrderInfo(NOLOCK) WHERE OrderID=@OrderID";        

        public static string Remove = @"UPDATE wms_OrderInfo SET Status=0 WHERE OrderID=@OrderID";

        public static string Delete = @"DELETE FROM wms_OrderInfo WHERE OrderID=@OrderID  DELETE FROM wms_OrderDetailInfo WHERE OrderID=@OrderID ";

        public static string GetOrderByKeys = @"SELECT * FROM wms_OrderInfo(NOLOCK) WHERE OrderID IN (#ids#)";

        public static string CreateNewOrder = @"INSERT INTO dbo.wms_OrderInfo(OrderNo,MergeNo,OrderType,ReceiverID,CustomerID,SendStorageID,ReceiverStorageID,CarrierID
                                                ,OrderDate,SendDate,configPrice,configHandInAmt,configSortPrice,configCosting,configHandOutAmt,configSortCosting,TempType,OrderStatus
                                                ,UploadStatus,Status,Remark,OperatorID,OrderSource,SalesMan,PromotionMan,LineID,CreateDate,ChangeDate)
			                                                VALUES(@OrderNo,@MergeNo,@OrderType,@ReceiverID,@CustomerID,@SendStorageID,@ReceiverStorageID,@CarrierID,
                                                @OrderDate,@SendDate,@configPrice,@configHandInAmt,@configSortPrice,@configCosting,@configHandOutAmt,@configSortCosting,@TempType,@OrderStatus
                                                ,@UploadStatus,@Status,@Remark,@OperatorID,@OrderSource,@SalesMan,@PromotionMan,@LineID,@CreateDate,@ChangeDate) select @@IDENTITY";

        public static string ModifyOrder = @"UPDATE wms_OrderInfo SET OrderNo = @OrderNo,MergeNo = @MergeNo,OrderType = @OrderType,ReceiverID = @ReceiverID,CustomerID = @CustomerID
                                                ,SendStorageID = @SendStorageID,ReceiverStorageID = @ReceiverStorageID,CarrierID = @CarrierID,OrderDate = @OrderDate,SendDate = @SendDate
                                                ,configPrice = @configPrice,configHandInAmt = @configHandInAmt,configSortPrice = @configSortPrice,configCosting = @configCosting,configHandOutAmt = @configHandOutAmt
                                                ,configSortCosting = @configSortCosting,TempType = @TempType,OrderStatus = @OrderStatus,UploadStatus = @UploadStatus,Status = @Status,Remark = @Remark
                                                ,OperatorID = @OperatorID,ChangeDate = @ChangeDate WHERE OrderID=@OrderID";

        /// <summary>
        /// 更新回单附件
        /// </summary>

        public static string UpdateOrderAttachmentIDs = @"UPDATE wms_OrderInfo SET AttachmentIDs=@AttachmentIDs WHERE OrderID=@OrderID";
        /// <summary>
        /// 更新送达时间
        /// </summary>
        public static string UpdateOrderArriverDate = @"UPDATE wms_OrderInfo SET ArriverDate = @ArriverDate,ChangeDate = GetDate() WHERE OrderID=@OrderID";

        /// <summary>
        /// 更新订单状态
        /// </summary>

        public static string UpdateOrderStatus = @"UPDATE wms_OrderInfo SET OrderStatus = @OrderStatus,ChangeDate = GetDate() WHERE OrderID=@OrderID";
        /// <summary>
        /// 更新接单状态
        /// </summary>

        public static string UpdateUploadStatus = @"UPDATE wms_OrderInfo SET UploadStatus = @UploadStatus,ChangeDate = GetDate() WHERE OrderID=@OrderID";

    }
}
