import { api } from '../api.js';
window.onload=()=>{
    var communitydatacard = document.getElementById("community-data-holder");
    var communityfeed = document.getElementById("community-feed");
    var createpostbtn = document.getElementById("createpost-btn");
    const params = new URLSearchParams(window.location.search);
    const communityId = params.get("id");
    
    createpostbtn.addEventListener('click',() => {
            //The post creation navigate to habiba^^ 
    });

    async function CommunityDataView() {
        try{
            const community = await api.get(`communities/${communityId}`);
            if(community){
                communitydatacard.innerHTML = `
                <div class="row align-items-center">
      
                <div class="col-md-3 text-center">
                
               <img src="${community.iconURL ? community.iconURL : ''}" 
                alt="Community Icon" 
                class="profile-img">
                </div>
      
                <div class="col-md-9">
                <h2 class="text-light  mb-1">${community.name}</h2>
                <p class="text-white-50" >${community.description}</p>
        
                <div class="d-flex gap-4"style="color:#bca1ec;">
                <small><strong>Creation Date:</strong>${community.dateCreated}</small>
                <small><strong>Owner Id:</strong>${community.ownerId}</small>
            </div>
            </div>
      
            </div>
                <div class="actions">
                    <button type="button" class="btn btn-outline-danger unjoin-btn">Unjoin Community</button>
                </div>
                `;
                 communitydatacard.querySelector('.unjoin-btn').addEventListener('click', (e) => {
                        handleAction(communityId, e.target);});
            }
        }
        catch(error){
                alert("Couldn't load community details: " + error.message);
        }
        
    }
    async function GetCommunityFeed() {
        try {
                communityfeed.innerHTML = `<p style="color:white">Loading...</p>`;
                const posts = await api.get(`communities/${communityId}/getCommunityFeed`);
                if (!posts || posts.length === 0) {
                communityfeed.innerHTML = `<p style="color:white">No posts found.</p>`;
                return;
                }

        communityfeed.innerHTML = "";
        posts.forEach(post => {
            communityfeed.innerHTML += `
                <div class="post-card">
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
                        <button class="btn-purple" onclick="viewPost(${post.id})">
                            <i class="fa-solid fa-eye"></i> View Post
                        </button>
                    </div>
                </div>
            `;
        });

    } catch (error) {
        postsContainer.innerHTML = `
            <p style="color:red">Failed to load posts. Please try again.</p>
        `;
        console.error(error);
    }
}


function viewPost(postId) {
    window.location.href = `../../HTML/Posts/PostDetail.html?id=${postId}`;
}

 CommunityDataView();
 GetCommunityFeed();

};

window.handleAction = async (communityId,buttonElement) => {
            buttonElement.disabled = true;
            try{
                const result = await api.delete(`communities/${communityId}/unjoin`);
                if(result.message ==="Unjoined Community Successfully"){
                    alert("Unjoined community!");
                    window.location.href = '../../HTML/Posts/Feed.html';
                }
            }
        catch(error){
            alert("Error: " + error.message);
            buttonElement.disabled = false;
        }

}
