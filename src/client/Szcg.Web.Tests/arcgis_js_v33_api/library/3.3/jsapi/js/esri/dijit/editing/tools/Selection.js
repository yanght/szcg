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
define("esri/dijit/editing/tools/Selection",["dijit","dojo","dojox","dojo/require!esri/dijit/editing/tools/DropDownToolBase,esri/dijit/editing/Util,esri/dijit/editing/tools/SelectionTools"],function(_1,_2,_3){_2.provide("esri.dijit.editing.tools.Selection");_2.require("esri.dijit.editing.tools.DropDownToolBase");_2.require("esri.dijit.editing.Util");_2.require("esri.dijit.editing.tools.SelectionTools");_2.declare("esri.dijit.editing.tools.Selection",[esri.dijit.editing.tools.DropDownToolBase],{_enabled:true,activate:function(){this.inherited(arguments);this._sConnect=_2.connect(this._toolbar,"onDrawEnd",this,"_onDrawEnd");},deactivate:function(){this.inherited(arguments);_2.disconnect(this._sConnect);delete this._sConnect;},_initializeTool:function(){this._createSymbols();this._initializeLayers();this._toolTypes=["select","selectadd","selectremove"];},_onDrawEnd:function(_4){this.inherited(arguments);this._settings.editor._hideAttributeInspector();var _5=this._settings.layers;this._selectMethod=this._activeTool._selectMethod;this._settings.editor._selectionHelper.selectFeaturesByGeometry(_5,_4,this._selectMethod,_2.hitch(this,"onFinished"));},_createSymbols:function(){this._pointSelectionSymbol=new esri.symbol.SimpleMarkerSymbol(esri.symbol.SimpleMarkerSymbol.STYLE_CIRCLE,10,new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID,new _2.Color([0,0,0]),1),new _2.Color([255,0,0,0.5]));this._polylineSelectionSymbol=new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID,new _2.Color([0,200,255]),2);this._polygonSelectionSymbol=new esri.symbol.SimpleFillSymbol(esri.symbol.SimpleFillSymbol.STYLE_SOLID,new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID,new _2.Color([0,0,0]),1),new _2.Color([0,200,255,0.5]));},_initializeLayers:function(){var _6=this._settings.layers;_2.forEach(_6,this._setSelectionSymbol,this);},_setSelectionSymbol:function(_7){var _8=null;switch(_7.geometryType){case "esriGeometryPoint":_8=this._pointSelectionSymbol;break;case "esriGeometryPolyline":_8=this._polylineSelectionSymbol;break;case "esriGeometryPolygon":_8=this._polygonSelectionSymbol;break;}_7.setSelectionSymbol(_7._selectionSymbol||_8);},_createTools:function(){_2.forEach(this._toolTypes,this._createTool,this);this.inherited(arguments);},_createTool:function(_9){var _a=_2.mixin(esri.dijit.editing.tools.SelectionTools[_9],{settings:this._settings,onClick:_2.hitch(this,"onItemClicked")});this._tools[_9.toUpperCase()]=new esri.dijit.editing.tools.Edit(_a);}});});