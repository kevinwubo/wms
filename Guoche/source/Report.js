   

        //下载报表
function DownloadReport(type,guid) {
    location.href = "ReportExportExcel?type=" + type + "&guid=" + guid;
}


function ReportToExcel() {
    var carrierID = $("#carrierid").val();
    var storageID = $("#storageid").val();
    var customerID = $("#customerid").val();
    var orderNo = $("#orderno").val();
    var orderType = $("#ordertype").val();
    var beginDate = $("#begindate").val();
    var endDate = $("#enddate").val();
    location.href = "CustomServiceToExcel?carrierid=" + carrierID + "&storageid=" + storageID + "&customerid=" + customerID + "&orderno=" + orderNo + "&ordertype=" + orderType + "&begindate=" + beginDate + "&enddate=" + endDate;
}



function ReportOOOToExcel(type) {
    var carrierID = $("#carrierid").val();
    var storageID = $("#storageid").val();
    var customerID = $("#customerid").val();
    var orderNo = $("#orderno").val();
    var orderType = $("#ordertype").val();
    var beginDate = $("#begindate").val();
    var endDate = $("#enddate").val();
    location.href = "ReportToExcel?carrierid=" + carrierID + "&storageid=" + storageID + "&customerid=" + customerID + "&orderno=" + orderNo + "&ordertype=" + orderType + "&begindate=" + beginDate + "&enddate=" + endDate + "&type=" + type;
}


///运输计划报表导出
function OrderDeliverPlanReportToExcel()
{
    var carrierID = $("#carrierid").val();
    var beginDate = $("#begindate").val();
    var endDate = $("#enddate").val();
    location.href = "OrderDeliverPlanReportToExcel?carrierid=" + carrierID + "&begindate=" + beginDate + "&enddate=" + endDate;
}