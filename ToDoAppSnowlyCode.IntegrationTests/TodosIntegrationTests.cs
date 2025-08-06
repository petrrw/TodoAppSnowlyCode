using Newtonsoft.Json;
using System.Net;
using System.Text;
using TodoAppSnowlyCode.DTO;

namespace ToDoAppSnowlyCode.IntegrationTests
{
    // Jednoduché IT na ukázku.. v praxi by se však hodilo mít nìjaký pomocný aparát na vkládání dat, u Success testu lépe kontrolovat že k vytvoøení skuteènì došlo
    // a samozøejmì více TC + pokrytí unit testy
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