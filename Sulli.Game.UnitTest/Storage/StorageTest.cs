using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sulli.Game.Storage;
using Sulli.Game.Storage.Layer;
using Sulli.Game.Mock;

namespace Sulli.Game.UnitTest.Storage
{
    /// <summary>
    /// ≤÷¥¢≤‚ ‘
    /// </summary>
    [TestClass]
    public class StorageTest : StorageTestBase
    {
        /// <summary>
        /// ≤È—Ø≤‚ ‘
        /// </summary>
        [TestMethod]
        public void QueryTest()
        {
            StudentPipe? pipe = StorageManager.GetStoragePipe("Student") as StudentPipe;

            Assert.IsNotNull(pipe);

            Mock.MockStorageContext context = new Mock.MockStorageContext();

            var query = pipe.FindAsync(p => p.Name == "zhangsan", context).Result;

            Assert.IsTrue(query != null && query.IsSuccess);
        }
    }
}