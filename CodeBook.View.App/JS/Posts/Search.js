import { api } from '../api.js';

let currentTab = 'posts';

document.getElementById('searchInput').addEventListener('keypress', (e) => {
    if (e.key === 'Enter') search();
});

document.getElementById('languageFilter').addEventListener('change', () => {
    search();
});

function switchTab(tab) {
    currentTab = tab;

    document.querySelectorAll('[id^="tab-"]').forEach(btn => {
        btn.style.backgroundColor = 'transparent';
        btn.style.color = '#7c3aed';
    });
    document.getElementById(`tab-${tab}`).style.backgroundColor = '#7c3aed';
    document.getElementById(`tab-${tab}`).style.color = 'white';

    if (tab === 'posts') {
        document.getElementById('postFilters').style.display = 'flex';
        document.getElementById('otherFilters').style.display = 'none';
    } else {
        document.getElementById('postFilters').style.display = 'none';
        document.getElementById('otherFilters').style.display = 'block';
    }

    document.getElementById('resultsContainer').innerHTML = 
        `<p style="color:#8b949e">Search for ${tab} above...</p>`;
    document.getElementById('resultsCount').style.display = 'none';

    const keyword = document.getElementById('searchInput').value.trim();
    if (keyword) search();
}

async function search() {
    const keyword = document.getElementById('searchInput').value.trim();
    const resultsContainer = document.getElementById('resultsContainer');
    const resultsCount = document.getElementById('resultsCount');

    resultsContainer.innerHTML = `<p style="color:white">Searching...</p>`;
    resultsCount.style.display = 'none';

    try {
        let results = [];

        if (currentTab === 'posts') {
            results = await searchPosts(keyword);
        } else if (currentTab === 'users') {
            results = await searchUsers(keyword);
        } else if (currentTab === 'communities') {
            results = await searchCommunities(keyword);
        }

        if (!results || results.length === 0) {
            resultsContainer.innerHTML = `
                <p style="color:#8b949e">
                    No ${currentTab} found. Try different keywords!
                </p>`;
            return;
        }

        resultsCount.textContent = `Found ${results.length} ${currentTab}`;
        resultsCount.style.display = 'block';

        resultsContainer.innerHTML = '';
        if (currentTab === 'posts') renderPosts(results);
        else if (currentTab === 'users') renderUsers(results);
        else if (currentTab === 'communities') renderCommunities(results);

    } catch (error) {
        resultsContainer.innerHTML = `
            <p style="color:#f85149">Search failed. Please try again.</p>`;
        console.error(error);
    }
}


async function searchPosts(keyword) {
    const language = document.getElementById('languageFilter').value;
    const tag = document.getElementById('tagFilter').value.trim();

    const params = new URLSearchParams();
    if (keyword) params.append('keyword', keyword);
    if (language) params.append('language', language);
    if (tag) params.append('tag', tag);

    return await api.get(`Post/search?${params.toString()}`);
}


async function searchUsers(keyword) {
    if (!keyword) return [];
    return await api.get(`User/search?keyword=${encodeURIComponent(keyword)}`);
}


async function searchCommunities(keyword) {
    if (!keyword) return [];
    return await api.get(`Community/search?keyword=${encodeURIComponent(keyword)}`);
}


function renderPosts(posts) {
    const container = document.getElementById('resultsContainer');
    posts.forEach(post => {
        container.innerHTML += `
            <div class="post-card">
                <h2 class="post-title">${post.title}</h2>
                <p style="color:#8b949e; font-size:13px">
                    by ${post.authorUsername || 'Unknown'} • 
                    ${new Date(post.dateCreated).toLocaleDateString()}
                </p>
                <p class="post-body">${post.body}</p>
                ${post.codeSnippet ? `
                <pre class="code-snippet"><code>${post.codeSnippet}</code></pre>
                ` : ''}
                ${post.language ? `
                <span style="color:#bca1ec; font-size:13px">
                    <i class="fa-solid fa-code"></i> ${post.language}
                </span>
                ` : ''}
                <div class="post-actions mt-3">
                    <button class="btn-purple" onclick="viewPost(${post.id})">
                        <i class="fa-solid fa-eye"></i> View Post
                    </button>
                </div>
            </div>
        `;
    });
}

function renderUsers(users) {
    const container = document.getElementById('resultsContainer');
    users.forEach(user => {
        container.innerHTML += `
            <div class="post-card d-flex align-items-center gap-3">
                <img src="${user.profilePicURL || 'https://via.placeholder.com/50'}" 
                     style="width:50px; height:50px; border-radius:50%; 
                            border:2px solid #7c3aed; object-fit:cover">
                <div>
                    <p style="color:white; font-weight:bold; margin:0">
                        ${user.userName}
                    </p>
                    <p style="color:#8b949e; font-size:13px; margin:0">
                        ${user.bio || 'No bio yet'}
                    </p>
                </div>
                <button class="btn-purple ms-auto" 
                        onclick="viewProfile(${user.id})">
                    <i class="fa-solid fa-user"></i> View Profile
                </button>
            </div>
        `;
    });
}

// Render Communities
function renderCommunities(communities) {
    const container = document.getElementById('resultsContainer');
    communities.forEach(community => {
        container.innerHTML += `
            <div class="post-card d-flex align-items-center gap-3">
                <img src="${community.iconURL || 'https://via.placeholder.com/50'}" 
                     style="width:50px; height:50px; border-radius:50%; 
                            border:2px solid #7c3aed; object-fit:cover">
                <div>
                    <p style="color:white; font-weight:bold; margin:0">
                        ${community.name}
                    </p>
                    <p style="color:#8b949e; font-size:13px; margin:0">
                        ${community.description || 'No description'}
                    </p>
                </div>
                <button class="btn-purple ms-auto" 
                        onclick="viewCommunity(${community.id})">
                    <i class="fa-solid fa-people-group"></i> View Community
                </button>
            </div>
        `;
    });
}

function viewPost(postId) {
    window.location.href = `PostDetail.html?id=${postId}`;
}

function viewProfile(userId) {
    window.location.href = `../html/User/UserProfile.html?id=${userId}`;
}

function viewCommunity(communityId) {
    window.location.href = `../html/Community.html?id=${communityId}`;
}

window.switchTab = switchTab;
window.search = search;
window.viewPost = viewPost;
window.viewProfile = viewProfile;
window.viewCommunity = viewCommunity;