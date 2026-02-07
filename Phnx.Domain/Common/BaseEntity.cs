using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Phnx.Domain.Common
{

    public abstract class BaseEntity
    {
        protected BaseEntity() { }
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public DateTime CreatedDate { get; private set; } = DateTime.UtcNow;
        [ConcurrencyCheck]
        public uint Xmin { get; internal set; }
    }
}