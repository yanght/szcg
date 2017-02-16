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
define("esri/_time",["dijit","dojo","dojox"],function(_1,_2,_3){_2.provide("esri._time");_2.declare("esri.TimeExtent",null,{constructor:function(_4){if(arguments.length>1){this._create(arguments[0],arguments[1]);}else{if(_4){if(_2.isArray(_4)){var _5=_4[0],_6=_4[1];this.startTime=(_5===null||_5==="null")?null:new Date(_5);this.endTime=(_6===null||_6==="null")?null:new Date(_6);}else{if(_4 instanceof Date){this._create(_4,null);}}}}},offset:function(_7,_8){var _9=new esri.TimeExtent();var _a=this.startTime,_b=this.endTime;if(_a){_9.startTime=this._getOffsettedDate(_a,_7,_8);}if(_b){_9.endTime=this._getOffsettedDate(_b,_7,_8);}return _9;},intersection:function(_c){return this._intersection(this,_c);},toJson:function(){var _d=[];var _e=this.startTime;_d.push(_e?_e.getTime():"null");var _f=this.endTime;_d.push(_f?_f.getTime():"null");return _d;},_create:function(_10,end){this.startTime=_10?new Date(_10.getTime()):null;this.endTime=end?new Date(end.getTime()):null;},_refData:{"esriTimeUnitsMilliseconds":{getter:"getUTCMilliseconds",setter:"setUTCMilliseconds",multiplier:1},"esriTimeUnitsSeconds":{getter:"getUTCSeconds",setter:"setUTCSeconds",multiplier:1},"esriTimeUnitsMinutes":{getter:"getUTCMinutes",setter:"setUTCMinutes",multiplier:1},"esriTimeUnitsHours":{getter:"getUTCHours",setter:"setUTCHours",multiplier:1},"esriTimeUnitsDays":{getter:"getUTCDate",setter:"setUTCDate",multiplier:1},"esriTimeUnitsWeeks":{getter:"getUTCDate",setter:"setUTCDate",multiplier:7},"esriTimeUnitsMonths":{getter:"getUTCMonth",setter:"setUTCMonth",multiplier:1},"esriTimeUnitsYears":{getter:"getUTCFullYear",setter:"setUTCFullYear",multiplier:1},"esriTimeUnitsDecades":{getter:"getUTCFullYear",setter:"setUTCFullYear",multiplier:10},"esriTimeUnitsCenturies":{getter:"getUTCFullYear",setter:"setUTCFullYear",multiplier:100}},_intersection:function(_11,_12){if(_11&&_12){var _13=_11.startTime,_14=_11.endTime;var _15=_12.startTime,_16=_12.endTime;_13=_13?_13.getTime():-Infinity;_15=_15?_15.getTime():-Infinity;_14=_14?_14.getTime():Infinity;_16=_16?_16.getTime():Infinity;var _17,end;if(_15>=_13&&_15<=_14){_17=_15;}else{if(_13>=_15&&_13<=_16){_17=_13;}}if(_14>=_15&&_14<=_16){end=_14;}else{if(_16>=_13&&_16<=_14){end=_16;}}if(!isNaN(_17)&&!isNaN(end)){var _18=new esri.TimeExtent();_18.startTime=(_17===-Infinity)?null:new Date(_17);_18.endTime=(end===Infinity)?null:new Date(end);return _18;}else{return null;}}else{return null;}},_getOffsettedDate:function(_19,_1a,_1b){var _1c=this._refData;var _1d=new Date(_19.getTime());if(_1a&&_1b){_1c=_1c[_1b];_1d[_1c.setter](_1d[_1c.getter]()+(_1a*_1c.multiplier));}return _1d;}});_2.declare("esri.TimeReference",null,{constructor:function(_1e){if(_1e){_2.mixin(this,_1e);}}});});