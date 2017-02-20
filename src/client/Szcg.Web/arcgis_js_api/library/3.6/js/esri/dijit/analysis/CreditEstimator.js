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
require({cache:{"url:esri/dijit/analysis/templates/CreditEstimator.html":"<div class=\"esriAnalysis esriSimpleForm\">\r\n  <table class=\"esriFormTable\"  data-dojo-attach-point=\"_table\"  style=\"border-collapse:collapse;border-spacing:5px;\">\r\n     <!--<tr>\r\n      <td>\r\n        <label class=\"esriFloatLeading\">${i18n.analysisLayersLabel}</label>\r\n      </td>\r\n      <td>\r\n      </td>\r\n    </tr>-->\r\n     <tr>\r\n      <td>\r\n        <label class=\"esriFloatLeading\">${i18n.totalRecordsLabel}</label>\r\n      </td>\r\n      <td data-dojo-attach-point=\"_totalRecordsNode\">\r\n      </td>\r\n    </tr>\r\n     <!--<tr>\r\n      <td>\r\n        <label class=\"esriFloatLeading\">${i18n.creditsAvailLabel}</label>\r\n      </td>\r\n      <td>\r\n      </td>\r\n    </tr>-->\r\n     <tr>\r\n      <td>\r\n        <label class=\"esriFloatLeading\">${i18n.creditsReqLabel}</label>\r\n      </td>\r\n      <td data-dojo-attach-point=\"_creditsReqNode\">\r\n      </td>\r\n    </tr>\r\n  </table>\r\n  <div data-dojo-attach-point=\"_messageDiv\"></div>\r\n</div>\r\n"}});define("esri/dijit/analysis/CreditEstimator",["require","dojo/_base/declare","dojo/_base/lang","dojo/_base/connect","dojo/_base/event","dojo/_base/kernel","dojo/has","dojo/dom-construct","dojo/dom-class","dojo/dom-attr","dojo/dom-style","dojo/string","dijit/_WidgetBase","dijit/_TemplatedMixin","dijit/_OnDijitClickMixin","dijit/_FocusMixin","esri/kernel","esri/lang","dojo/i18n!esri/nls/jsapi","dojo/text!esri/dijit/analysis/templates/CreditEstimator.html"],function(_1,_2,_3,_4,_5,_6,_7,_8,_9,_a,_b,_c,_d,_e,_f,_10,_11,_12,_13,_14){var _15=_2([_d,_e,_f,_10],{declaredClass:"esri.dijit.analysis.CreditEstimator",i18n:null,basePath:_1.toUrl("esri/dijit/analysis"),templateString:_14,postMixInProperties:function(){this.inherited(arguments);this.i18n={};_3.mixin(this.i18n,_13.common);_3.mixin(this.i18n,_13.analysisMsgCodes);_3.mixin(this.i18n,_13.creditEstimator);},postCreate:function(){this.inherited(arguments);},_setContentAttr:function(obj){var str="";if(obj.code&&!obj.messageCode){obj.messageCode=obj.code;}if(obj.messageCode){str=_12.isDefined(_13[obj.messageCode])?_13[obj.messageCode]:obj.message;str=_12.isDefined(obj.params)?_c.substitute(str,obj.params):str;_a.set(this._messageDiv,"display","block");_a.set(this._messageDiv,"innerHTML",str);_b.set(this._table,"display","none");}else{_b.set(this._table,"display","table");_a.set(this._messageDiv,"display","none");_a.set(this._messageDiv,"innerHTML","");_a.set(this._totalRecordsNode,"innerHTML",obj.totalRecords);_a.set(this._creditsReqNode,"innerHTML",obj.cost);}}});if(_7("extend-esri")){_3.setObject("dijit.analysis.CreditEstimator",_15,_11);}return _15;});