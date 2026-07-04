import { api } from '../api.js';

let viewedUserId = null;

window.onload = async() =>{
    const params = new URLSearchParams(window.location.search);
    viewedUserId = params.get('userId');
    await loadProfile();
}
async function loadProfile(){
    try{
        const data = await api.get(`User/viewprofile?userId=${viewedUserId}`);

        document.getElementById('username').innerText = data.userName;
        document.getElementById('bio').innerText = data.bio || 'No bio yet';
        document.getElementById('followersCount').innerText = data.followersCount;
        document.getElementById('followingCount').innerText = data.followingCount;

        const avatar = document.getElementById('avatar');
         avatar.src = data.avatarUrl || 'images/default-avatar.png';

     
    }
    catch(error){
        console.error('Failed to load Profile:',error);
        
    }
}
async function follow(){
    try{
        await api.post(`User/follow?userid=${viewedUserId}`);
        alert('Followed!!');
        await loadProfile();
    }
       catch(error){
        alert('Failed to follow: '+error.message);
    }
}
async function unfollow(){
    try{
        await api.delete(`User/unfollow?userid=${viewedUserId}`);
        alert('UnFollowed!!');
        await loadProfile();
    }
       catch(error){
        alert('Failed to unfollow: '+error.message);
    }
}
window.follow = follow;
window.unfollow = unfollow;
