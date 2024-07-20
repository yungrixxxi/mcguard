using MCGuard.Core;
using MCGuard.Utils;
using System.Diagnostics;

namespace MCGuard
{
    public partial class MainWindow : Form
    {
        private ConfigManager? mcguardConfig;
        private ConfigManager? spigotConfig;
        private Server server;

        /// <summary>
        /// Port serveru.
        /// </summary>
        private int serverPort = 0;

        /// <summary>
        /// Nadpis okna serveru, užiteèné když bìží víc serverù.
        /// </summary>
        private string? windowTitle;

        public MainWindow() => InitializeComponent();

        private void AppLoaded(object sender, EventArgs e)
        {
            mcguardConfig = new ConfigManager("mcguard.ini");
            spigotConfig = new ConfigManager("server.properties");

            string[] requiredMcgConfig = ["srv.title", "srv.joinmsg", "memory.max", "memory.min"];
            string[] requiredSpigotConfig = ["server-port"];

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

            string[]? joinMessage = mcguardConfig.GetValue("srv.joinmsg")?.Split('|') ?? new string[] { };

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

            server = new Server("server.jar", minimumMemory, maximumMemory, joinMessage, ConsoleListBox);

            server.CreateProcess();
            server.Start();
        }

        /// <summary>
        /// Odešle výstup pøíkazu do serveru.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendInput(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string text = ServerInputTxt.Text;
                ServerInputTxt.Clear();

                server.SendInput(text);

                if (text.Equals("stop", StringComparison.CurrentCultureIgnoreCase))
                {
                    Environment.Exit(0);
                }
            }
        }

        /// <summary>
        /// Vyèistí výstup z konzole.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearListBtn_Click(object sender, EventArgs e)
        {
            ConsoleListBox.Items.Clear();
        }

        /// <summary>
        /// Otevøe github stránku vývojáøe.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenCreatorsUrlBtn_Click(object sender, EventArgs e)
        {
            Process proc = new()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    CreateNoWindow = true,
                }
            };

            proc.Start();
            proc.StandardInput.WriteLine("start https://github.com/yungrixxxi");
            proc.Close();
        }

        /// <summary>
        /// Zobrazí dialog jestli si pøejete doopravdy stopnout server a ukonèit process.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AskToClose(object sender, FormClosingEventArgs e)
        {
            var dialogResult = Logger.Information("Server will be stopped..\n\nDo you want to continue?", "MCGuard - Information", true);

            if (dialogResult == DialogResult.Yes)
            {
                server.SendInput("stop");
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
