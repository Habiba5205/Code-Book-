using CodeBook.Business.App.Interfaces;
using CodeBook.Models.App;
using CodeBook.Data.App.IRepositories;
using System;
using CodeBook.Business.App.DTOs;


namespace CodeBook.Business.App.Services
{
    public class ModerationService : IModerationService
    {
        private readonly IPostRepository _postRepository;
        private readonly IReportRepository _reportRepository;

        public ModerationService(IPostRepository postRepository, IReportRepository reportRepository)
        {
            _postRepository = postRepository;
            _reportRepository = reportRepository;
        }

        public void RemovePost(int postId, RemovePostsDto dto)
        {
            var post = _postRepository.GetPostById(postId);

            if (post != null)
            {
                post.IsRemoved = true;
                post.DateUpdated = DateTime.Now;

                var removal = new PostRemoval
                {
                    PostId = postId,
                    RemoverId = dto.RemoverId,
                    ReportId = dto.ReportId,
                    Reason = dto.Reason,
                    DateCreated = DateTime.Now
                };
                _postRepository.AddRemovalRecord(removal);

                if (dto.ReportId != null)
                {
                    var report = _reportRepository.GetReportbyId(dto.ReportId);
                    if (report != null)
                    {
                        report.Status = ReportStatus.Accepted;
                        report.DateUpdated = DateTime.UtcNow;
                        _reportRepository.Update(report);
                    }

                }
                _postRepository.SaveChanges();
            }
        }
    }
}
