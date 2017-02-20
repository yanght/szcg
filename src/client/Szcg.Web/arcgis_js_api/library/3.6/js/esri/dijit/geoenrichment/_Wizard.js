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
define("esri/dijit/geoenrichment/_Wizard",["../../declare","esri/dijit/_EventedWidget","dojo/dom-construct"],function(_1,_2,_3){var _4=_1("esri.dijit.geoenrichment._Wizard",[_2],{_currentPage:null,pages:null,stacking:"stretch",constructor:function(){this.pages={};},buildRendering:function(){this.domNode=_3.create("div",{"class":"_Wizard_Root"});},loadPage:function(_5){var _6=this.pages[_5];if(this._currentPage){this.domNode.removeChild(this._currentPage.domNode);}this._currentPage=_6;this.domNode.appendChild(this._currentPage.domNode);if(!_6._started){_6.set("stacking",this.stacking);_6.startup();}_6.load();},destroy:function(){for(var id in this.pages){this.pages[id].destroyRecursive();}this.pages={};this.inherited(arguments);}});return _4;});