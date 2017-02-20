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
define("esri/dijit/geoenrichment/ReportData",["../../declare","dojo/number","./lang"],function(_1,_2,_3){var _4=_1("esri.dijit.geoenrichment.ReportData",null,{title:null,_cols:null,_rows:null,_indexes:null,constructor:function(){this._cols=[];this._rows=[];},addCol:function(_5){this._indexes=null;this._cols.push(_5);},getColCount:function(){return this._cols.length;},getCol:function(_6){return this._cols[_6];},addRow:function(_7){this._rows.push(_7);},getRow:function(_8){return this._rows[_8];},getRowCount:function(){return this._rows.length;},findField:function(_9){if(!this._indexes){this._indexes={};var _a=this._cols.length;for(var i=0;i<_a;i++){this._indexes[this._cols[i].name]=i;}}return this._indexes[_9];},getValue:function(_b,_c){if(_3.isNumber(_c)){return this._rows[_b][_c];}else{return this._rows[_b][this.findField(_c)];}},formatValue:function(_d,_e){var _f=_3.isNumber(_e)?_e:this.findField(_e);return this.getCol(_f).format(this._rows[_d][_f]);},clearRows:function(_10){if(!_3.isNumber(_10)){this._rows=[];}else{this._rows.splice(_10,this._rows.length-_10);}},clearCols:function(){this.clearRows();this._cols=[];this._indexes=null;}});var _11=_1(null,{name:null,alias:null,fullName:null,constructor:function(_12){this.name=_12.name;this.alias=_12.alias||_12.name;this.fullName=_12.fullName||null;}});var _13=_1([_11],{decimals:0,constructor:function(_14){this.decimals=_14.decimals||0;},format:function(_15){return _2.format(_15,{places:this.decimals});}});_4.NumericColumn=_13;var _16=_1([_13],{format:function(_17){return _2.format(_17/100,{places:this.decimals,type:"percent"});}});_4.PercentColumn=_16;var _18=_1([_13],{constructor:function(_19){this.symbol=_19.symbol||"$";},format:function(_1a){return _2.format(_1a,{places:this.decimals,type:"currency",symbol:this.symbol});}});_4.CurrencyColumn=_18;var _1b=_1([_11],{format:function(_1c){return _1c;}});_4.StringColumn=_1b;return _4;});