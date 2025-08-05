// Shopping Cart Functionality
let cart = [];

// DOM Elements
const cartBtn = document.getElementById('cart-btn');
const closeCartBtn = document.getElementById('close-cart');
const cartSidebar = document.getElementById('cart-sidebar');
const cartOverlay = document.getElementById('cart-overlay');
const cartItemsContainer = document.getElementById('cart-items');
const cartTotal = document.getElementById('cart-total');
const cartCount = document.getElementById('cart-count');
const checkoutBtn = document.getElementById('checkout-btn');

// Toggle Cart Visibility
cartBtn.addEventListener('click', toggleCart);
closeCartBtn.addEventListener('click', toggleCart);
cartOverlay.addEventListener('click', toggleCart);

// Checkout
checkoutBtn.addEventListener('click', () => {
    alert('¡Gracias por tu compra! Total: ' + cartTotal.textContent);
    cart = [];
    updateCartUI();
    toggleCart();
});

function toggleCart() {
    cartSidebar.classList.toggle('translate-x-full');
    cartOverlay.classList.toggle('hidden');

    if (!cartSidebar.classList.contains('translate-x-full')) {
        document.body.style.overflow = 'hidden';
    } else {
        document.body.style.overflow = 'auto';
    }
}

function addToCart(name, price, image) {
    // Check if item already exists in cart
    const existingItem = cart.find(item => item.name === name);

    if (existingItem) {
        existingItem.quantity += 1;
    } else {
        cart.push({
            name,
            price,
            image,
            quantity: 1
        });
    }

    updateCartUI();

    // Show a quick notification
    const notification = document.createElement('div');
    notification.className = 'fixed bottom-4 right-4 bg-green-600 text-white py-2 px-4 rounded-md shadow-lg animate-bounce';
    notification.textContent = `¡${name} añadido al carrito!`;
    document.body.appendChild(notification);

    setTimeout(() => {
        notification.remove();
    }, 2000);
}

function updateCartUI() {
    // Update cart count
    const totalItems = cart.reduce((sum, item) => sum + item.quantity, 0);
    cartCount.textContent = totalItems;

    // Update cart items
    if (cart.length === 0) {
        cartItemsContainer.innerHTML = `
            <div class="text-center text-gray-500 py-8">
                <i class="fas fa-shopping-cart text-4xl mb-2"></i>
                <p>Tu carrito está vacío</p>
            </div>
        `;
        checkoutBtn.disabled = true;
    } else {
        cartItemsContainer.innerHTML = cart.map(item => `
            <div class="cart-item flex items-center">
                <div class="flex-shrink-0 mr-3">
                    <img src="${item.image}" alt="${item.name}" class="w-12 h-12 object-cover rounded" />
                </div>
                <div class="flex-grow">
                    <h4 class="font-medium">${item.name}</h4>
                    <p class="text-gray-600">$${item.price.toFixed(2)}</p>
                </div>
                <div class="flex items-center">
                    <button onclick="updateQuantity('${item.name}', -1)" class="text-gray-500 hover:text-green-600 p-1">
                        <i class="fas fa-minus"></i>
                    </button>
                    <span class="mx-2">${item.quantity}</span>
                    <button onclick="updateQuantity('${item.name}', 1)" class="text-gray-500 hover:text-green-600 p-1">
                        <i class="fas fa-plus"></i>
                    </button>
                </div>
            </div>
        `).join('');

        checkoutBtn.disabled = false;
    }

    // Update total
    const total = cart.reduce((sum, item) => sum + (item.price * item.quantity), 0);
    cartTotal.textContent = `$${total.toFixed(2)}`;
}

function updateQuantity(name, change) {
    const item = cart.find(item => item.name === name);

    if (item) {
        item.quantity += change;

        // Remove item if quantity is 0 or less
        if (item.quantity <= 0) {
            cart = cart.filter(i => i.name !== name);
        }

        updateCartUI();
    }
}

    document.addEventListener('DOMContentLoaded', function() {
                const cartBtn = document.getElementById('cart-btn');
    const cartSidebar = document.getElementById('cart-sidebar');
    const closeCartBtn = document.getElementById('close-cart');
    const cartOverlay = document.getElementById('cart-overlay');

    cartBtn.addEventListener('click', function() {
        cartSidebar.classList.remove('translate-x-full');
    cartSidebar.classList.add('translate-x-0');
    cartOverlay.classList.remove('hidden');
                });

    function closeCart() {
        cartSidebar.classList.remove('translate-x-0');
    cartSidebar.classList.add('translate-x-full');
    cartOverlay.classList.add('hidden');
                }

    closeCartBtn.addEventListener('click', closeCart);
    cartOverlay.addEventListener('click', closeCart);
            });


