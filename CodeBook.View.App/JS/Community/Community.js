import { api } from '../api.js';
window.onload=()=>{
    var communitiesContainer = document.getElementById("communities-container");
    var explorecommunities = document.getElementById("explore-communities");


    async function GetCommunities() {
        try{
            const communities = await api.get('communities/getCommunities');
            if(!communities || communities.length === 0){
                communitiesContainer.innerHTML = '<span style="color: Red;">No Commmunities found.</span>';
                return; 
            }
            communitiesContainer.innerHTML = '';
            communities.forEach(community => {
                const communityCard = document.createElement('div');
                communityCard.innerHTML = `
                <a href="Communityfeed.html?id=${community.communityId}" class="card p-4 text-decoration-none text-dark shadow-sm hover-card">
                <div class="row align-items-center community-card">
                <div class="col-md-3 text-center row align-items-center">
                
               <img src="${community.iconURL ? community.iconURL : ''}" 
                alt="Community Icon" 
                class="profile-img w-100">
                </div>

                <div class="col-md-7">
                <h3 class="text-light  mb-1">
                <span id="name-display">${community.name}</span>
                <input type="text" id="name-input" class="form-control d-none" value="${community.name}">
                </h3>

                <p class="text-white-50" >
                <span id="desc-display">${community.description}</span>
                <textarea id="desc-input" class="form-control d-none">${community.description}</textarea>
                </p>

                <div class="d-flex gap-4"style="color:#bca1ec;">
                <small><strong>Creation Date: </strong>${new Date(community.dateCreated).toLocaleDateString()}</small>
                <small><strong>Members count: </strong>${community.memberscount}</small>
            </div>
            </div>
            </div>
             </a>`;
            communitiesContainer.appendChild(communityCard);
                
            });
        }
         catch(error){
            alert("Couldn't load communities: " + error.message);
        }
        
    }
     async function ExploreCommunities(){
         try{
            const communities = await api.get('communities/getunjoinedcommunities');
            if(!communities || communities.length === 0){
                explorecommunities.innerHTML = '<span style="color: Red;">No Commmunities found.</span>';
                return; 
            }
            explorecommunities.innerHTML = '';
            communities.forEach(community => {
                const communityCard = document.createElement('div');
                communityCard.innerHTML = `
                <div class="row align-items-center community-card">
                <div class="col-md-3 text-center row align-items-center">
                
               <img src="${community.iconURL ? community.iconURL : ''}" 
                alt="Community Icon" 
                class="profile-img w-100">
                </div>

                <div class="col-md-7">
                <h3 class="text-light  mb-1">
                <span id="name-display">${community.name}</span>
                <input type="text" id="name-input" class="form-control d-none" value="${community.name}">
                </h3>

                <p class="text-white-50" >
                <span id="desc-display">${community.description}</span>
                <textarea id="desc-input" class="form-control d-none">${community.description}</textarea>
                </p>

                <div class="d-flex gap-4"style="color:#bca1ec;">
                <small><strong>Creation Date: </strong>${new Date(community.dateCreated).toLocaleDateString()}</small>
                <small><strong>Members count: </strong>${community.memberscount}</small>
            </div>
            </div>
             <div class="actions">
                    <button class="btn w-100 mt-2 mb-3 join-btn"
                    style="background-color:#7c3aed;
                    color:white;">
                    Join Community
                    </button>
                </div>
            </div>`;
            communityCard.querySelector('.join-btn').addEventListener('click', (e) => {
                        handleAction(community.communityId, e.target);});
            explorecommunities.appendChild(communityCard);
                
            });
        }
         catch(error){
            alert("Couldn't load communities: " + error.message);
        }


     }

    GetCommunities();
    ExploreCommunities();
};

window.handleAction = async (communityId ,buttonElement) => {
            buttonElement.disabled = true;
            try{
                const role = 'Member';
                const result = await api.post(`communities/${communityId}/joincommunity`,{Role : role});
                if(result.message ==="Joined Community Successfully"){
                    alert("Joined community!");
                    window.location.href = '../../HTML/Community/Communityfeed.html?id=${communityId}';
                }
            }
        catch(error){
            alert("Error: " + error.message);
            buttonElement.disabled = false;
        }

}
