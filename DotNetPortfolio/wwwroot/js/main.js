(function($) {
    "use strict";

    //10. preloader
    $(window).on('load', function() {
        $('.preloader-wave-effect').fadeOut();
        $('#preloader-wrapper').delay(150).fadeOut('slow');
    });

    // slider owlCarousel
    $('.slider-active').owlCarousel({
        loop: true,
        margin: 30,
        autoplay: true,
        autoplayTimeout: 5000,
        nav: false,
        smartSpeed: 1500,
        mouseDrag: true,
        navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
        nav: true,
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

    // testimonial-active owlCarousel
    $('.testimonial-active').owlCarousel({
        loop: true,
        margin: 30,
        autoplay: true,
        autoplayTimeout: 5000,
        nav: false,
        smartSpeed: 1500,
        mouseDrag: true,
        navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
        nav: true,
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


// slicknav
    $('.mobail-menu').slicknav({
        prependTo:".menu"
    });

    // 2. Isotope Portfolio 
    $('.grid').imagesLoaded(function() {

        // filter items on button click
        $('.portfolio-menu').on('click', 'button', function() {
            var filterValue = $(this).attr('data-filter');
            $grid.isotope({
                filter: filterValue
            });
        });

        // init Isotope
        var $grid = $('.grid').isotope({
            itemSelector: '.grid-item',
            percentPosition: true,
            masonry: {
                // use outer width of grid-sizer for columnWidth
                columnWidth: '.grid-item',
            }
        });



    });

    $('.portfolio-menu button').on('click', function(event) {
        $(this).siblings('.active').removeClass('active');
        $(this).addClass('active');
        event.preventDefault();
    });


    //. smooth scrolling
    $(function() {
        $('.header-menu ul li  a ,.header-menu ul.mobail-menu li  a, .scrollup').bind('click', function(event) {
            var $anchor = $(this);
            $('html, body').stop().animate({
                scrollTop: $($anchor.attr('href')).offset().top - 1
            }, 1000, 'easeInOutExpo');
            event.preventDefault();
        });
        $('body').attr('id', 'scrolltop');
    });

    // sticky-header
    $(window).scroll(function() {

        if ($(window).scrollTop() > 10) {
            $('.sticky-header').addClass('sticky');
            $('.scrollup').addClass('show_hide');
        } else {
            $('.sticky-header').removeClass('sticky');
            $('.scrollup').removeClass('show_hide');
        }
    });

}(jQuery));

