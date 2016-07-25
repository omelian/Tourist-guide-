app.service('filterService', function() {
  var model = '';
  var pathId = 0;
  var addModel = function(newObj) {
      model = newObj;
  };

  var getModel = function(){
      return model;
  };
  var addPath = function (newObj) {
      pathId = newObj;
  }
  var getPath = function () {
      return pathId;
  };

  return {
    addModel: addModel,
    getModel: getModel,
    addPath: addPath,
    getPath: getPath
  };

});