import { api } from '../api.js';
import { fetchComments,addComment } from "../Interactions/Comments.js";

const urlParams = new URLSearchParams(window.location.search);
const postId = urlParams.get('id');



async function loadPost() {
    try {
        const post = await api.get(`Post/${postId}`);

        if (!post) {
            document.getElementById('postContainer').innerHTML = 
                '<p style="color:red">Post not found or access denied!</p>';
            return;
        }

        const currentUserid = parseInt(localStorage.getItem('userId'));
console.log('Post authorId:', post.authorId);
console.log('Current userId:', currentUserid);
console.log('Match:', post.authorId === currentUserid);
        document.getElementById('postContainer').innerHTML = `
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

                           <div class="d-flex gap-1">
        
                           <div class="post-reaction-total-box " id="reaction-count-${post.id}">
                 ${post.likeCount || 0}
                  </div>
                            <button class="btn-purple reaction-btn" 
                            data-post-id="${post.id}"
                              data-type="Like"
                              data-liked="false"
                              onclick="toggleReaction(this, ${post.id}, 'Like')">
                                👍<span class="like-count">${post.likeCount || 0}</span>
                            </button>
                            <button class="btn-purple reaction-btn" 
                              data-post-id="${post.id}"
                              data-type="Haha"
                              data-liked="false"
                              onclick="toggleReaction(this, ${post.id}, 'Haha')">
                                😂
                            </button>
                            <button class="btn-purple reaction-btn" 
                              data-post-id="${post.id}"
                              data-type="love"
                              data-liked="false"
                              onclick="toggleReaction(this, ${post.id}, 'love')">
                                   
                                ❤️
                            </button>
                              <button class="btn-purple reaction-btn" 
                            data-post-id="${post.id}"
                              data-type="Angry"
                              data-liked="false"
                              onclick="toggleReaction(this, ${post.id}, 'Angry')">
                                😠
                            </button>
                            <button class="btn-purple reaction-btn" 
                              data-post-id="${post.id}"
                              data-type="Care"
                              data-liked="false"
                              onclick="toggleReaction(this, ${post.id}, 'Care')">
                                🤗
                            </button>
                            <button class="btn-purple reaction-btn" 
                              data-post-id="${post.id}"
                              data-type="Celebrate"
                              data-liked="false"
                              onclick="toggleReaction(this, ${post.id}, 'Celebrate')">
                                   
                               🎉 
                            </button>
                      
                    </div>
                    <button class="btn-purple" onclick="savePost()">
                        <i class="fa-solid fa-bookmark"></i> Save Post
                    </button>

                    <a href="EditPosts.html?id=${postId}" class="btn-purple">
                        <i class="fa-solid fa-pen"></i> Edit Post
                    </a>
                     <a href="../../html/Report/Report-modal.html" class="btn-purple">
                       <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-postage-fill" viewBox="0 0 16 16">
  <path d="M4.5 3a.5.5 0 0 0-.5.5v9a.5.5 0 0 0 .5.5h7a.5.5 0 0 0 .5-.5v-9a.5.5 0 0 0-.5-.5z"/>
  <path d="M3.5 1a1 1 0 0 0 1-1h1a1 1 0 0 0 2 0h1a1 1 0 0 0 2 0h1a1 1 0 1 0 2 0H15v1a1 1 0 1 0 0 2v1a1 1 0 1 0 0 2v1a1 1 0 1 0 0 2v1a1 1 0 1 0 0 2v1a1 1 0 1 0 0 2v1h-1.5a1 1 0 1 0-2 0h-1a1 1 0 1 0-2 0h-1a1 1 0 1 0-2 0h-1a1 1 0 1 0-2 0H1v-1a1 1 0 1 0 0-2v-1a1 1 0 1 0 0-2V9a1 1 0 1 0 0-2V6a1 1 0 0 0 0-2V3a1 1 0 0 0 0-2V0h1.5a1 1 0 0 0 1 1M3 3v10a1 1 0 0 0 1 1h8a1 1 0 0 0 1-1V3a1 1 0 0 0-1-1H4a1 1 0 0 0-1 1"/>
</svg> Report Post
                    </a>
                    
                </div>

                <div id="saveMsg" class="success-msg mt-2"></div>
            </div>
        `;

    } catch (error) {
        document.getElementById('postContainer').innerHTML = 
            '<p style="color:#f85149">Failed to load post.</p>';
        console.error(error);
    }
}

/*async function loadComments() {
    try {
        const comments = await api.get(`Comment/${postId}/comments`);
        const container = document.getElementById('commentsContainer');

        if (!comments || comments.length === 0) {
            container.innerHTML = `
                <p style="color:#8b949e; margin-top:15px">
                    No comments yet. Be the first to comment!
                </p>`;
            return;
        }

        container.innerHTML = '';
        comments.forEach(comment => {
            container.innerHTML += `
                <div class="post-card" style="margin-top:15px">
                    <p style="color:#bca1ec; font-size:13px; margin-bottom:5px">
                        <i class="fa-solid fa-user"></i> 
                        ${comment.authorUsername || 'Unknown'} • 
                        ${new Date(comment.dateCreated).toLocaleDateString()}
                    </p>
                    <p style="color:#d1d5db">${comment.body}</p>
                </div>
            `;
        });

    } catch (error) {
        document.getElementById('commentsContainer').innerHTML = 
            '<p style="color:#f85149">Failed to load comments.</p>';
        console.error(error);
    }
}

async function addComment() {
    const body = document.getElementById('commentBody').value.trim();
    const errorMsg = document.getElementById('commentError');
    const successMsg = document.getElementById('commentSuccess');

    errorMsg.style.display = 'none';
    successMsg.style.display = 'none';

    if (!body) {
        errorMsg.textContent = 'Comment cannot be empty!';
        errorMsg.style.display = 'block';
        return;
    }

    try {
        const result = await api.post(`Comment/${postId}/comments`, {
            body: body,
            selfCommentId: null
        });

        if (result.message === 'Comment added successfully') {
            successMsg.textContent = 'Comment added!';
            successMsg.style.display = 'block';
            document.getElementById('commentBody').value = '';
            loadComments(); 
        } else {
            errorMsg.textContent = result.message || 'Failed to add comment';
            errorMsg.style.display = 'block';
        }

    } catch (error) {
        errorMsg.textContent = 'Failed to add comment. Are you logged in?';
        errorMsg.style.display = 'block';
        console.error(error);
    }
}*/

async function savePost() {
    try {
        const result = await api.post(`Post/${postId}/save`);
        const saveMsg = document.getElementById('saveMsg');

        saveMsg.textContent = result.message;
        saveMsg.style.display = 'block';
        saveMsg.style.color = result.message.includes('successfully') 
            ? '#bca1ec' : '#f85149';

    } catch (error) {
        console.error(error);
    }
}
document.addEventListener("DOMContentLoaded", async () => {

    if (!postId) {
        document.getElementById("postContainer").innerHTML =
            "<p style='color:red'>Post not found!</p>";
        return;
    }

    await loadPost();
    await fetchComments(postId);

    document
        .getElementById("postCommentBtn")
        .addEventListener("click", submitComment);

});
async function submitComment() {

    const body = document
        .getElementById("commentBody")
        .value
        .trim();

    if (!body) return;

    await addComment(postId, body);

    document.getElementById("commentBody").value = "";

    await fetchComments(postId);
}
window.savePost = savePost;