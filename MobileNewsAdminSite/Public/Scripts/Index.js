var userAgency = 1;
var agencyWebApiUrl = "http://" + location.hostname + ":53517/api/Agency";
var agencyDropDownDataSource;
var langaugeWebApiUrl = location.protocol + "//" + location.hostname + ":53517/api/language";
var locationWebApiUrl = location.protocol + "//" + location.hostname + ":53517/api/Location";
var serviceWebApiUrl = location.protocol + "//" + location.hostname + ":53517/api/Service";
var categoryForAgencyWebApiUrl = location.protocol + "//" + location.hostname + ":53517/admin/Category/Agency?agencyId=1";
var newsWebApiUrl = location.protocol + "//" + location.hostname + ":53517/api/MobileNews";
var adminNewsBaseWebApiUrl = location.protocol + "//" + location.hostname + ":53517/admin/MobileNews";
var categoryBaseWebApiUrl = location.protocol + "//" + location.hostname + ":53517/admin/Category";
var langaugeDropDownDataSource;
if (typeof (Storage) !== "undefined")
{
    
    localStorage.setItem("userId", "mjordan");
    localStorage.setItem("firstName", "Monty");
    localStorage.setItem("lastName", "Jordan");
    localStorage.setItem("userAgencyId", "1");
} else
{
    console.log("No way to store data locally")
}


$(function ()
{
    //hide nav bar after being clicked
    $('.navbar-collapse a').click(function () {
        $(".navbar-collapse").collapse('hide');
    });
    //change the background to show the active selected nav
    $('ul.nav > li').click(function (e) {
        e.preventDefault();
        $('ul.nav > li').removeClass('active');
        $(this).addClass('active');
    });

    //hide all the other nav containers and load the welcome page and show it on click
    $('#HomeMenu').click(function() 
    {
        console.log("Loaded home: " + homeIndexLoaded);
        $('#AgenciesContainer').hide();
        $('#ApplicationContainer').hide();
        $('#CategoryContainer').hide();
        $('#LocationContainer').hide();
        $('#NewsContainer').hide();
        $('#ServiceContainer').hide();
        $('#HomeContainer').load("../HTML/Welcome.html",
            function(resp, status, xhr)
            {
                if(status == 'success')
                {
                    $('#HomeContainer').show();
                    homeIndexLoaded = true;
                }
                else
                {
                    console.log("Unable to load welcome section");
                }
          
            });

    });

    //default page load it
    $('#HomeContainer').load("../HTML/Welcome.html",
      function(resp, status, xhr)
      {
          if(status == 'success')
          {
              $('#HomeContainer').show();
              homeIndexLoaded = true;
          }
          else
          {
              console.log("Unable to load welcome section");
          }
      });

    
    var url = "../../Public/Scripts/KendoUI/kendo.core.min.js";
    console.log("Going to get " + url);
    $.getScript(url, function () {
        console.log("loaded kendo core");
        console.log("Going to get kendo data");
        $.getScript("../../Public/Scripts/KendoUI/kendo.data.min.js", function () {
            console.log("loaded kendo data");
            createCommonDatasources();
            console.log("Going to get kendo popup");
            $.getScript("../../Public/Scripts/KendoUI/kendo.popup.min.js", function () {
                console.log("loaded kendo pop up");
                console.log("Going to get kendo calendar");
                $.getScript("../../Public/Scripts/KendoUI/kendo.calendar.min.js", function () {
                    console.log("loaded kendo calendar js");
                    console.log("Going to get kendo datepicker");
                    $.getScript("../../Public/Scripts/KendoUI/kendo.datepicker.min.js", function () {
                        //date picker dependency
                        //jquery.js
                        //kendo.core.js
                        //kendo.calendar.js
                        //kendo.popup.js
                        //kendo.datepicker.js
                        console.log("loaded kendo datepicker js");
                    });
                });
                console.log("Going to get kendo list");
                $.getScript("../../Public/Scripts/KendoUI/kendo.list.min.js", function () {
                    console.log("loaded kendo list");
                    console.log("Going to get multiselect");
                    $.getScript("../../Public/Scripts/KendoUI/kendo.multiselect.min.js", function () {
                        //these are the scripts necessary for use of multiselect
                        //http://docs.telerik.com/kendo-ui/intro/supporting/scripts-editors
                        //jquery.js
                        //kendo.core.js
                        //kendo.data.js
                        //kendo.popup.js
                        //kendo.list.js
                        console.log("loaded kendo multiselect"); 
                    });
                    console.log("Going to get kendo drop down list");
                    $.getScript("../../Public/Scripts/KendoUI/kendo.dropdownlist.min.js", function () {
                        console.log("loaded kendo drop down List js");
                    });
                });
            });
        });
          
          
        $("<link/>", {
            rel: "stylesheet",
            type: "text/css",
            href: "../../Public/Styles/kendo.common.min.css"
        }).appendTo("head");

        $("<link/>", {
            rel: "stylesheet",
            type: "text/css",
            href: "../../Public/Styles/kendo.default.min.css"
        }).appendTo("head");
    });// end of loading kendo UI stuff  JS and CSS
  
    //make the datasources for the common used things like the drop down of agency, language
    function createCommonDatasources() {
        console.log("Going to create agency datasource to " + agencyWebApiUrl);
        agencyDropDownDataSource = new kendo.data.DataSource({
            transport: {
                read: {
                    url: agencyWebApiUrl,
                    dataType: "json"
                }
            },
            change: function (e) {
                console.log("change in agency datasource");
                var data = this.data();
                console.log(data);
                console.log(e);
            },
            success: function (data) {
                console.log("got data back from agency service call for drop down");
                console.log(data);
                //$("#svcagency_id").kendoDropDownList({
                //    dataSource: {
                //        data: agencyDropDownDataSource
                //    },
                //    animation: false
                //});
            },
            error: function (xhr, error) {
                console.log("Error with kendo datasource for agency");
                console.debug(xhr);
                console.debug(error);

            }
            ,
            requestStart: function () {
                //kendo.ui.progress($("#products"), true);
                console.log("request start of kendo datasource for agency");
                console.log(event);
            },
            requestEnd: function () {
                //kendo.ui.progress($("#products"), false);
                console.log("request end of kendo datasource for agency");
                console.log(event);
            }
            //,
            //change: function() {
            //    $("#products").html(kendo.render(template, this.view()));
            //}
        });
        //end agency DS

        //Language datasource
        langaugeDropDownDataSource= new kendo.data.DataSource({
            transport: {
                read: {
                    url: langaugeWebApiUrl,
                    dataType: "json"
                }
            },
            change: function (e) {
                console.log("change in langauge datasource");
                var data = this.data();
                console.log(data);
            },
            success: function (data) {
                console.log("got data back from langauge service call ");
                console.log(data);
                
            },
            error: function (xhr, error) {
                console.debug(xhr);
                console.debug(error);

            }

        });
        
            //end language
    }
    });



//common functions for the SPA area
function onAgencyDropDownChange(e) {
    console.log("onAgencyDropDownChange");
    console.log(e);
};
