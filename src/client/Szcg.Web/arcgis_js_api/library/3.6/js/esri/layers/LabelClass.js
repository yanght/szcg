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
define("esri/layers/LabelClass",["dojo/_base/declare","dojo/_base/lang","dojo/has","esri/kernel","esri/lang","esri/symbols/TextSymbol"],function(_1,_2,_3,_4,_5,_6){var _7=_1(null,{declaredClass:"esri.layers.LabelClass",labelPlacement:null,labelExpression:null,useCodedValues:null,symbol:null,maxScale:0,minScale:0,where:null,labelPlacementLookup:{"above-center":"esriServerPointLabelPlacementAboveCenter","above-left":"esriServerPointLabelPlacementAboveLeft","above-right":"esriServerPointLabelPlacementAboveRight","below-center":"esriServerPointLabelPlacementBelowCenter","below-left":"esriServerPointLabelPlacementBelowLeft","below-right":"esriServerPointLabelPlacementBelowRight","center-center":"esriServerPointLabelPlacementCenterCenter","center-left":"esriServerPointLabelPlacementCenterLeft","center-right":"esriServerPointLabelPlacementCenterRight","above-after":"esriServerLinePlacementAboveAfter","above-along":"esriServerLinePlacementAboveAlong","above-before":"esriServerLinePlacementAboveBefore","above-start":"esriServerLinePlacementAboveStart","above-end":"esriServerLinePlacementAboveEnd","below-after":"esriServerLinePlacementBelowAfter","below-along":"esriServerLinePlacementBelowAlong","below-before":"esriServerLinePlacementBelowBefore","below-start":"esriServerLinePlacementBelowStart","below-end":"esriServerLinePlacementBelowEnd","center-after":"esriServerLinePlacementCenterAfter","center-along":"esriServerLinePlacementCenterAlong","center-before":"esriServerLinePlacementCenterBefore","center-start":"esriServerLinePlacementCenterStart","center-end":"esriServerLinePlacementCenterEnd","always-horizontal":"esriServerPolygonPlacementAlwaysHorizontal"},constructor:function(_8){if(_8){_2.mixin(this,_8);if(this.labelPlacementLookup.hasOwnProperty(_8.labelPlacement)){this.labelPlacement=this.labelPlacementLookup[_8.labelPlacement];}if(_8.symbol){this.symbol=new _6(_8.symbol);}}},toJson:function(){var _9=this;if(this.labelPlacementLookup.hasOwnProperty(_9.labelPlacement)){_9.labelPlacement=this.labelPlacementLookup[_9.labelPlacement];}if(this.symbol){_9.symbol=this.symbol.toJson();}return _5.fixJson(_9);}});if(_3("extend-esri")){_2.setObject("layers.LabelClass",_7,_4);}return _7;});