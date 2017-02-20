require(["esri/map", "tdlib/TDTLayer", "tdlib/TDTAnnoLayer", "esri/geometry/Point", "dijit/form/Button", "dojo/domReady!"], function (Map, TDTLayer, TDTAnnoLayer, Point, Button) {


    map = new Map("map", {
        basemap: "topo",
        center: [-122.45, 37.75], // long, lat
        zoom: 13,
        sliderStyle: "small"
    });


    //var basemap = new TDTLayer();
    //map.addLayer(basemap);
    //var annolayer = new TDTAnnoLayer();
    //map.addLayer(annolayer);

   // map.centerAndZoom(new Point({ "x": 120.200018, "y": 30.209999, "spatialReference": { "wkid": 4326 } }), 14);

})