namespace Client
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtNickname;
        private TextBox txtEmail;
        private Button btnConnect;
        private ComboBox cmbArchetype;
        private Button btnSelectArchetype;
        private Panel pnlGame;
        private Label lblCycle;
        private Label lblTurn;
        private Label lblResources;
        private Button btnEndTurn;
        private ListBox lstBuildings;
        private Button btnBuild;
        private Button btnUpgrade;
        private Button btnMakeSoldiers;
        private Button btnAttack;
        private TextBox txtLog;
        private GameFieldControl gameField;
        private Label lblWaiting;
        private Label lblTitle;
        private Label lblPlayersTitle;
        private ListBox lstPlayers;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtNickname = new TextBox();
            txtEmail = new TextBox();
            btnConnect = new Button();
            cmbArchetype = new ComboBox();
            btnSelectArchetype = new Button();
            pnlGame = new Panel();
            lblWaiting = new Label();
            lblCycle = new Label();
            lblTurn = new Label();
            gameField = new GameFieldControl();
            lblResources = new Label();
            lstBuildings = new ListBox();
            btnBuild = new Button();
            btnUpgrade = new Button();
            btnMakeSoldiers = new Button();
            btnAttack = new Button();
            btnEndTurn = new Button();
            txtLog = new TextBox();
            lblPlayersTitle = new Label();
            lstPlayers = new ListBox();
            lblTitle = new Label();
            txtAddress = new TextBox();
            pnlGame.SuspendLayout();
            SuspendLayout();
            // 
            // txtNickname
            // 
            txtNickname.Location = new Point(20, 49);
            txtNickname.Name = "txtNickname";
            txtNickname.PlaceholderText = "Никнейм";
            txtNickname.Size = new Size(200, 35);
            txtNickname.TabIndex = 0;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(240, 49);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "Email";
            txtEmail.Size = new Size(200, 35);
            txtEmail.TabIndex = 1;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(731, 49);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(200, 35);
            btnConnect.TabIndex = 2;
            btnConnect.Text = "В бой!";
            btnConnect.Click += BtnConnect_Click;
            // 
            // cmbArchetype
            // 
            cmbArchetype.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbArchetype.Location = new Point(960, 50);
            cmbArchetype.Name = "cmbArchetype";
            cmbArchetype.Size = new Size(200, 38);
            cmbArchetype.TabIndex = 3;
            cmbArchetype.Visible = false;
            // 
            // btnSelectArchetype
            // 
            btnSelectArchetype.Location = new Point(1180, 50);
            btnSelectArchetype.Name = "btnSelectArchetype";
            btnSelectArchetype.Size = new Size(220, 35);
            btnSelectArchetype.TabIndex = 4;
            btnSelectArchetype.Text = "Выбрать архетип";
            btnSelectArchetype.Visible = false;
            btnSelectArchetype.Click += BtnSelectArchetype_Click;
            // 
            // pnlGame
            // 
            pnlGame.BorderStyle = BorderStyle.FixedSingle;
            pnlGame.Controls.Add(lblWaiting);
            pnlGame.Controls.Add(lblCycle);
            pnlGame.Controls.Add(lblTurn);
            pnlGame.Controls.Add(gameField);
            pnlGame.Controls.Add(lblResources);
            pnlGame.Controls.Add(lstBuildings);
            pnlGame.Controls.Add(btnBuild);
            pnlGame.Controls.Add(btnUpgrade);
            pnlGame.Controls.Add(btnMakeSoldiers);
            pnlGame.Controls.Add(btnAttack);
            pnlGame.Controls.Add(btnEndTurn);
            pnlGame.Controls.Add(txtLog);
            pnlGame.Controls.Add(lblPlayersTitle);
            pnlGame.Controls.Add(lstPlayers);
            pnlGame.Location = new Point(20, 90);
            pnlGame.Name = "pnlGame";
            pnlGame.Size = new Size(1380, 738);
            pnlGame.TabIndex = 5;
            pnlGame.Visible = false;
            // 
            // lblWaiting
            // 
            lblWaiting.AutoSize = true;
            lblWaiting.ForeColor = Color.Red;
            lblWaiting.Location = new Point(1087, 200);
            lblWaiting.Name = "lblWaiting";
            lblWaiting.Size = new Size(0, 30);
            lblWaiting.TabIndex = 11;
            lblWaiting.Visible = false;
            // 
            // lblCycle
            // 
            lblCycle.Location = new Point(10, 10);
            lblCycle.Name = "lblCycle";
            lblCycle.Size = new Size(166, 32);
            lblCycle.TabIndex = 0;
            lblCycle.Text = "Цикл: 0";
            // 
            // lblTurn
            // 
            lblTurn.Location = new Point(10, 42);
            lblTurn.Name = "lblTurn";
            lblTurn.Size = new Size(166, 39);
            lblTurn.TabIndex = 1;
            lblTurn.Text = "Ход: 0";
            // 
            // gameField
            // 
            gameField.Location = new Point(10, 84);
            gameField.Name = "gameField";
            gameField.Size = new Size(409, 406);
            gameField.TabIndex = 2;
            gameField.PlaceClicked += GameField_PlaceClicked;
            // 
            // lblResources
            // 
            lblResources.Location = new Point(439, 10);
            lblResources.Name = "lblResources";
            lblResources.Size = new Size(297, 694);
            lblResources.TabIndex = 3;
            lblResources.Text = "Ресурсы:";
            // 
            // lstBuildings
            // 
            lstBuildings.Location = new Point(10, 490);
            lstBuildings.Name = "lstBuildings";
            lstBuildings.Size = new Size(409, 214);
            lstBuildings.TabIndex = 4;
            // 
            // btnBuild
            // 
            btnBuild.Location = new Point(750, 245);
            btnBuild.Name = "btnBuild";
            btnBuild.Size = new Size(221, 42);
            btnBuild.TabIndex = 5;
            btnBuild.Text = "Построить";
            btnBuild.Click += BtnBuild_Click;
            // 
            // btnUpgrade
            // 
            btnUpgrade.Location = new Point(750, 285);
            btnUpgrade.Name = "btnUpgrade";
            btnUpgrade.Size = new Size(221, 42);
            btnUpgrade.TabIndex = 6;
            btnUpgrade.Text = "Улучшить";
            btnUpgrade.Click += BtnUpgrade_Click;
            // 
            // btnMakeSoldiers
            // 
            btnMakeSoldiers.Location = new Point(750, 325);
            btnMakeSoldiers.Name = "btnMakeSoldiers";
            btnMakeSoldiers.Size = new Size(221, 42);
            btnMakeSoldiers.TabIndex = 7;
            btnMakeSoldiers.Text = "Создать солдат";
            btnMakeSoldiers.Click += BtnMakeSoldiers_Click;
            // 
            // btnAttack
            // 
            btnAttack.Location = new Point(750, 365);
            btnAttack.Name = "btnAttack";
            btnAttack.Size = new Size(221, 42);
            btnAttack.TabIndex = 8;
            btnAttack.Text = "Атаковать";
            btnAttack.Click += BtnAttack_Click;
            // 
            // btnEndTurn
            // 
            btnEndTurn.Enabled = false;
            btnEndTurn.Location = new Point(750, 560);
            btnEndTurn.Name = "btnEndTurn";
            btnEndTurn.Size = new Size(221, 52);
            btnEndTurn.TabIndex = 9;
            btnEndTurn.Text = "Завершить ход";
            btnEndTurn.Click += BtnEndTurn_Click;
            // 
            // txtLog
            // 
            txtLog.Location = new Point(977, 27);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new Size(386, 677);
            txtLog.TabIndex = 10;
            // 
            // lblPlayersTitle
            // 
            lblPlayersTitle.AutoSize = true;
            lblPlayersTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPlayersTitle.Location = new Point(750, 10);
            lblPlayersTitle.Name = "lblPlayersTitle";
            lblPlayersTitle.Size = new Size(108, 32);
            lblPlayersTitle.TabIndex = 12;
            lblPlayersTitle.Text = "Игроки:";
            // 
            // lstPlayers
            // 
            lstPlayers.Location = new Point(750, 55);
            lstPlayers.Name = "lstPlayers";
            lstPlayers.Size = new Size(221, 184);
            lstPlayers.TabIndex = 13;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTitle.ForeColor = Color.DarkGreen;
            lblTitle.Location = new Point(460, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(471, 32);
            lblTitle.TabIndex = 6;
            lblTitle.Text = "⚔️ Бойцы хлопковых плантаций 2 ⚔️";
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(460, 48);
            txtAddress.Name = "txtAddress";
            txtAddress.PlaceholderText = "Адрес сервера";
            txtAddress.Size = new Size(200, 35);
            txtAddress.TabIndex = 7;
            // 
            // Form1
            // 
            ClientSize = new Size(1416, 836);
            Controls.Add(txtAddress);
            Controls.Add(lblTitle);
            Controls.Add(txtNickname);
            Controls.Add(txtEmail);
            Controls.Add(btnConnect);
            Controls.Add(cmbArchetype);
            Controls.Add(btnSelectArchetype);
            Controls.Add(pnlGame);
            MaximumSize = new Size(1440, 900);
            MinimumSize = new Size(1440, 900);
            Name = "Form1";
            Text = "Бойцы хлопковых плантаций 2";
            pnlGame.ResumeLayout(false);
            pnlGame.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private TextBox txtAddress;
    }
}
