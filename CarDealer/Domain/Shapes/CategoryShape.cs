using System;
using System.Collections.Generic;
using System.Linq;
using CarDealer.Repository;

namespace CarDealer.Shape
{
    public class CategoryShape
    {
        public int CategoryNo { get; set; }
        public string Name { get; set; } = string.Empty;

        public CategoryShape()
        {

        }

        public CategoryShape Map(CategoryEntity source)
        {
            CategoryShape shape = new CategoryShape()
            {
                CategoryNo = source.CategoryNo,
                Name = source.Name
            };
            return shape;
        }

        public List<CategoryShape> MapList(List<CategoryEntity> listSource)
        {
            List<CategoryShape> ShapeList = new List<CategoryShape>();
            foreach (var source in listSource)
            {
                ShapeList.Add(Map(source));
            }

            return ShapeList;
        }
    }
}
