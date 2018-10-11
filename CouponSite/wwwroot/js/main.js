
var uri = 'p/api/main';
var cp;
(function ($) {
    "use strict";

    /*[ Load page ]
    ===========================================================*/



    $('#GoBackEnd').hide();
    $('#wrongText').hide();
    $('#displayCoupon').hide();

    var ProductID = getProductID();
    console.log('ProductID : ', ProductID);
    var detail = getProductDetailById(ProductID);

    isOwner();

    $("#GoBackEnd").on('click', function () {
        if (localStorage.getItem('user')) {
            location.href = '/View/BackEnd.html';
        }
    })

    $('#SubmitUserInfo').on('click', function () {
        var UserEmail = $('#UserEmail').val();
        var UserName = $('#UserName').val();
        var IsEmail = checkIsEmail(UserEmail);
        if (cp != undefined && UserName != undefined && IsEmail == true) {
            saveUserInfo(UserEmail, UserName, cp);
        }
        else {
            $('#wrongText').show();
        }
    });

    function checkIsEmail(email) {
        var regu =
            "^(([0-9a-zA-Z]+)|([0-9a-zA-Z]+[_.0-9a-zA-Z-]*[0-9a-zA-Z]+))@([a-zA-Z0-9-]+[.])+([a-zA-Z]{2}|net|NET|com|COM|gov|GOV|mil|MIL|org|ORG|edu|EDU|int|INT)$"
        var re = new RegExp(regu);
        if (email.search(re) != -1) {
            return true;
        } else {
            return false;
        }
    }

    function displayCouponCode() {
        console.log('displayCouponCode');
        $('#enterInfo').hide();
        $('#displayCoupon').show();
        $('#CouponCode').val(cp);
        $('#copyCoupon').on('click', function () {
            var $copyText = $('#CouponCode');
            $copyText.select();
            document.execCommand("Copy");
            console.log('copy :', $copyText);
        });
    }


    function saveUserInfo(UserEmail, UserName,Coupon) {
        var json = {
            "Email": UserEmail,
            "Name": UserName,
            "PCoupon": Coupon
        }
        $.ajax({
            url: 'm/api/Member/PostUserInfo',
            data: JSON.stringify(json),
            type: "post",
            contentType: "application/json",
            success: function (result) {
                if (result) {
                    console.log('Data is Saved !');
                    displayCouponCode();
                }
                else {
                    console.log('Fail to save Info !');
                    $('#wrongText').val('OOPS! Something went wrong , please try again later');
                }
            }

        });

    }
    
    
    $('.block2-btn-addcart').each(function () {
        var nameProduct = $(this).parent().parent().parent().find('.block2-name').html();
        $(this).on('click', function () {
            swal(nameProduct, "is added to cart !", "success");
        });
    });
    
    
    $('.block2-btn-addwishlist').each(function () {
        var nameProduct = $(this).parent().parent().parent().find('.block2-name').html();
        $(this).on('click', function () {
            swal(nameProduct, "is added to wishlist !", "success");
        });
    });
    
    /*
    $('.btn-addcart-product-detail').each(function () {
        var nameProduct = $('.product-detail-name').html();
        $(this).on('click', function () {
            swal(nameProduct, "is added to wishlist !", "success");
        });
    });
    */

    $(".animsition").animsition({
        inClass: 'fade-in',
        outClass: 'fade-out',
        inDuration: 1500,
        outDuration: 800,
        linkElement: '.animsition-link',
        loading: true,
        loadingParentElement: 'html',
        loadingClass: 'animsition-loading-1',
        loadingInner: '<div data-loader="ball-scale"></div>',
        timeout: false,
        timeoutCountdown: 5000,
        onLoadEvent: true,
        browser: [ 'animation-duration', '-webkit-animation-duration'],
        overlay : false,
        overlayClass : 'animsition-overlay-slide',
        overlayParentElement : 'html',
        transition: function(url){ window.location.href = url; }
    });
    
    /*[ Back to top ]
    ===========================================================*/
    var windowH = $(window).height()/2;

    $(window).on('scroll',function(){
        if ($(this).scrollTop() > windowH) {
            $("#myBtn").css('display','flex');
        } else {
            $("#myBtn").css('display','none');
        }
    });

    $('#myBtn').on("click", function(){
        $('html, body').animate({scrollTop: 0}, 300);
    });


    /*[ Show header dropdown ]
    ===========================================================*/
    $('.js-show-header-dropdown').on('click', function(){
        $(this).parent().find('.header-dropdown')
    });

    var menu = $('.js-show-header-dropdown');
    var sub_menu_is_showed = -1;

    for(var i=0; i<menu.length; i++){
        $(menu[i]).on('click', function(){ 
            
                if(jQuery.inArray( this, menu ) == sub_menu_is_showed){
                    $(this).parent().find('.header-dropdown').toggleClass('show-header-dropdown');
                    sub_menu_is_showed = -1;
                }
                else {
                    for (var i = 0; i < menu.length; i++) {
                        $(menu[i]).parent().find('.header-dropdown').removeClass("show-header-dropdown");
                    }

                    $(this).parent().find('.header-dropdown').toggleClass('show-header-dropdown');
                    sub_menu_is_showed = jQuery.inArray( this, menu );
                }
        });
    }

    $(".js-show-header-dropdown, .header-dropdown").click(function(event){
        event.stopPropagation();
    });

    $(window).on("click", function(){
        for (var i = 0; i < menu.length; i++) {
            $(menu[i]).parent().find('.header-dropdown').removeClass("show-header-dropdown");
        }
        sub_menu_is_showed = -1;
    });


     /*[ Fixed Header ]
    ===========================================================*/
    var posWrapHeader = $('.topbar').height();
    var header = $('.container-menu-header');

    $(window).on('scroll',function(){

        if($(this).scrollTop() >= posWrapHeader) {
            $('.header1').addClass('fixed-header');
            $(header).css('top',-posWrapHeader); 

        }  
        else {
            var x = - $(this).scrollTop(); 
            $(header).css('top',x); 
            $('.header1').removeClass('fixed-header');
        } 

        if($(this).scrollTop() >= 200 && $(window).width() > 992) {
            $('.fixed-header2').addClass('show-fixed-header2');
            $('.header2').css('visibility','hidden'); 
            $('.header2').find('.header-dropdown').removeClass("show-header-dropdown");
            
        }  
        else {
            $('.fixed-header2').removeClass('show-fixed-header2');
            $('.header2').css('visibility','visible'); 
            $('.fixed-header2').find('.header-dropdown').removeClass("show-header-dropdown");
        } 

    });
    
    /*[ Show menu mobile ]
    ===========================================================*/
    $('.btn-show-menu-mobile').on('click', function(){
        $(this).toggleClass('is-active');
        $('.wrap-side-menu').slideToggle();
    });

    var arrowMainMenu = $('.arrow-main-menu');

    for(var i=0; i<arrowMainMenu.length; i++){
        $(arrowMainMenu[i]).on('click', function(){
            $(this).parent().find('.sub-menu').slideToggle();
            $(this).toggleClass('turn-arrow');
        })
    }

    $(window).resize(function(){
        if($(window).width() >= 992){
            if($('.wrap-side-menu').css('display') == 'block'){
                $('.wrap-side-menu').css('display','none');
                $('.btn-show-menu-mobile').toggleClass('is-active');
            }
            if($('.sub-menu').css('display') == 'block'){
                $('.sub-menu').css('display','none');
                $('.arrow-main-menu').removeClass('turn-arrow');
            }
        }
    });


    /*[ remove top noti ]
    ===========================================================*/
    $('.btn-romove-top-noti').on('click', function(){
        $(this).parent().remove();
    })


    /*[ Block2 button wishlist ]
    ===========================================================*/
    $('.block2-btn-addwishlist').on('click', function(e){
        e.preventDefault();
        $(this).addClass('block2-btn-towishlist');
        $(this).removeClass('block2-btn-addwishlist');
        $(this).off('click');
    });

    /*[ +/- num product ]
    ===========================================================*/
    $('.btn-num-product-down').on('click', function(e){
        e.preventDefault();
        var numProduct = Number($(this).next().val());
        if(numProduct > 1) $(this).next().val(numProduct - 1);
    });

    $('.btn-num-product-up').on('click', function(e){
        e.preventDefault();
        var numProduct = Number($(this).prev().val());
        $(this).prev().val(numProduct + 1);
    });


    /*[ Show content Product detail ]
    ===========================================================*/
    $('.active-dropdown-content .js-toggle-dropdown-content').toggleClass('show-dropdown-content');
    $('.active-dropdown-content .dropdown-content').slideToggle('fast');

    $('.js-toggle-dropdown-content').on('click', function(){
        $(this).toggleClass('show-dropdown-content');
        $(this).parent().find('.dropdown-content').slideToggle('fast');
    });


    /*[ Play video 01]
    ===========================================================*/
    var srcOld = $('.video-mo-01').children('iframe').attr('src');

    $('[data-target="#modal-video-01"]').on('click',function(){
        $('.video-mo-01').children('iframe')[0].src += "&autoplay=1";

        setTimeout(function(){
            $('.video-mo-01').css('opacity','1');
        },300);      
    });

    $('[data-dismiss="modal"]').on('click',function(){
        $('.video-mo-01').children('iframe')[0].src = srcOld;
        $('.video-mo-01').css('opacity','0');
    });

})(jQuery);


function isOwner() {
    if (localStorage.getItem('user')) {
        $('#GoBackEnd').show();
    }
}

function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

function getProductID() {
    var productID = getUrlVars()["ProductID"];
    if (productID != undefined) {
        return productID;
    }
    return null;
}

function getProductDetailById(ProductID) {
    var id = ProductID;
    $.getJSON(uri + '/GetProduct/' + id)
        .done(function (data) {
            console.log('done data', data);
            if (data[0].Valid.replace(" ","") != "n") {
                console.log('done data detail ', data[0].ProductName);
                $('#product').text(displayProductsDetail(data[0]));
            }
            else {
                displayProductsAreSoldOut(data[0]);
            }
           
        })
        .fail(function (jqXHR, textStatus, err) {
            $('#product').text('Error: ' + err);
        });
}

function displayProductsDetail(item) {
    cp = item.PCoupon;
    $("#productName").text(item.ProductName);
    $("#tag-productName").text(item.ProductName);
    var strikePrice = ("$" + item.Price).strike();
    $("#ProductPrice").html(strikePrice);
    $("#ProductDiscount").text(item.Discount * 100 + '% off');
    var salePrice = item.Price * (1 - item.Discount);
    $("#ProductSalePrice").text(salePrice);
    $("#Coupon").text(item.PCoupon);
}

function displayProductsAreSoldOut(item) {
    console.log('Sold Out!!!');
    $("#productName").text(item.ProductName);
    $("#tag-productName").text(item.ProductName);
    var strikePrice = ("$" + item.Price).strike();
    $("#ProductPrice").html(strikePrice);
    $("#ProductDiscount").text(item.Discount * 100 + '% off');
    var salePrice = item.Price * (1 - item.Discount);
    $("#ProductSalePrice").text(salePrice);

    $('#GetCouponButton').text('Sold Out !');
    $('#GetCouponButton').attr('disabled', 'disabled');
    $('#GetCouponButton').removeClass('hov1');

}

