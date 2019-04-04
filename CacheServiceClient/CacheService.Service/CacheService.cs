using CacheService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheService.Service
{
    public class CacheService : ICacheService
    {
        public StudentDetails GetStudentDetails(int id)
        {
            // Change the implementation and return data as needed.

            Console.WriteLine($"Requested Id {id}");
            return new StudentDetails
            {
                Age = 10,
                Id = id,
                Name = "SMS"
            };
        }
    }
}
