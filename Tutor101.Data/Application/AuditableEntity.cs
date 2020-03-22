using System;
using Tutor101.Common.Utility;

namespace Tutor101.Data.Application
{
    public class AuditableEntity: IAuditableEntity
    {
        public virtual DateTime CreatedOn { get; set; }
        public virtual string CreatedById { get; set; }
        public virtual DateTime? EditedOn { get; set; }
        public virtual string EditedById { get; set; }
        public Enums.RecordState RecordState { get; set; } = Enums.RecordState.Active;

        protected AuditableEntity()
        {
            CreatedOn = DateTime.UtcNow;
            EditedOn = DateTime.UtcNow;
            RecordState = Enums.RecordState.Active;
        }
    }
}
