import { api } from '../api.js';
window.onload=()=>{
    var createcommunitybtn = document.getElementById("createcommunity-btn");
    var communitycontainer = document.getElementById("communities-container");
    var createcontainer = document.getElementById("creation-card");
    
     createcommunitybtn.addEventListener('click',(e) => {
                        createCommunity(e.target);});

    async function GetCommunities() {
        try{
            const communities = await api.get('communities/getownedcommunities');
            if(!communities || communities.length === 0){
                communitiesContainer.innerHTML = '<span style="color: Red;">No Commmunities found.</span>';
                return; 
            }
            communitycontainer.innerHTML = '';
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
                <h2 class="text-light  mb-1">
                <span id="name-display">${community.name}</span>
                <input type="text" id="name-input" class="form-control d-none" value="${community.name}">
                </h2>
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
                    <button type="button" class="btn btn-outline-secondary update-btn" id ="update">Update Community</button>
                    <button type="button" class="btn btn-outline-secondary save-btn d-none" id ="save">Save Changes</button>
                    <button type="button" class="btn btn-outline-danger delete-btn" id= "delete">Delete Community</button>
                </div>
            </div>`;
            communityCard.querySelector('.update-btn').addEventListener('click', (e) => {
                        Update_TextAreas(e.target);});
            communityCard.querySelector('.save-btn').addEventListener('click', (e) => {
                        UpdateCommunity(communityId, e.target);});
            communityCard.querySelector('.delete-btn').addEventListener('click', (e) => {
                        DeleteCommunity(communityId, e.target);});
            communitycontainer.appendChild(communityCard);
                
            });
        }
         catch(error){
            alert("Couldn't load reports: " + error.message);
        }
        
    }

    async function createCommunity(button) {
        button.disabled = true;
        createcontainer.innerHTML =`
                <div class="row align-items-center">
      
                <div class="col-md-3 text-center">
                
               <img src="${community.iconURL ? community.iconURL : ''}" 
                alt="Community Icon" 
                class="profile-img">
                </div>
      
                <div class="col-md-9">
                <h2 class="text-light  mb-1">
                <input type="text" id="newname-input" class="form-control" value="${community.name}">
                </h2>
                <p class="text-white-50" >
                 <textarea id="newdesc-input" class="form-control">${community.description}</textarea>
                 </p>
            </div>
            </div>
                <div class="actions">
                    <button type="button" class="btn btn-outline-success create-btn d-none" id ="create">Create Community</button>
                    <button type="button" class="btn btn-outline-danger cancel-btn" id= "cancel">Cancel</button>
                </div>
            </div>`;
            createcontainer.querySelector('.create-btn').addEventListener('click', (e) => {
                        create(e.target);});
            createcontainer.querySelector('.cancel-btn').addEventListener('click', (e) => {
                       createcontainer.innerHTML = '';
            });
    }

    GetCommunities();
};

window.Update_TextAreas= async(buttonElement) => {
        buttonElement.disabled = true;
            var namedisplay = document.getElementById("name-display");
            var nameinput = document.getElementById("name-input");
            var descdisplay = document.getElementById("desc-display");
            var descinput = document.getElementById("desc-input");

            nameDisplay.classList.add('d-none');
            nameInput.classList.remove('d-none');
    
            descDisplay.classList.add('d-none');
            descInput.classList.remove('d-none');

            var deletebtn = document.getElementById("delete");
            deletebtn.classList.add('d-none');
            var savebtn = document.getElementById("save");
            savebtn.classList.remove('d-none');
            buttonElement.classList.add('d-none');

}

window.UpdateCommunity = async (communityId,buttonElement) => {
            buttonElement.disabled = true;
            var namedisplay = document.getElementById("name-display");
            var nameinput = document.getElementById("name-input");
            var descdisplay = document.getElementById("desc-display");
            var descinput = document.getElementById("desc-input");
            const namevalue = nameinput.value;
            if(!namevalue){
            alert("Please Fill all the required fields!");
                return;
            }

            try{
                const result = await api.patch(`communities/${communityId}/updatecommunity`,{
                    description : descinput.value,
                    name : namevalue
                });
                if(result.message ==="Community Updated Successfully"){

            namedisplay.textContent = nameinput.value;
            descdisplay.textContent = descinput.value;
            namedisplay.classList.remove('d-none');
            nameinput.classList.add('d-none');
    
            descdisplay.classList.remove('d-none');
            descinput.classList.add('d-none');
                    
                var deletebtn = document.getElementById("delete");
                deletebtn.classList.remove('d-none');
                var updatebtn = document.getElementById("update");
                updatebtn.classList.remove('d-none');
                buttonElement.classList.add('d-none');

                    alert("Updated community!");
                    GetCommunities();
                }
            }
        catch(error){
            alert("Error: " + error.message);
            buttonElement.disabled = false;
        }

}

window.DeleteCommunity = async (communityId,buttonElement) => {
            buttonElement.disabled = true;
            try{
                const result = await api.delete(`communities/${communityId}/deletecommunity`);
                if(result.message ==="Community Deleted Successfully"){
                    alert("Community Deleted!");
                    GetCommunities();
                }
            }
        catch(error){
            alert("Error: " + error.message);
            buttonElement.disabled = false;
        }

}

window.create = async (buttonElement) => {
        buttonElement.disabled = true;
        var nameinput = document.getElementById("newname-input");
        var descinput = document.getElementById("newdesc-input");
        const namevalue = nameinput.value;
        if(!namevalue){
            alert("Please Fill all the required fields!");
                return;
            }

        try{
                const result = await api.post(`communities/createcommunity`,{
                    description : descinput.value,
                    name : namevalue
                });
                if(result.message === "Community Created Successfully"){
                    var createcontainer = document.getElementById("creation-card");
                    createcontainer.innerHTML = '';
                    alert("Community Created!");
                    GetCommunities();
                }
            }
            catch(error){
                alert("Error: " + error.message);
                buttonElement.disabled = false;
            }
}
