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
define("esri/dijit/editing/toolbars/ToolbarBase",["dijit","dojo","dojox","dojo/require!dijit/Toolbar,dijit/ToolbarSeparator"],function(_1,_2,_3){_2.provide("esri.dijit.editing.toolbars.ToolbarBase");_2.require("dijit.Toolbar");_2.require("dijit.ToolbarSeparator");_2.declare("esri.dijit.editing.toolbars.ToolbarBase",[_1.Toolbar],{_enabled:true,graphicsAdded:function(){},drawEnd:function(){},onApplyEdits:function(){},onDelete:function(){},constructor:function(_4,_5){if(!_4||!_4.settings){return;}this._tools=[];this._tbConnects=[];this._initialize(_4.settings);},postCreate:function(){this._createTools();this.deactivate();},destroy:function(){var _6=this._tools;for(var _7 in _6){if(_6.hasOwnProperty(_7)&&esri._isDefined(this._tools[_7])){this._tools[_7].destroy();}}_2.forEach(this._tbConnects,"dojo.disconnect(item)");this.inherited(arguments);},activate:function(_8){this._enabled=true;},deactivate:function(){this._enabled=false;this._layer=null;this._geometryType=null;var _9=this._tools;for(var _a in _9){if(_9.hasOwnProperty(_a)){this._tools[_a].deactivate();this._tools[_a].setChecked(false);}}},isEnabled:function(){return _enabled;},setActiveSymbol:function(_b){this._activeSymbol=_b;},_getSymbol:function(){},_createTools:function(){},_initialize:function(_c){this._settings=_c;this._toolbar=_c.drawToolbar;this._editToolbar=_c.editToolbar;this._initializeToolbar();},_activateTool:function(_d,_e){if(this._activeTool){this._activeTool.deactivate();}if(_e===true&&this._activeTool==this._tools[_d]){this._activeTool.setChecked(false);this._activeTool=null;}else{this._activeTool=this._tools[_d];this._activeTool.setChecked(true);this._activeTool.activate(null);}},_createSeparator:function(){this.addChild(new _1.ToolbarSeparator());}});});