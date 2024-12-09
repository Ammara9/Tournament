using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.DTOs
{
    public record GameUpdateDto
    {
        //public int Id { get; init; }
        public string? Title { get; init; }
        public DateTime Time { get; init; }
    }
}
