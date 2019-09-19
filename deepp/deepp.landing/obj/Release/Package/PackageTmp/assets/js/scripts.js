$(document).ready(function() {

    $("[rel^='prettyPhoto']").prettyPhoto({
        deeplinking: false
    });

    $('#backtop, #backtop2').on('click', function() {
        $('html, body').animate({ scrollTop: 0 }, 800);
        return false;
    });

    if ($('.owl-carousel').length != '') {
        $('.owl-carousel').owlCarousel({
            loop: true,
            margin: 0,
            nav: true,
            dots: false,
            navText: [
                "<i class='icon-arrow-left10'></i>",
                "<i class='icon-arrow-right10'></i>"
            ],
            responsive: {
                0: {
                    items: 1
                },
                600: {
                    items: 1
                },
                1000: {
                    items: 1
                }
            }
        });
    }

    if ($('.cs-partner').length != '') {
        $('.cs-partner').owlCarousel({
            loop: true,
            margin: 10,
            nav: true,
            dots: false,
            navText: [
                "<i class='icon-arrow-left10'></i>",
                "<i class='icon-arrow-right10'></i>"
            ],
            responsive: {
                0: {
                    items: 2
                },
                600: {
                    items: 4
                },
                1000: {
                    items: 6
                }
            }
        });
    }

});

$(window).load(function() {

    if ($('.flexslider').length != '') {
        $('.flexslider').flexslider({
            slideshowSpeed: 4000,
            animationDuration: 1100,
            animation: 'slide',
            directionNav: true,
            controlNav: false,
            pausePlay: true,
            pauseText: 'Pause',
            playText: 'Play',
            prevText: "<i class='icon-arrow-left10'></i>",
            nextText: "<i class='icon-arrow-right10'></i>",
            start: function(slider) {
                $('.flexslider').removeClass('cs-loading');
                $('.flexslider').find('.loader').remove();
            }
        });
    }

    if ($('.news-ticker').length != '') {
        $('.news-ticker').flexslider({
            slideshowSpeed: 2000,
            animationDuration: 1100,
            animation: 'fade',
            directionNav: true,
            controlNav: false,
            pausePlay: false,
            pauseText: 'Pause',
            playText: 'Play',
            prevText: "<i class='icon-arrow-left10'></i>",
            nextText: "<i class='icon-arrow-right10'></i>",
        });
    }

});

// Header Responsive
$(document).ready(function() {
    $(".navigation ul ul").parent('li').addClass('parentIcon');
    $(".navigation ul ul").parent('li').prepend("<span class='responsive-btn'><i class='icon-plus8'></i></span>");

    $(".navigation ul a").click(function(e) {

        var a = $(window).width();
        var b = 1000
        if (a <= b) {

            var dropCheck = $(this).parent('li').find('.sub-dropdown');

            if (dropCheck.length != '') {
                e.preventDefault();
                if ($(this).parent('li').hasClass('active')) {
                    $(this).parent('li').find('.responsive-btn').html("<i class='icon-plus8'></i>");
                    $(this).parent('li').removeClass('active');
                    $(this).parent('li').children('ul').hide();
                } else {
                    $(".navigation .responsive-btn").html("<i class='icon-plus8'></i>");
                    $(this).parent('li').find('.responsive-btn').html("<i class='icon-minus8'></i>");
                    $(this).parent('li').parent('ul').find('li').removeClass('active');
                    $(this).parent('li').addClass('active');
                    $(this).parent('li').parent('ul').find('li>ul').hide();
                    $(this).parent('li').children('ul').show();
                }
            }
        }
    });

    $('.cs-click-menu').on('click', function(e) {
        e.preventDefault();
        $(this).siblings('ul').slideToggle();
        $(".navigation ul ul").hide();
    });

    $('.counter').counterUp({
        delay: 10, // the delay time in ms
        time: 1000 // the speed time in ms
    });
});
$(window).resize(function() {
    var a = $(window).width();
    var b = 1000
    if (a >= b) {
        $(".navigation ul ul,.navigation ul").show();
    } else {
        $(".navigation ul ul,.navigation ul").hide();
    }
});