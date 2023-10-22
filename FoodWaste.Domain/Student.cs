using FoodWaste.Domain.DataAnnotations;
using System.ComponentModel;

namespace FoodWaste.Domain
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DisplayName("Date of birth")]
        [AgeDataAnnotations(16)]
        public DateTime DateOfBirth { get; set; }
        [DisplayName("City of study")]
        public City CityOfStudy { get; set; }
        [DisplayName("Student number")]
        public int StudentNumber { get; set; }
        public string Email { get; set; }
        [DisplayName("Phone number")]
        public string PhoneNumber { get; set; }
    }
}