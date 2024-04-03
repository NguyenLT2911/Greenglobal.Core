import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'Core',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44398/',
    redirectUri: baseUrl,
    clientId: 'Core_App',
    responseType: 'code',
    scope: 'offline_access Core',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44398',
      rootNamespace: 'Greenglobal.Core',
    },
    Core: {
      url: 'https://localhost:44335',
      rootNamespace: 'Greenglobal.Core',
    },
  },
} as Environment;
