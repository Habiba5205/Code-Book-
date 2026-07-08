import { api } from '../api.js';

window.onload = () => {
    const button = document.getElementById('registerBtn');
    const usernameInput = document.getElementById('username');
    const emailInput = document.getElementById('email');
    const passwordInput = document.getElementById('password');
    const confirmPasswordInput = document.getElementById('confirmPassword');

    button.addEventListener('click',async() =>{
        const username = usernameInput.value;
        const email = emailInput.value;
        const password = passwordInput.value;
        const confirmPassword = confirmPasswordInput.value;

        if(!username || !email || !password || !confirmPassword){
            alert("Please fill all required fields!");
        return;
        }
        if(password !== confirmPassword){
            alert("Passwords do not match!")
            return;
        }
        try{
            button.innerText = "Creating acount...";
            button.disabled = true;

            await api.post('Auth/register',{
                UserName:username,
                Email:email,
                Password:password
            });

            alert("Account created successfully!!");
            window.location.href = "../../HTML/Auth/Login.html";
        }
        catch(error){
            alert("Registeration failed: " + error.message);
        }finally{
            button.innerText = "Submit";
            button.disabled = false;
        }
    });
}