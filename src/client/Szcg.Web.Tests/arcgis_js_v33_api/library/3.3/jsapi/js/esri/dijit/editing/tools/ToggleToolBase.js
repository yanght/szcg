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
define("esri/dijit/editing/tools/ToggleToolBase",["dijit","dojo","dojox","dojo/require!esri/dijit/editing/tools/ToolBase,dijit/form/ToggleButton,esri/toolbars/draw"],function(_1,_2,_3){_2.provide("esri.dijit.editing.tools.ToggleToolBase");_2.require("esri.dijit.editing.tools.ToolBase");_2.require("dijit.form.ToggleButton");_2.require("esri.toolbars.draw");_2.declare("esri.dijit.editing.tools.ToggleToolBase",[_1.form.ToggleButton,esri.dijit.editing.tools.ToolBase],{postCreate:function(){this.inherited(arguments);if(this._setShowLabelAttr){this._setShowLabelAttr(false);}},destroy:function(){_1.form.ToggleButton.prototype.destroy.apply(this,arguments);esri.dijit.editing.tools.ToolBase.prototype.destroy.apply(this,arguments);},setChecked:function(_4){_1.form.ToggleButton.prototype.setChecked.apply(this,arguments);}});});