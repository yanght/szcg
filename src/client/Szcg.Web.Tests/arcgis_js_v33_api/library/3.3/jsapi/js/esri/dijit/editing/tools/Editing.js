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
define("esri/dijit/editing/tools/Editing",["dijit","dojo","dojox","dojo/require!esri/dijit/editing/tools/DropDownToolBase,esri/dijit/editing/Util,esri/dijit/editing/tools/EditingTools"],function(_1,_2,_3){_2.provide("esri.dijit.editing.tools.Editing");_2.require("esri.dijit.editing.tools.DropDownToolBase");_2.require("esri.dijit.editing.Util");_2.require("esri.dijit.editing.tools.EditingTools");_2.declare("esri.dijit.editing.tools.Editing",[esri.dijit.editing.tools.DropDownToolBase],{_enabled:false,deactivate:function(){if(!this._enabled){return;}this._enabled=false;this.inherited(arguments);this._settings.templatePicker.clearSelection();},onItemClicked:function(_4){this.inherited(arguments);if(this._activeTool===this._tools.AUTOCOMPLETE){this._settings.editor._drawingTool=esri.layers.FeatureTemplate.TOOL_AUTO_COMPLETE_POLYGON;}},_activateTool:function(_5,_6){this.enable(_6);for(var i in this._tools){if(this._tools.hasOwnProperty(i)){this.dropDown.removeChild(this._tools[i]);if(this._tools[i]._enabled===true){this.dropDown.addChild(this._tools[i]);}}}if(this._geometryType!==_6||this._activeTool._enabled===false){this._activeTool=this._tools[_5.toUpperCase()];}this._geometryType=_6;this._activeTool.activate();this._activeTool.setChecked(true);this._updateUI();},_initializeTool:function(_7){this.inherited(arguments);this._initializeTools();},_initializeTools:function(){var _8=this._settings.layers;var _9=this._settings.editor;var _a=false,_b=false,_c=false;var _d=this._toolTypes=[];var _e;_2.forEach(_8,function(_f){_e=_f.geometryType;_a=_a||_e==="esriGeometryPoint";_b=_b||_e==="esriGeometryPolyline";_c=_c||_e==="esriGeometryPolygon";_d=_d.concat(_2.map(this._getTemplatesFromLayer(_f),_2.hitch(this,function(_10){return _9._toDrawTool(_10.drawingTool,_f);})));},this);var _11=this._settings.createOptions;if(_a){this._toolTypes.push("point");}if(_b){this._toolTypes=this._toolTypes.concat(_11.polylineDrawTools);}if(_c){this._toolTypes=this._toolTypes.concat(_11.polygonDrawTools);}this._toolTypes=this._toUnique(this._toolTypes.concat(_d));},_toUnique:function(arr){var _12={};return _2.filter(arr,function(val){return _12[val]?false:(_12[val]=true);});},_getTemplatesFromLayer:function(_13){var _14=_13.templates||[];var _15=_13.types;_2.forEach(_15,function(_16){_14=_14.concat(_16.templates);});return _2.filter(_14,esri._isDefined);},_createTools:function(){_2.forEach(this._toolTypes,this._createTool,this);this.inherited(arguments);},_createTool:function(_17){var _18=_2.mixin(esri.dijit.editing.tools.EditingTools[_17],{settings:this._settings,onClick:_2.hitch(this,"onItemClicked")});this._tools[_17.toUpperCase()]=new esri.dijit.editing.tools.Edit(_18);}});});