$(function () {
    console.log("Service List JS loaded");

    console.log("Going to call read on agency datasource");
    //agencyDropDownDataSource.read();
    agencyDropDownDataSource.fetch(function () {
        console.log("Got data from agency serivce");
        console.log(this.data);
    });
    console.log(agencyDropDownDataSource);

    $("#svcagency_id").kendoDropDownList({
        dataTextField: "agency_name",
        dataValueField: "agency_id",
        optionLabel: "Select an Agency",
        dataSource: agencyDropDownDataSource,
        change: onAgencyDropDownChange
    });

    $("#svclanguage_id").kendoDropDownList({
        dataTextField: "language_name",
        dataValueField: "language_id",
        optionLabel: "Select a Language",
        dataSource: langaugeDropDownDataSource
    });

});


function findAllServices() {
   
    var jqxhr = $.ajax(
       {
           type: 'GET',
           url: serviceWebApiUrl,
           //xhrFields:
           //{
           //    withCredentials: true
           //}
       })
        .done(function (data) {
            //console.log("Data from ofa service call");
            //console.log(data);
            if (data != null) {
                var itemCount = 0;
                for (x = 0; x < data.length; x++) {
                    //console.log(data[x]);
                    if (x % 3 === 0) {
                        $("<div class='row'><div class='list-group'>")
                    }

                    var listGroupItemAnchor = $("<a href='#' class='list-group-item' id='svcLGI" + x + "'>");
                    listGroupItemAnchor.appendTo($('#serviceListMainContainer'));
                    listGroupItemAnchor.click(data[x], setServiceEditForm);
                    $("<h4 class='list-group-item-heading' id='serviceItemHeading" + data[x].service_id + "'>" + data[x].title +
                         "</h4><p class='list-group-item-text'>" + data[x].service_content + "</p>").appendTo(listGroupItemAnchor);

                    if (x % 3 === 0) {
                        $("</div></div>")
                    }

                }
            }
        })
	  .fail(function (jqXHR, textStatus) {
	      console.log("Ajax call fail: " + textStatus)
	      console.log(jqXHR);
	  });
}



function setServiceEditForm(event) {
    if (!jQuery.isEmptyObject(event.data)) {
        var data = event.data;
        console.log(" service data");
        console.log(data);
        //not working for some reason.  won't close on escape key
        //$('#serviceEditFormModal').modal({ keyboard: true });
        $('#serviceEditFormModal').modal('show');
        
        //assign the selected items data to the form
        $("#svctitle").val(data.title);
        $("#svcservice_content").val(data.service_content);
        $("#svccreated_date").val(data.created_date);
        $("#svcmodified_date").val(data.modified_date);
        $("#svclogical_delete_date").val(data.logical_delete_date);
        var agencyDDL = $("#svcagency_id").data("kendoDropDownList");
        agencyDDL.select(data.agency_id);
        $("#svclanguage_id").data("kendoDropDownList").select(data.language_id);

        //look up categories that this service is associated with.
        //ex: /admin/CategoryService/CategoriesForService?agencyId=1&serviceId=1
        //<span class="label label-default">Default</span>
        var uaid = localStorage.getItem("userAgencyId");
        var catForSrvUrl = location.protocol + "//" + location.hostname + ":53517/admin/CategoryService/CategoriesForService?agencyId=";
        catForSrvUrl += uaid;
        catForSrvUrl += "&serviceId=";
        catForSrvUrl += data.service_id;
        console.log("catForSrvUrl");
        console.log(catForSrvUrl);
        var catXHR = $.ajax(
       {
           type: 'GET',
           url: catForSrvUrl,
           xhrFields:
           {
               withCredentials: true
           }
       })
        .done(function (data) {
            console.log("got data from cat for srv api call");
            console.log(data);
            if (data != null) {
                var parentCont = $("#srvCatLabelContainer");
                parentCont.empty();
                for(x=0;x<data.length;x++)
                {
                    
                    var catLabel = $("<span class='label label-default'>");
                    catLabel.text(data[x].category_name);
                    catLabel.appendTo(parentCont);
                }
            }
        });
                
    }
    else {
        console.log("ERROR: Requested to set Service Edit Form with data but supplied none");
    }
}

