using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using StudentApp.Api.Data;
using StudentApp.Api.Data.Entities;

namespace StudentApp.Api.Data
{
    public static class DbSeed
    {
        public static async Task SeedAsync(AppDbContext dbContext)
        {
            if (!await dbContext.Students.AnyAsync())
            {
                var faker =  new Faker<StudentEntity>("tr")
                .RuleFor(s => s.studentName, f => f.Name.FirstName())
                .RuleFor(s => s.studentSurname, f => f.Name.LastName())
                .RuleFor(s => s.studentNo, f => f.Random.Int(100, 1000).ToString())
                .RuleFor(s => s.studentClass, f => f.PickRandom("10-A", "10-B", "11-A", "11-B","12-E","12-B","9-B"));

                var students = faker.Generate(20);

                await dbContext.Students.AddRangeAsync(students);
                await  dbContext.SaveChangesAsync();
            }

        }
    }
}
