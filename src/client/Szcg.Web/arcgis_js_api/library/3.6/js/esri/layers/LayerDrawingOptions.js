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
define("esri/layers/LayerDrawingOptions",["dojo/_base/declare","dojo/_base/lang","dojo/_base/array","dojo/has","esri/kernel","esri/lang","esri/renderers/jsonUtils","esri/layers/LabelClass"],function(_1,_2,_3,_4,_5,_6,_7,_8){var _9=_1(null,{declaredClass:"esri.layers.LayerDrawingOptions",constructor:function(_a){if(_a){_2.mixin(this,_a);if(_a.renderer){this.renderer=_7.fromJson(_a.renderer);}if(_a.labelingInfo&&_a.labelingInfo.length>0){this.labelingInfo=[];var _b;_3.forEach(_a.labelingInfo,function(_c){_b=new _8(_c);this.labelingInfo.push(_b);},this);}}},toJson:function(){var _d={renderer:this.renderer&&this.renderer.toJson(),transparency:this.transparency,scaleSymbols:this.scaleSymbols,showLabels:this.showLabels};if(this.labelingInfo&&this.labelingInfo.length>0){_d.labelingInfo=[];_3.forEach(this.labelingInfo,function(_e){_d.labelingInfo.push(_e.toJson());});}return _6.fixJson(_d);}});if(_4("extend-esri")){_2.setObject("layers.LayerDrawingOptions",_9,_5);}return _9;});