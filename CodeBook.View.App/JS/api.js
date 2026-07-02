const URL_BASE = "https://localhost:44313/";

async function apirequest(endpoint,verb,body = null){
    const options = {
        method: verb,
        credentials: 'include',
        headers: {
            'Content-Type': 'application/json'
        }
    };
    if(body){
        options.body = JSON.stringify(body);
    }

    try{
        const response = await fetch(`${URL_BASE}${endpoint}`,options);

        if (!response.ok) {
        const errorData = await response.json().catch(() => ({ message: response.message }));
        throw new Error(errorData.message);
        }
        if (response.status === 204) return { success: true };
        return await response.json();
    }
    catch(error){
        console.error('Failed to connect to API:', error);
        throw error;
    }
}


export const api = {
    get: (endpoint) => apirequest(endpoint,'GET'),
    post: (endpoint,body) => apirequest(endpoint,'POST',body),
    delete: (endpoint,body=null) => apirequest(endpoint,'DELETE',body),
    patch: (endpoint,body) => apirequest(endpoint,'PATCH',body)
};