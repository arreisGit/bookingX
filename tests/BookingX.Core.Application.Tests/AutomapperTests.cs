using AutoMapper;
using BookingX.Core.Application.Automapper;
using Xunit;

namespace BookingX.Core.Application.Tests
{
    // TODO: Implement automapper UnitTests for the distinct object to objects mappings.
    public class AutomapperTests
    {
        [Fact]
        public void AutomapperConfiguration_IsValid(){
             var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>());
            config.AssertConfigurationIsValid();
        }
       
    }
}