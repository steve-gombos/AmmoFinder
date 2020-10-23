using AmmoFinder.Persistence.Mappers;
using AutoMapper;
using Xunit;

namespace AmmoFinder.Persistence.UnitTests.Mappers
{
    public class MapperTests
    {
        private IMapper CreateMapper()
        {
            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.AddProfile<PersistenceMapper>();
            });

            return new Mapper(mapperConfiguration);
        }

        [Fact]
        public void AutoMapperProfile_IsValid()
        {
            // Arrange
            var mapper = CreateMapper();

            // Act

            // Assert
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
