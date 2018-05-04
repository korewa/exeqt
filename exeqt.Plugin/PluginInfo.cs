using System;

namespace exeqt.Plugin
{
    public class PluginInfo
    {
        public PluginInfo(string name, string description, Version version, PluginType type)
        {
            Name = name;
            Description = description;
            Version = version;
            Type = type;
        }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public Version Version { get; private set; }

        public PluginType Type { get; private set; }
    }
}