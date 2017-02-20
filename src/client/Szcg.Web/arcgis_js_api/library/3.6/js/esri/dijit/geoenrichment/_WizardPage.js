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
require({cache:{"url:esri/dijit/geoenrichment/templates/_WizardPage.html":"<div style=\"width: 100%; height: 100%;\">\r\n    <div data-dojo-type=\"esri/dijit/geoenrichment/Grid\" data-dojo-attach-point=\"_layoutGrid\" style=\"width: 100%; height: 100%;\">\r\n        ${content}\r\n    </div>\r\n</div>"}});define("esri/dijit/geoenrichment/_WizardPage",["../../declare","dojo/string","dojo/dom-class","dojox/mvc/Templated","dojo/text!./templates/_WizardPage.html","./Grid"],function(_1,_2,_3,_4,_5,_6){var _7={"busy":"Wizard_Loading","done":"Wizard_Done","error":"Wizard_Error"};return _1("esri.dijit.geoenrichment._WizardPage",[_4],{progressPromises:null,buildRendering:function(){this.templateString=_2.substitute(_5,{content:this.templateString});this.inherited(arguments);},load:function(){this.resize();},resize:function(){this._layoutGrid.resize();},_setStackingAttr:function(_8){this._layoutGrid.rows=[_6.AUTO,_6.AUTO,_6.AUTO];switch(_8){case "stretch":this._layoutGrid.rows[1]=_6.STRETCH;break;case "stack":this._layoutGrid.rows[1]=_6.STACK;break;}},showProgress:function(_9,_a){if(!this.progressPromises){this.progressPromises={};}var id;if(typeof _a=="string"||_a instanceof String){id=_a;}else{id=Math.random().toString();}if(this.progressPromises[id]){this.progressPromises[id].cancel();}var _b=_9.isResolved();var _c=this;if(!_b){this.progressPromises[id]=_9;_9.always(function(){delete _c.progressPromises[id];});this._setState("busy");}_9.then(function(){var _d=_a instanceof Function?_a:_c[_a];_d.apply(_c,arguments);if(!_b){_c._setState("done");}},function(_e){if(_e.name=="CancelError"){if(!_b){_c._setState("done");}}else{_c._setState("error",_e.toString());}});},cancelProgress:function(id){var _f=this.progressPromises&&this.progressPromises[id];if(_f){_f.cancel();}},_setState:function(_10,_11){if(!this.progressDiv){return;}this.progressDiv.innerHTML=_11||"";for(var s in _7){if(s==_10){_3.add(this.progressDiv,_7[s]);}else{_3.remove(this.progressDiv,_7[s]);}}},destroy:function(){if(this.progressPromises){for(var id in this.progressPromises){this.progressPromises[id].cancel();}this.progressPromises=null;}this.inherited(arguments);}});});