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
define("esri/dijit/editing/tools/ToolBase",["dijit","dojo","dojox","dojo/require!esri/toolbars/draw"],function(_1,_2,_3){_2.provide("esri.dijit.editing.tools.ToolBase");_2.require("esri.toolbars.draw");_2.declare("esri.dijit.editing.tools.ToolBase",null,{_enabled:true,showLabel:false,constructor:function(_4,_5){_4=_4||{};_2.mixin(this,_4);this.label=this._label?esri.bundle.widgets.editor.tools[this._label]:"";this._settings=_4.settings;this._toolbar=_4.settings.drawToolbar;this._editToolbar=_4.settings.editToolbar;this._initializeTool();},onFinished:function(){},onDrawEnd:function(){},onApplyEdits:function(){},postCreate:function(){this.deactivate();this.inherited(arguments);},destroy:function(){},activate:function(_6){if(this._toolbar){this._toolbar.deactivate();}if(this._editToolbar){this._editToolbar.deactivate();}if(!this._enabled){return;}this._checked=true;this._layer=_6;if(this._toolbar&&this._drawType){this._toolbar.activate(this._drawType);}},deactivate:function(){if(this._toolbar){this._toolbar.deactivate();}if(this._editToolbar){this._editToolbar.deactivate();}this.setChecked(false);this._updateUI();},setEnabled:function(_7){this._enabled=_7;this._updateUI();},setChecked:function(_8){this._checked=_8;},enable:function(_9){this._updateUI();},isEnabled:function(){return _enabled;},getToolName:function(){return this._toolName;},_initializeTool:function(){},_updateUI:function(){this.disabled=!this._enabled;this.attr("iconClass",this._enabled?this._enabledIcon:this._disabledIcon);}});});