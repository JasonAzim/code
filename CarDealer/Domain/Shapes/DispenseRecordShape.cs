using System;
using System.Collections.Generic;
using System.Linq;
using CarDealer.Repository;

namespace CarDealer.Shape
{
    public class DispenseRecordShape
    {
        public string? ID { get; set; }
        public string? LocationID { get; set; }

        public DispenseRecordShape()
        {

        }

        /*
        public DispenseRecordShape Map(Account source)
        {
            DispenseRecordShape shape = new DispenseRecordShape()
            {
                ID = source.AccountId.ToString(),
                LocationID = source.LocationId.ToString()
            };
            return shape;
        }

        public List<DispenseRecordShape> MapList(List<Account> listSource)
        {
            List<DispenseRecordShape> ShapeList = new List<DispenseRecordShape>();
            foreach (var source in listSource)
            {
                ShapeList.Add(Map(source));
            }

            return ShapeList;
        }
        */
    }
}
