<<<<<<< HEAD
import { api } from './api.js';
=======
import { api } from './api.js'

function decodeToken(token) {
    try {
        return JSON.parse(atob(token.split('.')[1]));
    } catch { 
        return null; 
    }
}
>>>>>>> eddaa2474a06677e53d6de47fa1bb03c373ab1ed
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

                const payload = decodeToken(response.token);

                localStorage.setItem('token', payload.token);
                localStorage.setItem('userId', payload.nameid || payload.sub);
                localStorage.setItem('role', payload.role);

                if (payload.role === 'Admin') {
                    window.location.href = "../admin/dashboard.html";
                } else {
                    window.location.href = "../html/Posts/Feed.html";
                }
            } catch (error) {
                alert("Login failed: " + error.message);
            } finally {
            button.innerText = "Sign In";
            button.disabled = false;
            }
    });

}

