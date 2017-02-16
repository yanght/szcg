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
define("esri/undoManager",["dijit","dojo","dojox"],function(_1,_2,_3){_2.provide("esri.undoManager");_2.declare("esri.UndoManager",null,{maxOperations:10,canUndo:false,canRedo:false,position:0,length:0,onUndo:function(){},onRedo:function(){},onAdd:function(){},onChange:function(){},constructor:function(_4){_4=_4||{};if(_4.maxOperations){this.maxOperations=_4.maxOperations;}this._historyStack=[];},add:function(_5){if(this.maxOperations>0){while(this._historyStack.length>=this.maxOperations){this._historyStack.shift();}}this._historyStack.splice(this.position,0,_5);this.position++;this.clearRedo();this.onAdd();this._checkAvailability();},undo:function(){if(this.position===0){return null;}var _6=this.peekUndo();this.position--;if(_6){_6.performUndo();}this.onUndo();this._checkAvailability();},redo:function(){if(this.position===this._historyStack.length){return null;}var _7=this.peekRedo();this.position++;if(_7){_7.performRedo();}this.onRedo();this._checkAvailability();},_checkAvailability:function(){this.length=this._historyStack.length;if(this.length===0){this.canRedo=false;this.canUndo=false;}else{if(this.position===0){this.canRedo=true;this.canUndo=false;}else{if(this.position===this.length){this.canUndo=true;this.canRedo=false;}else{this.canUndo=true;this.canRedo=true;}}}this.onChange();},clearUndo:function(){this._historyStack.splice(0,this.position);this.position=0;this._checkAvailability();},clearRedo:function(){this._historyStack.splice(this.position,this._historyStack.length-this.position);this.position=this._historyStack.length;this._checkAvailability();},peekUndo:function(){if(this._historyStack.length>0&&this.position>0){return this.get(this.position-1);}},peekRedo:function(){if(this._historyStack.length>0&&this.position<this._historyStack.length){return this.get(this.position);}},get:function(_8){return this._historyStack[_8];},remove:function(_9){if(this._historyStack.length>0){this._historyStack.splice(_9,1);if(this.position>0){if(_9<this.position){this.position--;}}this._checkAvailability();}},destroy:function(){this._historyStack=null;}});_2.declare("esri.OperationBase",null,{type:"not implemented",label:"not implemented",constructor:function(_a){_a=_a||{};if(_a.label){this.label=_a.label;}},performUndo:function(){console.error("performUndo has not been implemented");},performRedo:function(){console.error("performRedo has not been implemented");}});});