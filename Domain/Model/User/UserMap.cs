using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.User
{
    public class UserMap
    {
        public UserMap(EntityTypeBuilder<User> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(t => t.userId);
            entityTypeBuilder.Property(t => t.firstName).IsRequired();
            entityTypeBuilder.Property(t => t.lastName).IsRequired();
            entityTypeBuilder.Property(t => t.email).IsRequired();
            entityTypeBuilder.Property(t => t.phoneNumber).IsRequired();
            entityTypeBuilder.Property(t => t.password).IsRequired();

        }
    }
}
