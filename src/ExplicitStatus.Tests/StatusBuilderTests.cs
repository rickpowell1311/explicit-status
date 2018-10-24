using ExplicitStatus.Internals;
using System;
using Xunit;

namespace ExplicitStatus.Tests
{
    public class StatusBuilderTests
    {
        [Fact]
        public void IsStatus_WhenStatusAlreadyDefined_ThrowsAmbiguousStatusConfigurationException()
        {
            Assert.Throws<AmbiguousStatusConfigurationException<StatusBuilderTestsEnum>>(() =>
            {
                StatusBuilder
                    .BuildStatus<StatusBuilderTestsEnum>()
                    .FromType<StatusBuilderTestsType>()
                    .IsStatus(StatusBuilderTestsEnum.Test1).When(x => true)
                    .IsStatus(StatusBuilderTestsEnum.Test1).When(x => true)
                    .Build();
            });
        }
    }

    public class StatusBuilderTestsType
    {
    }

    public enum StatusBuilderTestsEnum
    {
        Test1,
        Test2
    }
}
