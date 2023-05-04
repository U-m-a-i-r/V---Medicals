using System;
using HotChocolate.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using V___Medicals.Data;

namespace V___Medicals.Models.TestGraphQL
{
    public class Book
    {
        public string Title { get; set; }

        public Author Author { get; set; }
    }
    public class Author
    {
        public string Name { get; set; }
    }
    
    public class Query
    {
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [Authorize]
        public IQueryable<Speciality> GetSpecialities(ApplicationDbContext context)=>context.Specialities.Where(s => s.IsActive == true);
        public Book GetBook() =>
            new Book
            {
                Title = "C# in depth.",
                Author = new Author
                {
                    Name = "Jon Skeet"
                }
            };
    }
   
}

