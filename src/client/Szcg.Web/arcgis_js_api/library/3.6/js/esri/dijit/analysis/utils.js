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
define("esri/dijit/analysis/utils",["dojo/_base/declare","dojo/_base/lang","dojo/_base/array","dojo/_base/connect","dojo/_base/event","dojo/_base/json","dojo/dom-attr","dojo/has","dojo/i18n","dojo/i18n!esri/nls/jsapi","dojo/json","dojo/query","dojo/dom-style","dijit/registry","esri/kernel","esri/lang","esri/dijit/analysis/HelpWindow"],function(_1,_2,_3,_4,_5,_6,_7,_8,_9,_a,_b,_c,_d,_e,_f,_10,_11){var _12={};_2.mixin(_12,{initHelpLinks:function(_13,_14){if(!esri.dijit._helpDialog){esri.dijit._helpDialog=new _11();}if(!_13){return;}var _15=_e.byNode(_13),_16=_15.get("helpFileName");_c("[esriHelpTopic]",_13).forEach(function(_17,_18,_19){if(_17){_d.set(_17,"display",!_10.isDefined(_14)||_14===true?"":"none");_4.connect(_17,"onclick",_2.hitch(this,function(e){_5.stop(e);esri.dijit._helpDialog.show(e,_7.get(_17,"esriHelpTopic"),_16);}));}},this);},constructAnalysisFeatColl:function(_1a){var obj={},_1b;obj.featureCollection=_1a.layerDefinition;for(_1b in obj.featureCollection){if((obj.featureCollection).hasOwnProperty(_1b)){if(_1b==="objectIdField"){obj.featureCollection.objectIdFieldName=_2.clone(obj.featureCollection.objectIdField);delete obj.featureCollection.objectIdField;}}}obj.featureCollection.features=_1a.featureSet.features;return obj;},constructAnalysisInputLyrObj:function(_1c){var obj={};if(_1c.url){obj={url:_1c.url};if(_1c.getDefinitionExpression&&_1c.getDefinitionExpression()){obj.filter=_1c.getDefinitionExpression();}else{if(_10.isDefined(_1c.definitionExpression)&&_1c.definitionExpression!==""){obj.filter=_1c.definitionExpression;}}if(_1c.credential){obj.serviceToken=_1c.credential.token;}}else{if(!_1c.url){obj=_1c.toJson();}}return obj;},buildReport:function(_1d,_1e){var _1f="",_20=/<\//g,_21,_22;if(!_1e){_1e={};_2.mixin(_1e,_a.analysisMsgCodes);}_3.forEach(_1d,function(_23,_24){var msg,_25,_26,_27,_28;if(typeof _23.message==="string"){msg=_10.isDefined(_1e[_23.messageCode])?_1e[_23.messageCode]:_23.message;_1f+=_23.style.substring(0,_23.style.indexOf("</"))+(_23.params?dojo.string.substitute(msg,_23.params):msg)+_23.style.substring(_23.style.indexOf("</"));}else{if(_2.isArray(_23.message)){_21=[];while((_22=_20.exec(_23.style))!==null){_21.push(_22.index);}_25="";_26="";_27=0;_28=0;_3.forEach(_23.message,function(_29,_2a){if(_2a===0){_25=_23.style;}msg=_10.isDefined(_1e[_23.messageCode+"_"+_2a])?_1e[_23.messageCode+"_"+_2a]:_29;_28=_21[_2a];_26=(_23.params?dojo.string.substitute(msg,_23.params):msg);_25=_25.substring(0,_28+_27)+_26+_25.substring(_28+_27);_27=_26.length;},this);_1f+=_25;}}});return _1f;}});if(_8("extend-esri")){_2.setObject("dijit.analysis.utils",_12,_f);}return _12;});