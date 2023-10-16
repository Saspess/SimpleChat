using AutoMapper;
using SimpleChat.Business.MappingProfiles;

namespace SimpleChat.UnitTests.MappingTests
{
    public class UserMappingTests
    {
        [Fact]
        public void ValidateUserMappingProfile_ShouldReturnSuccessResult()
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(
           cfg =>
           {
               cfg.AddProfile(new UserMappingProfile());
           });

            IMapper mapper = new Mapper(mapperConfig);

            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
