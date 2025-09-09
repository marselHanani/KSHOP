﻿using KASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repositories.Interface
{
    public interface IOrderItemRepository
    {
        Task AddRangeAsync(List<OrderItem> items);
    }
}
