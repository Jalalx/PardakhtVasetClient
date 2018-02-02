using Septa.PardakhtVaset.Client;
using System;
using Xunit;

namespace PardakhtVasetClientTests
{
    public class PardakhtVasetClientTests
    {
        [Fact]
        public void CreateInstance_WithNullParameterForOptions_ThrowsArgumentNullException()
        {
            var ex = Record.Exception(() => new PardakhtVasetClient(null));
            Assert.IsType<ArgumentNullException>(ex);
        }

        [Fact]
        public void CreateInstance_WithNullParameterForOptionsAndDbInitializer_ThrowsArgumentNullException()
        {
            var ex = Record.Exception(() => new PardakhtVasetClient(null, null));
            Assert.IsType<ArgumentNullException>(ex);
        }
    }
}
