using Sunrise.BlfTool;
using Sunrise.BlfTool.TitleConverters;
using System;
using System.Collections.Generic;
using WarthogInc.BlfChunks;

namespace SunriseBlfTool.BlfChunks
{
    public class TitleConverterVersionMap
    {
        public static TitleConverterVersionMap singleton = new TitleConverterVersionMap();

        private Dictionary<string, Type> converters = new Dictionary<string, Type>();

        private TitleConverterVersionMap()
        {
            RegisterTitles();
        }

        private void RegisterTitles()
        {
            RegisterTitle<TitleConverter_06481>();
            RegisterTitle<TitleConverter_08172>();
            RegisterTitle<TitleConverter_09699>();
            RegisterTitle<TitleConverter_10015>();
            RegisterTitle<TitleConverter_11729>();
            RegisterTitle<TitleConverter_11856>();
            RegisterTitle<TitleConverter_12070>();
        }

        private void RegisterTitle<T>() where T : ITitleConverter, new()
        {
            converters.Add(new T().GetVersion(), typeof(T));
        }

        public ITitleConverter GetConverter(string version)
        {
            return (ITitleConverter)Activator.CreateInstance(converters[version]);
        }
    }
}
