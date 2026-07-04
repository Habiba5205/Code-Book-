import { api } from '../api.js';
async function toggleReaction(button, postId, reactionType) {
    const container = button.parentElement;
    const currentReaction = container.querySelector(".reacted");
    

    const likeBtn = container.querySelector("[data-type='Like']");
    const countSpan = likeBtn ? likeBtn.querySelector(".like-count") : null;
    const currentCount = parseInt(countSpan?.textContent) || 0;

    if (currentReaction === button) {
        const success = await removeReaction(postId);
        if (success) {
            button.classList.remove("reacted");
            button.dataset.liked = "false";
            if (countSpan) countSpan.textContent = Math.max(0, currentCount - 1);
        }
        return;
    }

    if (currentReaction) {
        await removeReaction(postId);
        currentReaction.classList.remove("reacted");
        currentReaction.dataset.liked = "false";
        
    }

    const success = await addReaction(postId, reactionType);
    if (success) {
        button.classList.add("reacted");
        button.dataset.liked = "true";
        if (!currentReaction && countSpan) {
            countSpan.textContent = currentCount + 1;  
        }
    }
}

let reactionCallCount = 0;
async function addReaction(postId, reactionType) {
 console.log(`addReaction call #${reactionCallCount}`, new Date().getTime());
    console.trace();

    try {
        const data = await api.post("Reaction/addPostreaction", {
            postId: Number(postId),
            reactionType: reactionType
        });

        console.log("Reaction added:", data);
        return true;
    } catch (error) {
        console.error("Error adding reaction:", error);
        return false;
    }
}


async function removeReaction(postId) {
    try{
     await api.delete(`Reaction/removePostreaction?postId=${postId}`);
        return true;
     } catch (error) {
        console.error("Error removing reaction:", error);
        return false;
    }
}

async function addCommentReaction(postId, commentId, reactionType) {
    try {
        const data = await api.post("Reaction/addCommentreaction", {
            postId: Number(postId),
            commentId: Number(commentId),
            reactionType: reactionType
        });
        console.log("Comment reaction added:", data);
        return true;
    } catch (error) {
        console.error("Error adding comment reaction:", error);
        return false;
    }
}




async function removeCommentReaction(postId, commentId) {
    try {
        await api.delete(`Reaction/removeCommentreaction?postId=${postId}&commentId=${commentId}`);
        return true;
    } catch (error) {
        console.error("Error removing comment reaction:", error);
        return false;
    }
}


async function toggleCommentReaction(button, commentId, reactionType) {
    const container = button.parentElement;
    const selected = container.querySelector(".reacted");

    if (selected === button) {
        const success = await removeCommentReaction(commentId);
        if (!success) return;

        button.classList.remove("reacted");
        const count = button.querySelector(".reaction-count");
        if (count) count.textContent = Math.max(0, Number(count.textContent) - 1);
        return;
    }

    if (selected) {
        const success = await removeCommentReaction(commentId);
        if (!success) return;

        selected.classList.remove("reacted");
        const oldCount = selected.querySelector(".reaction-count");
        if (oldCount) oldCount.textContent = Math.max(0, Number(oldCount.textContent) - 1);
    }

    const success = await addCommentReaction(commentId, reactionType);
    if (!success) return;

    button.classList.add("reacted");
    const count = button.querySelector(".reaction-count");
    if (count) count.textContent = Number(count.textContent) + 1;
}



window.addReaction = addReaction;
window.removeReaction=removeReaction;
window.toggleReaction = toggleReaction;
window.toggleCommentReaction = toggleCommentReaction;