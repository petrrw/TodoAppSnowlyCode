class ApiClient {
  constructor(baseUrl) {
    this.baseUrl = baseUrl;
    this.todosEndpoint = `${baseUrl}/api/Todos`;
  }

  async request(url, options = {}) {
    const response = await fetch(url, options);

    if (!response.ok) 
      throw new Error("Request failed")

    if (response.status === 204) {
      return null;
    }

    return response.json()
  }

  async create(todo) {
    return this.request(this.todosEndpoint, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(todo),
    });
  }

  async getAll() {
    return this.request(this.todosEndpoint);
  }

  async getById(id) {
    return this.request(`${this.todosEndpoint}/${id}`);
  }

  async update(id, todo) {
    return this.request(`${this.todosEndpoint}/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(todo),
    });
  }

  async delete(id) {
    return this.request(`${this.todosEndpoint}/${id}`, {
      method: 'DELETE',
    });
  }
}

export default ApiClient;
