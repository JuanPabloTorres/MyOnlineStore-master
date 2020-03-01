using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyOnlineStore.Entities.Models.Stores
{
    [Table("Locations")]
    public class Location
    {
        public Guid Id { get; set; }
        public string Latitude { get; set; } = string.Empty;
        public string Longitude { get; set; } = string.Empty;
    }
}