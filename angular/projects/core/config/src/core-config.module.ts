import { ModuleWithProviders, NgModule } from '@angular/core';
import { CORE_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class CoreConfigModule {
  static forRoot(): ModuleWithProviders<CoreConfigModule> {
    return {
      ngModule: CoreConfigModule,
      providers: [CORE_ROUTE_PROVIDERS],
    };
  }
}
