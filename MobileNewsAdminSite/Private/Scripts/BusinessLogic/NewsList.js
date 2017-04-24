
function findAllNews() {
    console.log("findAllNews called");
    var jqxhr = $.ajax(
	{
	    type: 'GET',
	    url: newsWebApiUrl,
	    xhrFields:
        {
            withCredentials: true
        }
	})
	  .done(function (data) {
	      console.log("Data from ofa service call");
	      console.log(data);
	      if (data != null) {
	          var itemCount = 0;
	          for (x = 0; x < data.length; x++) {
	              console.log(data[x]);
	              if (x % 3 === 0) {
	                  $("<div class='row'>")
	              }


	              $("<div class='col-md-4'><div class='panel panel-default' id='newsPanel" + x +
                      "'><div class='panel-heading' id='newsPanelHeading" + x +
                      "'><h3 class='panel-title' id='newsPanelTitle" + x + "'>" + data[x].news_title +
                  "</h3> </div><div class='panel-body news-panel-body' id='newsPanelBody" + x + "'>" + data[x].news_content +
                  "</div></div></div>").appendTo($('#mainNewsContainer'));

	              if (x % 3 === 0) {
	                  $("</div>")
	              }

	          }
	      }
	  })
	  .fail(function (jqXHR, textStatus) {
	      console.log("Ajax call fail: " + textStatus)
	      console.log(jqXHR);
	  });
}

//upper level container for the tabs of categories is #newsTabList
//<li role="presentation" class="active"><a href="#">Category 0</a></li>
//<li role="presentation"><a href="#">Category 1</a></li>
//<li><a data-toggle="tab" href="#menu1">Menu 1</a></li>

$(function () {
    console.log("News List JS Loaded");
    //Get all the category news so we can create the top tabs to list each news under a category only.  maybe include an ALL category to show all news, with lazy loading of course
    //After the tab list of news is created list out the news for the first category.  If this is done blindly I will end up creating duplicate news objects when they are shared across categories
    //Attach click events on the news objects to get to an edit form
    //first idea is to make a modal overlay that shows the selected news object in a styled layout like the list is in on the left and then on the right show a form for to edit the news object
    //another idea is to go to a new page because we might need the real estate for a html format widget for allowing user to enter in the main content in a format that mobile wants it in.  HTML i think

    //get all cats ex: /admin/Category/AllNewsCategories?agencyId=1
    var firstCategory = getAllCategoriesThatHaveNews();
    //getCategorysNews(firstCategory);
    //make the drop downs from the datasources.  Might only need to do this if modal is shown but don't want to make it everytime it is shown.  maybe just refresh the ds
    $("#newsnewsagency_id").kendoDropDownList({
        dataTextField: "agency_name",
        dataValueField: "agency_id",
        optionLabel: "Select an Agency",
        dataSource: agencyDropDownDataSource,
        change: onAgencyDropDownChange
    });

    $("#newslanguage_id").kendoDropDownList({
        dataTextField: "language_name",
        dataValueField: "language_id",
        optionLabel: "Select a Language",
        dataSource: langaugeDropDownDataSource
    });
});


//<div id="home" class="tab-pane fade in active">
//        <h3>HOME</h3>
//        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
//    </div>
function getCategorysNews(category, tabPane) {
    console.log("Going to get news for this category");
    console.log(category);
    var catNewsWebApiUrl = adminNewsBaseWebApiUrl + "/FindNewsForCategory?categoryID=";
    var reducedCatname = '';
    if (category == 'all')
    {
        catNewsWebApiUrl = newsWebApiUrl;
        reducedCatname = 'AllNews';
    }
    else
    {
        catNewsWebApiUrl += category.category_id;
        reducedCatname = category.category_name;
        reducedCatname = reducedCatname.replace(/\s/g, '');
    }
  
    console.log("tabPane");
    console.log(tabPane);

    var jqxhr = $.ajax(
    {
        type: 'GET',
        url: catNewsWebApiUrl,
        xhrFields:
        {
            withCredentials: true
        }
    })
      .done(function (data) {
          console.log("Got news from backend service");
          console.log(data);

          if (data != null) {
              var rowCount = 0;
              var rowPane;
              for (x = 0; x < data.length; x++) {

                  if (x % 3 === 0) {
                      
                      rowPane = $("<div class='row' id='" + reducedCatname + "PaneRow" + rowCount + "'>");
                      console.log("rowPane");
                      console.log(rowPane);
                      rowPane.appendTo(tabPane);
                      rowCount++;
                  }




                  var colContainer = $("<div class='col-md-4' >");
                  var panel = $("<div class='panel panel-default' id='newsPanel" + x + "'>");
                  panel.css({ 'cursor': 'pointer' });
                  console.log(data[x]);
                  panel.click(data[x], editNews);
                  var panelHead = $("<div class='panel-heading' id='newsPanelHeading" + x + "'>");
                  var panelTitle = $("<h3 class='panel-title' id='newsPanelTitle" + x + "'>" + data[x].news_title + "</h3>");
                  var panelBody = $("<div class='panel-body news-panel-body' id='newsPanelBody" + x + "'>");
                  panelBody.html(data[x].news_content);
                  colContainer.appendTo(rowPane);
                  panel.appendTo(colContainer);
                  panelTitle.appendTo(panelHead);
                  panelHead.appendTo(panel);
                  panelBody.appendTo(panel);

                  if (x % 3 === 0) {
                      $("</div>")
                  }

              }
          }
      })
      .fail(function (jqXHR, textStatus) {
          console.log("Ajax call fail: " + textStatus)
          console.log(jqXHR);
      });

}





function getAllCategoriesThatHaveNews() {
    var catForNewsUrl = categoryBaseWebApiUrl + "/AllNewsCategories?agencyId=";
    var uaid = localStorage.getItem("userAgencyId");
    catForNewsUrl += uaid;
    var catXHR = $.ajax(
      {
          type: 'GET',
          url: catForNewsUrl,
          xhrFields:
          {
              withCredentials: true
          }
      })
       .done(function (data) {
           console.log("SUCCESS: got data for categores of NEWS api call");
           console.log(data);
           var firstCategory;
           //create the tabs
           var tabParent = $("#newsTabList");
           for (x = 0; x < data.length; x++) {
               var tab = $("<li role='presentation'>")

               //tab.text(data[x].category_name);
               tab.appendTo(tabParent);
               var tabAnchor = $("<a data-toggle='tab' href='#catTabPane" + data[x].category_id + "'>");
               tabAnchor.text(data[x].category_name);
               tabAnchor.appendTo(tab);

               var tabPane = $("<div id='catTabPane" + data[x].category_id + "' role='tabpanel' class='tab-pane fade in'>");
               tabPane.appendTo($('#newTabContent'));
               var params = { category: data[x], tabPane: tabPane };

               tabAnchor.click(params, clickedCategorysNews);
               if (x === 0) {
                   tab.addClass("active");
                   tabPane.addClass("active");
                   firstCategory = data[x];
                   getCategorysNews(firstCategory, tabPane);
               }
           }
           var tab = $("<li role='presentation'>")
            tab.appendTo(tabParent);
           var tabAnchor = $("<a data-toggle='tab' href='#catTabPaneAll'>");
           tabAnchor.text("All");
           tabAnchor.appendTo(tab);
            var tabPane = $("<div id='catTabPaneAll' role='tabpanel' class='tab-pane fade in'>");
           tabPane.appendTo($('#newTabContent'));
           var params = { category: 'all', tabPane: tabPane };
            tabAnchor.click(params, clickedCategorysNews);
           return firstCategory;
       });
}



function clickedCategorysNews(event) {
    //take apart the stacked paramaters then pass them along to the getcategorynews function
    console.log("clicked category news function init");
    console.log(event);
    var category = event.data.category;
    var tabPane = event.data.tabPane;
    tabPane.empty();
    getCategorysNews(category, tabPane[0]);
}




function editNews(newsData)
{
    //create the overlay to display both the news item and the form
    //populate the edit form
    console.log("passed data");
    console.log(newsData);
    console.log("event not passed");
    console.log(event);
    if (!jQuery.isEmptyObject(newsData.data)) {
        var data = newsData.data;
        console.log("news data");
        console.log(data);
        //not working for some reason.  won't close on escape key
        //$('#serviceEditFormModal').modal({ keyboard: true });
        $('#newsEditFormModal').modal('show');

        var agencyDDL = $("#newsnewsagency_id").data("kendoDropDownList");
        agencyDDL.select(data.agency_id);
        $("#newslanguage_id").data("kendoDropDownList").select(data.language_id);

        //assign the selected items data to the form
        console.log($("#newsnews_title"));
        console.log(data.news_title);
        $("#newsnews_title").val(data.news_title);
        $("#newsnews_content").val(data.news_content);
        //$("#news_image").val(data.news_image);
        
        $("#newspublish_date").val(data.publish_date);
        $("#newsarchive_flag").val(data.archive_flag);
        $("#newscreated_date").val(data.created_date);
        $("#newsmodified_date").val(data.modified_date);
        $("#newslogical_delete_date").val(data.logical_delete_date);
    }
}


function openCreateNewsModal()
{
    $('#createNewsHtmlContainer').load("../../Private/HTML/NewsCreate.html",
            function (resp, status, xhr) {
                if (status == 'success') {
                    console.log("create news section loaded");
                    $('#newsCreateFormModal').modal('show');
                }
                else {
                    console.log("Unable to load news create section");
                }

            });
}




function submitCreateNewsForm()
{
    console.log("submitCreateNewsForm");
    $("#createNewsForm").submit(function () {
        console.log("create news form's submit function creation");
        event.preventDefault();
        var createNewsWebApiUrl = location.protocol + "//" + location.hostname + ":53517/api/mobileNews";
        var jqxhr = $.post(createNewsWebApiUrl, $('#createNewsForm').serialize())
            .done(function () {
                console.log("Successfully sent create news");
                console.log(event);
            })
            .fail(function () {
                console.log("Error trying to create a news item");
                console.log(event);
            });
        return false;
    });
    console.log("Going to submit form");
    $("#createNewsForm").submit();
    console.log("Form submitted");
}