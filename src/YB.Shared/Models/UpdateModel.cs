using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace YB.Shared.Models
{
    public  class UpdateModel
    {
        [Range(1, Int32.MaxValue)]
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [JsonPropertyName("IsCompleted")]
        public bool IsCompleted { get; set; }
    }
}
