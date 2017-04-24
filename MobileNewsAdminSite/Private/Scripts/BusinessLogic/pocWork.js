$(document).ready(function() 
{
	var serviceUrl = "http://localhost:8080/ofa/api/services?aid=1";
	var devServiceUrl = "http://nbe023dw5app.svc.ny.gov/mobilenews/api/services?aid=1";
	console.log("referer");
	console.log(document.referer);
	console.log("user agent");
	console.log(navigator.userAgent);
	console.log("Cookie string");
	console.log(document.cookie);





	var req = new XMLHttpRequest();
	req.open('GET', document.location, false);
	req.send(null);
	var headers = req.getAllResponseHeaders().toLowerCase();
	console.log("xml http request loop");
	console.log(headers);


	var jqxhr = $.ajax(
	{
		type: 'GET',
		 url: serviceUrl ,
		 xhrFields: 
		 {
	      		withCredentials: true
	   	}
	})
	  .done(function(data) 
	  {
	  	console.log("Data from ofa service call");
	   console.log(data);

	   $('#serviceId').attr("value", data[0].service_id);
	   $('#serviceTitle').attr("value", data[0].title);
	   $('#serviceContent').text("Service Content: " +  data[0].service_content);
	  })
	  .fail(function(jqXHR, textStatus) 
	  {
	    console.log("Ajax call fail: " + textStatus)
	    console.log(jqXHR);
	  })
	  ;
	 
	// Perform other work here ...
	 
	// Set another completion function for the request above
	jqxhr.always(function() {
		console.log("Always do this function after ajax call");
	});            


});

