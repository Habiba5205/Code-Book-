import { api } from '../api.js';
console.log("Feed.js loaded");

const urlParams = new URLSearchParams(window.location.search);
let currentPage = parseInt(urlParams.get('page')) || 1;
const postsContainer = document.getElementById("postsContainer");

document.addEventListener("DOMContentLoaded", () => {
    loadFeed();
});
async function loadFeed() {
    console.log("loadFeed called!"); // ← add this
    console.trace(); 
    try {
        postsContainer.innerHTML = `<p style="color:white">Loading...</p>`;
        const posts = await api.get(`Post/feed?page=${currentPage}`);
        if (!posts || posts.length === 0) {
            postsContainer.innerHTML = `<p style="color:white">No posts found.</p>`;
            return;
        }

        postsContainer.innerHTML = "";
        posts.forEach(post => {
            const div=document.createElement("div");
            div.className="post-card"
            div.innerHTML = `
            
                    <h2 class="post-title">${post.title}</h2>
                    <p style="color:#8b949e;font-size:13px">
                        by ${post.authorUsername} • 
                        ${new Date(post.dateCreated).toLocaleDateString()}
                    </p>
                    <p class="post-body">${post.body}</p>

                    ${post.codeSnippet ? `
                    <pre class="code-snippet"><code>${post.codeSnippet}</code></pre>
                    ` : ''}

                    ${post.language ? `
                    <span style="color:#bca1ec;font-size:13px">
                        <i class="fa-solid fa-code"></i> ${post.language}
                    </span>
                    ` : ''}

                    <div class="post-actions">
                        <div class="d-flex gap-1">
                            <button class="btn-purple reaction-btn" 
                            data-post-id="${post.id}"
                              data-type="Like"
                              data-liked="false"
                              onclick="toggleReaction(this, ${post.id}, 'Like')">
                                👍<span class="like-count">${post.likeCount || 0}</span>
                            </button>
                            <button class="btn-purple reaction-btn" 
                              data-post-id="${post.id}"
                              data-type="Like"
                              data-liked="false"
                              onclick="toggleReaction(this, ${post.id}, 'Haha')">
                                😂<span class="like-count">${post.likeCount || 0}</span>
                            </button>
                            <button class="btn-purple reaction-btn" 
                              data-post-id="${post.id}"
                              data-type="Like"
                              data-liked="false"
                              onclick="toggleReaction(this, ${post.id}, 'love')">
                                   
                                ❤️<span class="like-count">${post.likeCount || 0}</span>
                            </button>
                        </div>

                        <button class="btn-purple" onclick="viewPost(${post.id})">
                            <i class="fa-solid fa-eye"></i> View Post
                        </button>
                    </div>

            `;
            postsContainer.appendChild(div);
        });

        console.log('Current page:', currentPage);
        // update page number
        document.getElementById('pageNumber').textContent = `Page ${currentPage}`;

        // show/hide prev button on page 1
        document.getElementById('prevBtn').style.display = 
            currentPage === 1 ? 'none' : 'inline-block';

    } catch (error) {
        postsContainer.innerHTML = `
            <p style="color:red">Failed to load posts. Please try again.</p>
        `;
        console.error(error);
    }
}

function changePage(direction) {
    if (currentPage + direction < 1) return;
    currentPage += direction;
    window.history.pushState({}, '', `Feed.html?page=${currentPage}`);
    loadFeed();
}

function viewPost(postId) {
    window.location.href = `PostDetail.html?id=${postId}`;
}