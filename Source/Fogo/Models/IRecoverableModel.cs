using System;

namespace Fogo.Models {

    public interface IRecoverableModel {
        bool IsDeleted { get; set; }
        DateTime? DeletedAt { get; set; }
    }
}