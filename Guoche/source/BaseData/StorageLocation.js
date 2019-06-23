var elemnets = {
    ids: ["StorageID", "StorageAreaNo", "StorageLocationNo"],
    methods: [
        {
            required: true,
            messages: ["请选择仓库", "", "", ""]
        },
        {
            required: true,
            messages: ["请选择库位区域", "", "", ""]
        },
        {
            required: true,
            messages: ["请输入库位编号", "", "", ""]
        }
    ]
};

var storageLocationInfo = {
    init: function () {
        valid.init(elemnets);
        storageLocationInfo.regEvent();
    },

    regEvent: function () {      
        $("#goback").click(function () {
            window.location.href = "/Carrier/";
        });

        $("#save").click(function () {
            if (valid.validate()) {
                $("#StorageLocationForm").submit();
            }
        });
    },    
}

