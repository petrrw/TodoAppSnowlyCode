using Newtonsoft.Json;
using System.Net;
using System.Text;
using TodoAppSnowlyCode.Data.Models;
using TodoAppSnowlyCode.DTO;

namespace ToDoAppSnowlyCode.IntegrationTests
{
    public class TodosIntegrationTests : ITTestsBase
    {
        [Test]
        public async Task Insert_ToDoItem_Success()
        {
            var todo = new CreateToDoItemDto
            {
                Title = $"{Guid.NewGuid}",
                DueDate = new DateTime(2025, 6,25),
                IsCompleted = false,
            };

            var json = JsonConvert.SerializeObject(todo);
            var reqContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _serverFactory.CreateClient();
            var res = await client.PostAsync("api/todos/", reqContent);
            var content = await res.Content.ReadAsStringAsync();

            var returnedItem = JsonConvert.DeserializeObject<ToDoItem>(content);

            Assert.That(returnedItem?.Title, Is.EqualTo(todo.Title));
            Assert.That(returnedItem.DueDate, Is.EqualTo(todo.DueDate));
            Assert.That(returnedItem.IsCompleted, Is.EqualTo(todo.IsCompleted));
            Assert.That(DateOnly.FromDateTime(returnedItem.CreatedAt), Is.EqualTo(DateOnly.FromDateTime(DateTime.Now)));

            Assert.That(res.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        [Test]
        public async Task Insert_ToDoItem_ShortTitle_Failure()
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