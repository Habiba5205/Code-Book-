using CodeBook.Business.App.Interfaces;
using CodeBook.Models.App;
using CodeBook.Data.App;
using System;


namespace CodeBook.Business.App.Services
{
    public class ModerationService : IModerationService
    {
        private CodeBookContext context;

        public ModerationService(CodeBookContext context)
        {
            this.context = context;
        }

        public void RemovePost(int removerid, int postId, int? reportId, string reason)
        {
            var post = context.posts.FirstOrDefault(p => p.Id == postId);

            if (post != null)
            {
                post.IsRemoved = true;
                post.DateUpdated = DateTime.Now;

                var removal = new PostRemoval
                {
                    PostId = postId,
                    RemoverId = removerid,
                    ReportId = reportId,
                    Reason = reason,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now
                };
                context.postsRemovals.Add(removal);

                if (reportId != null)
                {
                    var report = context.reports.FirstOrDefault(r => r.Id == reportId);
                    if (report != null)
                    {
                        report.Status = ReportStatus.Accepted;
                        report.DateUpdated = DateTime.UtcNow;
                    }

                }
                context.SaveChanges();
            }
        }
    }
}
