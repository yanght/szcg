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
define("esri/virtualearth/VEGeocoder",["dijit","dojo","dojox","dojo/require!esri/tasks/_task,esri/geometry,esri/utils"],function(_1,_2,_3){_2.provide("esri.virtualearth.VEGeocoder");_2.require("esri.tasks._task");_2.require("esri.geometry");_2.require("esri.utils");_2.declare("esri.virtualearth.VEGeocoder",esri.tasks._Task,{constructor:function(_4){try{_4=_2.mixin({bingMapsKey:null},_4||{});this.url="http://serverapi.arcgisonline.com/veadaptor/production/services/geocode/geocode";this._url=esri.urlToObject(this.url);this._queue=[];this.bingMapsKey=_4.bingMapsKey;this.culture=_4.culture||"en-US";this._errorHandler=_2.hitch(this,this._errorHandler);this._addressToLocationsHandler=_2.hitch(this,this._addressToLocationsHandler);if(!this.bingMapsKey){throw new Error(esri.bundle.virtualearth.vegeocode.bingMapsKeyNotSpecified);}}catch(e){this.onError(e);throw e;}},addressToLocations:function(_5,_6,_7){if(!this.bingMapsKey){console.debug(esri.bundle.virtualearth.vegeocode.requestQueued);this._queue.push(arguments);return;}var _8=_2.mixin({},this._url.query,{query:_5,token:this.bingMapsKey,culture:this.culture}),_9=this._addressToLocationsHandler,_a=this._errorHandler;var _b=new _2.Deferred(esri._dfdCanceller);_b._pendingDfd=esri.request({url:this._url.path,content:_8,callbackParamName:"callback",load:function(r,i){_9(r,i,_6,_7,_b);},error:function(r){_a(r,_7,_b);}});return _b;},_addressToLocationsHandler:function(_c,io,_d,_e,_f){try{_2.forEach(_c,function(_10,i){_c[i]=new esri.virtualearth.VEGeocodeResult(_10);});this._successHandler([_c],"onAddressToLocationsComplete",_d,_f);}catch(err){this._errorHandler(err,_e,_f);}},onAddressToLocationsComplete:function(){},setBingMapsKey:function(_11){this.bingMapsKey=_11;},setCulture:function(_12){this.culture=_12;}});_2.declare("esri.virtualearth.VEAddress",null,{constructor:function(_13){_2.mixin(this,{addressLine:null,adminDistrict:null,countryRegion:null,district:null,formattedAddress:null,locality:null,postalCode:null,postalTown:null},_13);}});_2.declare("esri.virtualearth.VEGeocodeResult",null,{constructor:function(_14){_2.mixin(this,{address:null,bestView:null,calculationMethod:null,confidence:null,displayName:null,entityType:null,location:null,matchCodes:null},_14);if(this.address){this.address=new esri.virtualearth.VEAddress(this.address);}if(this.bestView){this.bestView=new esri.geometry.Extent(this.bestView);}if(this.locationArray){this.calculationMethod=this.locationArray[0].calculationMethod;this.location=new esri.geometry.Point(this.locationArray[0]);}}});});