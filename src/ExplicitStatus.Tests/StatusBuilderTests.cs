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
                    .IsStatus(StatusBuilderTestsEnum.Test1)
                        .When(x => x.Property, p => !p.HasValue)
                    .IsStatus(StatusBuilderTestsEnum.Test1)
                        .When(x => x.Property, p => !p.HasValue)
                    .Build();
            });
        }
    }

    public class StatusBuilderTestsType
    {
        public int? Property { get; set; }
    }

    public enum StatusBuilderTestsEnum
    {
        Test1,
        Test2
    }
}
