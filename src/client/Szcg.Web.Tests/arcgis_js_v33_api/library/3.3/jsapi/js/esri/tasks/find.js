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
define("esri/tasks/find",["dijit","dojo","dojox","dojo/require!esri/tasks/_task"],function(_1,_2,_3){_2.provide("esri.tasks.find");_2.require("esri.tasks._task");_2.declare("esri.tasks.FindTask",esri.tasks._Task,{constructor:function(_4,_5){this._url.path+="/find";this._handler=_2.hitch(this,this._handler);this.gdbVersion=_5&&_5.gdbVersion;},_handler:function(_6,io,_7,_8,_9){try{var _a=[],_b=esri.tasks.FindResult;_2.forEach(_6.results,function(_c,i){_a[i]=new _b(_c);});this._successHandler([_a],"onComplete",_7,_9);}catch(err){this._errorHandler(err,_8,_9);}},execute:function(_d,_e,_f){var _10=this._encode(_2.mixin({},this._url.query,{f:"json"},_d.toJson())),_11=this._handler,_12=this._errorHandler;if(this.gdbVersion){_10.gdbVersion=this.gdbVersion;}var dfd=new _2.Deferred(esri._dfdCanceller);dfd._pendingDfd=esri.request({url:this._url.path,content:_10,callbackParamName:"callback",load:function(r,i){_11(r,i,_e,_f,dfd);},error:function(r){_12(r,_f,dfd);}});return dfd;},onComplete:function(){}});_2.declare("esri.tasks.FindParameters",null,{searchText:null,contains:true,searchFields:null,outSpatialReference:null,layerIds:null,returnGeometry:false,layerDefinitions:null,dynamicLayerInfos:null,toJson:function(){var _13={searchText:this.searchText,contains:this.contains,returnGeometry:this.returnGeometry,maxAllowableOffset:this.maxAllowableOffset},_14=this.layerIds,_15=this.searchFields,_16=this.outSpatialReference;if(_14){_13.layers=_14.join(",");}if(_15){_13.searchFields=_15.join(",");}if(_16){_13.sr=_16.wkid||_2.toJson(_16.toJson());}_13.layerDefs=esri._serializeLayerDefinitions(this.layerDefinitions);if(this.dynamicLayerInfos&&this.dynamicLayerInfos.length>0){var _17,_18=[];_2.forEach(this.dynamicLayerInfos,function(_19){if(!_19.subLayerIds){var _1a=_19.id;if(this.layerIds&&_2.indexOf(this.layerIds,_1a)!==-1){var _1b={id:_1a};_1b.source=_19.source&&_19.source.toJson();var _1c;if(this.layerDefinitions&&this.layerDefinitions[_1a]){_1c=this.layerDefinitions[_1a];}if(_1c){_1b.definitionExpression=_1c;}_18.push(_1b);}}},this);_17=_2.toJson(_18);if(_17==="[]"){_17="[{}]";}_13.dynamicLayers=_17;}return _13;}});_2.declare("esri.tasks.FindResult",null,{constructor:function(_1d){_2.mixin(this,_1d);this.feature=new esri.Graphic(_1d.geometry?esri.geometry.fromJson(_1d.geometry):null,null,_1d.attributes);delete this.geometry;delete this.attributes;}});});