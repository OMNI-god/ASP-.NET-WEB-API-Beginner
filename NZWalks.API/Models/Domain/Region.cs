﻿using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.Domain
{
    public class Region
    {
        [Key]
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
