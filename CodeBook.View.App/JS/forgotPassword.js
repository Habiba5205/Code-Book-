import { api } from './api.js';

window.onload = () => {
    const button = document.getElementById('resetBtn');
    const emailInput = document.getElementById('email');
    const passwordInput = document.getElementById('password');
    const confirmPasswordInput = document.getElementById('confirmPassword');

    button.addEventListener('click',async() =>{
        const email = emailInput.value;
        const password = passwordInput.value;
        const confirmPassword = confirmPasswordInput.value;

        if( !email || !password || !confirmPassword){
            alert("Please fill all required fields!");
        return;
        }
        if(password !== confirmPassword){
            alert("Passwords do not match!")
            return;
        }
        try{
            button.innerText = "Resetting...";
            button.disabled = true;

            await api.post("Auth/forgotPassword",{
                Email:email,
                NewPassword:password
            });

            alert("Password reset successfully!!");
            window.location.href = "Login.html";
        }
        catch(error){
            alert("Reset failed: " + error.message);
        }finally{
            button.innerText = "Confirm";
            button.disabled = false;
        }
    });
}