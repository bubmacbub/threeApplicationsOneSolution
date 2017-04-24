
$(document).ready(function() 
{
	$('#AgenciesMenu').click(function() 
	{
		$('#HomeContainer').hide();
    $('#ApplicationContainer').hide();
    $('#CategoryContainer').hide();
    $('#LocationContainer').hide();
    $('#NewsContainer').hide();
  	$('#ServiceContainer').hide();


      $('#AgenciesContainer').load("../../Private/HTML/AgenciesIndex.html",
        function(resp, status, xhr)
        {
          if(status == 'success')
          {
            $('#AgenciesContainer').show();
            
          }
          else
          {
            console.log("Unable to load welcome section");
          }
          
        });

	});
});
