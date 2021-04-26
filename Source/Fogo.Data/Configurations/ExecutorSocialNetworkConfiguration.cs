using Fogo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fogo.Data.Configurations {

    public class ExecutorSocialNetworkConfiguration {

        public ExecutorSocialNetworkConfiguration(EntityTypeBuilder<ExecutorSocialNetworkModel> builder) {
            builder.HasAlternateKey(x => new { x.ExecutorId, x.SocialNetworkId });
            builder.Property(x => x.Id).HasColumnType("varchar(32)");
            builder.Property(x => x.ExecutorId).HasColumnType("varchar(32)");
            builder.Property(x => x.SocialNetworkId).HasColumnType("varchar(32)");
            builder.Property(x => x.Url).HasColumnType("varchar(256)").IsRequired();
        }
    }
}