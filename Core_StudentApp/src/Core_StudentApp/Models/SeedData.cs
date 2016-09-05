using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Core_StudentApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Core_StudentApp.Models
{
    public class SeedData
    {
        public static void Initialise(IServiceProvider serviceProvider)
        {
            using (
                var context =
                    new ApplicationDbContext(
                        serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Student.Any())
                {
                    return;
                }

                context.Student.AddRange(
                    new Student
                    {
                        Dob = new DateTime(1986, 02, 24),
                        Gender = "Male",
                        Name = "Madhu",
                        Id = 1
                    },
                    new Student
                    {
                        Dob = new DateTime(1996, 02, 24),
                        Gender = "Male",
                        Name = "Raj",
                        Id = 2
                    }

                );
            }
        }
    }
}
