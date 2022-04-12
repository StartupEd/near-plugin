export function redirectToStripeCheckout(publishKey, sessionId) {
    var stripe = Stripe(publishKey);
    stripe.redirectToCheckout({
        sessionId: sessionId
    }).then(function (result) {
        if (result.error) {
            var displayError = document.getElementById("stripe-error");
            displayError.textContent = result.error.message;
        }
    });
};






window.bindCheckout = () => {
    var stripe = Stripe("pk_test_51HWG3mKv58B9Y3srxNgRGkO54i9fDsuM0t1wcpUmyObtr8EUOCsveFFEfACe6Q9MwyP6wmCi1ApscEpobUUZms1q00dWN2fuNt");

    var checkoutButton = document.getElementById("checkout-button");
    var checkoutButton2 = document.getElementById("checkout-button2");
    var checkoutButton3 = document.getElementById("checkout-button3");
    console.log("bind checkout worked");
    checkoutButton.addEventListener("click", function () {
        // Todo - Shreya to fix the hardcoded DNS
        fetch("http://localhost:5000/create-session", {
            method: "POST",
        })
            .then(function (response) {
                return response.json();
            })
            .then(function (session) {
                return stripe.redirectToCheckout({ sessionId: session.id });
            })
            .then(function (result) {
                // If redirectToCheckout fails due to a browser or network
                // error, you should display the localized error message to your
                // customer using error.message.
                if (result.error) {
                    alert(result.error.message);
                }
            })
            .catch(function (error) {
                console.error("Error:", error);
            });
    });
    checkoutButton2.addEventListener("click", function () {
        // Todo - Shreya to fix the hardcoded DNS
        fetch("http://localhost:5000/create-session", {
            method: "POST",
        })
            .then(function (response) {
                return response.json();
            })
            .then(function (session) {
                return stripe.redirectToCheckout({ sessionId: session.id });
            })
            .then(function (result) {
                // If redirectToCheckout fails due to a browser or network
                // error, you should display the localized error message to your
                // customer using error.message.
                if (result.error) {
                    alert(result.error.message);
                }
            })
            .catch(function (error) {
                console.error("Error:", error);
            });
    });
    checkoutButton3.addEventListener("click", function () {
        // Todo - Shreya to fix the hardcoded DNS
        fetch("http://localhost:5000/create-session", {
            method: "POST",
        })
            .then(function (response) {
                return response.json();
            })
            .then(function (session) {
                return stripe.redirectToCheckout({ sessionId: session.id });
            })
            .then(function (result) {
                // If redirectToCheckout fails due to a browser or network
                // error, you should display the localized error message to your
                // customer using error.message.
                if (result.error) {
                    alert(result.error.message);
                }
            })
            .catch(function (error) {
                console.error("Error:", error);
            });
    });
};

window.addCart = () => {
    var shoppingCart = (function () {
        //properties & methods
        cart = [];

        // Constructor
        function Item(name, price, count) {
            this.name = name;
            this.price = price;
            this.count = count;
        }

        // Save cart
        function saveCart() {
            sessionStorage.setItem('shoppingCart', JSON.stringify(cart));
        }

        // Load cart
        function loadCart() {
            cart = JSON.parse(sessionStorage.getItem('shoppingCart'));
        }
        if (sessionStorage.getItem("shoppingCart") != null) {
            loadCart();
        }


        // =============================
        // Public methods and propeties
        // =============================
        var obj = {};

        // Add to cart
        obj.addItemToCart = function (name, price, count) {
            for (var item in cart) {
                if (cart[item].name === name) {
                    cart[item].count++;
                    saveCart();
                    return;
                }
            }
            var item = new Item(name, price, count);
            cart.push(item);
            saveCart();
        }
        // Set count from item
        obj.setCountForItem = function (name, count) {
            for (var i in cart) {
                if (cart[i].name === name) {
                    cart[i].count = count;
                    break;
                }
            }
        };
        // Remove item from cart
        obj.removeItemFromCart = function (name) {
            for (var item in cart) {
                if (cart[item].name === name) {
                    cart[item].count--;
                    if (cart[item].count === 0) {
                        cart.splice(item, 1);
                    }
                    break;
                }
            }
            saveCart();
        }

        // Remove all items from cart
        obj.removeItemFromCartAll = function (name) {
            for (var item in cart) {
                if (cart[item].name === name) {
                    cart.splice(item, 1);
                    break;
                }
            }
            saveCart();
        }

        // Clear cart
        obj.clearCart = function () {
            cart = [];
            saveCart();
        }

        // Count cart 
        obj.totalCount = function () {
            var totalCount = 0;
            for (var item in cart) {
                totalCount += cart[item].count;
            }
            return totalCount;
        }

        // Total cart
        obj.totalCart = function () {
            var totalCart = 0;
            for (var item in cart) {
                totalCart += cart[item].price * cart[item].count;
            }
            return Number(totalCart.toFixed(2));
        }

        // List cart
        obj.listCart = function () {
            var cartCopy = [];
            for (i in cart) {
                item = cart[i];
                itemCopy = {};
                for (p in item) {
                    itemCopy[p] = item[p];

                }
                itemCopy.total = Number(item.price * item.count).toFixed(2);
                cartCopy.push(itemCopy)
            }
            return cartCopy;
        }


        return obj;
    })();

    // Add item
    $('.add-to-cart').click(function (event) {
        event.preventDefault();
        var name = $(this).data('name');
        var price = Number($(this).data('price'));
        shoppingCart.addItemToCart(name, price, 1);
        displayCart();
    });

    // Clear items
    $('.clear-cart').click(function () {
        shoppingCart.clearCart();
        displayCart();
    });


    function displayCart() {
        var cartArray = shoppingCart.listCart();
        var output = "";
        for (var i in cartArray) {
            output += "<tr>"
                + "<td>" + cartArray[i].name + "</td>"
                + "<td>(" + cartArray[i].price + ")</td>"
                + "<td><div class='input-group'><button class='minus-item input-group-addon btn btn-primary' data-name=" + cartArray[i].name + ">-</button>"
                + "<input type='number' class='item-count form-control' data-name='" + cartArray[i].name + "' value='" + cartArray[i].count + "'>"
                + "<button class='plus-item btn btn-primary input-group-addon' data-name=" + cartArray[i].name + ">+</button></div></td>"
                + "<td><button class='delete-item btn btn-danger' data-name=" + cartArray[i].name + ">X</button></td>"
                + " = "
                + "<td>" + cartArray[i].total + "</td>"
                + "</tr>";
        }
        $('.show-cart').html(output);
        $('.total-cart').html(shoppingCart.totalCart());
        $('.total-count').html(shoppingCart.totalCount());
    }

    // Delete item button

    $('.show-cart').on("click", ".delete-item", function (event) {
        var name = $(this).data('name')
        shoppingCart.removeItemFromCartAll(name);
        displayCart();
    })


    // -1
    $('.show-cart').on("click", ".minus-item", function (event) {
        var name = $(this).data('name')
        shoppingCart.removeItemFromCart(name);
        displayCart();
    })
    // +1
    $('.show-cart').on("click", ".plus-item", function (event) {
        var name = $(this).data('name')
        shoppingCart.addItemToCart(name);
        displayCart();
    })

    // Item count input
    $('.show-cart').on("change", ".item-count", function (event) {
        var name = $(this).data('name');
        var count = Number($(this).val());
        shoppingCart.setCountForItem(name, count);
        displayCart();
    });

    displayCart();
};


window.rangeSlider = () => {
    // Class definition
    var KTnoUiSliderDemos = function () {

        // Private functions
        var demo1 = function () {
            // init slider
            var slider = document.getElementById('kt_nouislider_1');

            noUiSlider.create(slider, {
                start: [0],
                step: 2,
                range: {
                    'min': [0],
                    'max': [10]
                },
                format: wNumb({
                    decimals: 0
                })
            });

            // init slider input
            var sliderInput = document.getElementById('kt_nouislider_1_input');

            slider.noUiSlider.on('update', function (values, handle) {
                sliderInput.value = values[handle];
            });

            sliderInput.addEventListener('change', function () {
                slider.noUiSlider.set(this.value);
            });
        }

        var demo2 = function () {
            // init slider
            var slider = document.getElementById('kt_nouislider_2');

            noUiSlider.create(slider, {
                start: [0],
                connect: [true, false],
                step: 1,
                range: {
                    'min': [0],
                    'max': [10]
                },
                format: wNumb({
                    decimals: 3,
                    thousand: '.',
                    postfix: ' ',
                })
            });

            // init slider input
            var sliderInput = document.getElementById('kt_nouislider_2_input');

            slider.noUiSlider.on('update', function (values, handle) {
                sliderInput.value = values[handle];
            });

            sliderInput.addEventListener('change', function () {
                slider.noUiSlider.set(this.value);
            });
        }

        var demo3 = function () {
            // init slider
            var slider = document.getElementById('kt_nouislider_3');

            noUiSlider.create(slider, {
                start: [20, 80],
                connect: true,
                direction: 'rtl',
                tooltips: [true, wNumb({ decimals: 1 })],
                range: {
                    'min': [0],
                    '10%': [10, 10],
                    '50%': [80, 50],
                    '80%': 150,
                    'max': 200
                }
            });


            // init slider input
            var sliderInput0 = document.getElementById('kt_nouislider_3_input');
            var sliderInput1 = document.getElementById('kt_nouislider_3.1_input');
            var sliderInputs = [sliderInput1, sliderInput0];

            slider.noUiSlider.on('update', function (values, handle) {
                sliderInputs[handle].value = values[handle];
            });
        }

        var demo4 = function () {

            var slider = document.getElementById('kt_nouislider_input_select');

            // Append the option elements
            for (var i = -20; i <= 40; i++) {

                var option = document.createElement('option');
                option.text = i;
                option.value = i;

                slider.appendChild(option);
            }

            // init slider
            var html5Slider = document.getElementById('kt_nouislider_4');

            noUiSlider.create(html5Slider, {
                start: [10, 30],
                connect: true,
                range: {
                    'min': -20,
                    'max': 40
                }
            });

            // init slider input
            var inputNumber = document.getElementById('kt_nouislider_input_number');

            html5Slider.noUiSlider.on('update', function (values, handle) {

                var value = values[handle];

                if (handle) {
                    inputNumber.value = value;
                } else {
                    slider.value = Math.round(value);
                }
            });

            slider.addEventListener('change', function () {
                html5Slider.noUiSlider.set([this.value, null]);
            });

            inputNumber.addEventListener('change', function () {
                html5Slider.noUiSlider.set([null, this.value]);
            });
        }

        var demo5 = function () {
            // init slider
            var slider = document.getElementById('kt_nouislider_5');

            noUiSlider.create(slider, {
                start: 20,
                range: {
                    min: 0,
                    max: 100
                },
                pips: {
                    mode: 'values',
                    values: [20, 80],
                    density: 4
                }
            });

            var sliderInput = document.getElementById('kt_nouislider_5_input');

            slider.noUiSlider.on('update', function (values, handle) {
                sliderInput.value = values[handle];
            });

            sliderInput.addEventListener('change', function () {
                slider.noUiSlider.set(this.value);
            });

            slider.noUiSlider.on('change', function (values, handle) {
                if (values[handle] < 20) {
                    slider.noUiSlider.set(20);
                } else if (values[handle] > 80) {
                    slider.noUiSlider.set(80);
                }
            });
        }

        var demo6 = function () {
            // init slider

            var verticalSlider = document.getElementById('kt_nouislider_6');

            noUiSlider.create(verticalSlider, {
                start: 40,
                orientation: 'vertical',
                range: {
                    'min': 0,
                    'max': 100
                }
            });

            // init slider input
            var sliderInput = document.getElementById('kt_nouislider_6_input');

            verticalSlider.noUiSlider.on('update', function (values, handle) {
                sliderInput.value = values[handle];
            });

            sliderInput.addEventListener('change', function () {
                verticalSlider.noUiSlider.set(this.value);
            });
        }

        // Modal demo

        var modaldemo1 = function () {
            var slider = document.getElementById('kt_nouislider_modal1');

            noUiSlider.create(slider, {
                start: [0],
                step: 2,
                range: {
                    'min': [0],
                    'max': [10]
                },
                format: wNumb({
                    decimals: 0
                })
            });

            // init slider input
            var sliderInput = document.getElementById('kt_nouislider_modal1_input');

            slider.noUiSlider.on('update', function (values, handle) {
                sliderInput.value = values[handle];
            });

            sliderInput.addEventListener('change', function () {
                slider.noUiSlider.set(this.value);
            });
        }

        var modaldemo2 = function () {
            var slider = document.getElementById('kt_nouislider_modal2');

            noUiSlider.create(slider, {
                start: [20000],
                connect: [true, false],
                step: 1000,
                range: {
                    'min': [20000],
                    'max': [80000]
                },
                format: wNumb({
                    decimals: 3,
                    thousand: '.',
                    postfix: ' (US $)',
                })
            });

            // init slider input
            var sliderInput = document.getElementById('kt_nouislider_modal2_input');

            slider.noUiSlider.on('update', function (values, handle) {
                sliderInput.value = values[handle];
            });

            sliderInput.addEventListener('change', function () {
                slider.noUiSlider.set(this.value);
            });
        }

        var modaldemo3 = function () {
            var slider = document.getElementById('kt_nouislider_modal3');

            noUiSlider.create(slider, {
                start: [20, 80],
                connect: true,
                direction: 'rtl',
                tooltips: [true, wNumb({ decimals: 1 })],
                range: {
                    'min': [0],
                    '10%': [10, 10],
                    '50%': [80, 50],
                    '80%': 150,
                    'max': 200
                }
            });


            // init slider input
            var sliderInput0 = document.getElementById('kt_nouislider_modal1.1_input');
            var sliderInput1 = document.getElementById('kt_nouislider_modal1.2_input');
            var sliderInputs = [sliderInput1, sliderInput0];

            slider.noUiSlider.on('update', function (values, handle) {
                sliderInputs[handle].value = values[handle];
            });
        }
        return {
            // public functions
            init: function () {
                demo1();
                demo2();
                demo3();
                demo4();
                demo5();
                demo6();
                modaldemo1();
                modaldemo2();
                modaldemo3();
            }
        };
    }();

    jQuery(document).ready(function () {
        KTnoUiSliderDemos.init();
    });
};