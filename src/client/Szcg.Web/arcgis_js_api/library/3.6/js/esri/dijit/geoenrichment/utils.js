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
define("esri/dijit/geoenrichment/utils",[],function(){return {getCeiling:function(_1,_2){if(_1==0){return 0;}var _3;if(_1<0){_1=-_1;_3=-1;}else{_3=1;}var _4=2;var _5=Math.pow(10,Math.ceil(Math.log(_1)/Math.LN10)-1);var _6=Math.ceil(_1/_5/_4)*_4*_5;if(_2&&Math.log(_6)/Math.LN10%1==0){_6*=_4;}return _6*_3;},supportsComparison:function(_7,_8){return _7=="OneVar"||_7!="Tapestry"&&_8;}};});