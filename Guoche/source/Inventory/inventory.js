var inventory = {
    init: function () {        
        inventory.regEvent();
        inventory.initInvent();
    },

    regEvent: function () {
        //根据仓库赛选 库位区域
        $("#storageid").on("change", function () {
            var $this = $(this);
            if (!!$this.val()) {
                $.ajax({
                    url: "GetStorageAreaNo",
                    type: 'POST',
                    async: false,
                    data: { sid: $this.val() },
                    success: function (data) {
                        if (!!data) {
                            $("#storageareano").html("").append("<option value=''>--请选择仓库区域--</option>");

                            for (var i = 0; i < data.length; i++) {
                                $("#storageareano").append("<option value='" + data[i].StorageAreaNo + "'>" + data[i].StorageAreaNo + "</option>");
                            }
                        }
                    }
                });
            }
            else {
                $("#storageareano").html("").append("<option value=''>--请选择仓库区域--</option>");
            }
        });

        //根据仓库赛选 库位区域
        $("#storageareano").on("change", function () {
            var $this = $(this);
            if (!!$this.val()) {
                $.ajax({
                    url: "GetStorageSubAreaNo",
                    type: 'POST',
                    async: false,
                    data: { areaNo: $this.val() },
                    success: function (data) {
                        if (!!data) {
                            $("#storagesubareano").html("").append("<option value=''>--请选择仓库子区域--</option>");

                            for (var i = 0; i < data.length; i++) {
                                $("#storagesubareano").append("<option value='" + data[i].StorageSubAreaNo + "'>" + data[i].StorageSubAreaNo + "</option>");
                            }
                        }
                    }
                });
            }
            else {
                $("#storagesubareano").html("").append("<option value=''>--请选择仓库子区域--</option>");
            }
        });
    },
    initInvent: function () {
        var sid = $("#hid_storageid").val(), pid = $("#hid_areano").val(), cid = $("#hid_subareano").val();
        if (sid != "" && sid != "0") {
            $("#storageid").val(sid).trigger("change");
            if (pid != "" && pid != "0") {
                $("#storageareano").val(pid);
            }
        }

        if (pid != "" && pid != "0") {
            $("#storageareano").val(pid).trigger("change");
            if (cid != "" && cid != "0") {
                $("#storagesubareano").val(cid);
            }
        }

    },
}

