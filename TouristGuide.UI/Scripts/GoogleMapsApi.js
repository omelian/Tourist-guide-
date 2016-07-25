// In the following example, markers appear when the user clicks on the map.
// The markers are stored in an array.
// The user can then click an option to hide, show or delete the markers.
var map;
var markers = [];
var Kiev = { lat: 50.450199, lng: 30.527283 };

function initMap(location) {
    map = new google.maps.Map(document.getElementById('map'), {
        zoom: 17,
        center: location,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    });

    // This event listener will call addMarker() when the map is clicked.
    map.addListener('click', function (event) {
        addMarkerById(event.latLng);
    });
}
function initEmptyMap() {
    map = new google.maps.Map(document.getElementById('map'), {
        zoom: 17,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    });
    setCurrentPosition();
    // This event listener will call addMarker() when the map is clicked.
    map.addListener('click', function (event) {
        clearMarkers();
        addMarker(event.latLng);
    });    
}

function codeAddress() {
    clearMarkers();
    var geocoder = new google.maps.Geocoder();
    var address = document.getElementById("searchInput").value;
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            addMarker(results[0].geometry.location);
        } else {
            alert("Geocode was not successful for the following reason: " + status);
        }
    });
}

function addMarkerById(location) {
    var marker = new google.maps.Marker({
        position: location,
        map: map
    });
    markers.push(marker);
}

// Adds a marker to the map and push to the array.
function addMarker(location) {
    var marker = new google.maps.Marker({
        position: location,             
        map: map
    });
    var componentForm = {
        street_number: 'short_name',
        route: 'long_name',
        locality: 'long_name',
        country: 'long_name'
    };

    document.getElementById("restLong").value = marker.getPosition().lat();
    document.getElementById("restLan").value = marker.getPosition().lng();
    markers.push(marker);
    var geocoder = geocoder = new google.maps.Geocoder();
    geocoder.geocode({ 'latLng': marker.getPosition() }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            if (results[0]) {                
                for (var i = 0; i < results[0].address_components.length; i++) {
                    var addressType = results[0].address_components[i].types[0];
                    if (componentForm[addressType]) {
                        var val = results[0].address_components[i][componentForm[addressType]];
                        document.getElementById(addressType).value = val;
                    }
                }                
            }
        }
    });
    var bounds = new google.maps.LatLngBounds();
    for (var i = 0; i < markers.length; i++) {
        bounds.extend(markers[i].getPosition());
    }
    map.fitBounds(bounds);
}



// Sets the map on all markers in the array.
function setMapOnAll(map) {
    for (var i = 0; i < markers.length; i++) {
        markers[i].setMap(map);
    }
}

// Removes the markers from the map, but keeps them in the array.
function clearMarkers() {
    setMapOnAll(null);
}

// Shows any markers currently in the array.
function showMarkers() {
    setMapOnAll(map);
}

// Deletes all markers in the array by removing references to them.
function deleteMarkers() {
    clearMarkers();
    markers = [];
    removeSelect();
}

function setCurrentPosition() {
    if (navigator.geolocation) {// navigator.geolocation	Returns a Geolocation object that can be used to locate the user's position
        navigator.geolocation.getCurrentPosition(function (position) {
            initialLocation = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
            map.setCenter(initialLocation);

            var marker = new google.maps.Marker({
                position: initialLocation,
                animation: google.maps.Animation.BOUNCE
            });
            marker.setMap(map);
        })
    }
}

function addCircle(location) {
    var circle = new google.maps.Circle({
        center: location,
        radius: 2000,
        strokeColor: "#0000FF",
        strokeOpacity: 0.8,
        strokeWeight: 2,
        fillColor: "#0000FF",
        fillOpacity: 0.4
    });
    circle.setMap(map);
}

function addAllCircles() {
    for (var i = 0; i < markers.length; i++) {
        addCircle(markers[i].getPosition());
    }
}

//makes direct path to selected marker
function addPolyline() {
    //if (navigator.geolocation) {// navigator.geolocation	Returns a Geolocation object that can be used to locate the user's position
    //    navigator.geolocation.getCurrentPosition(function (position) {
    //        initialLocation = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
    //        SaveRequest(initialLocation);
    //    });
    //}
    //alert(currL);
    var select = document.getElementById('SelectMarker');
    //alert();
    var myTrip = [Kiev, markers[select.selectedIndex].getPosition()];//markers[select.selectedIndex].getPosition()];
    var flightPath = new google.maps.Polyline({
        path: myTrip,
        strokeColor: "#0000FF",
        strokeOpacity: 0.8,
        strokeWeight: 2
    });
    flightPath.setMap(map);
    var meters = google.maps.geometry.spherical.computeLength(flightPath.getPath());

    document.getElementById('xx').innerHTML = "Length = " + flightPath.inKm();




}

google.maps.LatLng.prototype.kmTo = function (a) {
    var e = Math, ra = e.PI / 180;
    var b = this.lat() * ra, c = a.lat() * ra, d = b - c;
    var g = this.lng() * ra - a.lng() * ra;
    var f = 2 * e.asin(e.sqrt(e.pow(e.sin(d / 2), 2) + e.cos(b) * e.cos
    (c) * e.pow(e.sin(g / 2), 2)));
    return f * 6378.137;
}

google.maps.Polyline.prototype.inKm = function (n) {
    var a = this.getPath(n), len = a.getLength(), dist = 0;
    for (var i = 0; i < len - 1; i++) {
        dist += a.getAt(i).kmTo(a.getAt(i + 1));
    }
    return dist;
}
//This function fill select box when u are clicking on map
function fillSelect() {
    select = document.getElementById('SelectMarker');
    var opt = document.createElement('option');
    opt.value = markers.length
    opt.innerHTML = markers.length;
    select.appendChild(opt);
}

function removeSelect() {
    select = document.getElementById('SelectMarker');
    var i;
    for (i = select.options.length - 1; i >= 0; i--) {
        select.remove(i);
    }
}

//google.maps.event.addDomListener(window, 'load', initMap);//Add a DOM listener that will execute the initialize() function on window load (when the page is loaded):
//sipmlier could be used just|"initMap();"
