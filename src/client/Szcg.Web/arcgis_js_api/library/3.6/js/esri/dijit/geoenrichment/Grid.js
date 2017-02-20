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
define("esri/dijit/geoenrichment/Grid",["../../declare","dojo/dom-class","dijit/layout/_LayoutWidget"],function(_1,_2,_3){function _4(_5){_5.style.position="absolute";_2.add(_5,"Grid_Measure");var _6=_5.scrollHeight;_2.remove(_5,"Grid_Measure");return _6;};var _7=_1("esri.dijit.geoenrichment.Grid",[_3],{layout:function(){var _8=this.getChildren();var _9=[];for(var i=0;i<this.rows.length;i++){_9.push(0);}var _a=[];for(var i=0;i<_8.length;i++){var _b=_8[i];var _c=_b.row;var _d=_4(_b.domNode);_a.push(_d);if(_d>_9[_c]){_9[_c]=_d;}}var _e=this._contentBox.h;var _f=0;for(var i=0;i<_9.length;i++){switch(this.rows[i]){case _7.AUTO:_e-=_9[i];break;case _7.STRETCH:case _7.STACK:_f++;break;}}var _10=_e/_f;var y=0;for(var i=0;i<_8.length;i++){var _b=_8[i];var _c=_b.row;var dim={t:y,w:this._contentBox.w};switch(this.rows[_c]){case _7.AUTO:dim.h=_a[i];break;case _7.STRETCH:dim.h=_10;break;case _7.STACK:dim.h=Math.min(_10,_a[i]);break;}y+=dim.h;_b.resize(dim);}}});_7.AUTO="auto";_7.STRETCH="stretch";_7.STACK="stack";return _7;});