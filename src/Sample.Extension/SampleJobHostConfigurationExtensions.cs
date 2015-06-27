﻿using System;
using Microsoft.Azure.WebJobs.Host.Config;
using Sample.Extension;

namespace Microsoft.Azure.WebJobs
{
    public static class SampleJobHostConfigurationExtensions
    {
        public static void UseSample(this JobHostConfiguration config, SampleConfiguration sampleConfig = null)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            // Register our extension configuration provider
            config.RegisterExtensionConfigProvider(new SampleExtensionConfig(sampleConfig));
        }

        private class SampleExtensionConfig : IExtensionConfigProvider
        {
            private SampleConfiguration _config;

            public SampleExtensionConfig(SampleConfiguration config)
            {
                _config = config;
            }

            public void Initialize(ExtensionConfigContext context)
            {
                if (context == null)
                {
                    throw new ArgumentNullException("context");
                }

                // Register our extension binding providers
                context.Config.RegisterBindingExtensions(
                    new SampleAttributeBindingProvider(_config), 
                    new SampleTriggerAttributeBindingProvider(_config)
                );
            }
        }
    }
}