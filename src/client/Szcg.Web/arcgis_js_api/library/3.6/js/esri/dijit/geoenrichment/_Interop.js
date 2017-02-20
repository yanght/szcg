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
define("esri/dijit/geoenrichment/_Interop",["../../declare","esri/Evented"],function(_1,_2){var _3=_1("esri.dijit.geoenrichment._Interop",[_2],{converters:{},_eventMap:{"change":["property","value"],"command":["name","arg"]},_onChange:function(_4){this.onChange(_4,this.getValue(_4));},onChange:function(_5,_6){},_onCommand:function(){this.onCommand.apply(this,arguments);},onCommand:function(){},getValue:function(_7){var _8=this.converters[_7];if(_8){return _8.serialize(this.get(_7));}else{return this.get(_7);}},setValue:function(_9,_a){var _b=this.converters[_9];if(_b){this.set(_9,_b.deserialize(_a));}else{this.set(_9,_a);}}});_3.arrayConverter={serialize:function(_c){return _c.join(",");},deserialize:function(_d){return _d.split(",");}};_3.JsonConverter=function(_e){return {serialize:function(_f){return _f.toJson();},deserialize:function(_10){return _e(_10);}};};_3.CtorConverter=function(_11){return {serialize:function(obj){return obj.toJson();},deserialize:function(_12){return new _11(_12);}};};_3.RestConverter=function(_13){return {serialize:function(obj){var _14=obj.toJson();var _15=[];for(var p in _14){_15.push(p+"="+encodeURIComponent(JSON.stringify(_14[p])));}return _15.join("&");},deserialize:function(_16){var _17=_16.split("&");var _18={};for(var i=0;i<_17.length;i++){var _19=_17[i].indexOf("=");_18[_17[i].substr(0,_19)]=JSON.parse(decodeURIComponent(_17[i].substr(_19+1)));}return new _13(_18);}};};_3.BoolConverter={serialize:function(b){return b.toString();},deserialize:function(str){return str=="true";}};return _3;});