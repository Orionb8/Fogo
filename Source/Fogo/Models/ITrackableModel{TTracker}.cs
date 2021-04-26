namespace Fogo.Models {

    public interface ITrackableModel<TTracker> : ITrackableModel {
        TTracker CreatedBy { get; set; }
        TTracker UpdatedBy { get; set; }
    }
}