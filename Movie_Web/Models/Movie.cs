﻿namespace Movie_Web.Models
{
    public class Movie : BaseEntity
    {
        public string? Type { get; set; }
        public string? Title { get; set; }
        public string? Director { get; set; }
        public string? Cast { get; set; }
        public string? Country { get; set; }
        public string? Date_Added { get; set; }
        public int? Release_Year { get; set; }
        public string? Rating { get; set; }
        public string? Duration { get; set; }
        public string? Listed_In { get; set; }
        public string? Description { get; set; }
    }
}
