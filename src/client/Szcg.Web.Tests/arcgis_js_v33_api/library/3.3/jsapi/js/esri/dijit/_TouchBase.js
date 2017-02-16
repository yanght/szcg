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
define("esri/dijit/_TouchBase",["dijit","dojo","dojox"],function(_1,_2,_3){_2.provide("esri.dijit._TouchBase");_2.declare("esri.dijit._TouchBase",null,{_preventDefault:true,_swipeThreshold:20,constructor:function(_4,_5){this.domNode=_2.byId(_4);this.events=[_2.connect(this.domNode,"touchstart",this,this._touchStartHandler),_2.connect(this.domNode,"touchmove",this,this._touchMoveHandler),_2.connect(this.domNode,"touchend",this,this._touchEndHandler),_2.connect(this.domNode,"onclick",this,this._clickHandler)];},setPreventDefault:function(_6){this._preventDefault=_6;},disableOnClick:function(){_2.disconnect(this.events.pop());},_clickHandler:function(e){if(!this._moved){this.onclick(e);}else{e.preventDefault();}},_touchStartHandler:function(e){this._moved=false;this.client_x=e.targetTouches[0].clientX;this.client_y=e.targetTouches[0].clientY;this.down_x=e.targetTouches[0].pageX;this.down_y=e.targetTouches[0].pageY;e.downX=this.down_x;e.downY=this.down_y;this.onTouchStart(e);},_touchMoveHandler:function(e){if(this._preventDefault){e.preventDefault();}this._moved=true;this.up_x=e.targetTouches[0].pageX;this.cur_x=e.targetTouches[0].pageX-this.down_x;this.cur_y=e.targetTouches[0].pageY-this.down_y;e.curX=this.cur_x;e.curY=this.cur_y;this.onTouchMove(e);},_touchEndHandler:function(e){if(!this._moved){e.clientX=this.client_x;e.clientY=this.client_y;}else{e.curX=this.cur_x;e.curY=this.cur_y;if(this.down_x-this.up_x>this._swipeThreshold){e.swipeDirection="left";}else{if(this.up_x-this.down_x>this._swipeThreshold){e.swipeDirection="right";}}}this.onTouchEnd(e);},onTouchStart:function(_7){},onTouchMove:function(_8){},onTouchEnd:function(_9){},onclick:function(_a){}});});