using Newtonsoft.Json;
using System.Net;
using System.Text;
using TodoAppSnowlyCode.DTO;

namespace ToDoAppSnowlyCode.IntegrationTests
{
    // Jednoduch� IT na uk�zku.. v praxi by se v�ak hodilo m�t n�jak� pomocn� apar�t na vkl�d�n� dat, u Success testu l�pe kontrolovat �e k vytvo�en� skute�n� do�lo
    // a samoz�ejm� v�ce TC + pokryt� unit testy
    public class TodosIntegrationTests : ITTestsBase
    {
        [Test]
        public async Task InsertAndGetToDoItem_Success()
        {
            var todo = new CreateToDoItemDto
            {
                Title = "TestItem",
                DueDate = DateTime.Now.AddDays(20),
                IsCompleted = false,
            };

            var json = JsonConvert.SerializeObject(todo);
            var reqContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _serverFactory.CreateClient();
            var res = await client.PostAsync("api/todos/", reqContent);

            Assert.That(res.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        [Test]
        public async Task InsertAndGetToDoItem_ShortTitle_Failure()
        {
            var todo = new CreateToDoItemDto
            {
                Title = "a",
                DueDate = DateTime.Now.AddDays(20),
                IsCompleted = false,
            };

            var json = JsonConvert.SerializeObject(todo);
            var reqContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _serverFactory.CreateClient();
            var res = await client.PostAsync("api/todos/", reqContent);
            var content = await res.Content.ReadAsStringAsync();

            Assert.That(res.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(content, Is.EqualTo("{\"errors\":[{\"field\":\"Title\",\"error\":\"The length of 'Title' must be at least 3 characters. You entered 1 characters.\"}]}"));
        }
    }
}