using Ninject.Activation.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Users.Infrastructure
{
    public interface ISettings
    {
        GeneralSettings General { get; }
        SeoSettings Seo { get; }
        void Save();
    }

    public class Settings : ISettings
    {
        // 1
        private readonly Lazy<GeneralSettings> _generalSettings;
        // 2
        public GeneralSettings General { get { return _generalSettings.Value; } }

        private readonly Lazy<SeoSettings> _seoSettings;
        public SeoSettings Seo { get { return _seoSettings.Value; } }

        private readonly AppIdentityDbContext _unitOfWork;
        private readonly ICache _cache;
        public Settings(AppIdentityDbContext unitOfWork, ICache cache)
        {
            // ARGUMENT CHECKING SKIPPED FOR BREVITY
            _unitOfWork = unitOfWork;
            _cache = cache;
            _generalSettings = new Lazy<GeneralSettings>(CreateSettingsWithCache<GeneralSettings>);
            _seoSettings = new Lazy<SeoSettings>(CreateSettingsWithCache<SeoSettings>);
        }

        private T CreateSettingsWithCache<T>() where T : SettingsBase, new()
        {
            // this is where you would implement loading from ICache
            throw new NotImplementedException();
        }
        public Settings(AppIdentityDbContext unitOfWork)
        {
            // ARGUMENT CHECKING SKIPPED FOR BREVITY
            _unitOfWork = unitOfWork;
            // 3
            _generalSettings = new Lazy<GeneralSettings>(CreateSettings<GeneralSettings>);
            _seoSettings = new Lazy<SeoSettings>(CreateSettings<SeoSettings>);
        }

        public void Save()
        {
            // only save changes to settings that have been loaded
            if (_generalSettings.IsValueCreated)
                _generalSettings.Value.Save(_unitOfWork);

            if (_seoSettings.IsValueCreated)
                _seoSettings.Value.Save(_unitOfWork);

            _unitOfWork.SaveChanges();
        }
        // 4
        private T CreateSettings<T>() where T : SettingsBase, new()
        {
            var settings = new T();
            settings.Load(_unitOfWork);
            return settings;
        }
    }
}