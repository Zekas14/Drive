using DriveApp.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }
        [MinLength(2000)]
        [MaxLength(2025)]
        public int Model  { get; set; }
        public required string LicensePlate  { get; set; }
        [MinLength(1)]
        [MaxLength(8)]
        public required int Seats  { get; set; }
        public required string Color  { get; set; }
        [ForeignKey("Drivers")]
        public required string DriverId { get; set; }
        public Driver Driver { get; set; }  
    }
    public class Car :Vehicle
    {

    }   
    public class Motocycle :Vehicle
    {

    }

}