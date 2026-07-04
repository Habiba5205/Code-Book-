import { api } from './api.js'
;
window.onload=()=>{
        const button = document.getElementById('loginBtn');
        const emailinput = document.getElementById('email');
        const passwordinput = document.getElementById('password');
        button.addEventListener('click', async () =>
        {
            const email = emailinput.value;
            const password = passwordinput.value;
            if(!email || !password){
                alert("Please Fill all the required fields!");
                return;
            }
            try{
                button.innerText = "Signing in"
                button.disabled = true;
                await api.post('auth/login',{
                    Password: password,
                    Email: email
                });

                //window.location.href = "../admin/dashboard.html";
                window.location.href = "../HTML/Community.html";
            }
            catch (error) {
            alert("Login failed: " + error.message);
            } finally {
            button.innerText = "Sign In";
            button.disabled = false;
            }
    });

}

