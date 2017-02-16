var profile = (function(){
  var testResourceRe = /\/test\//,

    copyOnly = function(filename, mid){
      var list = {
        "put-selector/package.json":1,
        "put-selector/put-selector.profile":1,
        "put-selector/node-html":1
      };
      
      return (mid in list);
    };

  return {
    resourceTags:{
      test: function(filename, mid){
        return testResourceRe.test(mid);
      },

      copyOnly: function(filename, mid){
        return copyOnly(filename, mid);
      },

      amd: function(filename, mid){
        return !testResourceRe.test(mid) && !copyOnly(filename, mid) && /\.js$/.test(filename);
      }
    }
  };
})();
