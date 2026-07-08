import { api } from '../api.js';

window.onload = () => {
    const button = document.getElementById('resetBtn');
    const oldPasswordInput = document.getElementById('oldPassword');
    const newPasswordInput = document.getElementById('newPassword');
    const confirmPasswordInput = document.getElementById('confirmPassword');

    button.addEventListener('click',async() =>{
        const oldPassword = oldPasswordInput.value;
        const newPassword = newPasswordInput.value;
        const confirmPassword = confirmPasswordInput.value;

        if( !oldPassword || !newPassword || !confirmPassword){
            alert("Please fill all required fields!");
        return;
        }
        if(newPassword !== confirmPassword){
            alert("Passwords do not match!")
            return;
        }
        try{
            button.innerText = "Resetting...";
            button.disabled = true;

            await api.patch('Auth/resetPassword',{
                Password : oldPassword,
                newPassword : newPassword
            });

            alert("Password changed successfully!!");
            window.location.href = "../../HTML/Auth/Login.html";
        }
        catch(error){
            alert("Reset failed: " + error.message);
        }finally{
            button.innerText = "Confirm";
            button.disabled = false;
        }
    });
}
function goBack(){
    window.history.back();
}
window.goBack = goBack;
function setupPasswordToggle(inputId, toggleId) {
    const input = document.getElementById(inputId);
    const toggle = document.getElementById(toggleId);

    toggle.addEventListener("click", () => {
        const isPassword = input.type === "password";

        input.type = isPassword ? "text" : "password";

        toggle.classList.toggle("fa-eye", !isPassword);
        toggle.classList.toggle("fa-eye-slash", isPassword);
    });
}

setupPasswordToggle("oldPassword", "toggleOldPassword");
setupPasswordToggle("newPassword", "toggleNewPassword");
setupPasswordToggle("confirmPassword", "toggleConfirmPassword");