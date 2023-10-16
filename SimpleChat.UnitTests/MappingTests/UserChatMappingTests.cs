using AutoMapper;
using SimpleChat.Business.MappingProfiles;

namespace SimpleChat.UnitTests.MappingTests
{
    public class UserChatMappingTests
    {
        [Fact]
        public void ValidateUserChatMappingProfile_ShouldReturnSuccessResult()
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(
           cfg =>
           {
               cfg.AddProfile(new UserChatMappingProfile());
           });

            IMapper mapper = new Mapper(mapperConfig);

            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
