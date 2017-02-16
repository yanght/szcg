/*
 COPYRIGHT 2009 ESRI

 TRADE SECRETS: ESRI PROPRIETARY AND CONFIDENTIAL
 Unpublished material - all rights reserved under the
 Copyright Laws of the United States and applicable international
 laws, treaties, and conventions.

 For additional information, contact:
 Environmental Systems Research Institute, Inc.
 Attn: Contracts and Legal Services Department
 380 New York Street
 Redlands, California, 92373
 USA

 email: contracts@esri.com
 */
//>>built
define("esri/layers/agstiled",["dijit","dojo","dojox","dojo/require!esri/layers/tiled,esri/layers/agscommon"],function(_1,_2,_3){_2.provide("esri.layers.agstiled");_2.require("esri.layers.tiled");_2.require("esri.layers.agscommon");_2.declare("esri.layers.ArcGISTiledMapServiceLayer",[esri.layers.TiledMapServiceLayer,esri.layers.ArcGISMapServiceLayer],{constructor:function(_4,_5){if(_5){if(_5.roundrobin){_2.deprecated(this.declaredClass+" : "+esri.bundle.layers.agstiled.deprecateRoundrobin);_5.tileServers=_5.roundrobin;}this._setTileServers(_5.tileServers);this._loadCallback=_5.loadCallback;}this._params=_2.mixin({},this._url.query);this._initLayer=_2.hitch(this,this._initLayer);var _6=_5&&_5.resourceInfo;if(_6){this._initLayer(_6);}else{this._load=_2.hitch(this,this._load);this._load();}},_TILE_FORMATS:{PNG:"png",PNG8:"png",PNG24:"png",PNG32:"png",JPG:"jpg",JPEG:"jpg",GIF:"gif"},_setTileServers:function(_7){if(_7&&_7.length>0){this.tileServers=_7;var i,il=_7.length;for(i=0;i<il;i++){_7[i]=esri.urlToObject(_7[i]).path;}}},_initLayer:function(_8,io){this.inherited(arguments);this.resourceInfo=_2.toJson(_8);this.tileInfo=new esri.layers.TileInfo(_8.tileInfo);if(!this.spatialReference&&this.tileInfo.spatialReference){this.spatialReference=new esri.SpatialReference(this.tileInfo.spatialReference.toJson());}this.isPNG32=this.tileInfo.format==="PNG24"||this.tileInfo.format==="PNG32";if(_8.timeInfo){this.timeInfo=new esri.layers.TimeInfo(_8.timeInfo);}var _9=this._url.path;if(!this.tileServers){if(_8.tileServers){this._setTileServers(_8.tileServers);}else{var _a=(_9.search(/^https?\:\/\/server\.arcgisonline\.com/i)!==-1),_b=(_9.search(/^https?\:\/\/services\.arcgisonline\.com/i)!==-1);if(_a||_b){this._setTileServers([_9,_9.replace((_a?/server\.arcgisonline/i:/services\.arcgisonline/i),(_a?"services.arcgisonline":"server.arcgisonline"))]);}}}if(_9.search(/^https?\:\/\/(server|services)\.arcgisonline\.com\/arcgis\/rest\/services\/world_topo_map\/mapserver/i)!==-1){this.hasAttributionData=true;var _c=(window.location.protocol==="file:")?"http:":window.location.protocol;this.attributionDataUrl=this.attributionDataUrl||(_c+"//static.arcgis.com/attribution/World_Topo_Map");}this.loaded=true;this.onLoad(this);var _d=this._loadCallback;if(_d){delete this._loadCallback;_d(this);}},getTileUrl:function(_e,_f,col){var ts=this.tileServers,_10=this._url.query,_11=(ts?ts[_f%ts.length]:this._url.path)+"/tile/"+_e+"/"+_f+"/"+col;if(_10){_11+=("?"+_2.objectToQuery(_10));}var _12=this._getToken();if(_12&&(!_10||!_10.token)){_11+=(_11.indexOf("?")===-1?"?":"&")+"token="+_12;}return esri._getProxiedUrl(_11);}});});