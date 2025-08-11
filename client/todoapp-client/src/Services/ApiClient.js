class ApiClient {
  constructor(baseUrl) {
    this.baseUrl = baseUrl;
    this.todosEndpoint = `${baseUrl}/api/Todos`;
  }

  async create(todo) {
    const response = await fetch(this.todosEndpoint, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(todo),
    });
    return response.json();
  }

  async getAll() {
    const response = await fetch(this.todosEndpoint);
    return response.json();
  }

  async getById(id) {
    const response = await fetch(`${this.todosEndpoint}/${id}`);
    return response.json();
  }

  async update(id, todo) {
    const response = await fetch(`${this.todosEndpoint}/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(todo),
    });
    return response.json();
  }

  async delete(id) {
    await fetch(`${this.todosEndpoint}/${id}`, {
      method: 'DELETE',
    });
  }
}

export default ApiClient;