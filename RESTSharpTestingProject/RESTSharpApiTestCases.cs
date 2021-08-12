using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace RESTSharpTestingProject
{
    [TestClass]
    public class RESTSharpApiTestCases

    {
        RestClient client;

        [TestInitialize]
        public void SetUp()
        {
            client = new RestClient(" http://localhost:4000");
        }
        private  IRestResponse getEmployeeList()
        {
            RestRequest request = new RestRequest("/employees",Method.GET);

            IRestResponse response = client.Execute(request); 

            return response;
        }
        [TestMethod]
        public void OnCalling_ReturnEmployeeDB()
        {
            IRestResponse response = getEmployeeList();
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            List<EmployeeAttributes> employeeAttributesList = JsonConvert.DeserializeObject<List<EmployeeAttributes>>(response.Content);
            Assert.AreEqual(6, employeeAttributesList.Count);
        }
    }
}
