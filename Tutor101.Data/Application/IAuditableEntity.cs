using System;

namespace Tutor101.Data.Application
{
    public interface IAuditableEntity
    {
        DateTime CreatedOn { get; set; }
        string CreatedById { get; set; }
        DateTime? EditedOn { get; set; }
        string EditedById { get; set; }
    }
}
