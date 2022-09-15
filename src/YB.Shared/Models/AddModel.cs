using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace YB.Shared.Models
{
    public  class AddModel
    {
        [JsonPropertyName("Description")]
        [StringLength(200, ErrorMessage = "Invalid Description fromat")]
        [Required]
        public string? Description { get; set; } 
    }
}
