using Xunit;

namespace ExplicitStatus.Tests
{
    public class StatusTests
    {
        [Fact]
        public void Status_WhenNotAllPropertiesDefined_ThrowsInvalidStatusConfigurationException()
        {
            var status = StatusBuilder.BuildStatus<StatusTestsEnum>()
                .FromType<StatusTestsType>()
                .IsStatus(StatusTestsEnum.Incomplete)
                    .When(x => x.Property1, p => !p.HasValue)
                .Build();

            Assert.Throws<InvalidStatusConfigurationException<StatusTestsType, StatusTestsEnum>>(
                () => status.GetFor(new StatusTestsType()));
        }

        [Fact]
        public void Status_WhenNoStatusConditionsSatisfied_ThrowsUndefinedStatusException()
        {
            var status = StatusBuilder.BuildStatus<StatusTestsEnum>()
                .FromType<StatusTestsType>()
                .Build();

            Assert.Throws<UndefinedStatusException<StatusTestsType, StatusTestsEnum>>(
                () => status.GetFor(new StatusTestsType()));
        }

        [Fact]
        public void Status_WhenNotAllConditionsSatisfied_ThrowsUndefinedStatusException()
        {
            var status = StatusBuilder.BuildStatus<StatusTestsEnum>()
                .FromType<StatusTestsType>()
                .IsStatus(StatusTestsEnum.PartiallyComplete)
                    .When(x => x.Property1, p => p.HasValue)
                    .And(x => x.Property2, p => !p.HasValue)
                .Build();

            Assert.Throws<UndefinedStatusException<StatusTestsType, StatusTestsEnum>>(
                () => status.GetFor(new StatusTestsType
                {
                    Property1 = 1,
                    Property2 = 2
                }));
        }

        [Fact]
        public void Status_WhenProperty1Complete_AndProperty2Ignored_ReturnsStatus()
        {
            var status = StatusBuilder.BuildStatus<StatusTestsEnum>()
                .FromType<StatusTestsType>(config =>
                {
                    config.Ignore(x => x.Property2);
                })
                .IsStatus(StatusTestsEnum.Complete)
                    .When(x => x.Property1, p => p.HasValue)
                .Build();

            Assert.Equal(StatusTestsEnum.Complete, status.GetFor(new StatusTestsType { Property1 = 1 }));
        }

        [Fact]
        public void Status_WhenAllPropertiesDefined_ReturnsStatus()
        {
            var status = StatusBuilder.BuildStatus<StatusTestsEnum>()
                .FromType<StatusTestsType>()
                .IsStatus(StatusTestsEnum.Incomplete)
                    .When(x => x.Property1, p => !p.HasValue)
                    .And(x => x.Property2, p => !p.HasValue)
                .Build();

            Assert.Equal(StatusTestsEnum.Incomplete, status.GetFor(new StatusTestsType()));
        }

        [Fact]
        public void Status_WhenMultipleStatusesDefined_ReturnsAllStatuses()
        {
            var status = StatusBuilder.BuildStatus<StatusTestsEnum>()
                .FromType<StatusTestsType>()
                .IsStatus(StatusTestsEnum.Incomplete)
                    .When(x => x.Property1, p => !p.HasValue)
                    .And(x => x.Property2, p => !p.HasValue)
                .IsStatus(StatusTestsEnum.PartiallyComplete)
                    .When(x => x.Property1, p => p.HasValue)
                    .And(x => x.Property2, p => !p.HasValue)
                .IsStatus(StatusTestsEnum.Complete)
                    .When(x => x.Property1, p => p.HasValue)
                    .And(x => x.Property2, p => p.HasValue)
                .Build();

            Assert.Equal(StatusTestsEnum.Incomplete, status.GetFor(new StatusTestsType()));
            Assert.Equal(StatusTestsEnum.PartiallyComplete, status.GetFor(new StatusTestsType { Property1 = 1 }));
            Assert.Equal(StatusTestsEnum.Complete, status.GetFor(new StatusTestsType { Property1 = 1, Property2 = 2 }));
        }

        [Fact]
        public void Status_WhenMultipleStatusesDefinedWithDefaults_ReturnsAllStatuses()
        {
            var status = StatusBuilder.BuildStatus<StatusTestsEnum>()
                .FromType<StatusTestsType>()
                .IsStatus(StatusTestsEnum.Incomplete)
                    .When(x => x.Property1, p => !p.HasValue)
                    .And(x => x.Property2, p => !p.HasValue)
                .IsStatus(StatusTestsEnum.Complete)
                    .When(x => x.Property1, p => p.HasValue)
                    .And(x => x.Property2, p => p.HasValue)
                .IsStatus(StatusTestsEnum.PartiallyComplete)
                    .ByDefault()
                .Build();


            Assert.Equal(StatusTestsEnum.Incomplete, status.GetFor(new StatusTestsType()));
            Assert.Equal(StatusTestsEnum.PartiallyComplete, status.GetFor(new StatusTestsType { Property1 = 1 }));
            Assert.Equal(StatusTestsEnum.PartiallyComplete, status.GetFor(new StatusTestsType { Property2 = 2 }));
            Assert.Equal(StatusTestsEnum.Complete, status.GetFor(new StatusTestsType { Property1 = 1, Property2 = 2 }));
        }
    }

    public class StatusTestsType
    {
        public int? Property1 { get; set; }

        public int? Property2 { get; set; }
    }

    public enum StatusTestsEnum
    {
        Incomplete,
        PartiallyComplete,
        Complete
    }
}
