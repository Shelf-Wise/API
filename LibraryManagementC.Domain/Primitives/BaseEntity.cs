namespace LibraryManagementC.Domain.Primitives
{
    public class BaseEntity : BaseDomainAuditableEntity
    {
        public Guid Id { get; protected set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (BaseEntity)obj;

            return other.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
