/*
	Copyright (c) 2004-2011, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/

//>>built
define("dojo/io/script",["../_base/connect","../_base/kernel","../_base/lang","../sniff","../_base/window","../_base/xhr","../dom","../dom-construct","../request/script"],function(_1,_2,_3,_4,_5,_6,_7,_8,_9){function _a(_b,id){var _c=_b["jsonp_"+id];if(_c){delete _b["jsonp_"+id];if(_c.canceled){_2.global[_9._callbacksProperty][_c.ioArgs.requestId]();}}};dojo.deprecated("dojo/io/script","Use dojo/request/script.","2.0");var _d={get:function(_e){var _f,_10=this;var dfd=this._makeScriptDeferred(_e,function(dfd){_f&&_f.cancel();});var _11=dfd.ioArgs;_6._ioAddQueryToUrl(_11);_6._ioNotifyStart(dfd);_f=_9.get(_11.url,{timeout:_e.timeout,jsonp:_11.jsonp,checkString:_e.checkString,ioArgs:_11,frameDoc:_e.frameDoc,canAttach:function(_12){_11.requestId=_12.id;_11.scriptId=_12.scriptId;_11.canDelete=_12.canDelete;return _d._canAttach(_11);}},true);_f.then(function(){_a(_10,_11.id);dfd.resolve(dfd);}).otherwise(function(_13){_a(_10,_11.id);dfd.ioArgs.error=_13;dfd.reject(_13);});return dfd;},attach:_9._attach,remove:_9._remove,_makeScriptDeferred:function(_14,_15){var dfd=_6._ioSetArgs(_14,_15||this._deferredCancel,this._deferredOk,this._deferredError);var _16=dfd.ioArgs;_16.id=_2._scopeName+"IoScript"+(_14.callbackSuffix||(this._counter++));_16.canDelete=false;_16.jsonp=_14.callbackParamName||_14.jsonp;if(_16.jsonp){_16.query=_16.query||"";if(_16.query.length>0){_16.query+="&";}_16.query+=_16.jsonp+"="+(_14.frameDoc?"parent.":"")+_2._scopeName+".io.script.jsonp_"+_16.id+"._jsonpCallback";_16.frameDoc=_14.frameDoc;_16.canDelete=true;dfd._jsonpCallback=this._jsonpCallback;this["jsonp_"+_16.id]=dfd;}return dfd;},_deferredCancel:function(dfd){dfd.canceled=true;},_deferredOk:function(dfd){var _17=dfd.ioArgs;return _17.json||_17.scriptLoaded||_17;},_deferredError:function(_18,dfd){console.log("dojo.io.script error",_18);return _18;},_deadScripts:[],_counter:1,_addDeadScript:function(_19){_d._deadScripts.push({id:_19.id,frameDoc:_19.frameDoc});_19.frameDoc=null;},_validCheck:function(dfd){var _1a=_d._deadScripts;if(_1a&&_1a.length>0){for(var i=0;i<_1a.length;i++){_d.remove(_1a[i].id,_1a[i].frameDoc);_1a[i].frameDoc=null;}_d._deadScripts=[];}return true;},_ioCheck:function(dfd){var _1b=dfd.ioArgs;if(_1b.json||(_1b.scriptLoaded&&!_1b.args.checkString)){return true;}var _1c=_1b.args.checkString;return _1c&&eval("typeof("+_1c+") != 'undefined'");},_resHandle:function(dfd){if(_d._ioCheck(dfd)){dfd.callback(dfd);}else{dfd.errback(new Error("inconceivable dojo.io.script._resHandle error"));}},_canAttach:function(){return true;},_jsonpCallback:function(_1d){this.ioArgs.json=_1d;_2.global[_9._callbacksProperty][this.ioArgs.requestId](_1d);}};_3.setObject("dojo.io.script",_d);return _d;});