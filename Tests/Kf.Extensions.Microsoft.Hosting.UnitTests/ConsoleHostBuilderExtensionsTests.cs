using Kf.Extensions.Microsoft.Hosting.Console;
using System;
using Xunit;

namespace Kf.Extensions.Microsoft.Hosting.UnitTests
{
    public sealed class ConsoleHostBuilderExtensionsTests
    {
        [Fact]
        public void Throws_ConsoleHostBuilderException_when_no_Run_method_with_return_type_Task_is_defined()
        {
            var exception = Assert.Throws<ConsoleHostBuilderException>(() =>
            {
                ConsoleHostBuilder.CreateAndRunApplication<ConsoleHostBuilderExtensionsTests>();
            });

            exception.Message.Contains(" because the 'Run' method's return type is not of type 'Task'");
        }
    }
}
