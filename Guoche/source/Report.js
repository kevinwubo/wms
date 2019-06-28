
var reportInfo = {
    init: function () {
        reportInfo.regEvent();
    },

    regEvent: function () {      

        //下载报表
        function DownloadReport(type) {
            location.href = "ReportExportExcel?type=" + type;
        }
    },
    
}
