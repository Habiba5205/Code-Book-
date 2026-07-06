import { api } from '../api.js';

window.onload = async () => {
   var followingsContainer =   document.getElementById("followings-container");
   await loadFollowings(followingsContainer);
};

async function loadFollowings(followingsContainer) {
    try{
        const followings = await api.get('User/followings');

        if(!followings || followings.length===0){
            followingsContainer.innerHTML =  '<span style="color: Red;">No followings found.</span>';
            return;
        }

        followingsContainer.innerHTML = '';
        followings.forEach(user => {
            const followingCard = document.createElement('div');
            followingCard.className = 'following-card';
            followingCard.innerHTML = `
                <a href="OtherUserProfile.html?userId=${user.id}" class="card p-4 text-decoration-none text-dark shadow-sm hover-card">
                <div class="row align-items-center">
                <div class="col-md-3 text-center">
                <img src="${user.avatarUrl ? user.avatarUrl : 'images/default-avatar.png'}"
                alt="Profile Avatar"
                class="profile-img">
                </div>
                <div class="col-md-9">
                <h2 class="text-light mb-1">${user.userName}</h2>
                <p class="text-white-50">${user.bio || 'No bio yet'}</p>
                </div>
                </div>
                </a>`;
                followingsContainer.appendChild(followingCard);
         
        });
}
catch(error){
    alert("Couldn't load Followings: " + error.message);
}
};

function goBack() {
    window.history.back();
}
window.goBack = goBack;