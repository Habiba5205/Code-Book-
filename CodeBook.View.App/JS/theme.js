document.addEventListener("DOMContentLoaded", () => {

    const toggle = document.getElementById("themeToggle");
    const match = document.cookie.match(/theme=(light|dark)/);

    if (match && match[1] === "light") {
        document.body.classList.add("light-theme");
        if (toggle) {
            toggle.checked = true;
        }
    }

    if (toggle) {
        toggle.addEventListener("change", () => {

            if (toggle.checked) {
                document.body.classList.remove("light-theme");
                document.cookie = "theme=light; path=/; max-age=31536000";
            } else {
                document.body.classList.add("light-theme");
                document.cookie = "theme=dark; path=/; max-age=31536000";
            }

        });
    }

});