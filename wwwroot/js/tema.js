function toggleTheme() {
    const body = document.body;

    if (body.getAttribute("data-theme") === "dark") {
        body.setAttribute("data-theme", "light");
    } else {
        body.setAttribute("data-theme", "dark");
    }
}

document.getElementById("theme-toggle").addEventListener("click", toggleTheme);