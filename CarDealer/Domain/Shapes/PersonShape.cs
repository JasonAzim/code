using System;
using System.Collections.Generic;
using System.Linq;
using CarDealer.Repository;

namespace CarDealer.Shape
{
    public class PersonShape
    {
        public string PersonID { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string SSN { get; set; } = string.Empty;

        public PersonShape()
        {

        }

        public PersonShape Map(PersonEntity source)
        {
            PersonShape shape = new PersonShape()
            {
                PersonID = source.PersonID,
                FirstName = source.FirstName,
                LastName = source.LastName,
                DateOfBirth = source.DateOfBirth,
                SSN = source.SSN
            };
            return shape;
        }

        public List<PersonShape> MapList(List<PersonEntity> listSource)
        {
            List<PersonShape> ShapeList = new List<PersonShape>();
            foreach (var source in listSource)
            {
                ShapeList.Add(Map(source));
            }

            return ShapeList;
        }
    }
}
