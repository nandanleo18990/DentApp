$.fn.scrollView = function () {
    return this.each(function () {
      var Mypos = $(this).offset().top - 250;
      $('html, body').animate({
        scrollTop: Mypos 
      }, 1000);
    });
  }


$('#sc1').click(function () {
  
  $('#service-1th').scrollView();
});

$('#sc2').click(function () {
  
  $('#service-2th').scrollView();
});

$('#sc3').click(function () {
  
  $('#service-3th').scrollView();
});

$('#sc4').click(function () {
  
  $('#service-4th').scrollView();
});

$('#sc5').click(function () {
  
  $('#service-5th').scrollView();
});
