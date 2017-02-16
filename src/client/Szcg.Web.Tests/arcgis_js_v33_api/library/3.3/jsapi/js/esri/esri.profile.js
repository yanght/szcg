var profile = (function(){
  var testResourceRe = /^esri\/tests\//,

    copyOnly = function(filename, mid){
      var list = {
        "esri/package.json":1,
        "esri/esri.profile.js":1,
        "esri/esri.js":1
      };
      
      return (mid in list);
    },
    
    // When a LEGACY module is converted to AMD, add it
    // here so the build system knows about it.
    // https://dojotoolkit.org/documentation/tutorials/1.8/build/
    esriAmdModules = {
      "arcgis/Portal": 1,
      "dijit/Geocoder": 1,
      "dijit/Gauge": 1,
      "dijit/Scalebar": 1,
      "main": 1,
      "MapNavigationManager": 1,
      "MouseEvents": 1,
      "TouchEvents": 1
    };

  return {
    resourceTags:{
      test: function(filename, mid){
        return testResourceRe.test(mid) || (mid.search(/\.17$/) !== -1);
      },

      copyOnly: function(filename, mid){
        return copyOnly(filename, mid);
      },

      amd: function(filename, mid){
        mid = mid.replace(/^esri\//i, "");
        return (mid in esriAmdModules);
      }
    }
  };
})();
