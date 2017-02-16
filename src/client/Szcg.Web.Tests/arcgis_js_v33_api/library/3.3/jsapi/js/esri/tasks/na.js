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
define("esri/tasks/na",["dijit","dojo","dojox","dojo/require!esri/tasks/_task"],function(_1,_2,_3){_2.provide("esri.tasks.na");_2.require("esri.tasks._task");esri.tasks._NALengthUnit={esriFeet:"esriNAUFeet",esriKilometers:"esriNAUKilometers",esriMeters:"esriNAUMeters",esriMiles:"esriNAUMiles",esriNauticalMiles:"esriNAUNauticalMiles",esriYards:"esriNAUYards"};esri.tasks.NAOutputLine={NONE:"esriNAOutputLineNone",STRAIGHT:"esriNAOutputLineStraight",TRUE_SHAPE:"esriNAOutputLineTrueShape",TRUE_SHAPE_WITH_MEASURE:"esriNAOutputLineTrueShapeWithMeasure"};esri.tasks.NAUTurn={ALLOW_BACKTRACK:"esriNFSBAllowBacktrack",AT_DEAD_ENDS_ONLY:"esriNFSBAtDeadEndsOnly",NO_BACKTRACK:"esriNFSBNoBacktrack",AT_DEAD_ENDS_AND_INTERSECTIONS:"esriNFSBAtDeadEndsAndIntersections"};esri.tasks.NAOutputPolygon={NONE:"esriNAOutputPolygonNone",SIMPLIFIED:"esriNAOutputPolygonSimplified",DETAILED:"esriNAOutputPolygonDetailed"};esri.tasks.NATravelDirection={FROM_FACILITY:"esriNATravelDirectionFromFacility",TO_FACILITY:"esriNATravelDirectionToFacility"};_2.declare("esri.tasks.NAMessage",null,{constructor:function(_4){_2.mixin(this,_4);}});_2.mixin(esri.tasks.NAMessage,{TYPE_INFORMATIVE:0,TYPE_PROCESS_DEFINITION:1,TYPE_PROCESS_START:2,TYPE_PROCESS_STOP:3,TYPE_WARNING:50,TYPE_ERROR:100,TYPE_EMPTY:101,TYPE_ABORT:200});_2.declare("esri.tasks.DataLayer",null,{name:null,where:null,geometry:null,spatialRelationship:null,toJson:function(){var _5={type:"layer",layerName:this.name,where:this.where,spatialRel:this.spatialRelationship};var g=this.geometry;if(g){_5.geometryType=esri.geometry.getJsonType(g);_5.geometry=g.toJson();}return esri.filter(_5,function(_6){if(_6!==null){return true;}});}});_2.mixin(esri.tasks.DataLayer,esri.tasks._SpatialRelationship);_2.declare("esri.tasks.DirectionsFeatureSet",esri.tasks.FeatureSet,{constructor:function(_7,_8){this.routeId=_7.routeId;this.routeName=_7.routeName;_2.mixin(this,_7.summary);this.extent=new esri.geometry.Extent(this.envelope);var _9=this._fromCompressedGeometry,_a=this.features,sr=this.extent.spatialReference,_b=[];_2.forEach(_8,function(cg,i){_a[i].setGeometry(_b[i]=_9(cg,sr));});this.strings=_7.strings;this.mergedGeometry=this._mergePolylinesToSinglePath(_b,sr);this.geometryType="esriGeometryPolyline";delete this.envelope;},_fromCompressedGeometry:function(_c,sr){var _d=0,_e=0,_f=[],x,y,_10=_c.replace(/(\+)|(\-)/g," $&").split(" "),_11=parseInt(_10[1],32);for(var j=2,jl=_10.length;j<jl;j+=2){_d=(x=(parseInt(_10[j],32)+_d));_e=(y=(parseInt(_10[j+1],32)+_e));_f.push([x/_11,y/_11]);}var po=new esri.geometry.Polyline({paths:[_f]});po.setSpatialReference(sr);return po;},_mergePolylinesToSinglePath:function(_12,sr){var _13=[];_2.forEach(_12,function(_14){_2.forEach(_14.paths,function(_15){_13=_13.concat(_15);});});var _16=[],_17=[0,0];_2.forEach(_13,function(_18){if(_18[0]!==_17[0]||_18[1]!==_17[1]){_16.push(_18);_17=_18;}});return new esri.geometry.Polyline({paths:[_16]}).setSpatialReference(sr);}});});