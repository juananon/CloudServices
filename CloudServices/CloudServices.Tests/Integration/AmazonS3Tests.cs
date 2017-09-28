using CloudServices.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace CloudServices.Tests.Integration
{
    [TestClass]
    public class AmazonS3Tests
    {
        [TestMethod]
        public async Task Given_a_key_when_gets_a_file_must_return_a_valid_file()
        {
            //Configure();
            ICloudStorage amazonS3 = new AmazonS3();
            var file = await amazonS3.Get("MemberImages", "00dc8226-8dfe-4dc2-8672-e815f3fee5fe.jpg");
            Assert.IsNotNull(file);
        }

        [TestMethod]
        public async Task Given_a_bucket_when_list_a_bucket_must_return_a_valid_list_of_keys()
        {
            //Configure();
            var folder = "MemberImages";
            ICloudStorage amazonS3 = new AmazonS3();
            var keys = (await amazonS3.List(folder)).ToList();
        }
    }
}
