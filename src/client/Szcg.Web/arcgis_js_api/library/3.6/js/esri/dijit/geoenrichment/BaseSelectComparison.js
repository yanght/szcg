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
define("esri/dijit/geoenrichment/BaseSelectComparison",["../../declare","./BaseWidget","dijit/form/Select","dojo/dom-class"],function(_1,_2,_3,_4){var _5=_1("esri.dijit.geoenrichment.BaseSelectComparison",[_2],{updateUI:function(){this.inherited(arguments);this._state.selectedComparison=Math.min(this._state.selectedComparison||0,this._data.getRowCount()-2);},updateUIExpanded:function(){this.inherited(arguments);if(this.select){var _6=[];var _7=[];var _8=Math.max(this._data.getRowCount()-1,0);for(var i=0;i<_8;i++){var _9=this._createComboBoxLabel(i+1);var _a={label:_9,value:i.toString()};if(i>=this.select.options.length){_6.push(_a);}else{_7.push(_a);}}if(_6.length>0){this.select.addOption(_6);}if(_7.length>0){this.select.updateOption(_7);}while(this.select.options.length>_8){this.select.removeOption(this.select.options.length-1);}this.select.set("value",this._state.selectedComparison.toString());}},_createComboBoxLabel:function(_b){return this._data.getValue(_b,"NAME")||"";},_createComboBox:function(_c){var _d=this;_4.add(_c,"BaseSelectComparison_Select");this.select=new _3({maxHeight:151,onChange:function(){var _e=_d._state.selectedComparison;_d._state.selectedComparison=+_d.select.get("value");if(_d._state.selectedComparison!=_e){_d.updateUIExpanded();}}});this.select.placeAt(_c);},_getComparisonRow:function(){var _f=this._state.selectedComparison;if(_f>=0){return _f+1;}else{return undefined;}},destroy:function(){if(this.select){this.select.destroy();this.select=null;}this.inherited(arguments);}});return _5;});