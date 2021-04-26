using System.ComponentModel.DataAnnotations.Schema;

namespace Fogo.Models {

    public class ExecutorSocialNetworkModel {
        public string Id { get; set; }
        public string ExecutorId { get; set; }
        public string SocialNetworkId { get; set; }
        public string Url { get; set; }

        #region Navigation properties

        [ForeignKey(nameof(ExecutorId))]
        public virtual ExecutorModel Executor { get; set; }

        [ForeignKey(nameof(SocialNetworkId))]
        public virtual SocialNetworkModel SocialNetwork { get; set; }

        #endregion Navigation properties
    }
}