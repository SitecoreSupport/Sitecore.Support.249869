using System;
using Sitecore.Diagnostics;
using Sitecore.ExperienceAnalytics.Api;
using Sitecore.ExperienceAnalytics.Core.Diagnostics;
using Sitecore.ExperienceAnalytics.Core.Grouping;
using Sitecore.Globalization;
using Sitecore.StringExtensions;

namespace Sitecore.Support.ExperienceAnalytics.Api.Grouping
{
    internal class RegionLabeler : IVisitGroupLabeler
    {
        private readonly ILogger _logger;

        public RegionLabeler(ILogger logger)
        {
            _logger = logger;
        }

        public RegionLabeler()
            : this(ApiContainer.GetLogger())
        {
        }

        public string GetLabel(string groupId, Language language)
        {
            Assert.IsNotNullOrEmpty(groupId, "key");
            try
            {
                return (groupId == "ZZ") ? "[unknown region]" : GetRegionTranslation(groupId);
            }
            catch (Exception)
            {
                _logger.Warn("Could not resolve dimension key for region: '{0}'".FormatWith(groupId));
                return "[unknown region]";
            }
        }

        private static string GetRegionTranslation(string regionCode)
        {
            return typeof(Regions).GetField(regionCode).GetValue(null) as string;
        }
    }
}