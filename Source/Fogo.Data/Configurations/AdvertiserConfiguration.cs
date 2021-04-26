using Fogo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fogo.Data.Configurations {

    public class AdvertiserConfiguration {

        public AdvertiserConfiguration(EntityTypeBuilder<AdvertiserModel> builder) {
            builder.Property(x => x.Id).HasColumnType("varchar(32)").ValueGeneratedNever();
            builder.Property(x => x.UserId).HasColumnType("varchar(32)").IsRequired();
        }
    }
}