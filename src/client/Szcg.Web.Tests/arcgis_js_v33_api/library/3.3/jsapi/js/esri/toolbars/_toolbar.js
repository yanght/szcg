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
define("esri/toolbars/_toolbar",["dijit","dojo","dojox"],function(_1,_2,_3){_2.provide("esri.toolbars._toolbar");_2.declare("esri.toolbars._Toolbar",null,{constructor:function(_4){this.map=_4;},_cursors:{"move":"pointer","move-v":"pointer","move-gv":"pointer","box0":"nw-resize","box1":"n-resize","box2":"ne-resize","box3":"e-resize","box4":"se-resize","box5":"s-resize","box6":"sw-resize","box7":"w-resize","box8":"pointer"},_deactivateMapTools:function(_5,_6,_7,_8){var _9=this.map;if(_5){this._mapNavState={isDoubleClickZoom:_9.isDoubleClickZoom,isClickRecenter:_9.isClickRecenter,isPan:_9.isPan,isRubberBandZoom:_9.isRubberBandZoom,isKeyboardNavigation:_9.isKeyboardNavigation,isScrollWheelZoom:_9.isScrollWheelZoom};_9.disableDoubleClickZoom();_9.disableClickRecenter();_9.disablePan();_9.disableRubberBandZoom();_9.disableKeyboardNavigation();}if(_6){_9.hideZoomSlider();}if(_7){_9.hidePanArrows();}if(_8){_9.graphics.disableMouseEvents();}},_activateMapTools:function(_a,_b,_c,_d){var _e=this.map,_f=this._mapNavState;if(_a&&_f){if(_f.isDoubleClickZoom){_e.enableDoubleClickZoom();}if(_f.isClickRecenter){_e.enableClickRecenter();}if(_f.isPan){_e.enablePan();}if(_f.isRubberBandZoom){_e.enableRubberBandZoom();}if(_f.isKeyboardNavigation){_e.enableKeyboardNavigation();}if(_f.isScrollWheelZoom){_e.enableScrollWheelZoom();}}if(_b){_e.showZoomSlider();}if(_c){_e.showPanArrows();}if(_d){_e.graphics.enableMouseEvents();}}});});