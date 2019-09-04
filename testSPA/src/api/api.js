/**
 * Provides an interface for IntelligenceOIDC API actions
 */
import axios from './client.js'

export default {
  /**
   * @descript Fetches a qlik JWT session token for the given user context
   * @returns {Promise<object>}
   */
  getFoods() {
    return axios.get(`http://localhost:5000/api/values`);
  },
};