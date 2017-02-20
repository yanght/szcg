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
define("esri/Evented",["dojo/_base/declare","dojo/_base/lang","dojo/aspect","dojo/Evented","dojo/on","dojo/has","esri/kernel"],function(_1,_2,_3,_4,on,_5,_6){var _7=_1([_4],{declaredClass:"esri.Evented",registerConnectEvents:function(){var _8=this.constructor,_9=this.constructor._meta.parents,_a=[{}],_b={},_c,_d,_e,_f,k,_10=function(_11,_12){if(!_2.isArray(_11)){_11=[_11];}for(var i=0;i<_11.length;i++){var p=_11[i],_13;if(p._meta&&p._meta.parents){_10(p._meta.parents,_12);}if(p.prototype._eventMap){_12.push(_2.mixin({},p.prototype._eventMap));}}return _12;};if(!_8._onMap){_10(_9,_a);_a.push(this._eventMap);_c=_2.mixin.apply(this,_a);for(_d in this){if(/^on\w/.test(_d)&&_2.isFunction(this[_d])){_e=this._hyphenLower(_d).toLowerCase();if(!_c[_e]){_b[_e]={"method":_d};}}}for(k in _c){_f=this._onCamelCase(k);_b[k]={"method":_f,"argKeys":_c[k]};}_8._onMap=_b;return _8._onMap;}},on:function(_14,_15){var _16=this.constructor._onMap||this.registerConnectEvents(),_17=(typeof _14=="string")&&_14.toLowerCase(),_18=this._onCamelCase(_17),_19=_16&&_16[_17],_1a=(_19&&_19.method)||(this[_18]&&_2.isFunction(this[_18])&&_18),_1b;if(_1a){if(_19&&_2.isArray(_19.argKeys)){_1b=this._onArr2Obj(_15,_16[_17].argKeys);return _3.after(this,_1a,_1b,true);}else{return _3.after(this,_1a,_15,true);}}return this.inherited(arguments);},emit:function(_1c,_1d){var ret,_1e,_1f,_20,_21=_1c.toLowerCase(),_22=this._onCamelCase(_1c),_23=this.constructor._onMap||this.registerConnectEvents();_1f=(_23&&_23[_21]&&_23[_21].method)||(_2.isFunction(this[_22])&&_22);_20=_1f&&this[_1f];if(_1f&&_23&&_23[_21]){this._onObj2Arr(function(){_1e=Array.prototype.slice.call(arguments);},_23[_21].argKeys)(_1d);}_1d=_1d||{};if(!_1d.target){_1d.target=this;}if(_20){ret=_20.apply(this,_1e||[_1d]);}this.inherited(arguments);return ret;},_onObj2Arr:function(_24,_25){if(!_25){return _24;}else{var _26=this;return function(evt){var i,_27=[],_28=_25.length;for(i=0;i<_28;i++){_27[i]=evt[_25[i]];}_24.apply(_26,_27);};}},_onArr2Obj:function(_29,_2a){if(!_2a){return _29;}else{var _2b=this;return function(){var i,evt={},_2c=arguments.length;for(i=0;i<_2c;i++){evt[_2a[i]]=arguments[i];}if(!evt.target){evt.target=_2b;}_29(evt);};}},_hyphenLower:function(_2d){return _2d.replace(/^on/,"").replace(/[A-Z](?=[a-z])/g,function(m,off){return (off?"-":"")+m.toLowerCase();});},_onCamelCase:function(_2e){return "on"+_2e.substr(0,1).toUpperCase()+_2e.substr(1).replace(/\-([a-z])/g,function(m,s){return s.toUpperCase();});}});if(_5("extend-esri")){_6.Evented=_7;}return _7;});