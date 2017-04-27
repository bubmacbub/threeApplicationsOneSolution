$(document).ready(function() 
{
	$('#CategoryMenu').click(function() 
	{
		$('#AgenciesContainer').hide();
    $('#ApplicationContainer').hide();
    $('#HomeContainer').hide();
    $('#LocationContainer').hide();
    $('#NewsContainer').hide();
    $('#ServiceContainer').hide();
    $('#CategoryContainer').load("/Private/HTML/CategoryIndex.html", function (resp, status, xhr)
  			{
  				if(status == 'success')
  				{
  					$('#CategoryContainer').show();
  				}
  				else
  				{
  					console.log("Unable to load applicatoin section");
  				}
  				
   			});
	});
});