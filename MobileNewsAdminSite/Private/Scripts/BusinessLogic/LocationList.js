var locationServiceUrl = "/api/Location";
var googleMapsApiKey = "AIzaSyCexZJ46m5GcNzhKqL_JLG_EizlHV_iQEk";
var map;
$(window).on("load", function () {
    console.log("Location List javascript window loaded");
});
$(function () {
    // Handler for .ready() called.
    console.log("Location List javascript document loaded");
    setUpBaseHtmlStructure();
    findAllLocations(displayCategoryMultiSelect);
    //findAllLocations(createKendoMultiSelect);
    //displayCategoryMultiSelect();
});


function setUpBaseHtmlStructure()
{
    var locationContainer = $("<div id='locationContainer' class='col-sm-12'>").appendTo($('#locationListMainContainer'));
                var firstRow = $("<div class='row is-table-row'>").appendTo(locationContainer);
                var locPane = $("<div class='well col-md-3'>").appendTo(firstRow);
                var locFilterContainer = $("<div id='locFilter'>").text("Place some filter objects in here").appendTo(locPane);
                var locResultContainer = $("<div id='locResultContainer'>").attr("class", "list-group locPane").appendTo(locPane);
        //put some filters in this main pane then have another pane below it that is able to scroll thru the results held by a list group
                var mapPane = $("<div class='col-md-9' id='locRightPane'>").appendTo(firstRow);
                //Map section
                var mapContainer = $("<div id='locationMapContainer' class='mapContainer'>").appendTo(mapPane);
                var locFormContainer = $("<div id='locFormContainer' class='locFormCont'>").appendTo(mapPane);
}

function setEditForm(event) {
    var uaid = localStorage.getItem("userAgencyId");

    $("#locationAddress").val(event.data.address);
    $("#locationNameInput").val(event.data.location_name);
    $("#locationcityInput").val(event.data.city);
    $("#locationstateInput").val(event.data.state);
    $("#locationzipInput").val(event.data.zip);
    $("#locationlatitudeInput").val(event.data.latitude);
    $("#locationlongitudeInput").val(event.data.longitude);
    $("#locationwebsiteInput").val(event.data.website);
    $("#locationphoneInput").val(event.data.phone);
    $("#locationEmailInput").val(event.data.email);
    $("#locationCommentInput").val(event.data.comment);
    //drop downs of references to other tables  I think kendo does the index instead of the value of the option

    var dropdownlist = $("#locationAgencySelect").data("kendoDropDownList");
    dropdownlist.select(parseInt(uaid));
    var dropdownlist1 = $("#locationLanguageIdInput").data("kendoDropDownList");
    dropdownlist1.select(event.data.language_id);

    //dates
    var datepicker = $("#locationInputlogical_delete_date").data("kendoDatePicker");
    datepicker.value(event.data.logical_delete_date);
    $("#locationInputmodified_date").data("kendoDatePicker").value(event.data.modified_date);
    $("#locationInputcreated_date").data("kendoDatePicker").value(event.data.created_date);


}
function findAllLocations(callBack) {
    console.log("start find all locations with callback : ");
    var jqxhr = $.ajax(
       {
           type: 'GET',
           url: locationWebApiUrl,
           //xhrFields:
           //{
           //    withCredentials: true
           //}
       })
        .done(function (data) {

            if (data != null) {
                
                var locFormContainer = $("#locFormContainer");
                locFormContainer.load("../../Private/HTML/LocationEditForm.html",
                        function (resp, status, xhr) {
                            if (status == 'success') {
                                console.log("loaded the location edit form section");
                            }
                            else {
                                console.log("Unable to load location form section");
                            }
                        });
                var mapContainer = $("#locationMapContainer");
                mapContainer.text("Map placeholder");
                updateListOfLocations(data);

                callBack();
            }
        })
	  .fail(function (jqXHR, textStatus) {
	      console.log("Ajax call fail: " + textStatus)
	      console.log(jqXHR);
	  });
}

function updateListOfLocations(data)
{
    console.log("update list of locations called");
    if (data != null)
    {
       //update what the edit form has or don't.  maybe blank it out if anything
        //or bind the selected item to the form
        var maxLength = data.length > 10 ? 10:data.length <= 10 ? data.length:data.length;
        for (x = 0; x < data.length; x++)
        {
            var locResultAnch = $("<a id='locResult" + x + "'>");
            if (x === 0)
            {
                locResultAnch.attr("class", "list-group-item active");
            }
            else
            {
                locResultAnch.attr("class", "list-group-item");
            }

            locResultAnch.appendTo($('#locResultContainer'));
            locResultAnch.click(data[x], setMap);
            locResultAnch.click(data[x], setEditForm);
            locResultAnch.click(function (e) {
                $that = $(this);
                $that.parent().find('a').removeClass('active');
                $that.addClass('active');
            });

            var locResultHeading = $("<h4 id='locResultHeading'" + x + ">");
            locResultHeading.attr("class", "list-group-item-heading");
            locResultHeading.text(data[x].location_name);
            locResultHeading.appendTo(locResultAnch);
            var locResultText = $("<p id='locResultText'" + x + ">").attr("class", "list-group-item-text");
            var htmlResTxt = "<span>";
            htmlResTxt += data[x].address;
            htmlResTxt += "</span></br><span id='addie";
            htmlResTxt += x;
            htmlResTxt += "'>";
            htmlResTxt += data[x].city;
            htmlResTxt += ",";
            htmlResTxt += data[x].state;
            htmlResTxt += " ";
            htmlResTxt += data[x].zip;
            htmlResTxt += "</span>"
            locResultText.html(htmlResTxt);
            locResultText.appendTo(locResultAnch);

        }

        //only load the initial page with the first item after that it will be a selection thing
        var lattitude = data[0].latitude;
        var longitude = data[0].longitude;


        // Create a map object and specify the DOM element for display.
        map = new google.maps.Map(document.getElementById('locationMapContainer'), {
            center: { lat: lattitude, lng: longitude },
            scrollwheel: false,
            zoom: 18
        });
        var uluru = { lat: lattitude, lng: longitude };

        var marker = new google.maps.Marker({
            position: uluru,
            map: map
        });


        //had to do this since I couldn't call the click function directory from here but that is what I would love to do.  It would instantly do all the work that comes with selecting the item

        if (!(document.getElementById('locationAddress') === null)) {
            var uaid = localStorage.getItem("userAgencyId");

            $("#locationAddress").val(data[0].address);
            $("#locationNameInput").val(data[0].location_name);
            $("#locationcityInput").val(data[0].city);
            $("#locationstateInput").val(data[0].state);
            $("#locationzipInput").val(data[0].zip);
            $("#locationlatitudeInput").val(data[0].latitude);
            $("#locationlongitudeInput").val(data[0].longitude);
            $("#locationwebsiteInput").val(data[0].website);
            $("#locationphoneInput").val(data[0].phone);
            $("#locationEmailInput").val(data[0].email);
            $("#locationCommentInput").val(data[0].comment);

            console.log("#locationAddress");
            console.log($("#locationAddress"));
            console.log(document.getElementById('locationAddress'));

            //drop downs of references to other tables  I think kendo does the index instead of the value of the option

            var dropdownlist = $("#locationAgencySelect").data("kendoDropDownList");
            console.log("trying to set the edit form with data after the list of locations");
            console.log(dropdownlist);
            dropdownlist.select(parseInt(uaid));
            var dropdownlist1 = $("#locationLanguageIdInput").data("kendoDropDownList");
            dropdownlist1.select(data[0].language_id);

            //dates
            var datepicker = $("#locationInputlogical_delete_date").data("kendoDatePicker");
            datepicker.value(data[0].logical_delete_date);
            $("#locationInputmodified_date").data("kendoDatePicker").value(data[0].modified_date);
            $("#locationInputcreated_date").data("kendoDatePicker").value(data[0].created_date);
        }
    }
}



function displayCategoryMultiSelect() {
    var cxhr = $.ajax(
      {
          type: 'GET',
          url: categoryForAgencyWebApiUrl,
          //xhrFields:
          //{
          //    withCredentials: true
          //}
      });
    cxhr.done(function (data) {

        //build options for the select area. in locFilter
        var locFilterMainContainer = $('#locFilter');
        locFilterMainContainer.empty();

        // $("<label for='categoryMS'>Category</label>").appendTo($('#locFilter'));
        var catMS = $("<select id='categoryMS' multiple='multiple' data-placeholder='Filter by category...'>").appendTo($('#locFilter'));
        for (x = 0; x < data.length; x++) {
            $("<option value='" + data[x].category_id + "'>").text(data[x].category_name).appendTo(catMS);
        }
        createKendoMultiSelect();
    });

    cxhr.fail(function (jqXHR, textStatus) {
        console.log("Ajax call fail for getting categories: " + textStatus)
        console.log(jqXHR);
    });
}

function createKendoMultiSelect() {
    // create MultiSelect from select HTML element
    var required = $("#categoryMS").kendoMultiSelect(
        {
            dataBound: onDataBound,
            filtering: onFiltering,
            deselect: onDeselect,
            select: onSelect,
            change: onChange,
            close: onClose,
            open: onOpen
        }).data("kendoMultiSelect");

};


function setMap(data) {
    var locMapCont = $('#locationMapContainer');
    locMapCont.empty();

    map = new google.maps.Map(locMapCont[0], {
        center: { lat: data.data.latitude, lng: data.data.longitude },
        scrollwheel: false,
        zoom: 18
    });
    var uluru = { lat: data.data.latitude, lng: data.data.longitude };

    var marker = new google.maps.Marker({
        position: uluru,
        map: map
    });
    google.maps.event.trigger(map, 'resize');
}


function onDataBound(e) {

}

function onOpen() {
}

function onClose() {

}
function createFilterLocationUrl(categories) {
    //admin/Location/FilterLocation?agencyId=1&categories=1&categories=2
    var returnString = location.protocol + "//" + location.hostname + ":53517/admin/Location/";
    if (jQuery.isEmptyObject(categories)) {
        returnString = locationWebApiUrl;
    }
    else {
        returnString += "FilterLocation?agencyId=";
        returnString += localStorage.getItem("userAgencyId");
        for (x = 0; x < categories.length; x++) {
            returnString += "&categories=";
            returnString += categories[x];
        }
    }
    return returnString;
}
function onChange(e) {


    var multiselect = $("#categoryMS").data("kendoMultiSelect");

    // get data items for the selected options.
    var dataItems = multiselect.dataItems();

    var categories = [];
    for (x = 0; x < dataItems.length; x++) {
        categories[x] = dataItems[x].value;
    }

    var locFilterUrl = createFilterLocationUrl(categories);

    var lfXHR = $.ajax(
        {
            type: 'GET',
            url: locFilterUrl,
            //xhrFields:
            //    {
            //        withCredentials: true
            //    }
        })
        .done(function (data) {
            //empty out the old results
            var locResultContainer = $("#locResultContainer");
            locResultContainer.empty();
            
            //update list
            updateListOfLocations(data);
        });
};

function onSelect(e) {

    //var dataItem = e.dataItem;
};

function onDeselect(e) {

   // var dataItem = e.dataItem;
};

function onFiltering(e) {

};

