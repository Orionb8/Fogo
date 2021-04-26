using Fogo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fogo.Data.Configurations {

    public class ExecutorConfiguration {

        public ExecutorConfiguration(EntityTypeBuilder<ExecutorModel> builder) {
            builder.Property(x => x.Id).HasColumnType("varchar(32)").ValueGeneratedNever();
            builder.Property(x => x.UserId).HasColumnType("varchar(32)").IsRequired();
        }
    }
}