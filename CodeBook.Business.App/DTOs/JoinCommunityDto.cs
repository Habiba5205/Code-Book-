using CodeBook.Models.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBook.Business.App.DTOs
{
    public class JoinCommunityDto
    {
        public int UserId { get; set; }
        public CommunityRole Role { get; set; } = CommunityRole.Member;
    }
}
