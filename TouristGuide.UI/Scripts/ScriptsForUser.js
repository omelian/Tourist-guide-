function roleDropdownChanged(elem) {
        var loading = document.getElementById("admininfo");
        var IdRole = $(elem).val();
        if (IdRole == 0) {
            loading.style.visibility = "hidden";
            document.getElementById("network").value = "_";
        }
        else {
            loading.style.visibility = "visible";
            document.getElementById("network").value = "";
        }
 }



 function rolescript(role)
 {
     var loading = document.getElementById("admininfo");
     if (IdRole == "User") {
         loading.style.visibility = "hidden";
         document.getElementById("network").value = "_";
     }
     else {
         loading.style.visibility = "visible";
         document.getElementById("network").value = "";
     }
 }