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
        }
    }
}
