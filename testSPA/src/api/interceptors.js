import router from '../router/index'

const authorizationHeaderInterceptor = config => {
  // Use oidc-store to check token and auth flags...
//   if (isAuthorizedForApiRequest(Vue.prototype.$auth.getAccessToken())) {
//     const accessToken = await Vue.prototype.$auth.getAccessToken();
//     config.headers.Authorization = `Bearer ${accessToken}`
//     console.log(`request w/ headers.Authorization = ${config.headers.Authorization}`)
//   }
  return config;
};

const unauthorizedInterceptor = {
  fulfilled: response => response,
  rejected: error => {
    // store.commit('setErrorState', {
    //   errorStatusCode: error.response.status,
    //   errorMessage: error.message,
    //   raw: error,
    // });
    router.push({
      name: 'error',
    });
    return Promise.reject(error);
  },
};

export default {
  request: [authorizationHeaderInterceptor],
  response: [unauthorizedInterceptor],
};
