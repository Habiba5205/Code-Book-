import { api } from '../api.js';
window.onload=()=>{
    var reportPage = document.getElementById("report-page");
    var reportList = document.getElementById("list-group-report");
    var show_button = document.getElementById("ShowReportBtn");

    show_button.addEventListener('click',GetReports);

    
    async function GetReports() {
        try{
            const reports = await api.get('admin/reports');
            if(!reports || reports.length === 0){
                reportList.innerHTML = '<li>No reports found.</li>';
                return; 
            }
            reportList.innerHTML = '';
            reports.forEach(report => {
                const reportitem= document.createElement('li');
                reportitem.className = 'report-item';
                reportitem.innerHTML =`<details>
                        <summary class="report-header">
                            <span><strong>ID:</strong> #${report.id} <strong>Reason:</strong> ${report.reason}</span>
                            <i class="fa-solid fa-chevron-down arrow"></i>
                        </summary>
                        <div class="report-body">
                            <p><strong>Report ID:</strong> #${report.id}<br />
                            <strong>Reporter ID:</strong> #${report.reporterId}<br />
                            <strong>Description:</strong> ${report.description}<br />
                            <strong>Status:</strong> ${report.status}<br /></p>
                            ${report.commentId ? `<p><strong>Type:</strong>Comment<br /><strong>Comment ID:</strong>${report.commentId}<br /></p>`:''}
                            ${report.postId ? `<p><strong>Type:</strong>Post<br /><strong>Post ID:</strong>${report.postId}<br /></p>`:''}
                            <div class="actions">
                                <button type="button" class="btn btn-success accept-btn">Accept</button>
                                <button type="button" class="btn btn-danger reject-btn">Reject</button>
                            </div>
                        </div>
                    </details>`;

                    reportitem.querySelector('.accept-btn').addEventListener('click', (e) => {
                        handleAction(report, "Accepted", e.target);});

                    reportitem.querySelector('.reject-btn').addEventListener('click', (e) => {
                        handleAction(report, "Rejected", e.target);});
                
                reportList.appendChild(reportitem);
            });
        }
        catch(error){
            alert("Couldn't load reports: " + error.message);
        }
        
    }};

  window.handleAction = async (report, status,buttonElement) => {
            buttonElement.disabled = true;
    try {
        if(status === "Accepted"){
            if(report.commentId){
                const result = await api.delete(`admin/comments/${report.commentId}/${report.id}`);
                if(result.message === 'Comment removed successfully'){
                    alert("Comment removed successfully!");
                    document.querySelectorAll('.actions').forEach(el =>{el.classList.add('d-none');});
                    //add accepted
                    const text = document.createElement('p');
                    text.className = 'status-update'
                    text.innerHTML = '<i class="bi bi-check-lg"></i><span style="color: green;">Accepted</span>';
                    document.querySelectorAll('.actions').appendChild(text);
                }
            }
            else if(report.postId){
                const result = await api.delete(`admin/posts/${report.postId}/${report.id}`);
                if(result.message === 'Post removed successfully'){
                    alert("Post removed successfully!");
                    document.querySelectorAll('.actions').forEach(el =>{el.classList.add('d-none');});
                    //add accepted
                    const text = document.createElement('p');
                    text.className = 'status-update'
                    text.innerHTML = '<i class="bi bi-check-lg"></i><span style="color: green;">Accepted</span>';
                    document.querySelectorAll('.actions').appendChild(text);
                }
            }
            if(status === "Rejected"){
                const result = await api.patch(`admin/reports/${report.id}/status`, { Status: status });
                document.querySelectorAll('.actions').forEach(el =>{el.classList.add('d-none');});
                //add rejected here 
                const text = document.createElement('p');
                    text.className = 'status-update'
                    text.innerHTML = '<i class="bi bi-check-lg"></i><span style="color: red;">Rejected</span>';
                    document.querySelectorAll('.actions').appendChild(text);
            }
             GetReports();
        }
    } catch (error) {
        alert("Error: " + error.message);
        buttonElement.disabled = false;
        document.querySelectorAll('.actions').forEach(el =>{el.classList.remove('d-none');});
        document.querySelectorAll('.status-update').forEach(msg => {msg.remove()});
    }
};
