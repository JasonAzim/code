using System;
using System.Diagnostics;
using Global.Repository;

namespace CarDealer.Repository
{
    public class CategoryEntity : EntityBase
    {
        public int CategoryNo { get; set; }
        public string Name { get; set; }
    }
}
