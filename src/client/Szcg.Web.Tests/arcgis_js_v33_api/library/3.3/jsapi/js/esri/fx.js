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
define("esri/fx",["dijit","dojo","dojox","dojo/require!dojo/fx"],function(_1,_2,_3){_2.provide("esri.fx");_2.require("dojo.fx");esri.fx.animateRange=function(_4){var _5=_4.range;return new _2._Animation(_2.mixin({curve:new _2._Line(_5.start,_5.end)},_4));};esri.fx.resize=function(_6){var _7=(_6.node=_2.byId(_6.node)),_8=_6.start,_9=_6.end;if(!_8){var mb=_2._getMarginBox(_7),pb=_2._getPadBorderExtents(_7);_8=(_6.start={left:mb.l+pb.l,top:mb.t+pb.t,width:mb.w-pb.w,height:mb.h-pb.h});}if(!_9){var _a=_6.anchor?_6.anchor:{x:_8.left,y:_8.top},_b=_6.size;_9=_6.end={left:(_8.left-((_b.width-_8.width)*(_a.x-_8.left)/_8.width)),top:(_8.top-((_b.height-_8.height)*(_a.y-_8.top)/_8.height)),width:_b.width,height:_b.height};}return _2.animateProperty(_2.mixin({properties:{left:{start:_8.left,end:_9.left},top:{start:_8.top,end:_9.top},width:{start:_8.width,end:_9.width},height:{start:_8.height,end:_9.height}}},_6));};esri.fx.slideTo=function(_c){var _d=(_c.node=_2.byId(_c.node)),_e=_2.getComputedStyle,_f=null,_10=null,_11=(function(){var _12=_d;return function(){var pos=_12.style.position=="absolute"?"absolute":"relative";_f=(pos=="absolute"?_d.offsetTop:parseInt(_e(_d).top)||0);_10=(pos=="absolute"?_d.offsetLeft:parseInt(_e(_d).left)||0);if(pos!="absolute"&&pos!="relative"){var ret=_2.coords(_12,true);_f=ret.y;_10=ret.x;_12.style.position="absolute";_12.style.top=_f+"px";_12.style.left=_10+"px";}};})();_11();var _13=_2.animateProperty(_2.mixin({properties:{top:{start:_f,end:_c.top||0},left:{start:_10,end:_c.left||0}}},_c));_2.connect(_13,"beforeBegin",_13,_11);return _13;};esri.fx.flash=function(_14){_14=_2.mixin({end:"#f00",duration:500,count:1},_14);_14.duration/=_14.count*2;var _15=_2.byId(_14.node),_16=_14.start;if(!_16){_16=_2.getComputedStyle(_15).backgroundColor;}var end=_14.end,_17=_14.duration,_18=[],_19={node:_15,duration:_17};for(var i=0,il=_14.count;i<il;i++){_18.push(_2.animateProperty(_2.mixin({properties:{backgroundColor:{start:_16,end:end}}},_19)));_18.push(_2.animateProperty(_2.mixin({properties:{backgroundColor:{start:end,end:_16}}},_19)));}return _2.fx.chain(_18);};});