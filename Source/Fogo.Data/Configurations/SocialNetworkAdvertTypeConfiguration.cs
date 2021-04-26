using Fogo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fogo.Data.Configurations {

    public class SocialNetworkAdvertTypeConfiguration {

        public SocialNetworkAdvertTypeConfiguration(EntityTypeBuilder<SocialNetworkAdvertTypeModel> builder) {
            builder.HasKey(x => new { x.SocialNetworkId, x.AdvertTypeId });
            builder.Property(x => x.SocialNetworkId).HasColumnType("varchar(32)");
            builder.Property(x => x.AdvertTypeId).HasColumnType("varchar(32)");
        }
    }
}