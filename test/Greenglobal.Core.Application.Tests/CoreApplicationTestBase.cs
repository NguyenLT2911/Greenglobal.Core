﻿using Volo.Abp.Modularity;

namespace Greenglobal.Core;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class CoreApplicationTestBase<TStartupModule> : CoreTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
