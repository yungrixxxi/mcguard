using System.Diagnostics;

namespace MCGuard.Core
{
    internal class Server(string jarServerFile, int minimumMemory, int maximuMemory, string[] joinMessage, ListBox consoleListBox)
    {
        private Process? serverProcess;

        private readonly string[] joinMessage = joinMessage;
        private readonly string jarServerFile = jarServerFile;
        private readonly int minimumMemory = minimumMemory;
        private readonly int maximuMemory = maximuMemory;
        private ListBox consoleListBox = consoleListBox;

        /// <summary>
        /// Vytvoří a nastaví process pro jeho pozdější spuštění.
        /// </summary>
        public void CreateProcess() => serverProcess = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "java.exe",
                Arguments = "-Xms" + minimumMemory + "M -Xmx" + maximuMemory + "M -jar " + jarServerFile + " nogui",
                RedirectStandardInput = true,
                RedirectStandardError = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            }
        };

        /// <summary>
        /// Spustí server.
        /// </summary>
        public void Start()
        {
            if (serverProcess != null)
            {
                serverProcess.Start();
                serverProcess.BeginOutputReadLine();

                serverProcess.OutputDataReceived += (sender, e) =>
                {
                    if (e != null)
                    {
                        string? text = e.Data;

                        if (text != null && text.Contains("> ."))
                        {
                            string playerName = text.Split('<')[1].Trim();
                            playerName = playerName.Split('>')[0].Trim();
                            string command = text.Split(new[] { "> ." }, StringSplitOptions.None)[1].Trim();
                            OnCommandReceive(command, playerName);
                        }
                        else if (text != null && text.Contains("[/") && text.Contains("]") && text.Contains(":") && text.Contains("]:") && text.Contains("logged"))
                        {
                            string playerName = text.Split(new[] { "]: " }, StringSplitOptions.None)[1].Trim();
                            playerName = playerName.Split(new[] { "[/" }, StringSplitOptions.None)[0].Trim();
                            OnConnectionReceive(playerName);
                        }

                        if (text != null)
                        {
                            if (consoleListBox.InvokeRequired)
                            {
                                consoleListBox.Invoke(new Action(() =>
                                {
                                    consoleListBox.Items.Add(text);
                                    consoleListBox.TopIndex = consoleListBox.Items.Count - 1;
                                }));
                            }
                            else
                            {
                                consoleListBox.Items.Add(text);
                                consoleListBox.TopIndex = consoleListBox.Items.Count - 1;
                            }
                        }
                    }
                };
            }
        }

        /// <summary>
        /// Ukončí process serveru.
        /// </summary>
        public void Stop()
        {
            serverProcess?.Close();
        }

        /// <summary>
        /// Odešle vstup do procesu.
        /// </summary>
        /// <param name="command"></param>
        public void SendInput(string command)
        {
            serverProcess?.StandardInput.WriteLine(command);
            serverProcess?.StandardInput.Flush();
        }

        /// <summary>
        /// Spouští se při zadání příkazu do chatu začínajcí tečkou "."
        /// </summary>
        /// <param name="command"></param>
        /// <param name="playerName"></param>
        public void OnCommandReceive(string command, string playerName)
        {

        }

        /// <summary>
        /// Spouští se při připojení hráče.
        /// </summary>
        /// <param name="playerName"></param>
        public void OnConnectionReceive(string playerName)
        {
            foreach (var messageLine in joinMessage)
            {
                SendInput("tellraw " + playerName + " {\"text\":\"[Server] " + messageLine.Trim() + "\"}");
            }

            SendInput("tellraw " + playerName + " {\"text\":\"\"}");
        }
    }
}
