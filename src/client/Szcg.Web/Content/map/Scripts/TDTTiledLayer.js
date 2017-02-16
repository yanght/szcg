dojo.declare("TDTLayerbase", esri.layers.TiledMapServiceLayer, {
    constructor: function () {
        this.spatialReference = new esri.SpatialReference({ wkid: 4326 });
        this.initialExtent = (this.fullExtent = new esri.geometry.Extent(-180, -90, 180, 80, this.spatialReference));
        //this.initialExtent = (this.initialExtent = new esri.geometry.Extent(120.4986047744751, 30.54224967956543, 121.002491855621338, 30.992860794067383, this.spatialReference));//嘉兴
        this.initialExtent = (this.initialExtent = new esri.geometry.Extent(119.55075330734253, 28.112586736679077, 119.5711702823639, 28.116384744644165, this.spatialReference));//云和
        this.tileInfo = new esri.layers.TileInfo({
            "rows": 256,
            "cols": 256,
            "compressionQuality": 0,
            "origin": {
                "x": -180,
                "y": 90
            },
            "spatialReference": {
                "wkid": 4326
            },
            "lods": [
              { "level": 2, "resolution": 0.3515625, "scale": 147748796.52937502 },
              { "level": 3, "resolution": 0.17578125, "scale": 73874398.264687508 },
              { "level": 4, "resolution": 0.087890625, "scale": 36937199.132343754 },
              { "level": 5, "resolution": 0.0439453125, "scale": 18468599.566171877 },
              { "level": 6, "resolution": 0.02197265625, "scale": 9234299.7830859385 },
              { "level": 7, "resolution": 0.010986328125, "scale": 4617149.8915429693 },
              { "level": 8, "resolution": 0.0054931640625, "scale": 2308574.9457714846 },
              { "level": 9, "resolution": 0.00274658203125, "scale": 1154287.4728857423 },
              { "level": 10, "resolution": 0.001373291015625, "scale": 577143.73644287116 },
			  { "level": 11, "resolution": 6.866455078125e-004, "scale": 288571.87360433061404078620349 },
              { "level": 12, "resolution": 3.4332275390625e-004, "scale": 144285.93680216530702039310175 },
              { "level": 13, "resolution": 1.71661376953125e-004, "scale": 72142.968401082653510196550873 },
              { "level": 14, "resolution": 8.58306884765625e-005, "scale": 36071.484200541326755098275436 },
              { "level": 15, "resolution": 4.291534423828125e-005, "scale": 18035.742100270663377549137718 },
              { "level": 16, "resolution": 2.1457672119140625e-005, "scale": 9017.871050135331688774568859 },
              { "level": 17, "resolution": 1.07288360595703125e-005, "scale": 4508.9355250676658443872844296 },
              { "level": 18, "resolution": 5.36441802978515625e-006, "scale": 2254.4677625338329221936422148 },
              { "level": 19, "resolution": 2.682209014892578125e-06, "scale": 1127.2338812669164610968211074 },
			  { "level": 20, "resolution": 1.3411045074462890625e-06, "scale": 563.61694063345823054841055369 }
            ]
        });
        this.loaded = true;
        this.onLoad(this);
    }
});
dojo.declare("TDTLayer", TDTLayerbase, {
    getTileUrl: function (level, row, col) {
        console.log(level);
        var levelMap = "";
        if (level < 15) {

            return "http://t0.tianditu.com/vec_c/wmts?SERVICE=WMTS&REQUEST=GetTile&VERSION=1.0.0&LAYER=vec&STYLE=default&TILEMATRIXSET=c&TILEMATRIX=" + level + "&TILEROW=" + row + "&TILECOL=" + col + "&FORMAT=tiles";
        }
        else if (level < 18) {
            levelMap = "ZJEMAP";
            return "http://srv.zjditu.cn/ZJEMAP_2D/wmts?Service=WMTS&Request=GetTile&Version=1.0.0&Layer=" + levelMap + "&Style=default&Format=image/png&TileMatrixSet=TileMatrixSet0&TileMatrix=" + level + "&TileRow=" + row + "&TileCol=" + col;
        }
        else if (level < 21) {
            levelMap = "YHEMAP";
            // return "http://220.191.220.90/JXEMAP/service/wmts?SERVICE=WMTS&VERSION=1.0.0&REQUEST=GetTile&LAYER=" + levelMap + "&FORMAT=image/png&TILEMATRIXSET=TileMatrixSet0&TILEMATRIX=" + level + "&TILEROW=" + row + "&STYLE=default" + "&TILECOL=" + col;
            return "http://srv.tiandituls.cn/geoservices/YHEMAP_1/service/WMTS?Service=WMTS&Request=GetTile&Version=1.0.0&Layer=" + levelMap + "&Style=default&Format=image/png&TileMatrixSet=TileMatrixSet0&TileMatrix=" + level + "&TileRow=" + row + "&TileCol=" + col;

        }
    }
});
dojo.declare("TDTLayerAnno", TDTLayerbase, {
    getTileUrl: function (level, row, col) {
        if (level < 15) {
            levelMap = "tdt_vec_anno_dong_11_18";
            ////http://tile2.tianditu.com/DataServer?T=tdt_vec_anno_dong_11_18&X=6842&Y=1347&L=13
            // return "http://tile0.tianditu.com/DataServer?T=" + levelMap + "&" + "X=" + col + "&" + "Y=" + row + "&" + "L=" + level;
            return "http://t0.tianditu.com/cva_c/wmts?SERVICE=WMTS&REQUEST=GetTile&VERSION=1.0.0&LAYER=cva&STYLE=default&TILEMATRIXSET=c&TILEMATRIX=" + level + "&TILEROW=" + row + "&TILECOL=" + col + "&FORMAT=tiles";
        }
        if (level < 18) {
            levelMap = "ZJEMAPANNO";
            return "http://srv.zjditu.cn/ZJEMAPANNO_2D/wmts?Service=WMTS&Request=GetTile&Version=1.0.0&Layer=" + levelMap + "&Style=default&Format=image/png&TileMatrixSet=TileMatrixSet0&TileMatrix=" + level + "&TileRow=" + row + "&TileCol=" + col;
        }
        else if (level < 21) {
            // debugger
            levelMapBZ = "YHEMAPANNO";
            return "http://srv.tiandituls.cn/geoservices/YHEMAPANNO_1/service/WMTS?Service=WMTS&VERSION=1.0.0&REQUEST=GetTile&LAYER=" + levelMapBZ + "&FORMAT=image/png&TILEMATRIXSET=TileMatrixSet0&TILEMATRIX=" + level + "&TILEROW=" + row + "&STYLE=default" + "&TILECOL=" + col;
        }

    }
});
dojo.declare("TDTLayerLD", TDTLayerbase, {
    getTileUrl: function (level, row, col) {
        var levelMap = "";

        if (level < 21 && level > 10) {
            levelMapBZ = "CSLD";
            return "http://220.191.220.90/CSLD/service/wmts?SERVICE=WMTS&VERSION=1.0.0&REQUEST=GetTile&LAYER=" + levelMapBZ + "&FORMAT=image/png&TILEMATRIXSET=TileMatrixSet0&TILEMATRIX=" + level + "&TILEROW=" + row + "&STYLE=default" + "&TILECOL=" + col;
        }

    }
});
dojo.declare("TDTLayerYx", TDTLayerbase, {
    getTileUrl: function (level, row, col) {
        var levelMap = "";
        if (level < 15) {

            return "http://t0.tianditu.com/img_c/wmts?SERVICE=WMTS&REQUEST=GetTile&VERSION=1.0.0&LAYER=img&STYLE=default&TILEMATRIXSET=c&TILEMATRIX=" + level + "&TILEROW=" + row + "&TILECOL=" + col + "&FORMAT=tiles"
        }
        else if (level < 18) {
            levelMap = "ZJDOM2W1";
            return "http://srv.zjditu.cn/ZJDOM_2D/wmts?Service=WMTS&Request=GetTile&Version=1.0.0&Layer=" + levelMap + "&Style=Default&Format=image/png&TileMatrixSet=Matrix_0&TileMatrix=" + level + "&TileRow=" + row + "&TileCol=" + col;
        }
        else if (level < 21) {
            levelMap = "YHIMG";
            return "http://srv.tiandituls.cn/geoservices/YHIMG_1/service/WMTS?Service=WMTS&VERSION=1.0.0&REQUEST=GetTile&LAYER=" + levelMap + "&FORMAT=image/png&TILEMATRIXSET=TileMatrixSet0&TILEMATRIX=" + level + "&TILEROW=" + row + "&TILECOL=" + col + "&STYLE=default";
        }
    }
});
dojo.declare("TDTLayerYxAnno", TDTLayerbase, {
    getTileUrl: function (level, row, col) {
        if (level < 15) {
            return "http://t0.tianditu.com/cia_c/wmts?SERVICE=WMTS&REQUEST=GetTile&VERSION=1.0.0&LAYER=cia&STYLE=default&TILEMATRIXSET=c&TILEMATRIX=" + level + "&TILEROW=" + row + "&TILECOL=" + col + "&FORMAT=tiles"
        }
        else if (level < 18) {
            levelMap = "ZJIMGANNO";
            return "http://srv.zjditu.cn/ZJDOMANNO_2D/wmts?Service=WMTS&Request=GetTile&Version=1.0.0&Layer=" + levelMap + "&Style=default&Format=image/png&TileMatrixSet=TileMatrixSet0&TileMatrix=" + level + "&TileRow=" + row + "&TileCol=" + col;
        }
        else if (level < 21) {
            levelMap = "YHIMGANNO";
            return "http://srv.tiandituls.cn/geoservices/YHIMGANNO_1/service/WMTS?Service=WMTS&VERSION=1.0.0&REQUEST=GetTile&LAYER=" + levelMap + "&FORMAT=image/png&TILEMATRIXSET=TileMatrixSet0&TILEMATRIX=" + level + "&TILEROW=" + row + "&TILECOL=" + col + "&STYLE=default";
        }

    }
});