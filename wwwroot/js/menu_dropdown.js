// Função para alternar a visibilidade do menu dropdown com animação
function toggleDropdown() {
    const menu = document.getElementById('dropdownMenu');
    const isVisible = menu.style.opacity === '1';

    if (isVisible) {
        // Esconder o menu com animação
        menu.style.opacity = '0';
        menu.style.maxHeight = '0';
    } else {
        // Mostrar o menu com animação
        menu.style.display = 'block'; // Assegura que o menu é exibido
        setTimeout(() => {
            menu.style.opacity = '1';
            menu.style.maxHeight = '500px'; // Defina um valor máximo para a altura
        }, 10); // O atraso é para garantir que o display: block seja aplicado antes da animação
    }
}

// Fechar o menu ao clicar fora
window.addEventListener('click', function (event) {
    const menu = document.getElementById('dropdownMenu');
    const button = document.querySelector('.profile-dropdown-btn');

    // Verifica se o clique foi fora do botão ou do menu
    if (!button.contains(event.target) && !menu.contains(event.target)) {
        menu.style.opacity = '0';
        menu.style.maxHeight = '0';
    }
});

function toggleAccessibilityMenu() {
    const menu = document.getElementById("accessibilityMenu");
    menu.style.display = menu.style.display === "flex" ? "none" : "flex";
}

function increaseFont() {
    document.body.style.fontSize = "larger";
}

function decreaseFont() {
    document.body.style.fontSize = "smaller";
}

let isHighContrast = false;
function toggleContrast() {
    document.body.classList.toggle("high-contrast");
    isHighContrast = !isHighContrast;
}

let isFocusMode = false;
function toggleFocusMode() {
    if (isFocusMode) {
        document.body.style.filter = "none";
    } else {
        document.body.style.filter = "grayscale(100%) brightness(1.1)";
    }
    isFocusMode = !isFocusMode;
}
