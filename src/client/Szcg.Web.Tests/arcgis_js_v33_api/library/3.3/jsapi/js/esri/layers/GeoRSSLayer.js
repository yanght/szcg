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
define("esri/layers/GeoRSSLayer",["dijit","dojo","dojox","dojo/require!esri/layers/ServiceGeneratedFeatureCollection"],function(_1,_2,_3){_2.provide("esri.layers.GeoRSSLayer");_2.require("esri.layers.ServiceGeneratedFeatureCollection");_2.declare("esri.layers.GeoRSSLayer",[esri.layers._ServiceGeneratedFeatureCollection],{serviceUrl:"http://utility.arcgis.com/sharing/rss",constructor:function(_4,_5){if(esri.config.defaults.geoRSSService){this.serviceUrl=esri.config.defaults.geoRSSService;}this._createLayer();},parse:function(){this._io=esri.request({url:this.serviceUrl,content:{url:this._url.path,refresh:this.loaded?true:undefined,outSR:this._outSR?_2.toJson(this._outSR.toJson()):undefined},callbackParamName:"callback"});return this._io;},_initLayer:function(_6){this.inherited(arguments);if(!this.loaded){this.loaded=true;this.onLoad(this);}}});});