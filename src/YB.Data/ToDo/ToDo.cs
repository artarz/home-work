using System;
using System.Collections.Generic;

namespace YB.Data.ToDo
{
    public partial class ToDo
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public bool IsComplete { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedDate { get; set; }
    }
}
