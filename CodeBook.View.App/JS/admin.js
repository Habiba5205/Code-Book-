import { api } from './api.js';
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
                var typeinfo = '<p>General Report</p>';
                if(report.postId){
                    typeinfo = `<p><strong>Post ID:</strong> #${report.postId}</p>`;
                }
                else if(report.commentId) {
                  typeinfo = `<p><strong>Comment ID:</strong> #${report.commentId}</p>`;
                }
                reportitem.innerHTML =`<details>
                        <summary class="report-header">
                            <span><strong>ID:</strong> #${report.id} <strong>Reason:</strong> ${report.reason}</span>
                            <i class="fa-solid fa-chevron-down arrow"></i>
                        </summary>
                        <div class="report-body">
                            <p><strong>Report ID:</strong> #${report.id}<br />
                            <strong>Reporter ID:</strong> #${report.reporterId}<br />
                            <strong>Description:</strong> ${report.description}<br />
                            <strong>Status:</strong> ${report.status}<br />
                            ${typeinfo}<br /></p>
                            <div class="actions">
                                <button type="button" class="btn btn-success accept-btn">Accept</button>
                                <button type="button" class="btn btn-danger reject-btn">Reject</button>
                            </div>
                        </div>
                    </details>`;

                    reportitem.querySelector('.accept-btn').addEventListener('click', (e) => {
                        handleAction(report, 'Accepted', e.target);});

                    reportitem.querySelector('.reject-btn').addEventListener('click', (e) => {
                        handleAction(report, 'Rejected', e.target);});
                
                reportList.appendChild(reportitem);
            });
        }
        catch(error){
            alert("Couldn't load reports: " + error.message);
        }
        
    }};

  window.handleAction = async (report, status,buttonElement) => {
            buttonElement.disabled = true;
            const deletiondata={
                Reason : report.Reason,
                ReportId : report.Id
            }
    try {
        if(status === 'Accepted'){
            if(report.commentId){
                await api.delete(`admin/comments/${report.commentId}`,deletiondata);
                alert("Comment removed successfully!");
            }
            else if(report.commentId){
                await api.delete(`admin/posts/${report.postId}`,deletiondata);
                alert("Post removed successfully!");
            }
             await api.patch(`admin/reports/${report.id}/status`, { Status: status });
             buttonElement.innerText = "Done";
             GetReports();
        }
    } catch (error) {
        alert("Error: " + error.message);
        buttonElement.disabled = false;
    }
};
