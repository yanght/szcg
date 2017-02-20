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
require({cache:{"url:esri/dijit/geoenrichment/templates/InfographicsDataCollectionsPage.html":"<div data-dojo-type=\"dijit/layout/ContentPane\" data-dojo-props=\"row: 0\">\r\n    ${nls.chooseDataCollection}\r\n</div>\r\n<div data-dojo-type=\"dijit/layout/ContentPane\" data-dojo-props=\"row: 1\">\r\n    <div data-dojo-attach-point=\"itemsDiv\"></div>\r\n    <div data-dojo-attach-point=\"progressDiv\" class=\"Wizard_Progress\"></div>\r\n</div>\r\n<div data-dojo-type=\"dijit/layout/ContentPane\" data-dojo-props=\"row: 2\">\r\n    <button class=\"Wizard_Button\" data-dojo-attach-event=\"click: _back\">${nls.back}</button>\r\n    <button class=\"Wizard_Button\" data-dojo-attach-event=\"click: _cancel\">${nls.cancel}</button>\r\n</div>"}});define("esri/dijit/geoenrichment/InfographicsDataCollectionsPage",["../../declare","dojo/_base/lang","dojo/dom-class","dojo/dom-construct","./dom","dojo/on","./_WizardPage","dojo/i18n!../../nls/jsapi","dojo/text!./templates/InfographicsDataCollectionsPage.html","../../tasks/geoenrichment/GeoenrichmentTask","./config","dijit/layout/ContentPane","dijit/form/Select"],function(_1,_2,_3,_4,_5,on,_6,_7,_8,_9,_a){_7=_7.geoenrichment.dijit.DataCollectionsPage;return _1("esri.dijit.geoenrichment.InfographicsDataCollectionsPage",[_6],{templateString:_8,nls:_7,country:null,_request:null,_loaded:false,_task:null,_eventMap:{"pick":["dataCollection"],"back":true,"cancel":true},constructor:function(){this._task=new _9(_a.server);this._task.token=_a.token;},_setCountryAttr:function(_b){if(this.country==_b){return;}this._set("country",_b);this._loaded=false;},load:function(){this.inherited(arguments);if(this._loaded){return;}this.showProgress(this._task.getDataCollections(this.country),"_onDataCollectionsResponse");},_onDataCollectionsResponse:function(_c){_5.clear(this.itemsDiv);var _d=document.createDocumentFragment();for(var i=0;i<_c.length;i++){var _e=_c[i];var _f=_4.create("table",{"class":"DataCollection"},_d);var tr=_4.create("tr",null,_f);var td=_4.create("td",{"class":"DataCollection_Icon"},tr);var _10=_e.metadata["icon"];if(_10){_3.add(td,"DataCollection_"+_10);}_4.create("td",{"class":"DataCollection_Label",innerHTML:_e.metadata["title"]},tr);on(_f,"click",_2.hitch(this,this._onPick,_e));}this.itemsDiv.appendChild(_d);this._loaded=true;},_onPick:function(_11){this.onPick(_11);},onPick:function(_12){},_back:function(){this.cancelProgress("_onDataCollectionsResponse");this.onBack();},onBack:function(){},_cancel:function(){this.cancelProgress("_onDataCollectionsResponse");this.onCancel();},onCancel:function(){}});});