using Greenglobal.Core.MongoDB;
using Greenglobal.Core.Samples;
using Xunit;

namespace Greenglobal.Core.MongoDb.Applications;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleAppService_Tests : SampleAppService_Tests<CoreMongoDbTestModule>
{

}
