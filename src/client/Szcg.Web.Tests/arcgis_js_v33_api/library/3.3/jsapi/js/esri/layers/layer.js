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
define("esri/layers/layer",["dijit","dojo","dojox","dojo/require!esri/utils,esri/geometry"],function(_1,_2,_3){_2.provide("esri.layers.layer");_2.require("esri.utils");_2.require("esri.geometry");_2.declare("esri.layers.Layer",null,{constructor:function(_4,_5){if(_4&&_2.isString(_4)){this._url=esri.urlToObject(this.url=_4);}else{this.url=(this._url=null);_5=_5||_4;if(_5&&_5.layerDefinition){_5=null;}}this.spatialReference=new esri.SpatialReference(4326);this.initialExtent=new esri.geometry.Extent(-180,-90,180,90,new esri.SpatialReference(4326));this._map=this._div=null;this.normalization=true;if(_5){if(_5.id){this.id=_5.id;}if(_5.visible===false){this.visible=false;}if(esri.isDefined(_5.opacity)){this.opacity=_5.opacity;}if(esri.isDefined(_5.minScale)){this.setMinScale(_5.minScale);}if(esri.isDefined(_5.maxScale)){this.setMaxScale(_5.maxScale);}this.attributionDataUrl=_5.attributionDataUrl||"";this.hasAttributionData=!!this.attributionDataUrl;if(esri.isDefined(_5.showAttribution)){this.showAttribution=_5.showAttribution;}}this._errorHandler=_2.hitch(this,this._errorHandler);},id:null,visible:true,loaded:false,minScale:0,maxScale:0,visibleAtMapScale:false,suspended:true,attributionDataUrl:"",hasAttributionData:false,showAttribution:true,_errorHandler:function(_6){this.onError(_6);},_setMap:function(_7,_8,_9,_a){this._map=_7;this._lyrZEHandle=_2.connect(_7,"onZoomEnd",this,this._processMapScale);if(_7.loaded){this.visibleAtMapScale=this._isMapAtVisibleScale();}else{var _b=_2.connect(_7,"onLoad",this,function(){_2.disconnect(_b);_b=null;this._processMapScale();});}},_unsetMap:function(_c,_d){_2.disconnect(this._lyrZEHandle);this._lyrZEHandle=null;this._map=null;this.suspended=true;},_cleanUp:function(){this._map=this._div=null;},_fireUpdateStart:function(){if(this.updating){return;}this.updating=true;this.onUpdateStart();if(this._map){this._map._incr();}},_fireUpdateEnd:function(_e,_f){this.updating=false;this.onUpdateEnd(_e,_f);if(this._map){this._map._decr();}},_getToken:function(){var url=this._url,crd=this.credential;return (url&&url.query&&url.query.token)||(crd&&crd.token)||undefined;},_findCredential:function(){this.credential=esri.id&&this._url&&esri.id.findCredential(this._url.path);},_useSSL:function(){var _10=this._url,re=/^http:/i,rep="https:";if(this.url){this.url=this.url.replace(re,rep);}if(_10&&_10.path){_10.path=_10.path.replace(re,rep);}},refresh:function(){},show:function(){this.setVisibility(true);},hide:function(){this.setVisibility(false);},setMinScale:function(_11){this.setScaleRange(_11);},setMaxScale:function(_12){this.setScaleRange(null,_12);},setScaleRange:function(_13,_14){var _15=esri.isDefined(_13),_16=esri.isDefined(_14);if(!this.loaded){this._hasMin=this._hasMin||_15;this._hasMax=this._hasMax||_16;}var _17=this.minScale,_18=this.maxScale;this.minScale=(_15?_13:this.minScale)||0;this.maxScale=(_16?_14:this.maxScale)||0;if((_17!==this.minScale)||(_18!==this.maxScale)){this.onScaleRangeChange();this._processMapScale();}},suspend:function(){this._suspended=true;this.evaluateSuspension();},resume:function(){this._suspended=false;this.evaluateSuspension();},canResume:function(){return this.loaded&&this._map&&this._map.loaded&&this.visible&&this.visibleAtMapScale&&!this._suspended;},evaluateSuspension:function(){if(this.canResume()){if(this.suspended){this._resume();}}else{if(!this.suspended){this._suspend();}}},_suspend:function(){this.suspended=true;this.onSuspend();if(this._map){this._map.onLayerSuspend(this);}},_resume:function(){this.suspended=false;var _19=(this._resumedOnce===undefined);if(_19){this._resumedOnce=true;}this.onResume({firstOccurrence:_19});if(this._map){this._map.onLayerResume(this);}},_processMapScale:function(){var _1a=this.visibleAtMapScale;this.visibleAtMapScale=this._isMapAtVisibleScale();if(_1a!==this.visibleAtMapScale){this.onScaleVisibilityChange();this.evaluateSuspension();}},isVisibleAtScale:function(_1b){return (_1b?esri.layers.Layer.prototype._isMapAtVisibleScale.apply(this,arguments):false);},_isMapAtVisibleScale:function(_1c){if(!_1c&&(!this._map||!this._map.loaded)){return false;}_1c=_1c||this._map.getScale();var _1d=this.minScale,_1e=this.maxScale,_1f=!_1d,_20=!_1e;if(!_1f&&_1c<=_1d){_1f=true;}if(!_20&&_1c>=_1e){_20=true;}return (_1f&&_20)?true:false;},getAttributionData:function(){var url=this.attributionDataUrl,dfd=new _2.Deferred(esri._dfdCanceller);if(this.hasAttributionData&&url){dfd._pendingDfd=esri.request({url:url,content:{f:"json"},handleAs:"json",callbackParamName:"callback"});dfd._pendingDfd.then(function(_21){dfd.callback(_21);},function(_22){dfd.errback(_22);});}else{var err=new Error("Layer does not have attribution data");err.log=_2.config.isDebug;dfd.errback(err);}return dfd;},getResourceInfo:function(){var _23=this.resourceInfo;return _2.isString(_23)?_2.fromJson(_23):_2.clone(_23);},setNormalization:function(_24){this.normalization=_24;},setVisibility:function(v){if(this.visible!==v){this.visible=v;this.onVisibilityChange(this.visible);this.evaluateSuspension();}},onLoad:function(){},onVisibilityChange:function(){},onScaleRangeChange:function(){},onScaleVisibilityChange:function(){},onSuspend:function(){},onResume:function(){},onUpdate:function(){},onUpdateStart:function(){},onUpdateEnd:function(){},onError:function(){}});});