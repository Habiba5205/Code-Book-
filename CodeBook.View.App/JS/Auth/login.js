import { api } from '../api.js';

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

                const response = await api.post('Auth/login',{
                    Password: password,
                    Email: email
                });

                if (response.role === 'Admin') {
                    window.location.href = "../../HTML/admin/dashboard.html";
                } else {
                   window.location.href = "../../HTML/Posts/Feed.html";
                }
                   
            } catch (error) {
                alert("Login failed: " + error.message);
            } finally {
            button.innerText = "Sign In";
            button.disabled = false;
            }
    });

}
const password = document.getElementById("password");
const togglePassword = document.getElementById("togglePassword");

togglePassword.addEventListener("click", () => {

    if(password.type === "password"){
        password.type = "text";
        togglePassword.classList.replace("fa-eye", "fa-eye-slash");
    }else{
        password.type = "password";
        togglePassword.classList.replace("fa-eye-slash", "fa-eye");
    }

});

