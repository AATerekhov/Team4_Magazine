﻿using AutoFixture.AutoMoq;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using AutoFixture.Community.AutoMapper;
using MagazineHost.Mapping;
using Magazine.UnitTests.Applications;

namespace Magazine.UnitTests.Helps
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute() : base(fixtureFactory: fixtureFactory)
        { }
        private static readonly Func<IFixture> fixtureFactory = () =>
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            fixture.Customize<GetRewardMagazine>(c => c.OmitAutoProperties());

            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
             .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            fixture.Customize(new AutoMapperCustomization(cfg =>
            {
                cfg.AddProfile<RewardMagazineMappingProfile>();
            }));
            return fixture;
        };
    }
}
