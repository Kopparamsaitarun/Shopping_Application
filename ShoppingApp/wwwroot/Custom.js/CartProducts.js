function Checkout() {
    var haveAdd = parseInt(document.getElementById("listAddress").value);
    var haveProduct = parseInt(document.getElementById("totalItems").innerHTML);
    if (haveAdd > 0 && haveProduct > 0) {
        $.ajax({
            type: "POST",
            contentType: "application/json",
            dataType: "JSON",
            data: JSON.stringify({ addressId: haveAdd }),
            url: "/Cart/Checkout",
            success: function (response) {
                if (response.success) {
                    ShowSuccess();
                } else {
                    alert(response.message);
                }
            },
            error: function (errormessgae) {
                alert(errormessgae);
            }
        });
    } else {
        alert("Please check! Address and Cart items are required")
    };

}
function CalculateSum() {
    const cartDom = document.querySelector(".card");
    const cartItemsDom = cartDom.querySelectorAll(".cart-items");
    var total = 0;
    var totalItems = 0;
    cartItemsDom.forEach(cartItemDom => {
        var cartCount = cartItemDom.querySelector(".cart_item_quantity").innerText;
        var cartAmount = cartItemDom.querySelector(".cart_item_amount").innerText;
        total += (StrToNum(cartCount) * StrToNum(cartAmount));
        totalItems += (StrToNum(cartCount));
        document.querySelector(".total").innerText = "₹ " + (Math.round(total * 100) / 100).toFixed(2);
        document.querySelector(".total-items").innerText = "TOTAL ITEMS " + totalItems;
    });
}
function UpdateCartData() {
    const cartDom = document.querySelector(".card");
    const cartItemsDom = cartDom.querySelectorAll(".cart-items");
    var CartObj = [];
    cartItemsDom.forEach(cartItemDom => {
        var productId = StrToNum(cartItemDom.querySelector(".product-name").id);
        var cartCount = StrToNum(cartItemDom.querySelector(".cart_item_quantity").innerText);

        var item = {
            userId: 1,//Sangeeth UserId hardcoded need to change this
            productId: productId,
            count: cartCount
        };
        CartObj.push(item);
    });

    $.ajax({
        type: "POST",
        contentType: "application/json",
        dataType: "JSON",
        data: JSON.stringify(CartObj),
        url: "/Cart/UpdateCart",
        success: function (response) {
            if (response.success) {
            } else {
                alert(response.message);
            }
        },
        error: function (errormessgae) {
            alert(errormessgae);
        }
    });

}
function RemoveProductFromCart(itemId) {

    var productToremove = {
        "productId": itemId
    };

    console.log(JSON.stringify(productToremove));
    console.log(productToremove);

    $.ajax({
        type: "POST",
        contentType: "application/json",
        dataType: "JSON",
        data: JSON.stringify(productToremove),
        url: "/Cart/RemoveProductFromCart",
        success: function (response) {
            if (response.success) {
                CalculateSum();
                ReloadCart();
            } else {
                alert(response.message);
            }
        },
        error: function (errormessgae) {
            alert(errormessgae);
        }
    });
}
function IncreaseCount(itemId) {
    const cartDom = document.querySelector(".card");
    const cartItemsDom = cartDom.querySelectorAll(".cart-items");
    cartItemsDom.forEach(cartItemDom => {
        if (parseInt(cartItemDom.querySelector(".product-name").id) === itemId) {
            var cartCount = parseInt(cartItemDom.querySelector(".cart_item_quantity").innerText) + 1;
            if (cartCount <= 10) {
                cartItemDom.querySelector(".cart_item_quantity").innerText = cartCount;
            }
            else {
                alert("Maximum allowed is 10");
            }
        };
    });
    CalculateSum();
    UpdateCartData()
}
function DecreaseCount(itemId) {
    const cartDom = document.querySelector(".card");
    const cartItemsDom = cartDom.querySelectorAll(".cart-items");
    cartItemsDom.forEach(cartItemDom => {
        if (parseInt(cartItemDom.querySelector(".product-name").id) === itemId) {
            var cartCount = parseInt(cartItemDom.querySelector(".cart_item_quantity").innerText) - 1;
            if (cartCount >= 1) {
                cartItemDom.querySelector(".cart_item_quantity").innerText = cartCount;
            }
            else {
                alert("Choose atleaset 1 count");
            }
        };
    });
    CalculateSum();
    UpdateCartData()
}
function RemoveItem() {
}
function StrToNum(strVal) {
    return parseInt(strVal.replace(/^\D+/g, '')); // replace all leading non-digits with nothing    
}
function ShowSuccess() {
    window.location.href = '/Cart/CartItems';
    window.location.href = '/Cart/CheckoutSuccess';
}
function ReloadCart() {
    window.location.href = '/Cart/CartItems';
}

document.addEventListener('DOMContentLoaded', function () {
    ShowCompleteAddress();
}, false);

function SaveAddress() {
    var address = {
        address1: document.getElementById("address1").value,
        address2: document.getElementById("address2").value,
        city: document.getElementById("city").value,
        state: document.getElementById("state").value,
        postCode: document.getElementById("postCode").value,
        country: document.getElementById("country").value
    };
    console.log(address);
    $.ajax({
        type: "POST",
        contentType: "application/json",
        dataType: "JSON",
        data: JSON.stringify(address),
        url: "/Cart/SaveUserAddress",
        success: function (response) {
            if (response.success) {
                $(".modal-body input").val("")
                $('#addressModal').modal('hide');
                ReloadCart();
                return false;
            } else {
                alert(response.message);
            }
        },
        error: function (errormessgae) {
            alert(errormessgae);
        }
    });
}

function ShowCompleteAddress() {
    var e = document.getElementById("listAddress");
    var option = e.options[e.selectedIndex];
    var fullAddress = option.getAttribute("data-complete");

    console.log(fullAddress);
    console.log(option.dataset.complete);

    document.getElementById("labelAddress").innerHTML = 'Delivery Address - ' + fullAddress;
};