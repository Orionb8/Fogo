namespace Fogo.Models {

    public interface ITenantableModel<TTenant> {
        TTenant Tenant { get; set; }
    }
}