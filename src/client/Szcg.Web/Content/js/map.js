var projection = ol.proj.get('EPSG:4326');
var projectionExtent = projection.getExtent();

var res = [1.40625, 0.703125, 0.3515625, 0.17578125, 0.087890625, 0.0439453125, 0.02197265625, 0.010986328125,
  0.0054931640625, 0.00274658203125, 0.001373291015625, 0.0006866455078125, 0.00034332275390625, 0.000171661376953125,
  0.0000858306884765625, 0.00004291534423828125, 0.000021457672119140625, 0.000010728836059570312
];

var map = new ol.Map({
    layers: [
      new ol.layer.Tile({
          source: new ol.source.WMTS({
              name: "中国矢量1-4级",
              url: 'http://t{0-6}.tianditu.com/vec_c/wmts',
              layer: 'vec',
              style: 'default',
              matrixSet: 'c',
              format: 'tiles',
              wrapX: true,
              tileGrid: new ol.tilegrid.WMTS({
                  origin: ol.extent.getTopLeft(projectionExtent),
                  resolutions: res.slice(0, 15),
                  matrixIds: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14]
              })
          }),
          minResolution: 0.00004291534423828125,
          maxResolution: 1.40625
      }),
      new ol.layer.Tile({
          source: new ol.source.WMTS({
              name: "中国矢量注记1-4级",
              url: 'http://t{0-6}.tianditu.com/cva_c/wmts',
              layer: 'cva',
              style: 'default',
              matrixSet: 'c',
              format: 'tiles',
              wrapX: true,
              tileGrid: new ol.tilegrid.WMTS({
                  origin: ol.extent.getTopLeft(projectionExtent),
                  resolutions: res.slice(0, 15),
                  matrixIds: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14]
              })
          }),
          minResolution: 0.00004291534423828125,
          maxResolution: 1.40625
      }),
      new ol.layer.Tile({
          source: new ol.source.WMTS({
              name: "浙江矢量15-17级",
              url: 'http://srv{0-6}.zjditu.cn/ZJEMAP_2D/wmts',
              layer: 'TDT_ZJEMAP',
              style: 'default',
              matrixSet: 'TileMatrixSet0',
              format: 'image/png',
              wrapX: true,
              tileGrid: new ol.tilegrid.WMTS({
                  origin: ol.extent.getTopLeft(projectionExtent),
                  resolutions: res.slice(16),
                  matrixIds: [15, 16, 17]
              })
          }),
          minResolution: 0.000005364418029785156,
          maxResolution: 0.0000858306884765625
      }),
      new ol.layer.Tile({
          source: new ol.source.WMTS({
              name: "浙江矢量注记15-17级",
              url: 'http://srv{0-6}.zjditu.cn/ZJEMAPANNO_2D/wmts',
              layer: 'ZJEMAPANNO',
              style: 'default',
              matrixSet: 'TileMatrixSet0',
              format: 'image/png',
              wrapX: true,
              tileGrid: new ol.tilegrid.WMTS({
                  origin: ol.extent.getTopLeft(projectionExtent),
                  resolutions: res.slice(16),
                  matrixIds: [15, 16, 17]
              })
          }),
          minResolution: 0.000005364418029785156,
          maxResolution: 0.0000858306884765625
      })
    ],
    target: 'map',
    view: new ol.View({
        center: [119.56619, 28.11398],
        projection: projection,
        zoom: 14,
        maxZoom: 17,
        minZoom: 1
    })
});