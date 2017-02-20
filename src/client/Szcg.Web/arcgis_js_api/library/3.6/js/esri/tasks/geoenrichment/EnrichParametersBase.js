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
define("esri/tasks/geoenrichment/EnrichParametersBase",["../../declare","esri/SpatialReference","./StudyAreaOptions","./studyAreaFromJson","./studyAreaOptionsFromJson"],function(_1,_2,_3,_4,_5){return _1("esri.tasks.geoenrichment.EnrichParametersBase",null,{studyAreaOptions:null,studyAreas:null,countryID:null,datasetID:null,constructor:function(_6){this.studyAreas=[];if(_6){if(_6.studyAreas){var _7;if(_6.inSR){_7=_6.inSR;}var _8=_6.studyAreas;for(var i=0;i<_8.length;i++){var _9=_4(_8[i]);if(_7&&_9.geometry){_9.geometry.setSpatialReference(new _2(_7));}this.studyAreas.push(_9);}}if(_6.studyAreasOptions){this.studyAreaOptions=_5(_6.studyAreasOptions);}else{if(_6.studyAreaOptions){if(_6.studyAreaOptions instanceof _3){this.studyAreaOptions=_6.studyAreaOptions;}else{this.studyAreaOptions=_5(_6.studyAreaOptions);}}}var _a=_6.useData;if(_a){if(_a.sourceCountry){this.countryID=_a.sourceCountry;}if(_a.dataset){this.datasetID=_a.dataset;}}else{if(_6.countryID){this.countryID=_6.countryID;}if(_6.datasetID){this.datasetID=_6.datasetID;}}}},toJson:function(){var _b={};var _c=[];for(var i=0;i<this.studyAreas.length;i++){var _d=this.studyAreas[i];_c.push(_d.toJson());}if(_c.length>0){_b.studyAreas=_c;}if(this.studyAreaOptions){_b.studyAreasOptions=this.studyAreaOptions.toJson();}if(this.countryID||this.datasetID){var _e={};if(this.countryID){_e.sourceCountry=this.countryID;}if(this.datasetID){_e.dataset=this.datasetID;}_b.useData=_e;}return _b;}});});