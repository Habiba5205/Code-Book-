import { api } from '../api.js';

let currentUser = null;

window.onload = async() =>{
    await loadProfile();
}
async function loadProfile(){
    try{
        const data = await api.get('User/viewmyprofile');
        currentUser = data;

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

function showProfile(){
document.getElementById('content').innerHTML = `
        <div class="card p-4">
            <h4>My Profile</h4>
            <hr style="border-color:#30363d">
            <p><strong>Username:</strong> ${currentUser?.userName || ''}</p>
            <p><strong>Bio:</strong> ${currentUser?.bio || 'No bio yet'}</p>
    
        </div>
    `;
}
function showEdit() {
    document.getElementById('content').innerHTML = `
        <div class="card p-4">
            <h4>Edit Profile</h4>
            <hr style="border-color:#30363d">
            <div class="mb-3">
                <label class="form-label">Username</label>
                <input type="text" class="form-control" id="editUsername" value="${currentUser?.userName || ''}">
            </div>
            <div class="mb-3">
                <label class="form-label">Bio</label>
                <textarea class="form-control" id="editBio" rows="3">${currentUser?.bio || ''}</textarea>
            </div>
            <div class="mb-3">
                <label class="form-label">Avatar URL</label>
                <input type="text" class="form-control" id="editAvatar" value="${currentUser?.avatarUrl || ''}">
            </div>
            <button class="btn btn-purple w-100" onclick="saveProfile()">
                Save Changes
            </button>
        </div>
    `;
}
async function saveProfile(){
    try{
        const username = document.getElementById('editUsername').value;
        const bio = document.getElementById('editBio').value;
        const avatarUrl = document.getElementById('editAvatar').value;

        await api.patch('User/updatemyprofile',{
            UserName : username,
            Bio : bio,
            AvatarUrl : avatarUrl
        });

         alert('Profile updated successfully!!');
         await loadProfile();
    }
    catch(error){
        alert('Failed to update: '+error.message);
    }
}
window.showProfile = showProfile;
window.showEdit = showEdit;
window.saveProfile = saveProfile;

async function showPosts() {
    document.getElementById('content').innerHTML = `
        <div class="card p-4">
            <h4>My Posts</h4>
            <hr style="border-color:#30363d">
            <div id="postsList">
                <p style="color:#8b949e">Loading posts...</p>
            </div>
        </div>
    `;

    try {
        const data = await api.get('Post/feed?page=1');
        const posts = data.filter (p=>p.authorUsername === currentUser.userName);

        if (posts.length === 0) {
            document.getElementById('postsList').innerHTML = `
                <p style="color:#8b949e">No posts yet!</p>
            `;
            return;
        }

        document.getElementById('postsList').innerHTML = posts.map(post => `
            <div class="post-card">
                <h5 class="post-title">${post.title}</h5>
                <p class="post-body">${post.body}</p>
                ${post.codeSnippet ? `
                    <pre class="code-snippet"><code>${post.codeSnippet}</code></pre>
                ` : ''}
                <small style="color:#8b949e">
                    ${new Date(post.dateCreated).toLocaleDateString()}
                </small>
            </div>
        `).join('');

    } catch (error) {
        document.getElementById('postsList').innerHTML = `
            <p style="color:#f85149">Failed to load posts!</p>
        `;
    }
}
window.showPosts = showPosts;

function showPassword() {
    window.location.href = '../Auth/ResetPassword.html';
}
window.showPassword = showPassword;

function showSaved() {
    window.location.href = '../Posts/SavedPosts.html';
}
async function showDelete() {
    const confirmed = confirm("Are you sure you want to delete your account? This cannot be undone!");
    
    if (confirmed) {
        try {
            await api.delete('User/deletemyprofile');
            alert("Account deleted successfully!");
            window.location.href = "../Auth/Login.html";
        } catch (error) {
            alert("Failed to delete: " + error.message);
        }
    }
}

window.showDelete = showDelete;

function showNotification() {
    window.location.href = 'Notifications.html';
}

window.showNotification = showNotification;

function showCommunity() {
    window.location.href = '../Community/Community.html';
}

function showFollowers() {
    window.location.href = 'Followers.html';
}

function showFollowing() {
    window.location.href = 'Followings.html';
}

window.showCommunity = showCommunity;
window.showFollowers = showFollowers;
window.showFollowing = showFollowing;
window.showSaved = showSaved;