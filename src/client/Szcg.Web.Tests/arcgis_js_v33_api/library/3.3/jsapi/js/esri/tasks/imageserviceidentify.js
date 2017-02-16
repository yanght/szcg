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
define("esri/tasks/imageserviceidentify",["dijit","dojo","dojox","dojo/require!esri/tasks/_task"],function(_1,_2,_3){_2.provide("esri.tasks.imageserviceidentify");_2.require("esri.tasks._task");_2.declare("esri.tasks.ImageServiceIdentifyTask",esri.tasks._Task,{constructor:function(_4){this._url.path+="/identify";this._handler=_2.hitch(this,this._handler);},__msigns:[{n:"execute",c:3,a:[{i:0,p:["geometry"]}],e:2}],_handler:function(_5,io,_6,_7,_8){try{var _9=new esri.tasks.ImageServiceIdentifyResult(_5);this._successHandler([_9],"onComplete",_6,_8);}catch(err){this._errorHandler(err,_7,_8);}},execute:function(_a,_b,_c,_d){var _e=_d.assembly,_f=this._encode(_2.mixin({},this._url.query,{f:"json"},_a.toJson(_e&&_e[0]))),_10=this._handler,_11=this._errorHandler;return esri.request({url:this._url.path,content:_f,callbackParamName:"callback",load:function(r,i){_10(r,i,_b,_c,_d.dfd);},error:function(r){_11(r,_c,_d.dfd);}});},onComplete:function(){}});esri._createWrappers("esri.tasks.ImageServiceIdentifyTask");_2.declare("esri.tasks.ImageServiceIdentifyParameters",null,{geometry:null,mosaicRule:null,pixelSizeX:null,pixelSizeY:null,toJson:function(_12){var g=_12&&_12["geometry"]||this.geometry,_13={geometry:g,mosaicRule:this.mosaicRule?_2.toJson(this.mosaicRule.toJson()):null};if(g){_13.geometryType=esri.geometry.getJsonType(g);}if(esri._isDefined(this.pixelSizeX)&&esri._isDefined(this.pixelSizeY)){_13.pixelSize=_2.toJson({x:parseFloat(this.pixelSizeX),y:parseFloat(this.pixelSizeY)});}return _13;}});_2.declare("esri.tasks.ImageServiceIdentifyResult",null,{constructor:function(_14){if(_14.catalogItems){this.catalogItems=new esri.tasks.FeatureSet(_14.catalogItems);}if(_14.location){this.location=esri.geometry.fromJson(_14.location);}this.catalogItemVisibilities=_14.catalogItemVisibilities;this.name=_14.name;this.objectId=_14.objectId;this.value=_14.value;this.properties=_14.properties;}});});