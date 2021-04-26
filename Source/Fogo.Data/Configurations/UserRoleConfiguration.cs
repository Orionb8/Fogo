using Fogo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fogo.Data.Configurations {

    public class UserRoleConfiguration {

        public UserRoleConfiguration(EntityTypeBuilder<UserRoleModel> builder) {
            builder.HasKey(x => new { x.UserId, x.RoleId });
            builder.Property(x => x.UserId).HasColumnType("varchar(32)");
            builder.Property(x => x.RoleId).HasColumnType("varchar(32)");
        }
    }
}