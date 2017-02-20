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
define("esri/tasks/geoenrichment/GeographyQueryBase",["../../declare","esri/SpatialReference"],function(_1,_2){return _1("esri.tasks.geoenrichment.GeographyQueryBase",null,{countryID:null,datasetID:null,outSR:null,returnGeometry:false,returnCentroids:false,generalizationLevel:null,useFuzzySearch:false,featureLimit:null,constructor:function(_3){if(_3){if(_3.sourceCountry){this.countryID=_3.sourceCountry;}if(_3.optionalCountryDataset){this.datasetID=_3.optionalCountryDataset;}if(_3.outSR){this.outSR=new _2(_3.outSR);}if(_3.returnGeometry){this.returnGeometry=_3.returnGeometry;}if(_3.returnCentroids){this.returnCentroids=_3.returnCentroids;}if(_3.generalizationLevel){this.generalizationLevel=_3.generalizationLevel;}if(_3.useFuzzySearch){this.useFuzzySearch=_3.useFuzzySearch;}if(_3.featureLimit){this.featureLimit=_3.featureLimit;}}},toJson:function(){var _4={};if(this.countryID){_4.sourceCountry=this.countryID;}if(this.datasetID){_4.optionalCountryDataset=this.datasetID;}if(this.outSR){_4.outSR=this.outSR.toJson();}if(this.returnGeometry){_4.returnGeometry=this.returnGeometry;}if(this.returnCentroids){_4.returnCentroids=this.returnCentroids;}if(this.generalizationLevel){_4.generalizationLevel=this.generalizationLevel;}if(this.useFuzzySearch){_4.useFuzzySearch=this.useFuzzySearch;}if(this.featureLimit){_4.featureLimit=this.featureLimit;}return _4;}});});