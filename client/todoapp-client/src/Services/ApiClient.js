class ApiClient {
  constructor(baseUrl) {
    this.baseUrl = baseUrl;
  }

  async create(todo) {
    const response = await fetch(this.baseUrl, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(todo),
    });
    return response.json();
  }

  async getAll() {
    const response = await fetch(this.baseUrl);
    return response.json();
  }

  async getById(id) {
    const response = await fetch(`${this.baseUrl}/${id}`);
    return response.json();
  }

  async update(id, todo) {
    const response = await fetch(`${this.baseUrl}/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(todo),
    });
    return response.json();
  }

  async delete(id) {
    await fetch(`${this.baseUrl}/${id}`, {
      method: 'DELETE',
    });
  }
}

export default ApiClient;