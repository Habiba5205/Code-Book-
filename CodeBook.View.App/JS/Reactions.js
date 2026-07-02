const BASE_URL = "https://localhost:7241/api";
const token = localStorage.getItem("token");

function initReactions() {
const buttons=document.querySelectorAll(".react-btn");
buttons.forEach(button=>{
    button.addEventListener("click", handleReaction)
});
}

async function handleReaction(event) {
 const button = event.currentTarget;

 const postId = button.dataset.postId;
 const reactionId = button.dataset.reactionId;
 const isLiked = button.dataset.liked === "true";
 const countSpan = button.querySelector(".like-count");
 const originalCount = parseInt(countSpan.textContent);

 try{
 if (isLiked) {
    button.dataset.liked = "false";
    countSpan.textContent = originalCount - 1;
   const success= await removeReaction(reactionId);
   if (!success) {
    button.dataset.liked = "true";
    countSpan.textContent = originalCount + 1;
   }
   else{
    button.dataset.reactionId = "";
   }
 } else {
    button.dataset.liked = "true";
    countSpan.textContent = originalCount + 1;
    const newId = await addReaction(postId);
    if(!newId){
        button.dataset.liked = "false";
        countSpan.textContent = originalCount;
    }
    else{
        button.dataset.reactionId = newId;
    }
 }}
 catch (error) {
    console.error("Error handling reaction:", error);
    button.dataset.liked = isLiked.toString();
    countSpan.textContent = originalCount;
 }

}
async function addReaction(postId) {
    try{
        const response = await fetch(`${BASE_URL}/reactions`, {
            method: "POST",
            headers: {
                "Authorization": "Bearer " + token,
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                postId:postId,
                type:"Like"
            })
        });
    
    if (response.ok) {
        const data = await response.json();
        return data.id;
    }
    else {return null;

    }
}catch (error) {
    console.error("Error adding reaction:", error);
    return null;
}
}

async function removeReaction(reactionId) {
    try{
        const response = await fetch(`${BASE_URL}/reactions/${reactionId}`, {
            method: "DELETE",
            headers: {
                "Authorization": "Bearer " + token,
            }
        });
        return response.ok;
    }
    catch (error) {
        console.error("Error removing reaction:", error);
        return false;
    }
}
document.addEventListener("DOMContentLoaded", () => {
    initReactions();
});