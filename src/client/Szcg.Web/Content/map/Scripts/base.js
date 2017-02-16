function fajax(url) {
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: url,
        dataType: 'json',
        cache: true,
        success: function (result) {
            if (result != null) {
                var zphoto = "";
                var zmemo = "";
                for (var i = 0; i < result.length; i++) {
                    showMark(result[i]);
                }
            }
        },
        error: function (e)
        { alert("出错了") }
    });
}
function getqurl(qview, fclassid, spara) {
    var url = pathcontext + "/handler/cgrid.ashx?view=" + qview + "&fclassid=" + fclassid + "&spara=" + spara
    return url;
}

function query(qview) {
    var value = $.trim($("#selectbox").text());
    var fclassid=getvalue(value);
    if (fclassid != "all" && $("#spara").val()=="")
    {
        alert("请输入查询内容");
        return;
    }
    var spara = escape($("#spara").val());
    var pageCount;
    furl = getqurl(qview, fclassid, spara);
    $.ajax({
        type: 'get', cache: true, dataType: 'json',
        url: furl,
        success: function (result) {
            if (result != null) {
                map.graphics.clear();
                map.infoWindow.hide();
                var pcount;
                pageCount = result.length;
                $("#serjg").text(pageCount);
                    var pager = new PagerView('pager');
                    pager.itemCount = pageCount;
                    pager.size = pageSize;
                    pager.maxButtons = 3;
                    pager.render();

                    pager.onclick = function (index) {
                        InitTable(index)
                    };

                $("#dealList").empty();
                if (pageSize < pageCount) {
                    pcount = pageSize;
                }
                else {
                    pcount = pageCount;
                }
                getdata(result, pcount);
                getpoint(result, fclassid, spara);
                return;
            } else {
                $("#serjg").text("没有查询结果！");
                $("#dealList").empty();
                $("#pager").empty();
            }
        },
        error: function (e) {
            alert("出错了");
        }
    });
    $("#maptoolbar2").css('right', '300px');
   $("#deallist-y2").css("display", "block");
   
}
function PageCallback(index, jq) {
    InitTable(index);
}
function InitTable(pageIndex) {
var index= parseInt(pageIndex)-1;
    var fpurl = furl + "&page=" + index + "&pagesize=" + pageSize;
    $.ajax({
        type: "GET",
        dataType: "json",
        cache: true,
        url: fpurl,           
        success: function (result) {
            if (result != null) {
                getdata(result, result.length);
            }
        }
    });
}
function resize() {
    var dh = document.documentElement.clientHeight;
    if (dh - 130 > 0)//防止出现负数引起错误
    {
        $("#map").css("height", dh - 110 + "px");
        $("#deallist - y").css("height", dh - 110 + "px");
        $("#viewports").css("height", dh - 110 + "px");
        $("# tracks").css("height", dh - 110 + "px");
        $("#scrollbars").css("height", dh - 110 + "px");
        var lheight = dh / 126
        pageSize = Math.floor(lheight) - 1;
    }
}

function fshow() {
   
    if ($("#deallist-y2").css("display") == 'none') {
        $("#maptoolbar2").css('right','300px')
        $("#deallist-y2").css("display", "block")
    }
    else {
        $("#maptoolbar2").css('right', '0px')
        $("#deallist-y2").css("display", "none")
    }
    // resize();
}
function select() {
    $('.son_ul').hide(); //初始ul隐藏
    $("#selectbox").click(function () {
        $(this).parent().find('ul.son_ul').slideDown();  //找到ul.son_ul显示
        $(this).parent().find('li').hover(function () { $(this).addClass('hover') }, function () { $(this).removeClass('hover') }); //li的hover效果
        //$(this).parent().hover(function () { }, function () { $(this).parent().find("ul.son_ul").slideUp(); });
    })
    $('ul.son_ul li').click(function () {
        $(this).parents('li').find('span').html($(this).html());
        var cpara = $.trim($(this).html());
        $(this).parents('li').find('ul').slideUp();
        if (view == "tnode&fclassid=nxq") {
            getautold(cpara);
        }
        else {
            getauto(cpara);
        }

    });
}
function getvalue(para) {
    switch (para) {
        case "小区名称": return "smname"; break;
        case "全部": return "all"; break;
        case "地址": return "adder"; break;
        case "所在地址": return "tccadd"; break;
        case "绿道周边": return "nodename"; break;
        case "绿道名称": return "smname"; break;
        case "自行车数": return "ggzxccs"; break;
        case "古树名称": return "smname"; break;
        case "树龄": return "gsyear"; break;
        case "公共厕所地址": return "ggcs"; break;
        case "垃圾中转站地址": return "zzz"; break;
        case "泊位数": return "pwnum"; break;
    }
}
function getauto(para) {
    var url;
    var viewauto;
    var fclassid;
    $("#spara").attr('value', '');
    if ($.trim(para) != "全部") {
        $("#spara").attr('disabled', false);
    }
    else {
        $("#spara").attr('disabled', true);
    }
    var newvalue = getvalue($.trim(para));
   url = pathcontext + "/handler/fgrid.ashx?view=" + view + "&autoclass=" + newvalue + "&fclassid=" + newvalue;
    var cache = {}, lastXhr;
    $("#spara").autocomplete({
        minLength: 1,
        matchContains: true,
        source: function (request, response) {

            var term = request.term;
            if (term in cache) {
                response(cache[term]);
                return;
            }
            lastXhr = $.getJSON(url, request, function (data, status, xhr) {
                cache[term] = data;
                if (xhr === lastXhr) {
                    response(data);
                }
            })
        }
    })

}