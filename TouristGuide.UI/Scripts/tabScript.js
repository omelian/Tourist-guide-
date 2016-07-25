function tab_on_load()
{
    var taburl = window.location.href;
    var pos = taburl.indexOf("main");
    if (pos !== -1) {
        change('main')
    }
    else {
        pos = taburl.indexOf("restaurant");
        if (pos !== -1) {
            change('retaurantTab')
        }
        else {
            pos = taburl.indexOf("sightseeing");
            if (pos !== -1) {
                change('sightseeingTab')
            }
            else {
                pos = taburl.indexOf("leisure");
                if (pos !== -1) {
                    change('activerestTab')
                }
            }
        }
    }
}       
        
function tab_change(item) {
    switch (item) {
        case '1': change('menu')
            break
        case '2': change('retaurantTab')
            break
        case '3': change('sightseeingTab')
            break
        case '4': change('activerestTab')
            break               
    }
}           

function change(id)
{
    if (id === 'menu')
    {
        document.getElementById('retaurantTab').classList.remove('active');
        document.getElementById('sightseeingTab').classList.remove('active');
        document.getElementById('activerestTab').classList.remove('active');              
    }
    else
    {
        if (document.getElementById('retaurantTab').classList === null || document.getElementById('sightseeingTab').classList === null || document.getElementById('activerestTab').classList === null)
        {
        document.getElementById('retaurantTab').classList.remove('active');
        document.getElementById('sightseeingTab').classList.remove('active');
        document.getElementById('activerestTab').classList.remove('active');
        }
        else
        {
            document.getElementById('retaurantTab').classList.remove('active');
            document.getElementById('sightseeingTab').classList.remove('active');
            document.getElementById('activerestTab').classList.remove('active');
            if (document.getElementById(id).classList != null)
        {
            document.getElementById(id).classList.add('active');
        }        
    }
}
}
