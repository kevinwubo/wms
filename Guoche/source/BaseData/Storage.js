var elemnets = {
    ids: ["StorageName", "StorageNo", "Address"],
    methods: [
        {
            required: true,
            messages: ["请输入仓库名称", "", "", ""]
        },
        {
            required: true,
            messages: ["请输入仓库编号", "", "", ""]
        },
        {
            required: true,
            messages: ["请输入仓库地址", "", "", ""]
        }
    ]
};

var storageInfo = {
    init: function () {
        valid.init(elemnets);
        storageInfo.regEvent();
        storageInfo.initCity();
    },

    regEvent: function () {      
        $("#goback").click(function () {
            window.location.href = "/Storage/";
        });
        $("#ProvinceID").on("change", function () {
            var $this = $(this);
            if (!!$this.val()) {
                $.ajax({
                    url: "GetCity",
                    type: 'POST',
                    async: false,
                    data: { pid: $this.val() },
                    success: function (data) {
                        if (!!data) {
                            $("#CityID").html("").append("<option value=''>--城市--</option>");

                            for (var i = 0; i < data.length; i++) {
                                $("#CityID").append("<option value='" + data[i].CityID + "'>" + data[i].CityName + "</option>");
                            }
                        }
                    }
                });
            }
            else {
                $("#CityID").html("").append("<option value=''>--城市--</option>");
            }
        });

        $("#save").click(function () {
            var formParams = {};
            var params = {
                listContact: []
            };
            $("#fineTable").find("tr").each(function () {
                var tdArr = $(this).children();
                var name = tdArr.eq(0).find('input').val();//姓名
                var mobile = tdArr.eq(1).find('input').val();//手机
                var tel = tdArr.eq(2).find('input').val();//座机
                var email = tdArr.eq(3).find('input').val();//Email
                var remark = tdArr.eq(4).find('input').val();//备注
                var id = tdArr.eq(5).find('input').val();//联系人ID
                if (name != "Head") {
                    formParams = {
                        "ContactName": name,
                        "Mobile": mobile,
                        "Telephone": tel,
                        "Email": email,
                        "Remark": remark,
                        "ContactID": id
                    };
                    params.listContact.push(formParams);
                }

            });
            $("#ContactJson").val(JSON.stringify(params));

            if (valid.validate()) {
                $("#StorageForm").submit();
            }
        });
    },
    initCity: function () {
        var pid = $("#hid_provinceid").val(), cid = $("#hid_cityid").val();
        if (pid != "" && pid != "0") {
            $("#ProvinceID").val(pid).trigger("change");
            if (cid != "" && cid != "0") {
                $("#CityID").val(cid);
            }
        }
        
    }
}

