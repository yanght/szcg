//>>built
define("xstyle/elemental",[],function(){var _1=document.createElement("div");var _2={"dom-qsa2.1":!!_1.querySelectorAll};function _3(_4){return _2[_4];};var _5=_1.matchesSelector||_1.webkitMatchesSelector||_1.mozMatchesSelector||_1.msMatchesSelector||_1.oMatchesSelector;var _6=[];var _7={},_8={};var _9=[];var _a;require(["dojo/domReady!"],function(){_a=true;if(_3("dom-qsa2.1")){for(var i=0,l=_6.length;i<l;i++){_b(_6[i]);}_c();}else{var _d=document.all;for(var i=0,l=_d.length;i<l;i++){_e(_d[i]);}}});function _b(_f){var _10=[];var _11=document.querySelectorAll(_f.selector);var _12=_f.name;for(var i=0,l=_11.length;i<l;i++){var _13=_11[i];var _14=_13.elementalStyle;var _15=_13.elementalSpecificities;if(!_14){_14=_13.elementalStyle={};_15=_13.elementalSpecificities={};}if(true||_15[_12]<=_f.specificity){var _16=_13.renderings;if(!_16){_16=_13.renderings=[];_9.push(_13);}_16.push({name:_12,rendered:_14[_12]==_f.propertyValue,renderer:_f});_14[_12]=_f.propertyValue;}}};function _c(){for(var i=0;i<_9.length;i++){var _17=_9[i];var _18=_17.renderings,_19=_17.elementalStyle;for(var j=0;j<_18.length;j++){var _1a=_18[j];var _1b=_1a.renderer;var _1c=_1b.rendered;isCurrent=_19[_1a.name]==_1b.propertyValue;if(!_1c&&isCurrent){_1b.render(_17);}if(_1c&&!isCurrent&&_1b.unrender){_1b.unrender(_17);_18.splice(j--,1);}}}_9=[];};function _1d(_1e,_1f){for(var i=0,l=_1f.length;i<l;i++){_1f[i](_1e);}};put=typeof put=="undefined"?{}:put;put.onaddclass=function(_20,_21){var _22=classTriggers[_21];var _23=_22[selector];for(var i=0,l=_22.length;i<l;i++){var _24=_22[i];if(_5.apply(_20,_24.selector)){_24.render(_20);(_20.renderers=_20.renderers||[]).push(_24);}}};put.onremoveclass=function(_25){var _26=_25.renderers;if(_26){for(var i=_26.length-1;i>=0;i--){var _27=_26[i];if(!_5.apply(_25,_27.selector)){_27.unrender(_25);_26.splice(i,1);}}}};put.oncreateelement=function(_28){tagTriggers[_28.tagName];};function _e(_29){for(var i=0,l=_6.length;i<l;i++){var _2a=_6[i];if(_5?_5.apply(_29,_2a.selector):_29.currentStyle[_2a.name]==_2a.propertyValue){_2a.render(_29);}}};return {addRenderer:function(_2b,_2c,_2d,_2e){var _2f={selector:_2d.selector,propertyValue:_2c,name:_2b,render:_2e};_6.push(_2f);if(_a){_b(_2f);}_c();},update:_e};});