using System;

namespace Tests.Dtos
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Fullname { get; }
        private string PrivateName { get; set; }
        public int? NullableInt { get; set; }
        public bool? ActualNull { get; set; }
        public decimal AccountBalance { get; set; }
        public double FavoriteNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTimeOffset GlobalDateAdded { get; set; }
        public User ReferenceType { get; set; }
    }
}
