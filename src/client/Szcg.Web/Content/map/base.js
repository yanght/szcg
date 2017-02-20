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

function getattr(result) {
    var attr = { "ID": result.ID, "folder": result.folder, "photo": result.photo, "zxcs": result.zxcs, "zds": result.zds, "adder": result.adder, "POINT_X": result.POINT_X, "POINT_Y": result.POINT_Y, "mappic": result.mappic, "mappich": result.mappich };
    return attr;
}

function getpoint(result) {
    for (var i = 0; i < result.length; i++) {
        showMark(result[i]);
    }
}

function showMark(result) {
    if (result != undefined) {
        var width = "18";
        var height = "18";
        var fpaths = pathcontext + imgpath + result.mappic;
        //var fpaths = pathcontext + "/Image/syn_hover.png"
        var myPoint = new esri.geometry.Point(result.POINT_X, result.POINT_Y);
        var attr = getattr(result);
        if (result.fclass != undefined) {
            if (result.fclass == "sub" || result.fclass == "xp") {
                height = "12";
                width = "12"
            }
        }
        var fsymbol = new esri.symbol.PictureMarkerSymbol({ "url": fpaths, "height": height, "width": width });
        var fgraphic = new esri.Graphic(myPoint, fsymbol, attr);
        map.graphics.add(fgraphic);
    }
}

function showInfowindow(evt) {
    var evtgr = evt.graphic;
    if (evtgr.attributes != undefined) {
        getinfowin(evt)
    }
    else {
        alert("没有信息！");
    }
}

function getinfowin(evt) {
    // var evtgr = evt.attributes;
    var evtgr = evt.ID;
    if (evtgr == undefined) {
        getinfowincc(evt.graphic.attributes)
    }
    else {
        getinfowincc(evt)
    }
}
function getinfowincc(result) {
    if (result == undefined) {
        return;
    }
    var ypics = new Array(); //存储已处理的图片数组
    var infoWindows = map.infoWindow;
    var photo = result.photo;
    var POINT_X = result.POINT_X;
    var POINT_Y = result.POINT_Y;
    var status;
    var cldate;

    var content = getcontent(result);
    infoWindows.setContent(content);
    infoWindows.setTitle("公共自行车");

    if (photo != undefined) {
        document.getElementById("clhimg").style.cursor = "default";
        var ephotos = photo;
        var ypics = ephotos.split(','); //存储已处理的图片
        var clhimg = document.getElementById("clhimg");
        clhimg.src = pathcontext + "/ztphoto/ggzxc/" + ypics[0];
    }
    var myPoint = new esri.geometry.Point(POINT_X, POINT_Y);
    map.infoWindow.show(map.toScreen(myPoint), map.getInfoWindowAnchor(map.toScreen(myPoint)));
}
function getcontent(result) {
    var content =
                "<div >" +
                    "<div style='font-size:13px;'>" +
                             "<table >" +
                                "<tr>" +
                                      "<td class='infotdname'>" + "站点编号：" + "</td>" +
                                      "<td class='infotddata'>" + result.zds + "</td>" +
                                "</tr>" +
                                  "<tr>" +
                                     "<td class='infotdname'>" + "自行车数量：" + "</td>" +
                                      "<td class='infotddata'>" + result.zxcs + "</td>" +
                                "</tr>" +
                                "<tr>" +
                                     "<td class='infotdname'>" + "所在位置：" + "</td>" +
                                     "<td class='infotddata'>" + result.adder + "</td>" +
                                "</tr>" +
                         "</table>" +
                  "</div>" +
                  "<div style=';width:211px;margin-top:3px;border:1px solid #8D8881;background-color:#ffffff '>" +
                             " <a href='#infoimage' rel='facebox'><img  id='clhimg'  style='margin:5px 5px 5px 5px;height:150px;width:200px'  alt=''/></a>" +
                  "</div>" +
                  "<div id='infoimage' style='display:none'>" +
                         "<image id='dispimg'></image>" +
                "</div>" +
            "</div>"
    return content;
}
function showgrover(evt) {
    var g = evt.graphic;
    var fsymbol;
    var width = "20";
    var height = "20";
    var fpaths = pathcontext + imgpath + g.attributes["mappich"];
    if (g.geometry.type == "polyline") {
        if (g.symbol.style == "solid") {
            fsymbol = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([232, 81, 9]), 5);
        }
        else if (g.symbol.style == "dash") {
            fsymbol = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_DASH, new dojo.Color([232, 81, 9]), 5);
        }
    }
    else if (g.geometry.type == "point") {
        if (g.attributes["fclass"] == "sub" || g.attributes["fclass"] == "xp") {
            height = "14";
            width = "14"
        }
        fsymbol = new esri.symbol.PictureMarkerSymbol({ "url": fpaths, "height": height, "width": width });
    }
    g.setSymbol(fsymbol)
}
function showgrout(evt) {
    var g = evt.graphic;
    var fsymbol;
    var width = "18";
    var height = "18";
    var fpaths = pathcontext + imgpath + g.attributes["mappic"];
    if (g.geometry.type == "polyline") {

        if (g.symbol.style == "solid") {
            fsymbol = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([186, 109, 243]), 5);
        }
        else if (g.symbol.style == "dash") {
            fsymbol = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_DASH, new dojo.Color([186, 109, 243]), 5);
        }
    }
    else if (g.geometry.type == "point") {
        if (g.attributes["fclass"] == "sub" || g.attributes["fclass"] == "xp") {
            height = "12";
            width = "12"
        }
        fsymbol = new esri.symbol.PictureMarkerSymbol({ "url": fpaths, "height": height, "width": width });
    }
    g.setSymbol(fsymbol)
}


$(function () {
    $("#btn-search").click(function () {
        $("#resultlist").show();
    })
    $("#hideresultlist").click(function () {
        $("#resultlist").hide();
    })
})