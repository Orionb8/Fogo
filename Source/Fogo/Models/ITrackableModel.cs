using System;

namespace Fogo.Models {

    public interface ITrackableModel {
        DateTime CreatedAt { get; set; }
        DateTime? LastUpdatedAt { get; set; }
    }
}