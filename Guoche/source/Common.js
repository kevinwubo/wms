Array.prototype.remove = function (val) {
    var index = this.indexOf(val);
    if (index > -1) {
        this.splice(index, 1);
    }
};
//扩展字符串处理函数
String.prototype.StringFormat = function () {
    if (arguments.length == 0) {
        return this;
    }
    for (var stringFormatS = this, stringFormatI = 0; stringFormatI < arguments.length; stringFormatI++) {
        stringFormatS = stringFormatS.replace(new RegExp("\\{" + stringFormatI + "\\}", "g"), arguments[stringFormatI]);
    }
    return stringFormatS;
};



$(document).ready(function () {
    var rule = "#sidebar a[href='{0}']".StringFormat(location.pathname);
    $(rule).parent().parent().show();

    $(".b_C1F8A9").click(function () {//处理左边栏菜单收缩放开
        event.stopPropagation();
        console.log(1);
        //循环所有关闭
        $(".b_C1F8A9").each(function () {
            $(this).addClass("collapsed").next(".collapse").css("height", "0px");
        });
        //当前菜单打开
        $(this).removeClass("collapsed").next(".collapse").css("height", "auto");
    })

    $(document).keyup(function (event) {
        if (event.keyCode == "27") {
            if ($("#closeDeal").length) {
                $("#closeDeal").trigger("click");
                return false;
            }
        }
    })

    $('#imgContainer').mousedown(
        function (event) {
            var isMove = true;
            $(this).css("cursor", "move");
            var abs_x = event.pageX - $('#imgContainer').offset().left;
            var abs_y = event.pageY - $('#imgContainer').offset().top;
            $(document).mousemove(function (event) {
                if (isMove) {
                    var obj = $('#imgContainer');
                    obj.css({ 'left': (event.pageX - abs_x - 50) + "px", 'top': (event.pageY - abs_y - 50) + "px" });
                }
            }
            ).mouseup(
                    function () {
                        $(this).css("cursor", "default");
                        isMove = false;
                    }
            );
        });

})

var WebHelper = {
    getQueryStringByName: function (name) {
        var result = location.search.match(new RegExp("[\?\&]" + name + "=([^\&]+)", "i"));

        if (result == null || result.length < 1) {

            return "";

        }

        return result[1];
    }

}

function makeDivCenter(divid,le) {
    var target = jQuery("#" + divid),
    win = jQuery(window),
    docheight = jQuery(document).scrollTop(),
    left=((win.width() - target.outerWidth())) / 2;
    target.css({
        "width":"800px",
        "left": (!!le? left+le:left)+ "px",
        "position": "absolute",
        "top": (win.height() - target.outerHeight()) / 2 + docheight + "px"
    })
}

var valid = {
    elemnets: {},
    init: function (el) {
        valid.elemnets = el;
        valid.registerEvent();
    },
    registerEvent: function () {
        if (!!valid.elemnets) {
            for (var j = 0; j < valid.elemnets.ids.length; j++) {
                var id = valid.elemnets.ids[j];
                var current = $("#" + id);
                if (current.is("input")) {
                    current.keyup(function () {
                        $(this).removeClass("error").next(".verror").remove();
                    }).blur(function () {
                        if ($(this).val().length > 0)
                        {
                            $(this).removeClass("error").next(".verror").remove();
                        }
                    });
                }
                if (current.is("select")) {
                    current.change(function () {
                        $(this).removeClass("error").next(".verror").remove();
                    });
                }
            }
        }
    },
    validate: function () {
        if (!!valid.elemnets) {
            var hasError = 0;
            for (var i = 0; i < valid.elemnets.ids.length; i++) {
                var obj = valid.elemnets.methods[i], id = valid.elemnets.ids[i];
                var current = $("#" + id), iserror = false;
                current.parent().find(".verror").remove();
                if (!!obj.required && !$.trim(current.val()))//必填
                {
                    current.addClass("error").after('<span class="help-inline verror" style="color:red">' + obj.messages[0] + '</span>');
                    iserror = true;
                }

                if (!iserror && !!obj.maxlength)//最大
                {
                    if (current.val().length > obj.maxlength) {
                        current.addClass("error").after('<span class="help-inline verror" style="color:red">' + obj.messages[1] + '</span>');
                        iserror = true;
                    }
                }

                if (!iserror && (obj.minlength || 0) > 0) {//最小
                    if (current.val().length < obj.minlength) {
                        current.addClass("error").after('<span class="help-inline verror" style="color:red">' + obj.messages[2] + '</span>');
                        iserror = true;
                    }
                }

                if (!iserror && !!obj.rule && $.trim(current.val()).length>0) {//正则表达式
                    if (!obj.rule.test(current.val())) {
                        current.addClass("error").after('<span class="help-inline verror" style="color:red">' + obj.messages[3] + '</span>');
                        iserror = true;
                    }
                }

                if (iserror) { hasError++; }
            }

            return hasError < 1;
        }

        return true;
    }
}