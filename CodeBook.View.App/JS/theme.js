
    function toggleTheme(){
        const checkbox = document.getElementById('themeToggle');
        document.body.classList.toggle('light-mode',checkbox.checked);
        document.cookie = `theme=${checkbox.checked ? 'light' : 'dark'}; path=/; max-age=31536000`;
    }
        (function() {
            const match = document.cookie.match(/theme=(light|dark)/);
            if(match && match[1] === 'light'){
                document.body.classList.add('light-mode');
                window.addEventListener('DOMContentLoaded',() =>{
                    document.getElementById('themeToggle');
                    const toggle = document.getElementById("themeToggle");
       if (toggle) {
        toggle.checked = true;
      }
                });
            }
        })();
    
