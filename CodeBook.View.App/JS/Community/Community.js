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
                communityCard.className = 'community-card';
                communityCard.innerHTML = `
                <a href="Communityfeed.html?id=${community.communityId}" class="card p-4 text-decoration-none text-dark shadow-sm hover-card">
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
            alert("Couldn't load reports: " + error.message);
        }
        
    }
     async function ExploreCommunities(){
         try{
            const communities = await api.get('communities/getunjoinedcommunities');
            if(!communities || communities.length === 0){
                communitiesContainer.innerHTML = '<span style="color: Red;">No Commmunities found.</span>';
                return; 
            }
            communitiesContainer.innerHTML = '';
            communities.forEach(community => {
                const communityCard = document.createElement('div');
                communityCard.className = 'community-card';
                communityCard.innerHTML = `
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
                <small><strong>Creation Date: </strong>${new Date(community.dateCreated).toLocaleDateString()}</small>
                <small><strong>Members count: </strong>${community.memberscount}</small>
            </div>
            </div>
             <div class="actions">
                    <button class="btn w-100 mt-2 join-btn"
                    style="background-color:#7c3aed;
                    color:white;">
                    Join Community
                    </button>
                </div>
            </div>`;
            communityCard.querySelector('.join-btn').addEventListener('click', (e) => {
                        handleAction(communityId, e.target);});
            communitiesContainer.appendChild(communityCard);
                
            });
        }
         catch(error){
            alert("Couldn't load reports: " + error.message);
        }


     }

    GetCommunities();
    ExploreCommunities();
};

window.handleAction = async (communityId,buttonElement) => {
            buttonElement.disabled = true;
            try{
                const result = await api.post(`communities/${communityId}/joincommunity`,{Role : "Member"});
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
