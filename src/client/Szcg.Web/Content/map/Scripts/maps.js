var map, navToolbar;
var resizeTimer;
var basemap;
var fleng;
dojo.require("dijit.dijit");
dojo.require("esri.map");
dojo.require("esri.dijit.Scalebar");
dojo.require("esri.toolbars.navigation");
dojo.require("dijit.Toolbar");
dojo.require("dijit.layout.ContentPane");
dojo.require("dijit.layout.BorderContainer");
dojo.require("esri.dijit.InfoWindow");
dojo.require("esri.dijit.Popup");
dojoConfig = {
    parseOnLoad: true
}
function initMap() {
    var popupOptions = {
        'markerSymbol': new esri.symbol.SimpleMarkerSymbol('circle', 32, null, new dojo.Color([0, 0, 0, 0.25])),
        'marginLeft': '20',
        'marginTop': '20'
    };
    var popup = new esri.dijit.Popup(popupOptions, dojo.create("div"));
     map = new esri.Map("map", { logo: false, infoWindow: popup });
    dojo.addClass(map.infoWindow.domNode, "myTheme");
    dojo.connect(map, 'onLoad', function (map) {
        var scalebar = new esri.dijit.Scalebar({
            map: map,
            scalebarUnit: 'metric'
        });
        dojo.connect(dijit.byId('map'), 'resize', map, map.resize);
        navToolbar = new esri.toolbars.Navigation(map);
        dojo.connect(navToolbar, "onExtentHistoryChange", function () {
            navToolbar.deactivate();
        });
        map.infoWindow.resize(400, 370)
    });
    var basemap = new TDTLayer();
    var basemapAnno = new TDTLayerAnno();
    map.addLayer(basemap);
    map.addLayer(basemapAnno);
    mapAddlayer()
    dojo.connect(map.graphics, "onMouseOver", showgrover)
    dojo.connect(map.graphics, "onMouseOut", showgrout)
    dojo.connect(map.graphics, "onClick", showInfowindow)
    dojo.connect(map, "onZoomEnd", getLevel)

    initMarker();
}
var timer;
dojo.addOnLoad(initMap);
    function drawToMap(geometry) {
        map.graphics.clear();
        map.infoWindow.hide();
        var xmax = geometry.xmax;
        var xmin = geometry.xmin;
        var ymax = geometry.ymax;
        var ymin = geometry.ymin;
        var url = pathcontext + "/handler/cgrid.ashx?view=" + view + "&xmax=" + xman + "&xmin=" + xmin + "&ymax=" + yman + "&ymin=" + ymin;
        fajax(url);
        toolbar.deactivate();
    }
    function getLevel() {
        var ftext;
        var level = map.getLevel();
        switch (level) {
            case 0: ftext = "天地图"; break;
            case 1: ftext = "天地图"; break;
            case 2: ftext = "天地图.浙江"; break;
            case 3: ftext = "天地图.浙江"; break;
            case 4: ftext = "天地图.浙江"; break;
            case 5: ftext = "天地图.嘉兴"; break;
            case 6: ftext = "天地图.嘉兴"; break;
            case 7: ftext = "天地图.嘉兴"; break;
        }
       $("#ftext").text(ftext);
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
function showInfowindow2(evt) {
    alert(evt.mapPoint.x+"--"+evt.mapPoint.y);
}
function displaypoint() {
    var url = pathcontext + "/handler/cgrid.ashx?view=" + view
    fajax(url);
}
function clearpoint() {
    map.graphics.clear();
    map.infoWindow.hide();
}
function showgrover(evt) {
    var g = evt.graphic;
    var fsymbol;
    var width = "20";
    var height = "20";
    var fpaths = pathcontext + imgpath + g.attributes["mappich"];
    if(g.geometry.type=="polyline")
     {
         if (g.symbol.style == "solid") {
             fsymbol = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([232, 81, 9]), 5);
         }
         else if (g.symbol.style == "dash") {
             fsymbol = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_DASH, new dojo.Color([232, 81, 9]), 5);
         }
     }
     else if(g.geometry.type=="point"){
        if (g.attributes["fclass"] == "sub"||g.attributes["fclass"] == "xp") {
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
        if (g.attributes["fclass"] == "sub"||g.attributes["fclass"] == "xp") {
            height = "12";
            width = "12"
        }
        fsymbol = new esri.symbol.PictureMarkerSymbol({ "url": fpaths, "height": height, "width": width });
    }
    g.setSymbol(fsymbol)
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
            if (result.fclass == "sub"||result.fclass == "xp") {
                height = "12";
                width = "12"
            }
        }
        var fsymbol = new esri.symbol.PictureMarkerSymbol({ "url": fpaths, "height": height, "width": width });
        var fgraphic = new esri.Graphic(myPoint, fsymbol, attr);
        map.graphics.add(fgraphic);
    }
}

function initMarker()
{
    var MAX = 50000;
    var markers = [];
    var width = "18";
    var height = "18";
    var fpaths = "/tdtsys/Image/gssymbol4.png"

    for (var i = 0; i < MAX; i++) {
        var myPoint = new esri.geometry.Point(Math.random() * 40 + 85, Math.random() * 30 + 21);
        var fsymbol = new esri.symbol.PictureMarkerSymbol({ "url": fpaths, "height": height, "width": width });

        var result = {};

        result.ID = "111";
        result.folder = "";
        result.photo = "SAM_0382.jpg";
        result.zxcs = "35";
        result.zds = "2017";
        result.adder = "金都九月洋房";
        result.POINT_X =Math.random() * 40 + 85;
        result.POINT_Y = Math.random() * 30 + 21;
        result.mappic = "gssymbol4.png";
        result.mappich = "hgssymbol4.png";


        var attr = getattr(result);
        var fgraphic = new esri.Graphic(myPoint, fsymbol, attr);
        map.graphics.add(fgraphic);
    }
}

function showMark2(result) {
    if (result != undefined) {
        var width = "20";
        var height = "20";
        var fpaths = pathcontext + imgpath + result.mappich;
        var myPoint = new esri.geometry.Point(result.POINT_X, result.POINT_Y);
        var attr = getattr(result);
        if (result.fclass != undefined) {
            if (result.fclass == "sub"||result.fclass == "xp") {
                height = "14";
                width = "14"
            }
        }
        var fsymbol = new esri.symbol.PictureMarkerSymbol({ "url": fpaths, "height": height, "width": width });
        var fgraphic = new esri.Graphic(myPoint, fsymbol, attr);
        graphicarray.push(fgraphic);
        map.graphics.add(fgraphic);
        map.centerAt(myPoint);
    }
}

function delpoint() {
    for (x in graphicarray) {
        map.graphics.remove(graphicarray[x])
    }
}
function delline() {
    if (graphicline != undefined) {
        map.graphics.remove(graphicline)
    }
}
function delout() {
    var graphics = map.graphics.graphics;
    var len = graphics.length - 1;
    graphics.remove(graphics[len]);
}
function addline_onclick(result) {
    var cLine = new esri.geometry.Polyline();
    var myvalue = result.Coord;
    var iarry = new Array();

    var j = 0
    var k = 0
    var xxx = myvalue.split(",");
    if (xxx == undefined || xxx == "") {
        return;
    }
    var length = xxx.length / 2;

    for (var i = 0; i < xxx.length; i++) {

        if (length != 0) {

            if (i % 2 != 1) {
                iarry[j] = new Array();
                iarry[j][0] = xxx[i];
                iarry[j][1] = xxx[i + 1];
                length--
                j++
            }
        }
    }
    var status = result.status;
    cLine.addPath(iarry);
    addGeometry(cLine, status, result);
}
function addGeometry(Geo, status, result) {
    var symbol; //定义样式
    switch (Geo.type) {
        case "point":
            symbol = new esri.symbol.SimpleMarkerSymbol(esri.symbol.SimpleMarkerSymbol.STYLE_SQUARE, 10, new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([255, 0, 0]), 1), new dojo.Color([0, 255, 0, 0.25]));
            break;
        case "polyline":
            if (status == "规划") {
                symbol = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_DASH, new dojo.Color([186, 109, 243]), 5);
            }
            else {
                symbol = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([186, 109, 243]), 5)
            }
            break;
        case "polygon":
            symbol = new esri.symbol.SimpleFillSymbol(esri.symbol.SimpleFillSymbol.STYLE_NONE, new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_DASHDOT, new dojo.Color([255, 0, 0]), 2), new dojo.Color([255, 255, 0, 0.25]));
            break;
        case "extent":
            symbol = new esri.symbol.SimpleFillSymbol(esri.symbol.SimpleFillSymbol.STYLE_NONE, new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_DASHDOT, new dojo.Color([255, 0, 0]), 2), new dojo.Color([255, 255, 0, 0.25]));
            break;
        case "multipoint":
            symbol = new esri.symbol.SimpleMarkerSymbol(esri.symbol.SimpleMarkerSymbol.STYLE_DIAMOND, 20, new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([0, 0, 0]), 1), new dojo.Color([255, 255, 0, 0.5]));
            break;
    }
    var attr = getattrline(result);
    var graphic = new esri.Graphic(Geo, symbol, attr); //将geometry赋予样式并成为Graphics
    graphicline = graphic;
    map.graphics.add(graphic); //执行添加
}
function ZOOM_IN() {
    resize();
    navToolbar.activate(esri.toolbars.Navigation.ZOOM_IN);
}
function ZOOM_OUT() {
    resize();
    navToolbar.activate(esri.toolbars.Navigation.ZOOM_OUT);
}
function getImg() {
    $("#zoomin").attr('src', 'Image/maptools/map_zoomin1.png');
    $("#zoomout").attr('src', 'Image/maptools/map_zoomout1.png');
    $("#navpan").attr('src', 'Image/maptools/map_pan1.png');
    $("#zoomfull").attr('src', 'Image/maptools/map_full1.png');
    $("#Img2").attr('src', 'Image/maptools/map_full13.png');
    map.removeAllLayers()
    var basemapYx = new TDTLayerYx();
    var basemapYxAnno = new TDTLayerYxAnno();
    map.addLayer(basemapYx);
    map.addLayer(basemapYxAnno);
    getcsld();
}
function getVector() {
    var maptoolspath;
    switch (view) {
        case "ggzxc": maptoolspath = "ggzxc"; break;
        case "gsmp": maptoolspath = "gsmp"; break;
        case "hwss": maptoolspath = "hwss"; break;
        case "jxwd": maptoolspath = "jxwd"; break;
        case "tcc": maptoolspath = "tcc"; break;
        case "tnode&fclassid=nxq": maptoolspath = "csld"; break;
    }
    $("#zoomin").attr('src', 'Image/' + maptoolspath + '/maptools/map_zoomin1.png');
    $("#zoomout").attr('src', 'Image/' + maptoolspath + '/maptools/map_zoomout1.png');
    $("#navpan").attr('src', 'Image/' + maptoolspath + '/maptools/map_pan1.png');
    $("#zoomfull").attr('src', 'Image/' + maptoolspath + '/maptools/map_full1.png');
    $("#Img2").attr('src', 'Image/' + maptoolspath + '/maptools/map_full13.png');
    map.removeAllLayers()
    var basemap = new TDTLayer();
    var basemapAnno = new TDTLayerAnno();
    map.addLayer(basemap);
    map.addLayer(basemapAnno);
    getcsld();
}
function getcsld() {
    if (qview == "csld") {
        var basemapLD = new TDTLayerLD();
        map.addLayer(basemapLD);
    }
}



