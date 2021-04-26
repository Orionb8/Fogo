using Fogo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fogo.Data.Configurations {

    public class AdvertTypeConfiguration {

        public AdvertTypeConfiguration(EntityTypeBuilder<AdvertTypeModel> builder) {
            builder.Property(x => x.Id).HasColumnType("varchar(32)").ValueGeneratedNever();
            builder.Property(x => x.Name).HasColumnType("varchar(32)").IsRequired();
        }
    }
}