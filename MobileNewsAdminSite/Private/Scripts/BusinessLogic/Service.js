$(document).ready(function() 
{
	$('#ServiceMenu').click(function() 
	{
    $('#HomeContainer').hide();
		$('#AgenciesContainer').hide();
    $('#ApplicationContainer').hide();
    $('#CategoryContainer').hide();
    $('#LocationContainer').hide();
    $('#NewsContainer').hide();
    $('#ServiceContainer').load("../../Private/HTML/ServiceIndex.html", function (resp, status, xhr)
  			{
  				if(status == 'success')
  				{
  				    $('#ServiceContainer').show();
  				    findAllServices();
  				}
  				else
  				{
  					console.log("Unable to load Service section");
  				}
  				
   			});
	});
});