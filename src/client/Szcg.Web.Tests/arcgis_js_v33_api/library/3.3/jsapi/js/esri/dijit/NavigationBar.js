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
define("esri/dijit/NavigationBar",["dijit","dojo","dojox","dojo/require!esri/dijit/_TouchBase"],function(_1,_2,_3){_2.provide("esri.dijit.NavigationBar");_2.require("esri.dijit._TouchBase");_2.declare("esri.dijit.NavigationBar",null,{_items:[],constructor:function(_4,_5){this.container=_2.byId(_5);this._touchBase=esri.dijit._TouchBase(this.container,null);this._slideDiv=_2.create("div",{},this.container,"first");this.events=[_2.connect(this._touchBase,"onclick",this,this._onClickHandler)];this._items=_4.items;_2.addClass(this.container,"esriMobileNavigationBar");var _6=_2.create("div",{},this._slideDiv);for(var i=0;i<this._items.length;i++){var _7,_8;switch(this._items[i].type){case "img":_8=_2.create("div",{"class":"esriMobileNavigationItem"},_6);_7=_2.create("img",{src:this._items[i].src.toString(),style:{width:"100%",height:"100%"}},_8);break;case "span":_8=_2.create("div",{"class":"esriMobileNavigationItem"},_6);_7=_2.create("span",{innerHTML:this._items[i].text},_8);break;case "div":_8=_2.create("div",{"class":"esriMobileNavigationInfoPanel"},_6);_7=_2.create("div",{innerHTML:this._items[i].text},_8);break;}_2.addClass(_8,this._items[i].position);if(this._items[i].className){_2.addClass(_7,this._items[i].className);}_7._index=i;_7._item=this._items[i];this._items[i]._node=_7;}},startup:function(){this.onCreate(this._items);},destroy:function(){_2.forEach(this.events,_2.disconnect);this._touchBase=null;_2.query("img",this.container).forEach(function(_9){_9._index=null;_9._item=null;_2.destroy(_9);_9=null;});this._items=null;_2.destroy(this._slideDiv);_2.destroy(this.container);this.container=this._slideDiv=null;},getItems:function(){return this._items;},select:function(_a){this._markSelected(_a._node,_a);},onSelect:function(_b){},onUnSelect:function(_c){},onCreate:function(_d){},_onClickHandler:function(e){if(e.target.tagName.toLowerCase()==="img"){var _e=e.target;var _f=_e._index;var _10=_e._item;_2.query("img",this.container).forEach(function(_11){if(_11!==_e&&_11._item.toggleGroup===_10.toggleGroup){this._markUnSelected(_11,_11._item);}},this);this._toggleNode(_e,_10);}},_toggleNode:function(_12,_13){if(_13.toggleState==="ON"){_13.toggleState="OFF";if(_13.src){_12.src=_13.src.toString();}this.onUnSelect(_13);}else{_13.toggleState="ON";if(_13.srcAlt){_12.src=_13.srcAlt;}this.onSelect(_13);}},_markSelected:function(_14,_15){_15.toggleState="ON";if(_15.srcAlt){_14.src=_15.srcAlt;}this.onSelect(_15);},_markUnSelected:function(_16,_17){if(_17.toggleState==="ON"){_17.toggleState="OFF";if(_17.src){_16.src=_17.src.toString();}this.onUnSelect(_17);}}});});