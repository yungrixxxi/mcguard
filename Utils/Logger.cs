namespace MCGuard.Utils
{
    internal class Logger
    {
        /// <summary>
        /// Zobrazí dialogové okno s chybou.
        /// </summary>
        /// <param name="message">Zpráva chyby</param>
        /// <param name="caption">Nadpis chyby</param>
        /// <param name="yesNoButtons">Vyžaduje ano|ne tlačítka pokud je hodnota nastavena na TRUE</param>
        /// <returns>Vrací DialogResult hodnotu</returns>
        public static DialogResult Error(string message, string caption, bool yesNoButtons = false)
        {
            if (yesNoButtons)
            {
                return MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            }

            return MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Zobrazí dialogové okno s upozorněním.
        /// </summary>
        /// <param name="message">Zpráva varování</param>
        /// <param name="caption">Nadpis varování</param>
        /// <param name="yesNoButtons">Vyžaduje ano|ne tlačítka pokud je hodnota nastavena na TRUE</param>
        /// <returns>Vrací DialogResult hodnotu</returns>
        public static DialogResult Warning(string message, string caption, bool yesNoButtons = false)
        {
            if (yesNoButtons)
            {
                return MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }

            return MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Zobrazí dialogové okno s informací.
        /// </summary>
        /// <param name="message">Zpráva informace</param>
        /// <param name="caption">Nadpis informance</param>
        /// <param name="yesNoButtons">Vyžaduje ano|ne tlačítka pokud je hodnota nastavena na TRUE</param>
        /// <returns>Vrací DialogResult hodnotu</returns>
        public static DialogResult Information(string message, string caption, bool yesNoButtons = false)
        {
            if (yesNoButtons)
            {
                return MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            }

            return MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
