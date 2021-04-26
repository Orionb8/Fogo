using Fogo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fogo.Data.Configurations {

    public class UserConfiguration {

        public UserConfiguration(EntityTypeBuilder<UserModel> builder) {
            builder.Property(x => x.Id).HasColumnType("varchar(32)").ValueGeneratedNever();
            builder.Property(x => x.Phone).HasColumnType("varchar(32)").IsRequired();
            builder.Property(x => x.LastName).HasColumnType("nvarchar(64)").IsRequired();
            builder.Property(x => x.FirstName).HasColumnType("nvarchar(64)").IsRequired();
            builder.Property(x => x.PasswordHash).HasColumnType("varchar(256)").IsRequired();
        }
    }
}