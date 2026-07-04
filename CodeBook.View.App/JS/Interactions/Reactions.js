import { api } from '../api.js';
async function toggleReaction(button, postId, reactionType) {

    const container = button.parentElement;
    const currentReaction = container.querySelector(".reacted");
    if (currentReaction === button) {
        const success = await removeReaction(postId);
        if (success) {
            button.classList.remove("reacted");
            button.dataset.liked = "false";
            const count = button.querySelector(".like-count");
            count.textContent = Math.max(0, parseInt(count.textContent) - 1);
        }

        return;
    }

    if (currentReaction) {

        await removeReaction(postId);
        currentReaction.classList.remove("reacted");
        currentReaction.dataset.liked = "false";
        const oldCount = currentReaction.querySelector(".like-count");
        oldCount.textContent = Math.max(0, parseInt(oldCount.textContent) - 1);
    }
    const success = await addReaction(postId, reactionType);
    if (success) {
        button.classList.add("reacted");
        button.dataset.liked = "true";
        const count = button.querySelector(".like-count");
        count.textContent = parseInt(count.textContent) + 1;
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
        await api.post(
            `Reaction/addCommentreaction?commentId=${commentId}`,
            {
                postId: Number(postId),
                reactionType: reactionType
            }
        );

        return true;
    } catch (error) {
        console.error(error);
        return false;
    }
}
async function removeCommentReaction(postId, commentId) {
    try {
        await api.delete(
            `Reaction/removeCommentreaction?postId=${postId}&commentId=${commentId}`
        );

        return true;
    } catch (error) {
        console.error("Error removing comment reaction:", error);
        return false;
    }
}
async function toggleCommentReaction(button, postId, commentId, reactionType) {

    const container = button.parentElement;
    const currentReaction = container.querySelector(".reacted");

    if (currentReaction === button) {

        const success = await removeCommentReaction(postId, commentId);

        if (success) {
            currentReaction.classList.remove("reacted");
            currentReaction.dataset.reacted = "false";

            const count = currentReaction.querySelector(".reaction-count");
            count.textContent = Math.max(0, Number(count.textContent) - 1);
        }

        return;
    }

    if (currentReaction) {

        await removeCommentReaction(postId, commentId);

        currentReaction.classList.remove("reacted");
        currentReaction.dataset.reacted = "false";

        const oldCount = currentReaction.querySelector(".reaction-count");
        oldCount.textContent = Math.max(0, Number(oldCount.textContent) - 1);
    }

    const success = await addCommentReaction(postId, commentId, reactionType);

    if (success) {
        button.classList.add("reacted");
        button.dataset.reacted = "true";

        const count = button.querySelector(".reaction-count");
        count.textContent = Number(count.textContent) + 1;
    }
}


window.addReaction = addReaction;
window.removeReaction=removeReaction;
window.toggleReaction = toggleReaction;
window.toggleCommentReaction = toggleCommentReaction;