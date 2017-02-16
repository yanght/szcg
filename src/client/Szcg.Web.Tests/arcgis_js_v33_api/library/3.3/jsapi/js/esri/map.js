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
define("esri/map",["dijit","dojo","dojox","dojo/require!esri/main,esri/_coremap,esri/MapNavigationManager,esri/layers/agsdynamic,esri/layers/agstiled,dojo/_base/html,dijit/_base/manager,esri/layers/agsimageservice,dijit/form/HorizontalSlider,dijit/form/VerticalSlider,dijit/form/HorizontalRule,dijit/form/VerticalRule,dijit/form/HorizontalRuleLabels,dijit/form/VerticalRuleLabels"],function(_1,_2,_3){_2.provide("esri.map");_2.require("esri.main");_2.require("esri._coremap");_2.require("esri.MapNavigationManager");_2.require("esri.layers.agsdynamic");_2.require("esri.layers.agstiled");_2.require("dojo._base.html");_2.require("dijit._base.manager");_2.require("esri.layers.agsimageservice");_2.require("dijit.form.HorizontalSlider");_2.require("dijit.form.VerticalSlider");_2.require("dijit.form.HorizontalRule");_2.require("dijit.form.VerticalRule");_2.require("dijit.form.HorizontalRuleLabels");_2.require("dijit.form.VerticalRuleLabels");_2.declare("esri.Map",esri._CoreMap,(function(){var _4=30,_5=30,_6={up:"panUp",right:"panRight",down:"panDown",left:"panLeft"},_7={upperRight:"panUpperRight",lowerRight:"panLowerRight",lowerLeft:"panLowerLeft",upperLeft:"panUpperLeft"};var dc=_2.connect,_8=_2.disconnect,_9=_2.create,ds=_2.style,dh=_2.hitch,_a=_2.coords,_b=_2.deprecated,_c=_2.mixin;return {constructor:function(_d,_e){_c(this,{_slider:null,_navDiv:null,_mapParams:_c({attributionWidth:0.45,slider:true,nav:false,logo:true,sliderStyle:"small",sliderPosition:"top-left",sliderOrientation:"vertical",autoResize:true},_e||{})});_c(this,{isDoubleClickZoom:false,isShiftDoubleClickZoom:false,isClickRecenter:false,isScrollWheelZoom:false,isPan:false,isRubberBandZoom:false,isKeyboardNavigation:false,isPanArrows:false,isZoomSlider:false});if(_2.isFunction(esri._css)){esri._css=esri._css(this._mapParams.force3DTransforms);this.force3DTransforms=this._mapParams.force3DTransforms;}var _f=(esri._hasTransforms&&esri._hasTransitions);this.navigationMode=this._mapParams.navigationMode||(_f&&"css-transforms")||"classic";if(this.navigationMode==="css-transforms"&&!_f){this.navigationMode="classic";}this.fadeOnZoom=esri._isDefined(this._mapParams.fadeOnZoom)?this._mapParams.fadeOnZoom:(this.navigationMode==="css-transforms");if(this.navigationMode!=="css-transforms"){this.fadeOnZoom=false;}this.setMapCursor("default");this.smartNavigation=_e&&_e.smartNavigation;if(!esri._isDefined(this.smartNavigation)&&_2.isMac&&!esri.isTouchEnabled&&!(_2.isFF<=3.5)){var _10=navigator.userAgent.match(/Mac\s+OS\s+X\s+([\d]+)(\.|\_)([\d]+)\D/i);if(_10&&esri._isDefined(_10[1])&&esri._isDefined(_10[3])){var _11=parseInt(_10[1],10),_12=parseInt(_10[3],10);this.smartNavigation=((_11>10)||(_11===10&&_12>=6));}}var _13=true;this.showAttribution=esri._isDefined(this._mapParams.showAttribution)?this._mapParams.showAttribution:_13;this._onLoadHandler_connect=dc(this,"onLoad",this,"_onLoadInitNavsHandler");var _14=_9("div",{"class":"esriControlsBR"+(this._mapParams.nav?" withPanArrows":"")},this.root),_15;if(this.showAttribution){if(_2.getObject("esri.dijit.Attribution",false)){_15=_9("span",{"class":"esriAttribution"},_14);_2.style(_15,"width",Math.floor(this.width*this._mapParams.attributionWidth)+"px");this._connects.push(dc(_15,"onclick",function(){var _16="esriAttributionOpen";if(_2.hasClass(this,_16)){_2.removeClass(this,_16);}else{if(this.scrollWidth>this.clientWidth){_2.addClass(this,_16);}}}));this.attribution=new esri.dijit.Attribution({map:this},_15);}else{console.log("Unable to show map attribution. Did you forget to require 'esri.dijit.Attribution'?");}}if(this._mapParams.logo){var _17={};if(_2.isIE===6){_17.filter="progid:DXImageTransform.Microsoft.AlphaImageLoader(enabled='true', sizingMethod='crop', src='"+_2.moduleUrl("esri")+"images/map/logo-med.png"+"')";}var _18=this._ogol=_9("div",{style:_17},_14);if((this.root.clientWidth*this.root.clientHeight)<250000){_2.addClass(_18,"logo-sm");}else{_2.addClass(_18,"logo-med");}if(!esri.isTouchEnabled){this._ogol_connect=dc(_18,"onclick",this,"_openLogoLink");}}var _19=(this.navigationManager=new esri.MapNavigationManager(this)),_1a=_19.mouseEvents,_1b=_19.touchEvents;if(_1a){this.registerConnectEvents(_1a.constructor.connectEvents);}if(_1b){this.registerConnectEvents(_1b.constructor.connectEvents);}if(_e&&_e.basemap){this.setBasemap(_e.basemap);}this.autoResize=this._mapParams.autoResize;if(this.autoResize){var _1c=_1.getEnclosingWidget(this.container);this._connects.push(dc((_1c&&_1c.resize)?_1c:window,"resize",this,this.resize));this._connects.push(dc(window,"orientationchange",this,this.resize));}},_cleanUp:function(){this.disableMapNavigation();this.navigationManager.destroy();var i;for(i=this._connects.length;i>=0;i--){_8(this._connects[i]);delete this._connects[i];}_8(this._slider_connect);_8(this._ogol_connect);var _1d=this._slider;if(_1d&&_1d.destroy&&!_1d._destroyed){_1d.destroy();}var _1e=this._navDiv,_1f=this.attribution;if(_1e){_2.destroy(_1e);}if(_1f){_1f.destroy();}this.attribution=this.navigationManager=null;this.inherited("_cleanUp",arguments);},_isPanningOrZooming:function(){return this.__panning||this.__zooming;},_canZoom:function(_20){var _21=this.getLevel();return !this.__tileInfo||!((_21===this.getMinZoom()&&_20<0)||(_21===this.getMaxZoom()&&_20>0));},_onLoadInitNavsHandler:function(){this.enableMapNavigation();this._createNav();if(this._mapParams.sliderStyle==="small"||!this._createSlider){this._createSimpleSlider();}else{this._createSlider();}_8(this._onLoadHandler_connect);},_createNav:function(){if(this._mapParams.nav){var div,v,i,_22=_2.addClass,id=this.id;this._navDiv=_9("div",{id:id+"_navdiv"},this.root);_22(this._navDiv,"navDiv");var w2=this.width/2,h2=this.height/2,wh;for(i in _6){v=_6[i];div=_9("div",{id:id+"_pan_"+i},this._navDiv);_22(div,"fixedPan "+v);if(i==="up"||i==="down"){wh=parseInt(_a(div).w,10)/2;ds(div,{left:(w2-wh)+"px",zIndex:_4});}else{wh=parseInt(_a(div).h,10)/2;ds(div,{top:(h2-wh)+"px",zIndex:_4});}this._connects.push(dc(div,"onclick",dh(this,this[v])));}this._onMapResizeNavHandler_connect=dc(this,"onResize",this,"_onMapResizeNavHandler");for(i in _7){v=_7[i];div=_9("div",{id:id+"_pan_"+i,style:{zIndex:_4}},this._navDiv);_22(div,"fixedPan "+v);this._connects.push(dc(div,"onclick",dh(this,this[v])));}this.isPanArrows=true;}},_onMapResizeNavHandler:function(_23,wd,ht){var id=this.id,w2=wd/2,h2=ht/2,_24=_2.byId,i,div,wh;for(i in _6){div=_24(id+"_pan_"+i);if(i==="up"||i==="down"){wh=parseInt(_a(div).w,10)/2;ds(div,"left",(w2-wh)+"px");}else{wh=parseInt(_a(div).h,10)/2;ds(div,"top",(h2-wh)+"px");}}},_createSimpleSlider:function(){if(this._mapParams.slider){var _25=(this._slider=_9("div",{id:this.id+"_zoom_slider","class":this._getSliderClass(),style:{zIndex:_5}})),_26=(esri.isTouchEnabled&&!_2.isFF)?"touchstart":"onclick",_27=_9("div",{"class":"esriSimpleSliderIncrementButton"},_25),_28=_9("div",{"class":"esriSimpleSliderDecrementButton"},_25);_27.innerHTML="+";_28.innerHTML="&ndash;";if(_2.isIE<8){_2.addClass(_28,"dj_ie67Fix");}this._connects.push(dc(_27,_26,this,this._simpleSliderChangeHandler));this._connects.push(dc(_28,_26,this,this._simpleSliderChangeHandler));if(_2.isIE<10){_2.setSelectable(_25,false);}this.root.appendChild(_25);this.isZoomSlider=true;}},_simpleSliderChangeHandler:function(evt){var _29=(evt.currentTarget.className.indexOf("IncrementButton")!==-1)?true:false;this._extentUtil({numLevels:_29?1:-1});},_getSliderClass:function(_2a){var _2b="",_2c=(_2a?"Large":"Simple"),_2d=this._mapParams.sliderOrientation,_2e=this._mapParams.sliderPosition||"";_2d=(_2d&&_2d.toLowerCase()==="horizontal")?"esri"+_2c+"SliderHorizontal":"esri"+_2c+"SliderVertical";if(_2e){switch(_2e.toLowerCase()){case "top-left":_2e="esri"+_2c+"SliderTL";break;case "top-right":_2e="esri"+_2c+"SliderTR";break;case "bottom-left":_2e="esri"+_2c+"SliderBL";break;case "bottom-right":_2e="esri"+_2c+"SliderBR";break;default:break;}}return "esri"+_2c+"Slider"+" "+_2d+" "+_2e;},_createSlider:function(){if(this._mapParams.slider){var div=_9("div",{id:this.id+"_zoom_slider"},this.root),_2f=esri.config.defaults.map,_30=this._getSliderClass(true),_31=(_30.indexOf("Horizontal")!==-1),_32=(_30.indexOf("SliderTL")!==-1||_30.indexOf("SliderBL")!==-1),_33=(_30.indexOf("SliderTL")!==-1||_30.indexOf("SliderTR")!==-1),_34=_31?_1.form.HorizontalSlider:_1.form.VerticalSlider,_35=this.getNumLevels(),_36=_1.form,i,il,_37;if(_35>0){var _38,_39,_3a=this._mapParams.sliderLabels,_3b=!!_3a,_3c=(_3a!==false);if(_3c){var _3d=_31?_36.HorizontalRule:_36.VerticalRule,_3e=_31?_36.HorizontalRuleLabels:_36.VerticalRuleLabels,_3f,_40=_31?"bottomDecoration":"rightDecoration";if(!_3a){_3a=[];for(i=0,il=_35;i<il;i++){_3a[i]="";}}_3f=[{"class":"esriLargeSliderTicks",container:_40,count:_35,dijitClass:_3d},{"class":_3b&&"esriLargeSliderLabels",container:_40,count:_35,labels:_3a,dijitClass:_3e}];_2.forEach(_3f,function(_41){var _42=_9("div"),_43=_41.dijitClass;delete _41.dijitClass;div.appendChild(_42);if(_43===_3d){_38=new _43(_41,_42);}else{_39=new _43(_41,_42);}});}_37=(this._slider=new _34({id:div.id,"class":_30,minimum:this.getMinZoom(),maximum:this.getMaxZoom(),discreteValues:_35,value:this.getLevel(),clickSelect:true,intermediateChanges:true,style:"z-index:"+_5+";"},div));_37.startup();if(_3c){_38.startup();_39.startup();}this._slider_connect=dc(_37,"onChange",this,"_onSliderChangeHandler");this._connects.push(dc(this,"onExtentChange",this,"_onExtentChangeSliderHandler"));this._connects.push(dc(_37._movable,"onFirstMove",this,"_onSliderMoveStartHandler"));}else{_37=(this._slider=new _34({id:div.id,"class":_30,minimum:0,maximum:2,discreteValues:3,value:1,clickSelect:true,intermediateChanges:_2f.sliderChangeImmediate,style:"height:50px; z-index:"+_5+";"},div));var _44=_37.domNode.firstChild.childNodes;for(i=1;i<=3;i++){ds(_44[i],"visibility","hidden");}_37.startup();this._slider_connect=dc(_37,"onChange",this,"_onDynSliderChangeHandler");this._connects.push(dc(this,"onExtentChange",this,"_onExtentChangeDynSliderHandler"));}var _45=_37.incrementButton,_46=_37.decrementButton;_45.style.outline="none";_46.style.outline="none";_37.sliderHandle.style.outline="none";_37._onKeyPress=function(){};var _47=_37._movable;if(_47){var _48=_47.onMouseDown;_47.onMouseDown=function(e){if(_2.isIE<9&&e.button!==1){return;}_48.apply(this,arguments);};}this.isZoomSlider=true;}},_onSliderMoveStartHandler:function(){_8(this._slider_connect);_8(this._slidermovestop_connect);this._slider_connect=dc(this._slider,"onChange",this,"_onSliderChangeDragHandler");this._slidermovestop_connect=dc(this._slider._movable,"onMoveStop",this,"_onSliderMoveEndHandler");},_onSliderChangeDragHandler:function(_49){this._extentUtil({targetLevel:_49});},_onSliderMoveEndHandler:function(){_8(this._slider_connect);_8(this._slidermovestop_connect);},_onSliderChangeHandler:function(_4a){this.setLevel(_4a);},_updateSliderValue:function(_4b,_4c){_8(this._slider_connect);var _4d=this._slider;var _4e=_4d._onChangeActive;_4d._onChangeActive=false;_4d.set("value",_4b);_4d._onChangeActive=_4e;this._slider_connect=dc(_4d,"onChange",this,_4c);},_onExtentChangeSliderHandler:function(_4f,_50,_51,lod){_8(this._slidermovestop_connect);this._updateSliderValue(lod.level,"_onSliderChangeHandler");},_onDynSliderChangeHandler:function(_52){this._extentUtil({numLevels:_52>0?1:-1});},_onExtentChangeDynSliderHandler:function(){this._updateSliderValue(1,"_onDynSliderChangeHandler");},_openLogoLink:function(evt){window.open(esri.config.defaults.map.logoLink,"_blank");_2.stopEvent(evt);},enableMapNavigation:function(){this.navigationManager.enableNavigation();},disableMapNavigation:function(){this.navigationManager.disableNavigation();},enableDoubleClickZoom:function(){if(!this.isDoubleClickZoom){this.navigationManager.enableDoubleClickZoom();this.isDoubleClickZoom=true;}},disableDoubleClickZoom:function(){if(this.isDoubleClickZoom){this.navigationManager.disableDoubleClickZoom();this.isDoubleClickZoom=false;}},enableShiftDoubleClickZoom:function(){if(!this.isShiftDoubleClickZoom){_b(this.declaredClass+": "+esri.bundle.map.deprecateShiftDblClickZoom,null,"v2.0");this.navigationManager.enableShiftDoubleClickZoom();this.isShiftDoubleClickZoom=true;}},disableShiftDoubleClickZoom:function(){if(this.isShiftDoubleClickZoom){_b(this.declaredClass+": "+esri.bundle.map.deprecateShiftDblClickZoom,null,"v2.0");this.navigationManager.disableShiftDoubleClickZoom();this.isShiftDoubleClickZoom=false;}},enableClickRecenter:function(){if(!this.isClickRecenter){this.navigationManager.enableClickRecenter();this.isClickRecenter=true;}},disableClickRecenter:function(){if(this.isClickRecenter){this.navigationManager.disableClickRecenter();this.isClickRecenter=false;}},enablePan:function(){if(!this.isPan){this.navigationManager.enablePan();this.isPan=true;}},disablePan:function(){if(this.isPan){this.navigationManager.disablePan();this.isPan=false;}},enableRubberBandZoom:function(){if(!this.isRubberBandZoom){this.navigationManager.enableRubberBandZoom();this.isRubberBandZoom=true;}},disableRubberBandZoom:function(){if(this.isRubberBandZoom){this.navigationManager.disableRubberBandZoom();this.isRubberBandZoom=false;}},enableKeyboardNavigation:function(){if(!this.isKeyboardNavigation){this.navigationManager.enableKeyboardNavigation();this.isKeyboardNavigation=true;}},disableKeyboardNavigation:function(){if(this.isKeyboardNavigation){this.navigationManager.disableKeyboardNavigation();this.isKeyboardNavigation=false;}},enableScrollWheelZoom:function(){if(!this.isScrollWheelZoom){this.navigationManager.enableScrollWheelZoom();this.isScrollWheelZoom=true;}},disableScrollWheelZoom:function(){if(this.isScrollWheelZoom){this.navigationManager.disableScrollWheelZoom();this.isScrollWheelZoom=false;}},showPanArrows:function(){if(this._navDiv){esri.show(this._navDiv);this.isPanArrows=true;}},hidePanArrows:function(){if(this._navDiv){esri.hide(this._navDiv);this.isPanArrows=false;}},showZoomSlider:function(){if(this._slider){ds(this._slider.domNode||this._slider,"visibility","visible");this.isZoomSlider=true;}},hideZoomSlider:function(){if(this._slider){ds(this._slider.domNode||this._slider,"visibility","hidden");this.isZoomSlider=false;}}};}()));});