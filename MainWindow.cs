using MCGuard.Utils;

namespace MCGuard
{
    public partial class MainWindow : Form
    {
        private static ConfigManager? mcguardConfig;
        private static ConfigManager? spigotConfig;

        /// <summary>
        /// Port serveru.
        /// </summary>
        private int serverPort = 0;

        /// <summary>
        /// Nadpis okna serveru, užiteèné když bìží víc serverù.
        /// </summary>
        private string? windowTitle;

        public MainWindow()
        {
            InitializeComponent();

            
        }

        private void AppLoaded(object sender, EventArgs e)
        {
            mcguardConfig = new ConfigManager("mcguard.ini");
            spigotConfig = new ConfigManager("server.properties");

            string[] requiredMcgConfig = { "srv.title", "memory.max", "memory.min" };
            string[] requiredSpigotConfig = { "server-port" };

            if (!mcguardConfig.LoadConfiguration(requiredMcgConfig))
            {
                Logger.Error("Cannot load a configuration file " + mcguardConfig.ConfigFile, "MCGuard - Error");
                Environment.Exit(1);
            }

            if (!spigotConfig.LoadConfiguration(requiredSpigotConfig))
            {
                Logger.Error("Cannot load a configuration file " + spigotConfig.ConfigFile + ", please, try firstly run minecraft server without this software.", "MCGuard - Error");
                Environment.Exit(1);
            }

            if (int.TryParse(spigotConfig.GetValue("server-port"), out int port))
            {
                serverPort = port;
            }

            windowTitle = mcguardConfig.GetValue("srv.title");

            Text = "MCGuard Final v1 - " + windowTitle + " : " + serverPort;

            int minimumMemory = 256;   // minimální doporuèená velikost pro verzi 1.8.X
            int maximumMemory = 1024;  // maximální hodnota mùže bejt 1024, dostaèující.

            if (int.TryParse(mcguardConfig.GetValue("memory.min"), out int min))
            {
                minimumMemory = min;
            }

            if (int.TryParse(mcguardConfig.GetValue("memory.max"), out int max))
            {
                maximumMemory = max;
            }

            if (maximumMemory < minimumMemory)
            {
                Logger.Error("Maximum value of memory must be greater or equal to minimum value.\n\nABORT!", "MCGuard - Error");
                Environment.Exit(1);
            }
            else if (minimumMemory < 128)
            {
                var dialogResult = Logger.Warning("Recommended minumum memory value is 256M or more!\n\nDo you want use YOUR settings?", "MCGuard - Warning", true);

                if (dialogResult == DialogResult.No)
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
