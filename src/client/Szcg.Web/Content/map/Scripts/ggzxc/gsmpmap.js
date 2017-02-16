function getattr(result) {
    var attr = { "ID": result.ID, "folder": result.folder, "photo": result.photo, "zxcs": result.zxcs, "zds": result.zds, "adder": result.adder, "POINT_X": result.POINT_X, "POINT_Y": result.POINT_Y, "mappic": result.mappic, "mappich": result.mappich };
 return attr;
}
function getpoint(result) {
    for (var i = 0; i < result.length; i++) {
        showMark(result[i]);
    }
}
function mapAddlayer() {
    displaypoint();
}   

