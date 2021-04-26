namespace Fogo.Models {

    public interface IRecoverableModel<TDeleter> : IRecoverableModel {
        TDeleter DeletedBy { get; set; }
    }
}