using System.Diagnostics;

namespace MCGuard.Core
{
    internal class Server(string jarServerFile, int minimumMemory, int maximuMemory, string[] joinMessage, ListBox consoleListBox, ListView playerListView)
    {
        private Process? serverProcess;
        private DateTime serverUptime;

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
            serverUptime = DateTime.Now;

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
                            return;
                        }
                        else if (text != null && text.Contains("[/") && text.Contains("]") && text.Contains(":") && text.Contains("]:") && text.Contains("logged"))
                        {
                            string playerName = text.Split(new[] { "]: " }, StringSplitOptions.None)[1].Trim();
                            playerName = playerName.Split(new[] { "[/" }, StringSplitOptions.None)[0].Trim();

                            string playerIp = text.Split(new[] { "[/" }, StringSplitOptions.None)[1].Trim();
                            playerIp = playerIp.Split(':')[0].Trim();

                            string playerId = text.Split(new[] { "entity id" }, StringSplitOptions.None)[1].Trim();
                            playerId = playerId.Split(new[] { " at (" }, StringSplitOptions.None)[0].Trim();

                            OnConnectionReceive(playerName, playerIp, playerId);
                            return;
                        }
                        else if (text != null && text.Contains("left the game.") && text.Contains("]:"))
                        {
                            string playerName = text.Split(new[] { "left the game." }, StringSplitOptions.None)[0].Trim();
                            playerName = playerName.Split(new[] { "]:" }, StringSplitOptions.None)[1].Trim();

                            OnDisconnectionReceive(playerName);
                            return;
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
            if (command.Equals("info", StringComparison.CurrentCultureIgnoreCase))
            {
                TimeSpan timeSpan = (DateTime.Now - serverUptime);

                SendInput("tellraw " + playerName + " {\"text\":\"> Server info:\"}");
                SendInput("tellraw " + playerName + " {\"text\":\"\"}");
                SendInput("tellraw " + playerName + " {\"text\":\"Uptime: " + timeSpan.Days + " days " + timeSpan.Hours + " hours " + timeSpan.Minutes+ " minutes\"}");
                SendInput("tellraw " + playerName + " {\"text\":\"\"}");
            }
            else
            {
                SendInput("tellraw " + playerName + " {\"text\":\"[Server] Unknown mcguard command: ." + command + "\"}");
            }
        }

        /// <summary>
        /// Spouští se při připojení hráče.
        /// </summary>
        /// <param name="playerName"></param>
        public void OnConnectionReceive(string playerName, string playerIp, string playerId)
        {
            foreach (var messageLine in joinMessage)
            {
                SendInput("tellraw " + playerName + " {\"text\":\"[Server] " + messageLine.Trim() + "\"}");
            }

            SendInput("tellraw " + playerName + " {\"text\":\"\"}");

            ListViewItem lvi = new ListViewItem((playerListView.Items.Count + 1).ToString());
            lvi.SubItems.Add(playerId);
            lvi.SubItems.Add(playerName);
            lvi.SubItems.Add(playerIp);

            playerListView.Items.Add(lvi);
        }

        /// <summary>
        /// Spouští se při odpojení hráče.
        /// </summary>
        /// <param name="playerName"></param>
        public void OnDisconnectionReceive(string playerName)
        {
            for (int i = 0; i < playerListView.Items.Count; i++)
            {
                if (playerListView.Items[i].SubItems[2].Text.Contains(playerName)) {
                    playerListView.Items.RemoveAt(i);
                    break;
                }
            }
        }
    }
}
