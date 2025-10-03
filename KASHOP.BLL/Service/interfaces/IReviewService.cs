using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KASHOP.DAL.DTO.Request;

namespace KASHOP.BLL.Service.interfaces
{
    public interface IReviewService
    {
        Task<bool> AddReviewAsync(ReviewRequest request, string userId);
    }
}
