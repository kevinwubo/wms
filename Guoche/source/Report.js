   

        //下载报表
function DownloadReport(type,guid) {
    location.href = "ReportExportExcel?type=" + type + "&guid=" + guid;
}