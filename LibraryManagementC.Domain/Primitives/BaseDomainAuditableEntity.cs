namespace LibraryManagementC.Domain.Primitives
{
    public abstract class BaseDomainAuditableEntity
    {
        public string? CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; } = string.Empty;
        public DateTime UpdateAt { get; set; }
    }
}
