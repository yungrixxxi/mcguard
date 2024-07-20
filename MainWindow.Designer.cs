namespace MCGuard
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ServerInputTxt = new TextBox();
            ConsoleListBox = new ListBox();
            ClearListBtn = new Button();
            PlayerListView = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            BanSelectedBtn = new Button();
            SelectedPlayerLbl = new Label();
            KickSelectedBtn = new Button();
            KillSelectedBtn = new Button();
            OpenCreatorsUrlBtn = new Button();
            SuspendLayout();
            // 
            // ServerInputTxt
            // 
            ServerInputTxt.Location = new Point(852, 476);
            ServerInputTxt.Name = "ServerInputTxt";
            ServerInputTxt.Size = new Size(343, 23);
            ServerInputTxt.TabIndex = 0;
            ServerInputTxt.KeyDown += SendInput;
            // 
            // ConsoleListBox
            // 
            ConsoleListBox.FormattingEnabled = true;
            ConsoleListBox.ItemHeight = 15;
            ConsoleListBox.Location = new Point(852, 90);
            ConsoleListBox.Name = "ConsoleListBox";
            ConsoleListBox.Size = new Size(343, 379);
            ConsoleListBox.TabIndex = 1;
            // 
            // ClearListBtn
            // 
            ClearListBtn.Location = new Point(852, 56);
            ClearListBtn.Name = "ClearListBtn";
            ClearListBtn.Size = new Size(75, 28);
            ClearListBtn.TabIndex = 2;
            ClearListBtn.Text = "Clear List";
            ClearListBtn.UseVisualStyleBackColor = true;
            ClearListBtn.Click += ClearListBtn_Click;
            // 
            // PlayerListView
            // 
            PlayerListView.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4 });
            PlayerListView.FullRowSelect = true;
            PlayerListView.GridLines = true;
            PlayerListView.Location = new Point(12, 11);
            PlayerListView.Name = "PlayerListView";
            PlayerListView.Size = new Size(834, 458);
            PlayerListView.TabIndex = 3;
            PlayerListView.UseCompatibleStateImageBehavior = false;
            PlayerListView.View = View.Details;
            PlayerListView.SelectedIndexChanged += OnItemSelection;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Number";
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Player ID";
            columnHeader2.Width = 80;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Player name";
            columnHeader3.Width = 200;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "IP Address";
            columnHeader4.Width = 160;
            // 
            // BanSelectedBtn
            // 
            BanSelectedBtn.Location = new Point(608, 475);
            BanSelectedBtn.Name = "BanSelectedBtn";
            BanSelectedBtn.Size = new Size(76, 25);
            BanSelectedBtn.TabIndex = 4;
            BanSelectedBtn.Text = "Ban";
            BanSelectedBtn.UseVisualStyleBackColor = true;
            BanSelectedBtn.Click += BanSelectedBtn_Click;
            // 
            // SelectedPlayerLbl
            // 
            SelectedPlayerLbl.AutoSize = true;
            SelectedPlayerLbl.Location = new Point(852, 11);
            SelectedPlayerLbl.Name = "SelectedPlayerLbl";
            SelectedPlayerLbl.Size = new Size(96, 15);
            SelectedPlayerLbl.TabIndex = 5;
            SelectedPlayerLbl.Text = "Selected player #";
            // 
            // KickSelectedBtn
            // 
            KickSelectedBtn.Location = new Point(689, 475);
            KickSelectedBtn.Name = "KickSelectedBtn";
            KickSelectedBtn.Size = new Size(76, 25);
            KickSelectedBtn.TabIndex = 6;
            KickSelectedBtn.Text = "Kick";
            KickSelectedBtn.UseVisualStyleBackColor = true;
            KickSelectedBtn.Click += KickSelectedBtn_Click;
            // 
            // KillSelectedBtn
            // 
            KillSelectedBtn.Location = new Point(770, 475);
            KillSelectedBtn.Name = "KillSelectedBtn";
            KillSelectedBtn.Size = new Size(76, 25);
            KillSelectedBtn.TabIndex = 7;
            KillSelectedBtn.Text = "Kill";
            KillSelectedBtn.UseVisualStyleBackColor = true;
            KillSelectedBtn.Click += KillSelectedBtn_Click_1;
            // 
            // OpenCreatorsUrlBtn
            // 
            OpenCreatorsUrlBtn.Location = new Point(1084, 56);
            OpenCreatorsUrlBtn.Name = "OpenCreatorsUrlBtn";
            OpenCreatorsUrlBtn.Size = new Size(111, 28);
            OpenCreatorsUrlBtn.TabIndex = 8;
            OpenCreatorsUrlBtn.Text = "yungrixxxi.xyz";
            OpenCreatorsUrlBtn.UseVisualStyleBackColor = true;
            OpenCreatorsUrlBtn.Click += OpenCreatorsUrlBtn_Click;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1205, 511);
            Controls.Add(OpenCreatorsUrlBtn);
            Controls.Add(KillSelectedBtn);
            Controls.Add(KickSelectedBtn);
            Controls.Add(SelectedPlayerLbl);
            Controls.Add(BanSelectedBtn);
            Controls.Add(PlayerListView);
            Controls.Add(ClearListBtn);
            Controls.Add(ConsoleListBox);
            Controls.Add(ServerInputTxt);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            Name = "MainWindow";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MainWindow";
            FormClosing += AskToClose;
            Load += AppLoaded;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox ServerInputTxt;
        private ListBox ConsoleListBox;
        private Button ClearListBtn;
        private ListView PlayerListView;
        private Button BanSelectedBtn;
        private Label SelectedPlayerLbl;
        private Button KickSelectedBtn;
        private Button KillSelectedBtn;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private Button OpenCreatorsUrlBtn;
    }
}
