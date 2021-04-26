using Fogo.Data.Repositories.Implementations;

namespace Fogo.Data.Repositories {

    public class FogoRepository<TModel> : Repository<FogoDbContext, TModel> where TModel : class {

        public FogoRepository(FogoDbContext context) : base(context) {
        }
    }
}