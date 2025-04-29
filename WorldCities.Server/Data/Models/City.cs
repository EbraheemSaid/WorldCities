using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldCities.Server.Data.Models
{
    [Table("Cities")]
    [Index(nameof(Name))]
    [Index(nameof(Latitude))]
    [Index(nameof(Longitude))]
    public class City
    {
        #region Properties

        /// <summary>
        ///  the unique identifier for the city
        ///  </summary>
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// City name 
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// City latitude
        /// </summary>
        [Column(TypeName = "decimal(7,4)")]
        public decimal Latitude { get; set; }

        /// <summary>
        /// City Longitude
        /// </summary>
        [Column(TypeName = "decimal(7,4)")]
        public decimal Longitude { get; set; }

        /// <summary>
        /// Country Id (foreign key)
        /// </summary
        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }

        #endregion

        #region Navigation Properties
        /// <summary>
        /// The country this city belongs to
        /// </summary>
        public Country? Country { get; set; }
        #endregion
    }
}
