$(document).ready(function() 
{
	$('#AgenciesContainer').hide();
    $('#HomeContainer').hide();
    $('#CategoryContainer').hide();
    $('#LocationContainer').hide();
    $('#NewsContainer').hide();
  	$('#ServiceContainer').hide();
	$('#ApplicationMenu').click(function() 
	{
		$('#AgenciesContainer').hide();
		$('#ApplicationContainer').load("/Private/HTML/ApplicationIndex.html", function (resp, status, xhr)
  			{
  				if(status == 'success')
  				{
  					$('#ApplicationContainer').show();
  				}
  				else
  				{
  					console.log("Unable to load applicatoin section");
  				}
  				
   			});
	});
});