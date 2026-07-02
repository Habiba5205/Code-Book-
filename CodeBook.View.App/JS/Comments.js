const BASE_URL = "https://localhost:7241/api";
const token = localStorage.getItem("token");

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
  try{  const response = await fetch(`${BASE_URL}/posts/${postId}/comments`, {
        headers: {
            "Authorization": "Bearer " + token,
        }
    });
    if (!response.ok) {
        console.error("Failed to fetch comments");
        return;
    }
    const comments = await response.json();
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

    return div;
}
function getCurrentUserId() {
    if(!token) return null;
    const payload = JSON.parse(atob(token.split('.')[1]));
    return parseInt(payload.nameid); 
}