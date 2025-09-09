using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service.interfaces
{
    public interface ICheckOutService
    {
        Task<CheckOutResponse> ProcessPaymentAsync(CheckOutRequest request, string userId, HttpRequest Request);
        Task<bool> HandlePaymentSuccessAsync(int orderId);
    }
}
