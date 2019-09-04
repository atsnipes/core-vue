import * as axiosClient from 'axios'
import interceptors from './interceptors'

// interceptors.request.forEach(interceptor => {
//   axiosClient.interceptors.request.use(interceptor)
// })

// interceptors.response.forEach(interceptor => {
//   axiosClient.interceptors.response.use(interceptor.fulfilled, interceptor.rejected)
// })

export default axiosClient
