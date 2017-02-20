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
define("esri/tasks/geoenrichment/ReportParameters",["../../declare","./EnrichParametersBase"],function(_1,_2){return _1("esri.tasks.geoenrichment.ReportParameters",[_2],{reportID:null,format:"pdf",fields:null,constructor:function(_3){if(_3){this.reportID=_3.report||_3.reportID||null;this.format=_3.format;this.fields=_3.reportFields||_3.fields||null;}},toJson:function(){var _4=this.inherited(arguments);if(this.reportID){_4.report=this.reportID;}if(this.format){_4.format=this.format;}if(this.fields){_4.reportFields=this.fields;}return _4;}});});