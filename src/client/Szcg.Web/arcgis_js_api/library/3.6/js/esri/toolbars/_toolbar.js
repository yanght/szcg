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
define("esri/toolbars/_toolbar",["dojo/_base/declare","dojo/_base/lang","dojo/has","esri/kernel","esri/Evented"],function(_1,_2,_3,_4,_5){var TB=_1([_5],{declaredClass:"esri.toolbars._Toolbar",constructor:function(_6){this.map=_6;},_cursors:{"move":"pointer","move-v":"pointer","move-gv":"pointer","box0":"nw-resize","box1":"n-resize","box2":"ne-resize","box3":"e-resize","box4":"se-resize","box5":"s-resize","box6":"sw-resize","box7":"w-resize","box8":"pointer"},_deactivateMapTools:function(_7,_8,_9,_a){var _b=this.map;if(_7){this._mapNavState={isDoubleClickZoom:_b.isDoubleClickZoom,isClickRecenter:_b.isClickRecenter,isPan:_b.isPan,isRubberBandZoom:_b.isRubberBandZoom,isKeyboardNavigation:_b.isKeyboardNavigation,isScrollWheelZoom:_b.isScrollWheelZoom};_b.disableDoubleClickZoom();_b.disableClickRecenter();_b.disablePan();_b.disableRubberBandZoom();_b.disableKeyboardNavigation();}if(_8){_b.hideZoomSlider();}if(_9){_b.hidePanArrows();}if(_a){_b.graphics.disableMouseEvents();}},_activateMapTools:function(_c,_d,_e,_f){var map=this.map,_10=this._mapNavState;if(_c&&_10){if(_10.isDoubleClickZoom){map.enableDoubleClickZoom();}if(_10.isClickRecenter){map.enableClickRecenter();}if(_10.isPan){map.enablePan();}if(_10.isRubberBandZoom){map.enableRubberBandZoom();}if(_10.isKeyboardNavigation){map.enableKeyboardNavigation();}if(_10.isScrollWheelZoom){map.enableScrollWheelZoom();}}if(_d){map.showZoomSlider();}if(_e){map.showPanArrows();}if(_f){map.graphics.enableMouseEvents();}}});if(_3("extend-esri")){_2.setObject("toolbars._Toolbar",TB,_4);}return TB;});