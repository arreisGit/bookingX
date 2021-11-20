using System;
using AutoMapper;
using BookingX.Core.Application.Common;

namespace BookingX.Core.Application.Tests.ClassFixtures
{
    public class AutoMapperFixture : IDisposable
    {
        public IMapper Mapper { get; private set; }

        public AutoMapperFixture()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(MappingProfiles).Assembly);
            });
            Mapper = configuration.CreateMapper();
        }

        public void Dispose()
        { }

    }
}
