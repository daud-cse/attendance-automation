GMaps.prototype.checkGeofence = function(lat, lng, fence) {
  return fence.containsLatLng(new google.maps.LatLng(lat, lng));
};

GMaps.prototype.checkMarkerGeofence = function(marker, outside_callback) {
  if (marker.fences) {
    for (var i = 0, fence; fence = marker.fences[i]; i++) {
      var deepp = marker.getPosition();
      if (!this.checkGeofence(deepp.lat(), deepp.lng(), fence)) {
        outside_callback(marker, fence);
      }
    }
  }
};
