using System;
using System.Diagnostics;
using Global.Repository;

namespace Pharmacy.Repository
{
    public class PersonEntity : EntityBase
    {
        public string PersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string SSN { get; set; }
    }
}
