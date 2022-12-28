﻿using Domain.Model.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dashboard
{
    public interface IDashboardRepository
    {
        IEnumerable<Productlist> GetAllProduct();
        void InsertProduct(Productlist entity);

        Productlist GetProduct(long id);
        void DeleteProduct(long id);

    }
}
