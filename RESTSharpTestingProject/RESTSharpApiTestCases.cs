using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            Assert.AreEqual(7, employeeAttributesList.Count);
        }
        [TestMethod]
        public void OnCalling_AddPersonToJsonDB()
        {
            RestRequest request = new RestRequest("/employees", Method.POST);
            JObject jsonObjectBody = new JObject();
            jsonObjectBody.Add("id", "8");
            jsonObjectBody.Add("firstName", "Charan");
            jsonObjectBody.Add("lastName", "Kanduri");
            jsonObjectBody.Add("salary", "2400");

            request.AddParameter("application/json", jsonObjectBody, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            EmployeeAttributes employeeAttributes = JsonConvert.DeserializeObject<EmployeeAttributes>(response.Content);
            Assert.AreEqual("Charan", employeeAttributes.firstName);
        }        
    }
}
