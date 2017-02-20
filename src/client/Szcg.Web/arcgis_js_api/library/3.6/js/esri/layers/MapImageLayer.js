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
define("esri/layers/MapImageLayer",["dojo/_base/declare","dojo/_base/connect","dojo/_base/lang","dojo/_base/array","dojo/dom-construct","dojo/dom-style","esri/kernel","esri/config","esri/sniff","esri/domUtils","esri/geometry/Point","esri/geometry/webMercatorUtils","esri/layers/layer"],function(_1,_2,_3,_4,_5,_6,_7,_8,_9,_a,_b,_c,_d){var _e=_1([_d],{declaredClass:"esri.layers.MapImageLayer","-chains-":{constructor:"manual"},constructor:function(_f){this.inherited(arguments,[null,_f]);this._mapImages=[];var _10=_3.hitch;this._panStart=_10(this,this._panStart);this._pan=_10(this,this._pan);this._extentChange=_10(this,this._extentChange);this._zoom=_10(this,this._zoom);this._zoomStart=_10(this,this._zoomStart);this._scale=_10(this,this._scale);this._resize=_10(this,this._resize);_2.connect(this,"onSuspend",this,this._onSuspend);_2.connect(this,"onResume",this,this._onResume);this.loaded=true;this.onLoad(this);},opacity:1,_transform:_7._getDOMAccessor("transform"),addImage:function(_11){var _12=this._mapImages.push(_11);_12=_12-1;_11._idx=_12;_11._layer=this;if(this._div){this._createImage(_11,_12);}},removeImage:function(_13){if(_13){var idx=_13._idx,_14=this._mapImages;if(_14[idx]===_13){delete _14[idx];var _15=_13._node;if(_15){this._clearEvents(_15);_15.e_idx=_15.e_bl=_15.e_tr=_15.e_l=_15.e_t=_15.e_w=_15.e_h=null;if(_15.parentNode){_15.parentNode.removeChild(_15);_5.destroy(_15);}}_13._node=_13._idx=_13._layer=null;}}},removeAllImages:function(){var _16=this._mapImages,i,len=_16.length;for(i=0;i<len;i++){var _17=_16[i];if(_17){this.removeImage(_17);}}this._mapImages=[];},getImages:function(){var _18=this._mapImages,_19=[],i,len=_18.length;for(i=0;i<len;i++){if(_18[i]){_19.push(_18[i]);}}return _19;},setOpacity:function(_1a){if(this.opacity!=_1a){this._opacityChanged(this.opacity=_1a);this.onOpacityChange();}},onOpacityChange:function(){},_opacityChanged:function(_1b){var div=this._div,i,len,_1c;if(div){if(!_9("ie")||_9("ie")>8){_6.set(div,"opacity",_1b);}else{_1c=div.childNodes;len=_1c.length;for(i=0;i<len;i++){_6.set(_1c[i],"opacity",_1b);}}}},_createImage:function(_1d,idx){var _1e=_5.create("img");_6.set(_1e,{position:"absolute"});if(_9("ie")<=8){_6.set(_1e,"opacity",this.opacity);}if(_1d.rotation){var _1f="rotate("+(360-_1d.rotation)+"deg)";if(_9("ie")<9){}else{_6.set(_1e,this._transform,_1f);_6.set(_1e,"transform",_1f);}}_1d._node=_1e;_1e.e_idx=idx;_1e.e_layer=this;_1e.e_load=_2.connect(_1e,"onload",_e.prototype._imageLoaded);_1e.e_error=_2.connect(_1e,"onerror",_e.prototype._imageError);_1e.e_abort=_2.connect(_1e,"onabort",_e.prototype._imageError);_1e.src=_1d.href;},_imageLoaded:function(evt,img){var _20=img||evt.target||evt.currentTarget,_21=_20.e_layer,_22=_21._mapImages[_20.e_idx],map=_21._map;if(map&&(map.__zooming||map.__panning||!_21._sr)){_21._standby.push(_20);return;}_21._clearEvents(_20);if(!_22||_22._node!==_20){return;}if(map){_21._attach(_22);}},_imageError:function(evt){var _23=evt.target||evt.currentTarget,_24=_23.e_layer,_25=_24._mapImages[_23.e_idx];_24._clearEvents(_23);if(_25){_25._node=null;}},_clearEvents:function(_26){var _27=_2.disconnect;_27(_26.e_load);_27(_26.e_error);_27(_26.e_abort);_26.e_load=_26.e_error=_26.e_abort=_26.e_layer=null;},_attach:function(_28){var _29=_28.extent,_2a=_29.spatialReference,_2b=this._sr,div=this._div,_2c=_28._node,_2d=new _b({x:_29.xmin,y:_29.ymin,spatialReference:_2a}),_2e=new _b({x:_29.xmax,y:_29.ymax,spatialReference:_2a});if(!_2b.equals(_2a)){if(_2b.isWebMercator()&&_2a.wkid===4326){_2d=_c.geographicToWebMercator(_2d);_2e=_c.geographicToWebMercator(_2e);}else{if(_2a.isWebMercator()&&_2b.wkid===4326){_2d=_c.webMercatorToGeographic(_2d);_2e=_c.webMercatorToGeographic(_2e);}}}_2c.e_bl=_2d;_2c.e_tr=_2e;if(_28.visible){this._setPos(_2c,div._left,div._top);(this._active||div).appendChild(_2c);}},_setPos:function(_2f,_30,_31){var _32=_2f.e_bl,_33=_2f.e_tr,map=this._map;_32=map.toScreen(_32);_33=map.toScreen(_33);var _34=_32.x-_30,top=_33.y-_31,_35=Math.abs(_33.x-_32.x),_36=Math.abs(_32.y-_33.y),css={width:_35+"px",height:_36+"px"},_37=this._mapImages[_2f.e_idx];if(map.navigationMode==="css-transforms"){css[_7._css.names.transform]=_7._css.translate(_34,top)+(_37.rotation?(" "+_7._css.rotate(360-_37.rotation)):"");}else{css.left=_34+"px";css.top=top+"px";}_6.set(_2f,css);_2f.e_l=_34;_2f.e_t=top;_2f.e_w=_35;_2f.e_h=_36;},managedSuspension:true,_setMap:function(map,_38){this.inherited(arguments);var div=this._div=_5.create("div",null,_38),_39=_7._css.names,css={position:"absolute"},vd=map.__visibleDelta;if(!_9("ie")||_9("ie")>8){css.opacity=this.opacity;}if(map.navigationMode==="css-transforms"){css[_39.transform]=_7._css.translate(vd.x,vd.y);_6.set(div,css);div._left=vd.x;div._top=vd.y;css={position:"absolute",width:map.width+"px",height:map.height+"px",overflow:"visible"};this._active=_5.create("div",null,div);_6.set(this._active,css);this._passive=_5.create("div",null,div);_6.set(this._passive,css);}else{div._left=0;div._top=0;_6.set(div,css);}this._standby=[];var _3a=this._mapImages,i,len=_3a.length;for(i=0;i<len;i++){var _3b=_3a[i],_3c=_3b._node;if(!_3c){this._createImage(_3b,_3b._idx);}}_a.hide(div);return div;},_unsetMap:function(map,_3d){this._disconnect();var div=this._div;if(div){var _3e=this._mapImages,i,len=_3e.length;for(i=0;i<len;i++){var _3f=_3e[i];if(_3f){var _40=_3f._node;if(_40){this._clearEvents(_40);_40.e_idx=_40.e_bl=_40.e_tr=_40.e_l=_40.e_t=_40.e_w=_40.e_h=null;}_3f._node=null;}}_3d.removeChild(div);_5.destroy(div);}this._map=this._div=this._sr=this._active=this._passive=this._standby=null;this.inherited(arguments);},_onSuspend:function(){this._disconnect();_a.hide(this._div);},_onResume:function(evt){if(evt.firstOccurrence){this._sr=this._map.spatialReference;this._processStandbyList();}var map=this._map,div=this._div,vd=map.__visibleDelta;if(map.navigationMode==="css-transforms"){div._left=vd.x;div._top=vd.y;_6.set(div,_7._css.names.transform,_7._css.translate(div._left,div._top));}this._redraw(map.navigationMode==="css-transforms");this._connect(map);_a.show(div);},_connect:function(map){if(!this._connections){var _41=_2.connect,_42=(map.navigationMode==="css-transforms");this._connections=[_41(map,"onPanStart",this._panStart),_41(map,"onPan",this._pan),_41(map,"onExtentChange",this._extentChange),_42&&_41(map,"onZoomStart",this._zoomStart),_42?_41(map,"onScale",this._scale):_41(map,"onZoom",this._zoom),_42&&_41(map,"onResize",this._resize)];}},_disconnect:function(){if(this._connections){_4.forEach(this._connections,_2.disconnect);this._connections=null;}},_panStart:function(){this._panL=this._div._left;this._panT=this._div._top;},_pan:function(_43,_44){var div=this._div;div._left=this._panL+_44.x;div._top=this._panT+_44.y;if(this._map.navigationMode==="css-transforms"){_6.set(div,_7._css.names.transform,_7._css.translate(div._left,div._top));}else{_6.set(div,{left:div._left+"px",top:div._top+"px"});}},_extentChange:function(_45,_46,_47){if(_47){this._redraw(this._map.navigationMode==="css-transforms");}else{if(_46){this._pan(_45,_46);}}this._processStandbyList();},_processStandbyList:function(){var i,_48=this._standby;if(_48&&_48.length){for(i=_48.length-1;i>=0;i--){this._imageLoaded(null,_48[i]);_48.splice(i,1);}}},_redraw:function(_49){if(_49){var _4a=this._passive,_4b=_7._css.names;_6.set(_4a,_4b.transition,"none");this._moveImages(_4a,this._active);_6.set(_4a,_4b.transform,"none");}var div=this._active||this._div,_4c=this._div._left,_4d=this._div._top,i,len=div.childNodes.length,_4e;for(i=0;i<len;i++){_4e=div.childNodes[i];this._setPos(_4e,_4c,_4d);}},_zoom:function(_4f,_50,_51){var div=this._div,_52=div._left,_53=div._top,i,len=div.childNodes.length,_54;for(i=0;i<len;i++){_54=div.childNodes[i];var _55=_54.e_w*_50,_56=_54.e_h*_50,_57=(_51.x-_52-_54.e_l)*(_55-_54.e_w)/_54.e_w,_58=(_51.y-_53-_54.e_t)*(_56-_54.e_h)/_54.e_h;_57=isNaN(_57)?0:_57;_58=isNaN(_58)?0:_58;_6.set(_54,{left:(_54.e_l-_57)+"px",top:(_54.e_t-_58)+"px",width:_55+"px",height:_56+"px"});}},_zoomStart:function(){this._moveImages(this._active,this._passive);},_moveImages:function(_59,_5a){var _5b=_59.childNodes,i,len=_5b.length;if(len>0){for(i=len-1;i>=0;i--){_5a.appendChild(_5b[i]);}}},_scale:function(mtx,_5c){var css={},_5d=_7._css.names,_5e=this._passive;_6.set(_5e,_5d.transition,_5c?"none":(_5d.transformName+" "+_8.defaults.map.zoomDuration+"ms ease"));css[_5d.transform]=_7._css.matrix(mtx);_6.set(_5e,_5d.transform,_7._css.matrix(mtx));},_resize:function(_5f,_60,_61){_6.set(this._active,{width:_60+"px",height:_61+"px"});_6.set(this._passive,{width:_60+"px",height:_61+"px"});}});if(_9("extend-esri")){_3.setObject("layers.MapImageLayer",_e,_7);}return _e;});