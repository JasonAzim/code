using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.Text;
using Newtonsoft.Json;
using CarDealer;
using Global.Utility;
using Global.Repository;
using CarDealer.Repository;
using CarDealer.Shape;

namespace CarDealer.Service
{
    public class serviceSQL
    {
        // Get Category from Category Table
        public Task<CategoryShape[]> CategoryGetAll()
        {
            // TestQueries();
            var entity = MasterRepository.Instance.GetAll();
            var shape = new CategoryShape();
            var data = shape.MapList(entity);
            return System.Threading.Tasks.Task.FromResult(data.ToArray());
        }

        // Get Persons from ISHUB. This will return a union of Person data from PioneerRX and CPRSQL
        public Task<CategoryShape[]> Categories()
        {
            var entity = MasterRepository.Instance.GetCategories();
            var shape = new CategoryShape();
            var data = shape.MapList(entity);
            return System.Threading.Tasks.Task.FromResult(data.ToArray());
        }

        #region Test Queries
        public void TestQueries()
        {
            var test = CategoryGetAll();
        }

        #endregion
    }
}