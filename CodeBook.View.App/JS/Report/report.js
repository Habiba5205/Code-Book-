import { api } from '../api.js';

window.onload= () =>{
    var myModal = new bootstrap.Modal(document.getElementById('reportModal'));
    myModal.show();
    const params = new URLSearchParams(window.location.search);
    const commentId = params.get("commentId");
    const postId = params.get("postId");
    var submitbtn = document.getElementById("submitReportBtn");
    var cancelbtn = document.getElementById("cancel-report-btn");
    cancelbtn.addEventListener('click',()=>{
        window.location.href = `../Posts/Feed.html`;
    });

    const descriptionInput = document.getElementById("reportDescription");
    const reportForm = document.getElementById('reportForm');
    submitbtn.addEventListener('click', (e) => {
            e.preventDefault();
            const selectedReason = document.querySelector('input[name="reportReason"]:checked').value;
            const description = descriptionInput.value;
            const reportData = {
            postId: postId,
            commentId: commentId,
            reason: selectedReason,
            details: description,
        };

    try{
        var result = api.post("report/submitreport",reportData);
        if(result === "Report Submitted Successfully!"){
            alert("Report Submitted!");
            window.location.href = `../Posts/Feed.html`;
        }
    }
    catch(error){
        alert("Couldn't submit report: " + error.message);
    }
    });

}