   

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
    location.href = "CustomServiceToExcel?carrierid=" + carrierID + "&storageid=" + storageID + "&customerid=" + customerID + "&orderno=" + orderNo + "&ordertype=" + orderType;
}