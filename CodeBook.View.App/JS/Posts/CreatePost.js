import { api } from '../api.js';

const urlParams = new URLSearchParams(window.location.search);
const communityId = urlParams.get('communityId');

document.addEventListener('DOMContentLoaded', () => {
    if (communityId) {
        document.getElementById('communityInfo').style.display = 'block';
        document.getElementById('communityId').value = communityId;
        loadCommunityName(communityId);

        document.getElementById('isPublic').checked = true;
    }
});

async function createPost() {
    const title = document.getElementById('title').value.trim();
    const body = document.getElementById('body').value.trim();
    const codeSnippet = document.getElementById('codeSnippet').value.trim();
    const language = document.getElementById('language').value;
    const isPublic = document.getElementById('isPublic').checked;

    const communityIdValue = document.getElementById('communityId').value;

    const errorMsg = document.getElementById('errorMsg');
    const successMsg = document.getElementById('successMsg');

    // hide previous messages
    errorMsg.style.display = 'none';
    successMsg.style.display = 'none';

    if (!title) {
        errorMsg.textContent = 'Title is required!';
        errorMsg.style.display = 'block';
        return;
    }

    if (!body) {
        errorMsg.textContent = 'Body is required!';
        errorMsg.style.display = 'block';
        return;
    }

    try {
        const result = await api.post('Post/create', {
            title: title,
            body: body,
            codeSnippet: codeSnippet || null,
            language: language || null,
            isPublic: isPublic,
            communityId: communityIdValue ? parseInt(communityIdValue) : null,
            tagIds: []
        });

        if (result.message === 'Post created successfully') {
            successMsg.textContent = 'Post created successfully!';
            successMsg.style.display = 'block';

            // redirect to feed after 5 seconds
            setTimeout(() => {
               if(communutyIdValue){
                window.location.href = `../html/Community/Community.html?id=${communityIdValue}`;
               }
               else{
                window.location.href = 'Feed.html';
               }
            }, 5000);
        } else {
            errorMsg.textContent = result.message || 'Failed to create post';
            errorMsg.style.display = 'block';
        }

    } catch (error) {
        errorMsg.textContent = 'Failed to create post. Are you logged in?';
        errorMsg.style.display = 'block';
        console.error(error);
    }
}