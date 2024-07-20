using MCGuard.Structures;

namespace MCGuard.Utils
{
    internal class ConfigManager
    {
        /// <summary>
        /// Vlastnost jména konfiguračního souboru.
        /// </summary>
        public string ConfigFile { get; private init; }

        /// <summary>
        /// Oddělovací čára v konfiguraci.
        /// např. jmenohodnoty = hodnota
        /// </summary>
        private char configDelimiter = '=';

        /// <summary>
        /// List který načte konfiguraci ze souboru a poté se z něj konfigurace také čte.
        /// </summary>
        private List<Config> configList = new List<Config>();

        /// <summary>
        /// Konstruktor pro instanci konfigurace.
        /// </summary>
        /// <param name="configFile">Jméno souboru konfigurace.</param>
        public ConfigManager(string configFile)
        {
            ConfigFile = configFile;
        }

        /// <summary>
        /// Načte konfigurační soubor do paměti.
        /// </summary>
        /// <exception cref="Exception"></exception>
        public bool LoadConfiguration(string[] requiredConfiguration)
        {
            if (File.Exists(ConfigFile))
            {
                string[] configContent = File.ReadAllLines(ConfigFile);

                if (configContent != null)
                {
                    foreach (string configLine in configContent)
                    {
                        if (configLine != null && !string.IsNullOrEmpty(configLine) && configLine.Contains(configDelimiter))
                        {
                            string[] arguments = configLine.Split(configDelimiter);

                            string keyName = arguments[0].Trim();
                            string keyValue = arguments[1].Trim();

                            configList.Add(new Config { KeyName = keyName, KeyValue = keyValue });
                        }
                    }

                    foreach (var requiredConfig in requiredConfiguration)
                    {
                        bool exists = false;
                        foreach (var configLine in configList)
                        {
                            if (configLine.KeyName.Contains(requiredConfig))
                            {
                                exists = true;
                            }
                        }

                        if (!exists)
                        {
                            Logger.Error("Missing configuration '" + requiredConfig + "', please, make sure, the configuration is valid!", "MCGuard - Error");
                            Environment.Exit(1);
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Získá konfiguraci z listu podle klíče.
        /// </summary>
        /// <param name="keyName">Jméno klíče v konfiguraci.</param>
        /// <returns>Hodnota klíče.</returns>
        public string? GetValue(string keyName)
        {
            foreach (var config in configList)
                if (config.KeyName == keyName)
                    return config.KeyValue;

            return null;
        }
    }
}
