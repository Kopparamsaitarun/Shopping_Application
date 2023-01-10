function CalculateSum() {
    const cartDom = document.querySelector(".card");
    const cartItemsDom = cartDom.querySelectorAll(".cart-items");
    var total = 0;
    cartItemsDom.forEach(cartItemDom => {
        var cartCount = parseInt(cartItemDom.querySelector(".cart_item_quantity").innerText);
        var cartAmount = parseInt(1000);
        total += (cartCount * cartAmount);
        document.querySelector(".total").innerText = total;
    });
}
function UpdateCartData() {

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
}
function RemoveItem() {
}