import { api } from './api.js';


function getPostId(){  //get post id from url 
    const params = new URLSearchParams(window.location.search);
    return params.get("id");
}

async function initComments() {
    const postId = getPostId();
    if(!postId){
        console.error("Post ID not found in URL");
        return;
    }
    await fetchComments(postId);
}
document.addEventListener("DOMContentLoaded", () => {
    initComments();
});

async function fetchComments(postId) {
  try{  
    const comments =   await api.get(`posts/${postId}/comments`);
    renderComments(comments);
}catch (error) {
    console.error("Error fetching comments:", error);
}
}

function renderComments(comments) {
    const commentsContainer = document.getElementById("comments-container");
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
        <span class="fw-semibold">${comment.authorName}</span>
        <p class="mb-1">${comment.body}</p>
        <div class="d-flex gap-3">
        <small class="text-secondary">${formatTime(comment.dateCreated)}</small>
          <button class="reply-btn btn btn-link btn-sm p-0"
                            data-comment-id="${comment.id}">
                        Reply
                      </button>
                    ${comment.authorId === getCurrentUserId() ? 
                        `<button class="delete-comment-btn btn btn-link btn-sm p-0 text-danger"
                                 data-comment-id="${comment.id}">
                            Delete
                        </button>` : ""}
                </div>
            </div>
        </div>
        <div class="replies ms-4 mt-2" id="replies-${comment.id}"></div>
    `;
        const repliesContainer = div.querySelector(`#replies-${comment.id}`);
    const replies = getReplies(comment.id);
    replies.forEach(reply => {
        const replyCard = createCommentCard(reply, getReplies);
        repliesContainer.appendChild(replyCard);
    });
const replybtn=div.querySelector(".reply-btn");
const deletebtn=div.querySelector(".delete-comment-btn");
if (replybtn) {
    replybtn.addEventListener("click", () => {
        showReplyForm(comment.id);
    });
}

if (deletebtn) {
    deletebtn.addEventListener("click", () => {
        deleteComment(comment.id);
    });
}
    return div;
}
async function getCurrentUserId() {
       try {
        const user = await api.get("auth/me");
        return user.id;
    } catch {
        return null;
    }
}
async function addComment(postId,body,selfCommentId=null){
try{
   
  await api.post(`posts/${postId}/comments`,{
    body: body,
    selfCommentId: selfCommentId
});
 await fetchComments(postId);
}catch(error){
    console.error("Error adding comment:", error)
}
}
async function deleteComment(commentId){
try{
await api.delete(`comments/${commentId}`);
 await fetchComments(getPostId());
}catch(error){
    console.error("Error deleting comment:", error)
}
}
function formatTime(dateString) {
    const date = new Date(dateString);
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