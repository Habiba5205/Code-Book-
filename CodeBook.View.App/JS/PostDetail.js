const urlParams = new URLSearchParams(window.location.search);
const postId = urlParams.get('id');

document.addEventListener('DOMContentLoaded', () => {
    if (!postId) {
        document.getElementById('postContainer').innerHTML = 
            '<p style="color:red">Post not found!</p>';
        return;
    }
    loadPost();
    loadComments();
});

async function loadPost() {
    try {
        const post = await api.get(`Post/${postId}`);

        if (!post) {
            document.getElementById('postContainer').innerHTML = 
                '<p style="color:red">Post not found or access denied!</p>';
            return;
        }

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
                    <button class="btn-purple" onclick="savePost()">
                        <i class="fa-solid fa-bookmark"></i> Save Post
                    </button>
                    <a href="edit-post.html?id=${postId}" class="btn-purple">
                        <i class="fa-solid fa-pen"></i> Edit Post
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

async function loadComments() {
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
}

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