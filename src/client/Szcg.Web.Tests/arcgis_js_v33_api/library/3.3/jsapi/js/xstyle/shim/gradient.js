//>>built
define("xstyle/shim/gradient",["./vendorize"],function(_1){var _2=/#(\w{6})/;var _3={"-webkit-":function(_4,_5,_6,to){return "background-image: -webkit-gradient("+_4.substring(0,6)+", left top, left bottom, from("+_6+"), to("+to+"))";},"-moz-":function(_7,_8,_9,to){return "background-image: -moz-"+_7+"("+_8+","+_9+","+to+")";},"-o-":function(_a,_b,_c,to){return "background-image: -o-"+_a+"("+_b+","+_c+","+to+")";},"-ms-":function(_d,_e,_f,to){_f=_f.match(_2);to=to.match(_2);if(_f&&to){return "border-radius: 0px; filter: progid:DXImageTransform.Microsoft.gradient(startColorstr=#FF"+_f[1]+",endColorstr=#FF"+to[1]+",gradientType="+(_e=="left"?1:0)+");";}}}[_1.prefix];return {onIdentifier:function(_10,_11,_12){var _13=_11.match(/(\w+-gradient)\(([^\)]*)\)/);var _14=_13[1];var _15=_13[2].split(/,\s*/);var _16=_15[0];var _17=_15[1];var end=_15[2];return _3(_14,_16,_17,end);}};});