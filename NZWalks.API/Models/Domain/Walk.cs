﻿using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.Domain
{
    public class Walk
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid RegionId { get; set; }
        public Guid DifficultyId { get; set; }
        public Region Region { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}
