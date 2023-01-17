/*Usage : To Checkout order
  Validating if there is address and items in cart
  After validation calling the CartController function - Checkout
*/
function Checkout() {
    var haveAddress = parseInt(document.getElementById("listAddress").value);
    var haveProduct = parseInt(document.getElementById("totalItems").value);
    if (haveAddress > 0 && haveProduct > 0) {
        $.ajax({
            type: "POST",
            contentType: "application/json",
            dataType: "JSON",
            data: JSON.stringify({ addressId: haveAddress }),
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

/*Usage : To calculate the sum after any update in the product
  Updating the calculated Product count and amount and pass into UI
*/
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
        document.getElementById("totalItems").value = totalItems;
    });
}

/*Usage : To update the product count
  Passing productId and product count into CartController function UpdateCart()
*/
function UpdateCartData() {
    const cartDom = document.querySelector(".card");
    const cartItemsDom = cartDom.querySelectorAll(".cart-items");
    var CartObj = [];
    cartItemsDom.forEach(cartItemDom => {
        var productId = StrToNum(cartItemDom.querySelector(".product-name").id);
        var cartCount = StrToNum(cartItemDom.querySelector(".cart_item_quantity").innerText);

        var item = {
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

/*Usage : To remove the product from cart
  Passing productId into CartController function RemoveProductFromCart()
*/
function RemoveProductFromCart(itemId) {

    var productToremove = {
        "productId": itemId
    };

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

/*Usage : To increase count of the product in cart
  Just updating in the UI and calling UpdateCartData(),CalculateSum() function
*/
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

/*Usage : To decrease count of the product in cart
  Just updating in the UI and calling UpdateCartData(),CalculateSum() function
*/
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

/*Usage : Replace all leading non-digits with nothing
  used in the functions to remove characters and take out number from string
*/
function StrToNum(strVal) {
    return parseInt(strVal.replace(/^\D+/g, ''));   
}

//Usage : On page load calling the function ShowCompleteAddress()
document.addEventListener('DOMContentLoaded', function () {
    ShowCompleteAddress();
}, false);

/*Usage : To Save address from the modal address
  Taking field values from the html and
  passing to the CartController function - SaveUserAddress()
*/
function SaveAddress() {
    var address = {
        address1: document.getElementById("address1").value,
        address2: document.getElementById("address2").value,
        city: document.getElementById("city").value,
        state: document.getElementById("state").value,
        postCode: document.getElementById("postCode").value,
        country: document.getElementById("country").value
    };

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

/*Usage : To show the complete address based on the selected dropdown address
  Taking the complete address from dropdown data and updating to label
*/
function ShowCompleteAddress() {
    var e = document.getElementById("listAddress");
    var option = e.options[e.selectedIndex];
    var fullAddress = option.getAttribute("data-complete");
    document.getElementById("labelAddress").innerHTML = 'Delivery Address - ' + fullAddress;
};

//Usage : function to reload the CartItems page and redirect CheckoutSuccess message view
function ShowSuccess() {
    window.location.href = '/Cart/CartItems';
    window.location.href = '/Cart/CheckoutSuccess';
}
//Usage : Function to reload the CartItems page
function ReloadCart() {
    window.location.href = '/Cart/CartItems';
}