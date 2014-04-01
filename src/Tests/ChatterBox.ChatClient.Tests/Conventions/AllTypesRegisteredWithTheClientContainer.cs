﻿using System;
using Autofac;
using Autofac.Builder;
using NUnit.Framework;

namespace ChatterBox.ChatClient.Tests.Conventions
{
    [TestFixture]
    public class AllTypesRegisteredWithTheClientContainer : AllTypesRegisteredWithTheContainer
    {
        protected override IContainer CreateContainer()
        {
            return IoC.LetThereBeIoC(ContainerBuildOptions.IgnoreStartableComponents);
        }

        protected override bool Filter(Type serviceType)
        {
            return true;
        }

        protected override bool IsKnownOffender(Type serviceType)
        {
            //if (serviceType == typeof(IReportingDataService)) return true;

            return false;
        }
    }
}