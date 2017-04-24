$(document).ready(function() 
{
	$('#LocationMenu').click(function() 
	{
    $('#HomeContainer').hide();
		$('#AgenciesContainer').hide();
    $('#ApplicationContainer').hide();
    $('#CategoryContainer').hide();
    $('#ServiceContainer').hide();
    $('#NewsContainer').hide();
    $('#LocationContainer').load("../../Private/HTML/LocationIndex.html", function (resp, status, xhr)
  			{
  				if(status == 'success')
  				{
  				    $('#LocationContainer').show();
  				    
  				}
  				else
  				{
  					console.log("Unable to load Location section");
  				}
  				
   			});
	});
});