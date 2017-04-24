$(document).ready(function() 
{
	$('#NewsMenu').click(function() 
	{
    $('#HomeContainer').hide();
		$('#AgenciesContainer').hide();
    $('#ApplicationContainer').hide();
    $('#CategoryContainer').hide();
    $('#LocationContainer').hide();
    $('#ServiceContainer').hide();
    $('#NewsContainer').load("../../Private/HTML/NewsIndex.html", function (resp, status, xhr)
  			{
  				if(status == 'success')
  				{
  				    $('#NewsContainer').show();
                    //used to preset objects before actuallyloading 
  				    //findAllNews();
  				}
  				else
  				{
  					console.log("Unable to load News section");
  				}
  				
  		});
    $('head').append($('<link rel="stylesheet" type="text/css" />').attr('href', '../../Public/Styles/news.css'));
	});
});