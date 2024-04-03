import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { CoreComponent } from './components/core.component';
import { CoreRoutingModule } from './core-routing.module';

@NgModule({
  declarations: [CoreComponent],
  imports: [CoreModule, ThemeSharedModule, CoreRoutingModule],
  exports: [CoreComponent],
})
export class CoreModule {
  static forChild(): ModuleWithProviders<CoreModule> {
    return {
      ngModule: CoreModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<CoreModule> {
    return new LazyModuleFactory(CoreModule.forChild());
  }
}
