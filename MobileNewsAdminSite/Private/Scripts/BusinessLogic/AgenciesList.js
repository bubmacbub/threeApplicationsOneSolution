var devServiceUrl = "http://nbe023dw5app.svc.ny.gov/mobilenews/api/services?aid=1";
var serviceUrl = "http://localhost:8080/ofa/api/agencies";

$(function () {
    console.log("Agency list javascript file loaded");
    //findAllAgencies();
    createAgencyDropDown();
});

function createAgencyDropDown()
{
    console.log("create agency drop down function called");

    //agencyDropDownDataSource.fetch(function () {
    //    console.log("Got data from agency serivce");
    //    console.log(agencyDropDownDataSource.data());
    //});
    //console.log("agencyDropDownDataSource");
    //console.log(agencyDropDownDataSource);

    $("#agencySelect").kendoDropDownList({
        dataTextField: "agency_name",
        dataValueField: "agency_id",
        optionLabel: "Select an Agency",
        dataSource: agencyDropDownDataSource,
        change: onAgencyDropDownChange
    });
}

function findAllAgencies()
{
	console.log("findAllAgencies called");
	var jqxhr = $.ajax(
	{
		type: 'GET',
		url: agencyWebApiUrl,
		 xhrFields: 
		 {
	      		withCredentials: true
	   	}
	})
	  .done(function(data) 
	  {
	   if(data != null)
	   	{
	   		//$('#dropDownOfAgencies').selectmenu({});
	   		for(x=0; x<data.length;x++)
	   		{
	   			console.log(data[x]);
	   			$("<option value='"+data[x].agency_id+"'>"+data[x].agency_name+"</option>").appendTo($('#agencySelect'));
	   			//*$("<li id='"+data[x].agency_id+"'>"+data[x].agency_name+"</li>").appendTo($('#agencyListUL'));*/
	   		}$('#agencySelect').selectmenu();
	   	}

	   $('#dropDownOfAgencies').dropdown();
	  })
	  .fail(function(jqXHR, textStatus) 
	  {
	    console.log("Ajax call fail: " + textStatus)
	    console.log(jqXHR);
	  });
}

function loadCreateAgency()
{
    $('#AgenciesContainer').load("/Private/HTML/AgenciesCreate.html",
        function (resp, status, xhr) {
            if (status == 'success') {
                $('#AgenciesContainer').show();
                
            }
            else {
                console.log("Unable to load welcome section");
            }

        });
}