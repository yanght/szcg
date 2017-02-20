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
define("esri/tasks/geoenrichment/StandardGeographyQuery",["../../declare","./taskHelper","esri/tasks/FeatureSet","./GeographyQueryBase","./GeographyQuery","./SubGeographyQuery"],function(_1,_2,_3,_4,_5,_6){return _1("esri.tasks.geoenrichment.StandardGeographyQuery",null,{constructor:function(_7){this.url=_7||location.protocol+"//geoenrich.arcgis.com/arcgis/rest/services/World/GeoenrichmentServer";},execute:function(_8){return _2.invokeMethod(this,"/StandardGeographyQuery/execute",function createParams(){if(!(_8 instanceof _4)){if(_8.returnSubGeographyLayer){_8=new _6(_8);}else{_8=new _5(_8);}}return _2.jsonToRest(_8.toJson());},function readResponse(_9){if(!_9.results||_9.results.length<1||!_9.results[0].value){_2.throwEmptyResponse();}var _a={featureSet:new _3(_9.results[0].value),messages:_9.messages};return _a;},"onExecuteComplete","onError");},onExecuteComplete:function(_b){},onError:function(_c){}});});