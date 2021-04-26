using Fogo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fogo.Data.Configurations {

    public class SocialNetworkConfiguration {

        public SocialNetworkConfiguration(EntityTypeBuilder<SocialNetworkModel> builder) {
            builder.Property(x => x.Id).HasColumnType("varchar(32)").ValueGeneratedNever();
            builder.Property(x => x.Name).HasColumnType("varchar(32)").IsRequired();
            builder.Property(x => x.Url).HasColumnType("varchar(256)").IsRequired();
        }
    }
}