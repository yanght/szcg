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
define("esri/dijit/editing/tools/DropDownToolBase",["dijit","dojo","dojox","dojo/require!dijit/form/ComboButton,esri/dijit/editing/tools/ToolBase"],function(_1,_2,_3){_2.provide("esri.dijit.editing.tools.DropDownToolBase");_2.require("dijit.form.ComboButton");_2.require("esri.dijit.editing.tools.ToolBase");_2.declare("esri.dijit.editing.tools.DropDownToolBase",[_1.form.ComboButton,esri.dijit.editing.tools.ToolBase],{_enabled:false,_checked:false,postCreate:function(){this._tools=[];this._createTools();this.inherited(arguments);if(this._setShowLabelAttr){this._setShowLabelAttr(false);}},destroy:function(){var _4=this._tools;for(var _5 in _4){if(_4.hasOwnProperty(_5)&&esri._isDefined(_4[_5])){_4[_5].destroy();}}this.inherited(arguments);},_createTools:function(){var _6=new _1.Menu();this.dropDown=_6;for(var i in this._tools){if(this._tools.hasOwnProperty(i)){_6.addChild(this._tools[i]);}}this._activeTool=_6.getChildren()[0];this._updateUI();},activate:function(_7){this.inherited(arguments);if(!this._activeTool){this._activateDefaultTool();}else{this._activeTool.activate();}},deactivate:function(){this.inherited(arguments);if(this._activeTool){this._activeTool.deactivate();}},enable:function(_8){for(var _9 in this._tools){if(this._tools.hasOwnProperty(_9)){this._tools[_9].enable(_8);}}this.setEnabled(true);this.inherited(arguments);},setChecked:function(_a){this._checked=_a;this._updateUI();},_onDrawEnd:function(_b){},onLayerChange:function(_c,_d,_e){this._activeTool=null;this._activeType=_d;this._activeTemplate=_e;this._activeLayer=_c;},onItemClicked:function(_f){if(this._activeTool){this._activeTool.deactivate();}this._activeTool=_1.byId(_f.currentTarget.id);if(this._checked===false){this._onClick();}else{this._updateUI();if(this._activeTool){this._activeTool.activate();this._activeTool.setChecked(true);}}},_onClick:function(evt){if(this._enabled===false){return;}this._checked=!this._checked;this.inherited(arguments);},_updateUI:function(){this.attr("disabled",!this._enabled);_2.style(this.focusNode,{outline:"none"});_2.style(this.titleNode,{padding:"0px",border:"none"});if(this._checked){_2.style(this.titleNode,{backgroundColor:"#D4DFF2",border:"1px solid #316AC5"});}else{_2.style(this.titleNode,{backgroundColor:"",border:""});}if(this._activeTool){this.attr("iconClass",this._checked?this._activeTool._enabledIcon:this._activeTool._disabledIcon);this.attr("label",this._activeTool.label);}}});});