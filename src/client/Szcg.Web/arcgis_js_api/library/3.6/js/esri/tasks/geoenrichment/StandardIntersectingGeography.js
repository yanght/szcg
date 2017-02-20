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
define("esri/tasks/geoenrichment/StandardIntersectingGeography",["../../declare","./IntersectingGeography"],function(_1,_2){var _3=_1("esri.tasks.geoenrichment.StandardIntersectingGeography",[_2],{geographyLayerID:null,constructor:function(_4){if(_4){this.geographyLayerID=_4.geographyLayer||_4.geographyLayerID||null;}},toJson:function(){var _5=this.inherited(arguments);_5.geographyType="standard";_5.geographyLayer=this.geographyLayerID;return _5;}});return _3;});