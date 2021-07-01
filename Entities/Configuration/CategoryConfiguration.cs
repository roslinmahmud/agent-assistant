using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<StatementCategory>
    {
        public void Configure(EntityTypeBuilder<StatementCategory> builder)
        {
            //builder.HasData(
            //    new StatementCategory
            //    {
            //        Id = 1,
            //        CategoryName = "Commision",
            //        IsIncome = true
            //    },
            //    new StatementCategory
            //    {
            //        Id = 2,
            //        CategoryName = "AIT"
            //    },
            //    new StatementCategory
            //    {
            //        Id = 3,
            //        CategoryName = "Salary"
            //    },
            //    new StatementCategory
            //    {
            //        Id = 4,
            //        CategoryName = "Rent"
            //    },
            //    new StatementCategory
            //    {
            //        Id = 5,
            //        CategoryName = "Fund Transfer"
            //    },
            //    new StatementCategory
            //    {
            //        Id = 6,
            //        CategoryName = "Utility Bill"
            //    });
        }
    }
}
