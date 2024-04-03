using Greenglobal.Core.Samples;
using Xunit;

namespace Greenglobal.Core.MongoDB.Domains;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleDomain_Tests : SampleManager_Tests<CoreMongoDbTestModule>
{

}
