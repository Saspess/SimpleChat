using AutoMapper;
using SimpleChat.Business.MappingProfiles;

namespace SimpleChat.UnitTests.MappingTests
{
    public class ChatMappingTests
    {
        [Fact]
        public void ValidateChatMappingProfile_ShouldReturnSuccessResult()
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(
           cfg =>
           {
               cfg.AddProfile(new ChatMappingProfile());
           });

            IMapper mapper = new Mapper(mapperConfig);

            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
