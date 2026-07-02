import { api } from './api.js';
window.onload=()=>{
    var reportPage = document.getElementById("report-page");
    var reportList = document.getElementById("reports-list");
    var reportDetails = document.getElementById("report-details");

    let currentReport = null;
    async function GetReports() {
        try{
            const reprorts = await api.get('admin/reports');
            if(!reports || reports.length === 0){
                reportList.innerHTML = '<li>No reports found.</li>';
                return; 
            }
            reports.forEach(report => {
                const reportitem= document.createElement('li');
                reportitem.className = 'report-item';
                reportitem.innerHTML =`
                <span>Report Id : ${report.Id} Reporter Id : ${report.ReporterId} Status: ${report.Status}</span>
                <button onclick = "showReportDetails('${report})">Details</button>`;
                reportList.appendChild(reportitem);
            });
        }
        catch(error){
            alert("Couldn't load reports" + error.message);
        }
        
    }

    async function showReportDetails(report) {
        currentReport = report;
        if(!report){
            reportDetails.innerHTML ='<h3>No Details to show!</h3>';
        }
        typeinfo = '';
        if(report.PostId){
            typeinfo = `<span> Type : Post (ID : ${report.PostId})</span>
            <button onclick ="RemovePost('${report}')>Delete Post</button>
            <button onclick ="UpdateStatus('${report}','Rejected')>Reject Report</button>`;
        }
        else if(report.CommentId) {
            typeinfo = `<span> Type : Comment (ID : ${report.CommentId})</span>
            <button onclick ="RemoveComment('${report}')>Delete Comment</button>
            <button onclick ="UpdateStatus('${report}','Rejected')>Reject Report</button>`;
        }

        reportDetails.innerHTML = `
        <h3>ReportId : ${report.Id}</h3>
        <h3>ReporterId : ${report.ReporterId}</h3>
        <span>Reason : ${report.Reason}</span>
        <span> Description : ${report.Description}</span>
        <span> CreatedAt : ${report.DateCreated} </span>
        ${typeinfo}`;
        
    }

    async function RemoveComment(report) {
        try{
            const deletiondata ={
                Reason : report.Reason,
                ReportId : report.Id
            };
            await api.delete(`admin/comments/${report.CommentId}`,deletiondata);
            alert("Comment removed successfully!");
             UpdateStatus(report,'Accepted');
        }
        catch(error){
            alert("Error: " + err.message);
        }
    }

        async function RemovePost(report) {
        try{
            const deletiondata ={
                Reason : report.Reason,
                ReportId : report.Id
            };
            await api.delete(`admin/posts/${report.PostId}`,deletiondata);
            alert("Post removed successfully!");
            UpdateStatus(report,'Accepted');

        }
        catch(error){
            alert("Error: " + err.message);
        }
    }

    async function UpdateStatus(report,status) {

        try{
            await api.patch(`admin/reports/${report.Id}/status`,{Status: status});
             alert("Report status updated successfully!");
        }
        catch(error){
            alert("Error: " + err.message);
        }
    }

}

