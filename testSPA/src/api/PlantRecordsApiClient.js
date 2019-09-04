import Vue from 'vue'
import api from './client'

export default {
  async execute(method, resource, data) {
    const accessToken = await Vue.prototype.$auth.getAccessToken();
    const url = `https://localhost:5001/api/plants${resource}`;
    return api({
      method,
      url: url,
      data,
      headers: {
        Authorization: `Bearer ${accessToken}`,
        'Access-Control-Allow-Origin': '*'
      }
    }).then(req => {
      console.log(req)
      return req.data
    })
  },
  async getAll() {
    return this.execute('GET', '/')
  },
  create(data) {
    return this.execute('POST', '/', data)
  },
  update(id, data) {
    return this.execute('PUT', `/${id}`, data)
  },
  delete(id) {
    return this.execute('DELETE', `/${id}`)
  }
};