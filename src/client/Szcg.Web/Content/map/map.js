var map;

var pathcontext = "/content/map/images";var imgpath = "/";

require(["esri/map",
    "tdlib/TDTLayer", "tdlib/TDTAnnoLayer", "tdlib/TDTLayerYx", "tdlib/TDTLayerYxAnno", "tdlib/TDTLayerLD",
    "esri/geometry/Point", "esri/geometry/Polyline",
    "esri/SpatialReference",
    "esri/toolbars/draw", "esri/toolbars/navigation",
    "esri/symbols/SimpleMarkerSymbol", "esri/symbols/SimpleLineSymbol", "esri/symbols/PictureFillSymbol", "esri/symbols/CartographicLineSymbol",
    "esri/graphic",
    "dijit/form/Button", "dijit/Toolbar", "esri/dijit/Scalebar",
    "dojo/_base/Color", "dojo/dom", "dojo/on",
    "dojo/domReady!"],
function (Map,
    TDTLayer, TDTAnnoLayer, TDTLayerYx, TDTLayerYxAnno, TDTLayerLD,
    Point, Polyline,
    SpatialReference,
    Draw, Navigation,
    SimpleMarkerSymbol, SimpleLineSymbol, PictureFillSymbol, CartographicLineSymbol,
    Graphic,
    Button, Toolbar, Scalebar,
    Color, dom, on
    ) {

    map = new Map("map", {
        logo: false,
        sliderStyle: "large",
    });
    map.on("load", initToolbar);

    var basemap = new TDTLayer();
    map.addLayer(basemap);
    var annolayer = new TDTAnnoLayer();
    map.addLayer(annolayer);
    //dojo.connect(map.graphics, "onMouseOver", showgrover)
    //dojo.connect(map.graphics, "onMouseOut", showgrout)
    dojo.connect(map.graphics, "onClick", showInfowindow)

    //map.centerAndZoom(new Point({ "x": 120.200018, "y": 30.209999, "spatialReference": { "wkid": 4326 } }), 14);

    fajax("/Tdtsys/handler");


    //on(dom.byId("btn-point"), "click", function (evt) {
    //    ShowLocation(120.4986047744751, 30.54224967956543);
    //});
    //on(dom.byId("btn-line"), "click", function (evt) {
    //    var points = [[[120.200018, 30.209999], [120.4986047744751, 30.54224967956543], [120.69658208117, 30.75842902028], [120.69031139171, 30.75591936765]]];
    //    polyline(points);
    //});
    //on(dom.byId("btn-mark"), "click", function (evt) {
    //    var point = { POINT_X: 120.200018, POINT_Y: 30.209999 };
    //    pointMark(point);
    //});


    //清除地图标注
    on(dom.byId("btn-clear"), "click", function (evt) {
        map.graphics.clear();
        map.infoWindow.hide();
    });

    //加载影像地图
    on(dom.byId("getVector"), "click", function (evt) {
        map.removeAllLayers()
        var basemap = new TDTLayer();
        var basemapAnno = new TDTAnnoLayer();
        map.addLayer(basemap);
        map.addLayer(basemapAnno);
    });

    //加载平面地图
    on(dom.byId("getImg"), "click", function (evt) {
        map.removeAllLayers()
        var basemapYx = new TDTLayerYx();
        var basemapYxAnno = new TDTLayerYxAnno();
        map.addLayer(basemapYx);
        map.addLayer(basemapYxAnno);
    });

    //地图全览
    on(dom.byId("btn-view"), "click", function (evt) {
        var navToolbar = new Navigation(map);
        navToolbar.zoomToFullExtent();
    })


    //坐标画点
    function ShowLocation(x, y) {
        var point = new Point(x, y, new SpatialReference({ wkid: 4326 }));
        var simpleMarkerSymbol = new SimpleMarkerSymbol();
        var graphic = new Graphic(point, simpleMarkerSymbol.setStyle(
        SimpleMarkerSymbol.STYLE_CIRCLE).setColor(
        new Color([0, 0, 0, 0.5])), null, null);

        map.graphics.add(graphic);

    };

    //坐标划线
    function polyline(points) {
        var myLine = {
            geometry: {
                "paths": points,
                "spatialReference": { "wkid": 4326 }
            },
            "symbol": { "color": [0, 0, 0, 255], "width": 1, "type": "esriSLS", "style": "esriSLSSolid" }
        };
        var gra = new Graphic(myLine);
        map.graphics.add(gra);
    }

    //图片标注
    function pointMark(point) {
        var fpaths = pathcontext + imgpath + "gssymbol4.png";
        var myPoint = new esri.geometry.Point(point.POINT_X, point.POINT_Y);
        var fsymbol = new esri.symbol.PictureMarkerSymbol({ "url": fpaths, "height": 12, "width": 12 });
        var fgraphic = new esri.Graphic(myPoint, fsymbol, null);
        map.graphics.add(fgraphic);
    }

    //初始化工具栏按钮
    function initToolbar() {

        var markerSymbol = new SimpleMarkerSymbol();
        markerSymbol.setPath("M16,4.938c-7.732,0-14,4.701-14,10.5c0,1.981,0.741,3.833,2.016,5.414L2,25.272l5.613-1.44c2.339,1.316,5.237,2.106,8.387,2.106c7.732,0,14-4.701,14-10.5S23.732,4.938,16,4.938zM16.868,21.375h-1.969v-1.889h1.969V21.375zM16.772,18.094h-1.777l-0.176-8.083h2.113L16.772,18.094z");
        markerSymbol.setColor(new Color("#00FFFF"));

        // lineSymbol used for freehand polyline, polyline and line. 
        var lineSymbol = new CartographicLineSymbol(
          CartographicLineSymbol.STYLE_SOLID,
          new Color([255, 0, 0]), 2,
          CartographicLineSymbol.CAP_ROUND,
          CartographicLineSymbol.JOIN_MITER, 5
        );

        // fill symbol used for extent, polygon and freehand polygon, use a picture fill symbol
        // the images folder contains additional fill images, other options: sand.png, swamp.png or stiple.png
        var fillSymbol = new PictureFillSymbol(
          "/content/map/images/mangrove.png",
          new SimpleLineSymbol(
            SimpleLineSymbol.STYLE_SOLID,
            new Color('#000'),
            1
          ),
          42,
          42
        );

        tb = new Draw(map);
        tb.on("draw-end", addGraphic);
        on(dom.byId("info"), "click", function (evt) {
            if (evt.target.id === "info") {
                return;
            }
            var tool = evt.target.id.toLowerCase();
            if (tool == "") {
                return;
            }
            map.disableMapNavigation();
            tb.activate(tool);
        });

        function addGraphic(evt) {
            //deactivate the toolbar and clear existing graphics 
            tb.deactivate();
            map.enableMapNavigation();

            // figure out which symbol to use
            var symbol;
            if (evt.geometry.type === "point" || evt.geometry.type === "multipoint") {
                symbol = markerSymbol;
            } else if (evt.geometry.type === "line" || evt.geometry.type === "polyline") {
                symbol = lineSymbol;
            }
            else {
                symbol = fillSymbol;
            }

            map.graphics.add(new Graphic(evt.geometry, symbol));
        }

    }

    //加载比例尺 
    function showscalebar() {
        var scalebar = new Scalebar({
            map: map,
            //地图对象
            attachTo: "bottom-left",
            //控件的位置，左下角 
            scalebarStyle: "ruler",
            //line 比例尺样式类型 
            scalebarUnit: "metric"
            //显示地图的单位，这里是km 
        });

    }


});










