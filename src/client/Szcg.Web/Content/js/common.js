﻿var utils = {};

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
            var json = $.parseJSON(r);
            callback(json);
        }
    });
}

//模态弹出框
utils.dialog = function (obj, title, width, height) {

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

        $("#dialog").html(data);

        var dialog = $("#dialog").removeClass('hide').dialog({
            modal: true,
            width: width,
            height:height,
            title: "<div class='widget-header widget-header-small'><h4 class='smaller'> " + title + "</h4></div>",
            title_html: true,
            buttons: [
                {
                    text: "Cancel",
                    "class": "btn btn-xs",
                    click: function () {
                        $(this).dialog("close");
                    }
                },
                {
                    text: "OK",
                    "class": "btn btn-primary btn-xs",
                    click: function () {
                        $(this).dialog("close");
                    }
                }
            ]
        });
    })

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

    $("#dialog").html("<div style=\"text-align:center\">" + message + "</div>").removeClass('hide').dialog({
        resizable: false,
        modal: true,
        title: "<div class='widget-header'><h4 class='smaller'><i class='ace-icon fa fa-exclamation-triangle red'></i> title</h4></div>",
        title_html: true,
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

    $("#dialog").html("<div style=\"text-align:center\">" + message + "</div>").removeClass('hide').dialog({
        resizable: false,
        modal: true,
        title: "<div class='widget-header'><h4 class='smaller'><i class='ace-icon fa fa-exclamation-triangle red'></i> title</h4></div>",
        title_html: true,
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
            ,
            {
                html: "<i class='ace-icon fa fa-times bigger-110'></i>取消",
                "class": "btn btn-xs",
                click: function () {
                    $(this).dialog("close");
                }
            }
        ]
    });
}

utils.getFormatDate = function (str, fomat) {
    var d = eval('new ' + str.substr(1, str.length - 2));
    var ar_date = [d.getFullYear(), d.getMonth() + 1, d.getDate(), d.getHours(), d.getMinutes(), d.getSeconds()];
    for (var i = 0; i < ar_date.length; i++) ar_date[i] = dFormat(ar_date[i]);
    return ar_date.slice(0, 3).join('-') +" "+ ar_date.slice(3, 6).join(':');
    function dFormat(i) { return i < 10 ? "0" + i.toString() : i; }
}
