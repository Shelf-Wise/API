using LibraryManagementC.Domain.Enums;
using LibraryManagementC.Domain.Primitives;

namespace LibraryManagementC.Domain.Entities
{
    public class Fine : BaseEntity
    {
        public int Amount { get; set; }
        public PaidStatus PaidStatus { get; set; }
    }
}
