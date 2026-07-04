import { api } from '../api.js';
let currentUserId = null;

function getPostId(){  //get post id from url 
    const params = new URLSearchParams(window.location.search);
    return params.get("id");
}


export async function fetchComments(postId) {
  try{  
    currentUserId=await getCurrentUserId();
    const comments =   await api.get(`Comment/${postId}/comments`);
    renderComments(comments);
}catch (error) {
    console.error("Error fetching comments:", error);
}
}

function renderComments(comments) {
    const commentsContainer = document.getElementById("commentsContainer");
    commentsContainer.innerHTML = ""; // Clear existing comments
    const topLevel=comments.filter(c=>c.selfCommentId===null);
     
    function getReplies(commentId) {
        return comments.filter(c => c.selfCommentId === commentId)
    }

    topLevel.forEach(comment => {
        const card = createCommentCard(comment, getReplies);
        commentsContainer.appendChild(card);
    });

    if(comments.length === 0) {
        commentsContainer.innerHTML = 
    `<p class='text-secondary'>No comments yet. Be the first to comment!</p>`;
}
}

function createCommentCard(comment, getReplies) {
    const div = document.createElement("div");
    div.className = "comment-card mb-2";

    div.innerHTML = `
        <div class="d-flex gap-2">
            <div class="flex-grow-1">
                <span class="fw-semibold">${comment.authorUsername}</span>
                <p class="mb-1">${comment.body}</p>

                <div class="d-flex gap-1 mt-2">
                    <button class="btn-purple comment-reaction"
                        data-reacted="false"
                        onclick="toggleCommentReaction(this, ${getPostId()}, ${comment.id}, 'Like')">
                        👍 <span class="reaction-count">${comment.likeCount || 0}</span>
                    </button>

                    <button class="btn-purple comment-reaction"
                        data-reacted="false"
                        onclick="toggleCommentReaction(this, ${getPostId()}, ${comment.id}, 'Haha')">
                        😂
                    </button>

                    <button class="btn-purple comment-reaction"
                        data-reacted="false"
                        onclick="toggleCommentReaction(this, ${getPostId()}, ${comment.id}, 'love')">
                        ❤️
                    </button>
                </div>

                <div class="d-flex gap-3 mt-1">
                    <small class="text-secondary">${formatTime(comment.dateCreated)}</small>

                    <button class="reply-btn btn btn-link btn-sm p-0">
                        Reply
                    </button>

                    ${
                        comment.authorId === currentUserId
                        ? `<button class="delete-comment-btn btn btn-link btn-sm p-0 text-danger">
                                Delete
                           </button>`
                        : ""
                    }

                    <button class="toggle-replies btn btn-link btn-sm p-0 text-secondary">
                        ▶ Replies
                    </button>
                </div>
            </div>
        </div>

        <div class="replies ms-4 mt-2 d-none" id="replies-${comment.id}"></div>
    `;

    // Replies
    const repliesContainer = div.querySelector(`#replies-${comment.id}`);
    const replies = getReplies(comment.id);

    replies.forEach(reply => {
        repliesContainer.appendChild(createCommentCard(reply, getReplies));
    });

    // Reply button
    div.querySelector(".reply-btn")
        ?.addEventListener("click", () => showReplyForm(comment.id));

    // Delete button
    div.querySelector(".delete-comment-btn")
        ?.addEventListener("click", () => deleteComment(comment.id));

    // Toggle replies
    const toggleBtn = div.querySelector(".toggle-replies");

    if (replies.length === 0) {
        toggleBtn.style.display = "none";
    } else {
        toggleBtn.addEventListener("click", () => {
            repliesContainer.classList.toggle("d-none");

            toggleBtn.textContent =
                repliesContainer.classList.contains("d-none")
                    ? "▶ Replies"
                    : "▼ Replies";
        });
    }

    return div;
}
async function getCurrentUserId() {
    
    return Number(localStorage.getItem("userId"));
}
export async function addComment(postId,body,selfCommentId=null){
try{
   
  await api.post(`Comment/${postId}/comments`,{
    body: body,
    selfCommentId: selfCommentId
});
 await fetchComments(postId);
}catch(error){
    console.error("Error adding comment:", error)
}
}
export async function deleteComment(commentId){
try{
await api.delete(`Comment/${commentId}/deleteComment`);
const postId=getPostId();
 await fetchComments(postId);
}catch(error){
    console.error("Error deleting comment:", error)
}
}
function formatTime(dateString) {
    const date = new Date(dateString+"Z");
    const now = new Date();
    const diffInMins = Math.floor((now - date) / 60000);

    if (diffInMins < 1)    return "Just now";
    if (diffInMins < 60)   return `${diffInMins} minutes ago`;
    if (diffInMins < 1440) return `${Math.floor(diffInMins / 60)} hours ago`;
    
    return `${Math.floor(diffInMins / 1440)} days ago`;
   
    
}
function showReplyForm(selfCommentId) {
    const postId = getPostId();

    const existing = document.getElementById(`reply-form-${selfCommentId}`);
    if (existing) {
        existing.remove();
        return;
    }

    const form = document.createElement("div");
    form.id = `reply-form-${selfCommentId}`;
    form.className = "ms-4 mt-2";
    form.innerHTML = `
        <div class="input-group">
            <input type="text" 
                   class="form-control form-control-sm" 
                   id="reply-input-${selfCommentId}"
                   placeholder="Write a reply...">
            <button class="btn btn-sm btn-purple" 
                    onclick="submitReply(${selfCommentId})">
                Reply
            </button>
        </div>
    `;

    const repliesContainer = document.getElementById(`replies-${selfCommentId}`);
    if (repliesContainer) {
        repliesContainer.prepend(form);
    }
}

async function submitReply(selfCommentId) {
    const input = document.getElementById(`reply-input-${selfCommentId}`);
    const body = input.value.trim();
    if (!body) return;

    const postId = getPostId();
    await addComment(postId, body, selfCommentId);
}

window.submitReply = submitReply; 