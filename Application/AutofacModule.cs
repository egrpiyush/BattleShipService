using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Common;

namespace Application
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterMatchingTypesAsImplementedInterfaces(typeof(AutofacModule).Assembly,
                new[] { ".+Command$", ".+Query$", ".+Service$" });
        }
    }
}
