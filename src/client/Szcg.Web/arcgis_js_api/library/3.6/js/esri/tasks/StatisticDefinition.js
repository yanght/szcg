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
define("esri/tasks/StatisticDefinition",["dojo/_base/declare","dojo/_base/lang","dojo/has","esri/kernel"],function(_1,_2,_3,_4){var _5=_1(null,{declaredClass:"esri.tasks.StatisticDefinition",statisticType:null,onStatisticField:null,outStatisticFieldName:null,toJson:function(){var _6={statisticType:this.statisticType,onStatisticField:this.onStatisticField};if(this.outStatisticFieldName){_6.outStatisticFieldName=this.outStatisticFieldName;}return _6;}});if(_3("extend-esri")){_2.setObject("tasks.StatisticDefinition",_5,_4);}return _5;});