var token = "ef26267b-c8c7-11ea-b16d-9289328232ea";
var tokenGHTK = "46b6093CA6daA69ea42de5d2B70624B290040600";
var ShopId = "1257313";

var l = 0; var w = 0; var h = 0; var we = 0; var to = 0; var feeship = 0;
$(document).ready(function () {

    LoadProvince();
    $('input[name="paymentMenthod"]').off('click').on('click', function () {
        if ($(this).val() == 'TienMat') {
            $('.boxContent').hide();
            $('#pick_order').show();

            $('#pick_order').click(function () {
                if ($('#freeship_GHN').html() == "0") {
                    alert("Địa Chỉ Giao Hàng Không Được Bỏ Chống.");
                }
                else {

                    var order = [];
                    order.push({
                        id_province: $('#to_province').val(),
                        name_province: $('#to_province option:selected').text(),
                        id_dictrict: $('#to_district').val(),
                        name_dictrict: $('#to_district option:selected').text(),
                        id_ward: $('#to_ward').val(),
                        name_ward: $('#to_ward option:selected').text(),
                        name_address: $('#to_address').val(),
                        id_service: $('#service').val(),
                        length: l,
                        width: w,
                        height: h,
                        weight: we,
                        feeship: feeship,

                        token: token,
                        shopid: ShopId
                    });
                    //console.log(order);
                    objectOrder = JSON.stringify({ 'data': order });
                    if (order == 0) {
                        alert("Đơn Hàng Trống!");
                    }
                    else {
                        console.log(objectOrder);
                        $.ajax({
                            url: "/Home/Pick_OrderGHN",
                            type: "POST",
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            data: objectOrder,
                            success: function (result) {
                                if (result == 1) {
                                    alert("Success.");
                                    window.location.replace('../../Home/MyOrder');
                                }
                                else if (result == -1) { alert("Bạn Cần Đăng Nhập Để Mua Hàng!"); }
                                else {
                                    alert("Đơn Hàng Rỗng!");
                                }
                            },
                            error: function (errormessage) {
                                alert(errormessage.responseText);
                            }
                        });
                    }
                }
            });
        }
        else if ($(this).val() == 'Paypal') {
            $('.boxContent').hide();
            $('#paypal').show();
            $('#pick_order').hide();
        }

    });
});

//------------FeeShip--------------------
function GHN_CheckFeeShip(length, width, height, weight, total) {
    // <check Fee_Ship
    // get value from disctrictID
    //var selectBox1 = document.getElementById("from_district");
    var district1 = 1450;//selectBox1.options[selectBox1.selectedIndex].value;

    // get value to districtID
    var selectBox2 = document.getElementById("to_district");
    var district2 = selectBox2.options[selectBox2.selectedIndex].value;

    // get value to wardID
    var selectBox3 = document.getElementById("to_ward");
    var ward1 = selectBox3.options[selectBox3.selectedIndex].value;
    //console.log(ward1);
    // get value to serviceID
    var selectBox4 = document.getElementById("service");
    var service = selectBox4.options[selectBox4.selectedIndex].value;
    $.ajax({
        url: "https://online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/fee",
        type: "GET",
        headers: {
            'Content-Type': 'application/json',
            'token': token,
            'ShopId': ShopId,
            'Content-Type': 'text/plain'
        },
        data: {
            "from_district_id": 1450,//district1,

            "service_id": service,
            //"service_type_id": null,

            "to_district_id": district2,
            "to_ward_code": ward1,
            "height": height.toString(),
            //"length": length.toString(),
            "weight": (weight*1000).toString(),
            "width": width.toString(),

            "insurance_fee": 10000,
            "CoDAmount": total
        },
        contentType: "application/json", //;charset=utf-8  
        dataType: "json",
        success: function (result) {
            if (result == -1) {
                alert("Bạn Chưa Có Giỏ Hàng.");
            }
            else {
                $('#freeship_GHN').html(result.data.service_fee.toLocaleString('vi', { style: 'currency', currency: 'VND' }));
                $('#t').html((Number($('#tonggia').val()) + result.data.service_fee).toLocaleString('vi', { style: 'currency', currency: 'VND' }));
                feeship = result.data.service_fee;
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return true;
}

// Load Province
function LoadProvince() {
    $.ajax({
        url: "https://online-gateway.ghn.vn/shiip/public-api/master-data/province",
        type: "GET",
        headers: {
            'Content-Type': 'application/json',
            'Token': token
        },
        contentType: "application/json", //;charset=utf-8  
        dataType: "json",
        success: function (result) {
            var html = '<option value="0" selected>Tỉnh/Thành</option>';
            var ii = 1;
            for (var i = 0; i < result.data.length; i++) {
                html += '<option value="' + result.data[i].ProvinceID + '">' + result.data[i].ProvinceName + '</option>';
                ii++;
            }
            $('#to_province').html(html);
            //document.getElementById('from_province').style.display = "block";
            document.getElementById('to_province').style.display = "block";
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    
}

// Load District From
function from_LoadDistrict(ProvinceID) {
    $.ajax({
        url: "https://online-gateway.ghn.vn/shiip/public-api/master-data/district",
        type: "GET",
        headers: {
            'Content-Type': 'application/json',
            'Token': token
        },
        data: {
            "province_id": ProvinceID
        },
        contentType: "application/json", //;charset=utf-8  
        dataType: "json",
        success: function (result) {
            var html = '<option selected>Quận/Huyện</option>';
            var ii = 1;
            for (var i = 0; i < result.data.length; i++) {
                html += '<option value="' + result.data[i].DistrictID + '">' + result.data[i].DistrictName + '</option>';
                ii++;
            }
            $('#from_district').html(html);
            document.getElementById('from_district').style.display = "block";
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
// Load Ward From
function from_LoadWard(WardCode) {
    $.ajax({
        url: "https://online-gateway.ghn.vn/shiip/public-api/master-data/ward",
        type: "GET",
        headers: {
            'Content-Type': 'application/json',
            'Token': token
        },
        data: {
            "district_id": WardCode
        },
        contentType: "application/json", //;charset=utf-8  
        dataType: "json",
        success: function (result) {
            var html = '<option selected>Xã/Phường</option>';
            var ii = 1;
            for (var i = 0; i < result.data.length; i++) {
                html += '<option value="' + result.data[i].WardCode + '">' + result.data[i].WardName + '</option>';
                ii++;
            }
            $('#from_ward').html(html);
            document.getElementById('from_ward').style.display = "block";
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function from_LoadWardCode(WarCode) { };
/// -------------To----------------
// Load District To
function to_LoadDistrict(ProvinceID) {
    if (ProvinceID != 0) {
        $.ajax({
            url: "https://online-gateway.ghn.vn/shiip/public-api/master-data/district",
            type: "GET",
            headers: {
                'Content-Type': 'application/json',
                'Token': token
            },
            data: {
                "province_id": ProvinceID
            },
            contentType: "application/json", //;charset=utf-8  
            dataType: "json",
            success: function (result) {
                var html = '<option value="0" selected>Quận/Huyện</option>';
                var ii = 1;
                for (var i = 0; i < result.data.length; i++) {
                    html += '<option value="' + result.data[i].DistrictID + '">' + result.data[i].DistrictName + '</option>';
                    ii++;
                }
                $('#to_district').html(html);
                document.getElementById('to_district').style.display = "block";
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
    
}

// Load Ward To
function to_LoadWard(WardCode) {
    //LoadService();
    if (WardCode != 0) {
        $.ajax({
            url: "https://online-gateway.ghn.vn/shiip/public-api/master-data/ward",
            type: "GET",
            headers: {
                'Content-Type': 'application/json',
                'Token': token
            },
            data: {
                "district_id": WardCode
            },
            contentType: "application/json", //;charset=utf-8  
            dataType: "json",
            success: function (result) {
                var html = '<option value="0" selected>Xã/Phường</option>';
                var ii = 1;
                for (var i = 0; i < result.data.length; i++) {
                    var a = new Number(result.data[i].WardCode);
                    html += '<option value="' + a + '">' + result.data[i].WardName + '</option>';
                    ii++;
                }
                $('#to_ward').html(html);
                document.getElementById('to_ward').style.display = "block";
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
   
}

//load service
function to_LoadWardCode(WardCode) {

    // get value from disctrictID
    //var selectBox1 = document.getElementById("from_district");
    //var district1 = selectBox1.options[selectBox1.selectedIndex].value;

    // get value to districtID
    var selectBox2 = document.getElementById("to_district");
    var district2 = selectBox2.options[selectBox2.selectedIndex].value;
    //alert(district1 + "_" + district2);
    $.ajax({
        url: "https://online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/available-services",
        type: "GET",
        headers: {
            'Content-Type': 'application/json',
            'Token': token
        },
        data: {
            "Shop_Id": ShopId,
            "from_district": 1450,//district1, 
            "to_district": district2
        },
        contentType: "application/json", //;charset=utf-8  
        dataType: "json",
        success: function (result) {
            var html = '<option value="0" selected>---</option>';
            var ii = 1;
            for (var i = 0; i < result.data.length; i++) {
                if (result.data[i].short_name == "Bay")
                    html += '<option value="' + result.data[i].service_id + '">Giao Hàng Nhanh</option>';
                else if (result.data[i].short_name == "Đi bộ")
                    html += '<option value="' + result.data[i].service_id + '">Giao Hàng Tiết Kiệm</option>';
                else
                    html += '<option value="' + result.data[i].service_id + '">' + result.data[i].short_name + '</option>';
                ii++;
            }
            $('#service').html(html);
            document.getElementById('service').style.display = "block";
            console.log(result);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function LoadService(serviceID) {
    //get TheTich Sp
    console.log(serviceID);
    if (serviceID != 0) {
        $.ajax({
            url: "/Home/GetTheTichHoaDon",
            type: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                debugger;
                if (result == -1) { alert("Lỗi Tính Phí."); }
                else {
                    
                    GHN_CheckFeeShip(result.length, result.width, result.height, result.weight, result.total);
                    //GHTK_CheckFeeShip();
                    l = result.length; w = result.width; h = result.height; we = result.weight; to = result.total;
                }
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }

};
