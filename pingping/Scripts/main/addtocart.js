
//$(document).ready(function () {
//    $("#adqdtocart").click(function () {
//        $("#loginForm").hide({ direction: "right" }, 1000);
//        $("#registerForm").show({ direction: "right" }, 2000);
//    });

//})
var number = 0;
var value_cart = [];
function addtocart(id,name, price) {
    var flat = 0;

    if (value_cart != null) {
        for (var i = 0; i < number; i++) {
            if (value_cart[i].id == id) { //so sánh id nha
                value_cart[i].quantity += 1;
                break;
            }
            else {
                flat += 1;
            }
    }
        if (flat == number) {
            value_cart.push({
            id: id, name: name, price: price, quantity: 1
        });
            number += 1;
        }
    }
    else {
        value_cart.push({
            id: id, name: name, price: price, quantity: 1
        });
        number += 1;
    }
    DetailCart();
}
function DetailCart() {
    var html = ''; var total = 0; var i = 0;
    $.each(value_cart, function (key, item) {
        html += '<li class="product_' + i + '">';
        html += '<a href="#" class="remove" title="Remove this item"><i class="fa fa-remove" onclick="RemoveItem(' + i + ',' + item.id + ')"></i></a>';
        html += '<a class="cart-img" href="#"><img src="https://via.placeholder.com/70x70" alt="#"></a>';
        html += '<h4><a href="#">' + item.name + '</a></h4>';
        html += '<p class="quantity" style="color:red">' + item.quantity + 'x - <span class="amount">' + item.price + '</span></p>';
        html += '</li>';
        total += Number(item.price) * item.quantity;
        i += 1;
        notify();
});

    $('.shopping-list').html(html);
    $('.total-count').html(number);
    $('.item-quantity').html(number +'  Items');
    //console.log(total);
    $('.total-amount').html(total.toLocaleString('vi', {style: 'currency', currency: 'VND' }));
}
function RemoveItem(removeItem,id) { // id sp
    $('.product_' + removeItem).hide({ direction: "right" }, 1200).removeClass();
    //var removeIndex = value_cart.map(function (item) { return item.price; }).indexOf(price);
    //value_cart.splice(removeIndex, 1);
    var result = value_cart.splice(value_cart.findIndex(a => a.id === id), 1);
    if (result) number -= 1;
        setTimeout(function () {
        DetailCart();
    }, 900);
    // alert(value_cart.splice(value_cart.findIndex(a => a.price === price), 1));
}

function checkout() {
    //value_cart.push({
    //    id: id, name: name, price: price, quantity: 1
    //});
    objectCart = JSON.stringify({ 'data': value_cart });
    if (value_cart == 0) {
        alert("Đơn Hàng Trống!");
    }
    else {
        $.ajax({
            url: "/Home/Set_CheckOut",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: objectCart,
            success: function (result) {
                if (result == 1) {
                    alert("Success.");
                    window.location.replace('../../Home/CheckOut');
                }
                else if (result == -1) { alert("Bạn Cần Đăng Nhập Để Mua Hàng!"); }
                else {
                    alert("Bạn Cần Đăng Nhập Bằng Tài Khoản Người Mua.");
                }
                //setTimeout(function () {
                //    $("#overlay").fadeOut(300);
                //}, 500);
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

