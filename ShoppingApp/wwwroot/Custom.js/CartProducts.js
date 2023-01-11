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
            userId: 1,
            productId: productId,            
            count: cartCount
        };
        CartObj.push(item);
    });
    console.log(JSON.stringify(CartObj));
    console.log(CartObj);
    $.ajax({
        type: "POST",
        contentType:"application/json",
        dataType: "JSON",
        data: JSON.stringify(CartObj),
        url: "/Home/UpdateCart",
        success: function (response) {
            if (true) {
                alert("Updated");
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
