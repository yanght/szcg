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
require({cache:{"url:esri/dijit/geoenrichment/templates/VariablesPage.html":"<div data-dojo-type=\"dijit/layout/ContentPane\" data-dojo-props=\"row: 0\">\r\n    <table class=\"VariablesPage_DataCollection\">\r\n        <tr>\r\n            <td class=\"DataCollection_Icon\" data-dojo-attach-point=\"dataCollectionIcon\"></td>\r\n            <td class=\"DataCollection_Label\">\r\n                <span data-dojo-attach-point=\"dataCollectionName\"></span>\r\n            </td>\r\n        </tr>\r\n    </table>\r\n</div>\r\n<div data-dojo-type=\"dijit/layout/ContentPane\" data-dojo-props=\"row: 1\" style=\"padding-top: 0;\">\r\n    <div data-dojo-attach-point=\"varsDiv\"></div>\r\n</div>\r\n<div data-dojo-type=\"dijit/layout/ContentPane\" data-dojo-props=\"row: 2\">\r\n    <button class=\"Wizard_Button\" data-dojo-attach-event=\"click: _back\">${nls.back}</button>\r\n    <button class=\"Wizard_Button\" data-dojo-attach-event=\"click: _ok\">${nls.ok}</button>\r\n</div>"}});define("esri/dijit/geoenrichment/VariablesPage",["../../declare","./_WizardPage","dojo/i18n!../../nls/jsapi","dojo/text!./templates/VariablesPage.html","dojo/dom-construct","dijit/form/CheckBox","dojo/dom-class","dijit/layout/ContentPane"],function(_1,_2,_3,_4,_5,_6,_7){_3=_3.geoenrichment.dijit.VariablesPage;return _1("esri.dijit.geoenrichment.VariablesPage",[_2],{templateString:_4,nls:_3,dataCollection:null,checks:null,_checkBoxes:null,_eventMap:{"ok":true,"back":true},_setDataCollectionAttr:function(_8){if(this.dataCollection){_7.remove(this.dataCollectionIcon,"DataCollection_"+this.dataCollection.metadata["icon"]);}this._set("dataCollection",_8);this.dataCollectionName.innerHTML=_8.metadata["title"];_7.add(this.dataCollectionIcon,"DataCollection_"+_8.metadata["icon"]);this._checkBoxes=[];var _9=document.createDocumentFragment();for(var i=0;i<_8.variables.length;i++){var _a=_8.variables[i];var _b=new _6({checked:!this.checks||this.checks[_a.id]});this._checkBoxes.push(_b);var _c=_5.create("label",{"class":"VariablesPage_Variable"},_9);if(i%2==0){_7.add(_c,"Wizard_AltRow");}_b.placeAt(_c);_5.create("span",{innerHTML:_a.alias},_c);}this.varsDiv.innerHTML="";this.varsDiv.appendChild(_9);},_setChecksAttr:function(_d){this._set("checks",_d);for(var i=0;i<this._checkBoxes.length;i++){this._checkBoxes[i].set("checked",!_d||_d[this.dataCollection.variables[i].id]);}},_ok:function(){var _e=true;var _f={};for(var i=0;i<this._checkBoxes.length;i++){if(this._checkBoxes[i].get("checked")){_f[this.dataCollection.variables[i].id]=true;}else{_f[this.dataCollection.variables[i].id]=false;_e=false;}if(_e){this.checks=null;}else{this.checks=_f;}}this.onOK();},onOK:function(){},_back:function(){this.onBack();},onBack:function(){}});});