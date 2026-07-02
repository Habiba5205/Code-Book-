const URL_BASE = "https://localhost:44313/";

async function apirequest(endpoint,method = "GET",body = null){
    const options = {
        method,
        credentials: "include",
        headers: {
            "Content-Type": "application/json"
        }
    };
    if(body){
        options.body = JSON.stringify(body);
    }

    try{
        const response = await fetch(`${URL_BASE}${endpoint}`,options);
        if(response.status === 401){
            //redirect to login hna el token expired 5las
            window.location.href = "/login.html";
            return;
        }
        if (response.status === 204) return { success: true };

        const data = await response.json();
        if(!data.success){
            const error = new Error(json.message || "Request failed");
            error.errors = json.errors;
            throw error;
        }
        return data.data;
    }
    catch(error){
        console.error('Failed to connect to API:', error);
        throw error;
    }
}


const api = {
    get: (endpoint) => apirequest(endpoint,"GET"),
    post: (endpoint,body) => apirequest(endpoint,"POST",body),
    delete: (endpoint,body=null) => apirequest(endpoint,"DELETE",body),
    patch: (endpoint,body) => apirequest(endpoint,"PATCH",body)

};