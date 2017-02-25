var utils = {};

/*
*    说明：异步调用
*    参数：url：        接口地址
*          requestType：   请求类型get、post
*          args：          请求参数，类型json格式{"key1":"value1","key2":"value2"}
*          callback:       回调函数
*/
utils.httpClient = function (url, requestType, args, callback) {
    args = args || {};
    $.ajax({
        url: url,
        data: args,
        async: true,
        type: requestType,
        success: function (r) {
            if (r != undefined && r != "") {
                var json = $.parseJSON(r);
                callback(json);
            }
        }
    });
}

//模态弹出框
utils.dialog = function (obj, title, width, height, okcallback, canclecallback) {
    //override dialog's title function to allow for HTML titles
    $.widget("ui.dialog", $.extend({}, $.ui.dialog.prototype, {
        _title: function (title) {
            var $title = this.options.title || '&nbsp;'
            if (("title_html" in this.options) && this.options.title_html == true)
                title.html($title);
            else title.text($title);
        }
    }));

    var url = $(obj).attr("data-url");

    if (url == undefined || url == "") return;

    $.get(url, function (data) {

        dialog = $("<div style='margin-bottom:20px;'>" + data + "</div>").appendTo("body").dialog({
            modal: true,
            width: width,
            height: height,
            title: "<div class='widget-header widget-header-small'><h4 class='smaller'> " + title + "</h4></div>",
            title_html: true,
            close: function () {
                $(this).dialog("destroy").remove();
            },
            buttons: [
                {
                    text: "确定",
                    "class": "btn btn-primary btn-xs",
                    click: function () {
                        if (okcallback) {
                            okcallback();
                        }
                        $(this).dialog("close");
                    }
                },
                 {
                     text: "取消",
                     "class": "btn btn-xs",
                     click: function () {
                         if (canclecallback) {
                             canclecallback();
                         }
                         $(this).dialog("close");
                     }
                 }
            ]
        });
    })
}

utils.dialog1 = function dialog1(obj, buttons) {
    var url = $(obj).attr("data-url");

    if (url == undefined || url == "") return;
    $.get(url, function (data) {
        bootbox.dialog({
            message: data,
            buttons:
            {
                "success":
                 {
                     "label": "<i class='ace-icon fa fa-check'></i> Success!",
                     "className": "btn-sm btn-success",
                     "callback": function () {
                         return false;
                     }
                 },
                "danger":
                {
                    "label": "Danger!",
                    "className": "btn-sm btn-danger",
                    "callback": function () {
                        //Example.show("uh oh, look out!");
                    }
                },
                "click":
                {
                    "label": "Click ME!",
                    "className": "btn-sm btn-primary",
                    "callback": function () {
                        //Example.show("Primary button");
                    }
                },
                "button":
                {
                    "label": "Just a button...",
                    "className": "btn-sm"
                }
            }
        });
    });
}

//确认提醒框
utils.alert = function (message, okcallback) {
    //override dialog's title function to allow for HTML titles
    $.widget("ui.dialog", $.extend({}, $.ui.dialog.prototype, {
        _title: function (title) {
            var $title = this.options.title || '&nbsp;'
            if (("title_html" in this.options) && this.options.title_html == true)
                title.html($title);
            else title.text($title);
        }
    }));

    var diaDiv = $("<div style=\"text-align:center\">" + message + "</div>");

    diaDiv.dialog({
        resizable: false,
        modal: true,
        title: "<div class='widget-header'><h4 class='smaller'><i class='ace-icon fa fa-exclamation-triangle red'></i>提醒</h4></div>",
        title_html: true,
        close: function () {
            $(this).dialog("destroy").remove();
        },
        buttons: [
            {
                html: "<i class='ace-icon fa fa-trash-o bigger-110'></i> 确定",
                "class": "btn btn-danger",
                click: function () {
                    if (okcallback) {
                        okcallback();
                    }
                    $(this).dialog("close");
                }
            }
        ]
    });
}

utils.alert1 = function (message, callback) {
    //bootbox.dialog({
    //    message: message,
    //    buttons:
    //    {
    //        "success":
    //         {
    //             "label": "<i class='ace-icon fa fa-check'></i> 确定",
    //             "className": "btn-sm btn-success",
    //             "callback": function () {
    //                 if (callback)
    //                     callback();
    //             }
    //         }
    //    }
    //});
    bootbox.alert(message, callback)
}

//选择对话框
utils.confirm = function (message, okcallback) {

    //override dialog's title function to allow for HTML titles
    $.widget("ui.dialog", $.extend({}, $.ui.dialog.prototype, {
        _title: function (title) {
            var $title = this.options.title || '&nbsp;'
            if (("title_html" in this.options) && this.options.title_html == true)
                title.html($title);
            else title.text($title);
        }
    }));


    var diaDiv = $("<div style=\"text-align:center\">" + message + "</div>");

    diaDiv.dialog({
        resizable: false,
        modal: true,
        title: "<div class='widget-header'><h4 class='smaller'><i class='ace-icon fa fa-exclamation-triangle red'></i> title</h4></div>",
        title_html: true,
        close: function () {
            $(this).dialog("destroy").remove();
        },
        buttons: [
            {
                html: "<i class='ace-icon fa fa-trash-o bigger-110'></i> 确定",
                "class": "btn btn-danger",
                click: function () {
                    if (okcallback) {
                        okcallback();
                    }
                    $(this).dialog("close");
                    return true;
                }
            }
            ,
            {
                html: "<i class='ace-icon fa fa-times bigger-110'></i>取消",
                "class": "btn btn-xs",
                click: function () {
                    $(this).dialog("close");
                    return false;
                }
            }
        ]
    });
}

//格式化日期
utils.getFormatDate = function (str, fomat) {
    var d = eval('new ' + str.substr(1, str.length - 2));
    var ar_date = [d.getFullYear(), d.getMonth() + 1, d.getDate(), d.getHours(), d.getMinutes(), d.getSeconds()];
    for (var i = 0; i < ar_date.length; i++) ar_date[i] = dFormat(ar_date[i]);
    return ar_date.slice(0, 3).join('-') + " " + ar_date.slice(3, 6).join(':');
    function dFormat(i) { return i < 10 ? "0" + i.toString() : i; }
}

utils.yearOfWeeks = function (year) {
    var weeks = new Array();
    var d = new Date(year, 0, 1);
    var to = new Date(year + 1, 0, 1);
    var i = 1;
    for (var from = d; from.getFullYear() < to.getFullYear() ;) {
        weeks.push(i);
        from.setDate(from.getDate() + 6);
        if (from < to)
            from.setDate(from.getDate() + 1);
        i++;
    }
    return weeks;
}

/**
 * 判断年份是否为润年
 *
 * @param {Number} year
 */
utils.isLeapYear = function isLeapYear(year) {
    return (year % 400 == 0) || (year % 4 == 0 && year % 100 != 0);
}

/**
 * 获取某一年份的某一月份的天数
 *
 * @param {Number} year
 * @param {Number} month
 */
utils.getMonthDays = function getMonthDays(year, month) {
    return [31, null, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31][month] || (utils.isLeapYear(year) ? 29 : 28);
}

/**
 * 获取某年的某天是第几周
 * @param {Number} y
 * @param {Number} m
 * @param {Number} d
 * @returns {Number}
 */
utils.getWeekNumber = function getWeekNumber(y, m, d) {
    var now = new Date(y, m - 1, d),
        year = now.getFullYear(),
        month = now.getMonth(),
        days = now.getDate();
    //那一天是那一年中的第多少天
    for (var i = 0; i < month; i++) {
        days += utils.getMonthDays(year, i);
    }

    //那一年第一天是星期几
    var yearFirstDay = new Date(year, 0, 1).getDay() || 7;

    var week = null;
    if (yearFirstDay == 1) {
        week = Math.ceil(days / yearFirstDay);
    } else {
        days -= (7 - yearFirstDay + 1);
        week = Math.ceil(days / 7) + 1;
    }
    return week;
}