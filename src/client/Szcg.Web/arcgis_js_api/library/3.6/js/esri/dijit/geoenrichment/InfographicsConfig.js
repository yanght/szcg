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
define("esri/dijit/geoenrichment/InfographicsConfig",["../../declare","dojo/_base/lang","dojo/aspect","dojo/string","./_Wizard","./_Interop","./InfographicsOptions","./InfographicsMainPage","./InfographicsDataCollectionsPage","./VariablesPage","./lang"],function(_1,_2,_3,_4,_5,_6,_7,_8,_9,_a,_b){function _c(_d,_e){if(_d.type!="OneVar"){return false;}if(_d.variables.length!=1){return false;}if(!_b.startsWith(_d.variables[0],_e.id)){return false;}return true;};var _f=_1("esri.dijit.geoenrichment.InfographicsConfig",[_5],{options:null,_eventMap:{"ok":true,"cancel":true},constructor:function(){this.pages["main"]=new _8({onAddVariables:_2.hitch(this,this._addVariables),onOK:_2.hitch(this,this._onOK),onCancel:_2.hitch(this,this._onCancel)});},startup:function(){this.inherited(arguments);if(!this.options){this.set("options",new _7());}this.loadPage("main");},_setOptionsAttr:function(_10){this._set("options",_10);this.pages["main"].set("options",_10);},_addVariables:function(){if(!this.pages["dataCollections"]){this.pages["dataCollections"]=new _9({onPick:_2.hitch(this,this._onDataCollectionPicked),onBack:_2.hitch(this,this.loadPage,"main"),onCancel:_2.hitch(this,this._onCancel)});}this.pages["dataCollections"].set("country",this.pages["main"].get("country"));this.loadPage("dataCollections");},_onDataCollectionPicked:function(_11){if(!this.pages["variables"]){this.pages["variables"]=new _a({onOK:_2.hitch(this,this._applyVariables),onBack:_2.hitch(this,this.loadPage,"dataCollections"),onCancel:_2.hitch(this,this._onCancel)});}this.pages["variables"].set("dataCollection",_11);var _12={};var _13=this.pages["main"].get("items");for(var i=0;i<_13.length;i++){var _14=_13[i];if(!_c(_14,_11)){continue;}var _15=_14.variables[0].split(".")[1];_12[_15]=true;}this.pages["variables"].set("checks",_12);this.loadPage("variables");},_applyVariables:function(){var _16={};var _17=this.pages["variables"].get("dataCollection");var _18=this.pages["variables"].get("checks");var _19=_17.variables;for(var i=0;i<_19.length;i++){_16[_19[i].id]=!_18||_18[_19[i].id];}var _1a=this.pages["main"].get("items");for(var i=_1a.length-1;i>=0;i--){var _1b=_1a[i];if(!_c(_1b,_17)){continue;}var _1c=_1b.variables[0].split(".")[1];if(_16[_1c]){_16[_1c]=false;}else{_1a.splice(i,1);}}for(var i=0;i<_19.length;i++){if(_16[_19[i].id]){var _1d=_19[i];var _1b=new _7.Item("OneVar",[_17.id+"."+_1d.id]);_1b.title=_1d.alias;var _1e=_17.metadata.datasets;if(_1e){_1b.datasetID=_4.trim(_1e.split(",")[0]);}_1a.push(_1b);}}this.pages["main"].set("options",this.options);this.loadPage("main");},_onOK:function(){this.onOK();},onOK:function(){},_onCancel:function(){this.onCancel();},onCancel:function(){}});_f.Interop=_1([_f,_6],{postMixInProperties:function(){_3.after(this,"onCancel",_2.hitch(this,this._onCancel));_3.after(this,"onOK",_2.hitch(this,this._onOK));this.inherited(arguments);},_onOK:function(){this._onChange("options");this._onCommand("OnOK");},_onCancel:function(){this._onCommand("OnCancel");},converters:{"options":new _6.CtorConverter(_7)}});return _f;});