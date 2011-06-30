namespace _PS360Drum
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.checkForDrums = new System.Windows.Forms.Timer(this.components);
            this.lblMIDIChannel = new System.Windows.Forms.Label();
            this.lblMIDIDevice = new System.Windows.Forms.Label();
            this.nupMIDIChannel = new System.Windows.Forms.NumericUpDown();
            this.ddlMIDIDevice = new System.Windows.Forms.ComboBox();
            this.pnlHolder = new System.Windows.Forms.Panel();
            this.ddlPedal = new System.Windows.Forms.ComboBox();
            this.ddlCymBlue = new System.Windows.Forms.ComboBox();
            this.ddlGreen = new System.Windows.Forms.ComboBox();
            this.ddlCymGreen = new System.Windows.Forms.ComboBox();
            this.ddlCymYellow = new System.Windows.Forms.ComboBox();
            this.ddlBlue = new System.Windows.Forms.ComboBox();
            this.ddlRed = new System.Windows.Forms.ComboBox();
            this.ddlYellow = new System.Windows.Forms.ComboBox();
            this.chkPedal = new System.Windows.Forms.CheckBox();
            this.chkGreen = new System.Windows.Forms.CheckBox();
            this.chkCymBlue = new System.Windows.Forms.CheckBox();
            this.chkCymGreen = new System.Windows.Forms.CheckBox();
            this.chkCymYellow = new System.Windows.Forms.CheckBox();
            this.chkBlue = new System.Windows.Forms.CheckBox();
            this.chkRed = new System.Windows.Forms.CheckBox();
            this.chkYellow = new System.Windows.Forms.CheckBox();
            this.pbCymGreen = new System.Windows.Forms.ProgressBar();
            this.pbPedal = new System.Windows.Forms.ProgressBar();
            this.pbCymYellow = new System.Windows.Forms.ProgressBar();
            this.pbGreen = new System.Windows.Forms.ProgressBar();
            this.pbBlue = new System.Windows.Forms.ProgressBar();
            this.pbRed = new System.Windows.Forms.ProgressBar();
            this.pbCymBlue = new System.Windows.Forms.ProgressBar();
            this.pbYellow = new System.Windows.Forms.ProgressBar();
            this.btnPedal = new System.Windows.Forms.Button();
            this.btnCymBlue = new System.Windows.Forms.Button();
            this.nupPedal = new System.Windows.Forms.NumericUpDown();
            this.btnCymGreen = new System.Windows.Forms.Button();
            this.btnYellow = new System.Windows.Forms.Button();
            this.btnCymYellow = new System.Windows.Forms.Button();
            this.nupCymBlue = new System.Windows.Forms.NumericUpDown();
            this.btnBlue = new System.Windows.Forms.Button();
            this.nupCymGreen = new System.Windows.Forms.NumericUpDown();
            this.nupYellow = new System.Windows.Forms.NumericUpDown();
            this.nupCymYellow = new System.Windows.Forms.NumericUpDown();
            this.btnGreen = new System.Windows.Forms.Button();
            this.nupBlue = new System.Windows.Forms.NumericUpDown();
            this.btnRed = new System.Windows.Forms.Button();
            this.nupGreen = new System.Windows.Forms.NumericUpDown();
            this.nupRed = new System.Windows.Forms.NumericUpDown();
            this.lowerValues = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnReset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nupMIDIChannel)).BeginInit();
            this.pnlHolder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupPedal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupCymBlue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupCymGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupYellow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupCymYellow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupBlue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupRed)).BeginInit();
            this.SuspendLayout();
            // 
            // checkForDrums
            // 
            this.checkForDrums.Interval = 1000;
            // 
            // lblMIDIChannel
            // 
            this.lblMIDIChannel.Location = new System.Drawing.Point(192, 423);
            this.lblMIDIChannel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMIDIChannel.Name = "lblMIDIChannel";
            this.lblMIDIChannel.Size = new System.Drawing.Size(107, 30);
            this.lblMIDIChannel.TabIndex = 21;
            this.lblMIDIChannel.Text = "MIDI Channel";
            this.lblMIDIChannel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMIDIDevice
            // 
            this.lblMIDIDevice.Location = new System.Drawing.Point(11, 423);
            this.lblMIDIDevice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMIDIDevice.Name = "lblMIDIDevice";
            this.lblMIDIDevice.Size = new System.Drawing.Size(96, 30);
            this.lblMIDIDevice.TabIndex = 20;
            this.lblMIDIDevice.Text = "MIDI Device";
            this.lblMIDIDevice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nupMIDIChannel
            // 
            this.nupMIDIChannel.Location = new System.Drawing.Point(192, 453);
            this.nupMIDIChannel.Margin = new System.Windows.Forms.Padding(4);
            this.nupMIDIChannel.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nupMIDIChannel.Name = "nupMIDIChannel";
            this.nupMIDIChannel.Size = new System.Drawing.Size(96, 22);
            this.nupMIDIChannel.TabIndex = 19;
            this.nupMIDIChannel.ValueChanged += new System.EventHandler(this.UpdateMidi);
            // 
            // ddlMIDIDevice
            // 
            this.ddlMIDIDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlMIDIDevice.FormattingEnabled = true;
            this.ddlMIDIDevice.Location = new System.Drawing.Point(11, 453);
            this.ddlMIDIDevice.Margin = new System.Windows.Forms.Padding(4);
            this.ddlMIDIDevice.Name = "ddlMIDIDevice";
            this.ddlMIDIDevice.Size = new System.Drawing.Size(160, 24);
            this.ddlMIDIDevice.TabIndex = 18;
            this.ddlMIDIDevice.SelectedIndexChanged += new System.EventHandler(this.UpdateMidi);
            // 
            // pnlHolder
            // 
            this.pnlHolder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlHolder.BackgroundImage")));
            this.pnlHolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlHolder.Controls.Add(this.ddlPedal);
            this.pnlHolder.Controls.Add(this.ddlCymBlue);
            this.pnlHolder.Controls.Add(this.ddlGreen);
            this.pnlHolder.Controls.Add(this.ddlCymGreen);
            this.pnlHolder.Controls.Add(this.ddlCymYellow);
            this.pnlHolder.Controls.Add(this.ddlBlue);
            this.pnlHolder.Controls.Add(this.ddlRed);
            this.pnlHolder.Controls.Add(this.ddlYellow);
            this.pnlHolder.Controls.Add(this.chkPedal);
            this.pnlHolder.Controls.Add(this.chkGreen);
            this.pnlHolder.Controls.Add(this.chkCymBlue);
            this.pnlHolder.Controls.Add(this.chkCymGreen);
            this.pnlHolder.Controls.Add(this.chkCymYellow);
            this.pnlHolder.Controls.Add(this.chkBlue);
            this.pnlHolder.Controls.Add(this.chkRed);
            this.pnlHolder.Controls.Add(this.chkYellow);
            this.pnlHolder.Controls.Add(this.pbCymGreen);
            this.pnlHolder.Controls.Add(this.pbPedal);
            this.pnlHolder.Controls.Add(this.pbCymYellow);
            this.pnlHolder.Controls.Add(this.pbGreen);
            this.pnlHolder.Controls.Add(this.pbBlue);
            this.pnlHolder.Controls.Add(this.pbRed);
            this.pnlHolder.Controls.Add(this.pbCymBlue);
            this.pnlHolder.Controls.Add(this.pbYellow);
            this.pnlHolder.Controls.Add(this.btnPedal);
            this.pnlHolder.Controls.Add(this.btnCymBlue);
            this.pnlHolder.Controls.Add(this.nupPedal);
            this.pnlHolder.Controls.Add(this.btnCymGreen);
            this.pnlHolder.Controls.Add(this.btnYellow);
            this.pnlHolder.Controls.Add(this.btnCymYellow);
            this.pnlHolder.Controls.Add(this.nupCymBlue);
            this.pnlHolder.Controls.Add(this.btnBlue);
            this.pnlHolder.Controls.Add(this.nupCymGreen);
            this.pnlHolder.Controls.Add(this.nupYellow);
            this.pnlHolder.Controls.Add(this.nupCymYellow);
            this.pnlHolder.Controls.Add(this.btnGreen);
            this.pnlHolder.Controls.Add(this.nupBlue);
            this.pnlHolder.Controls.Add(this.btnRed);
            this.pnlHolder.Controls.Add(this.nupGreen);
            this.pnlHolder.Controls.Add(this.nupRed);
            this.pnlHolder.Location = new System.Drawing.Point(11, 10);
            this.pnlHolder.Margin = new System.Windows.Forms.Padding(4);
            this.pnlHolder.Name = "pnlHolder";
            this.pnlHolder.Size = new System.Drawing.Size(672, 423);
            this.pnlHolder.TabIndex = 15;
            // 
            // ddlPedal
            // 
            this.ddlPedal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlPedal.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlPedal.FormattingEnabled = true;
            this.ddlPedal.Location = new System.Drawing.Point(341, 394);
            this.ddlPedal.Margin = new System.Windows.Forms.Padding(4);
            this.ddlPedal.Name = "ddlPedal";
            this.ddlPedal.Size = new System.Drawing.Size(52, 21);
            this.ddlPedal.TabIndex = 38;
            // 
            // ddlCymBlue
            // 
            this.ddlCymBlue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlCymBlue.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlCymBlue.FormattingEnabled = true;
            this.ddlCymBlue.Location = new System.Drawing.Point(446, 56);
            this.ddlCymBlue.Margin = new System.Windows.Forms.Padding(4);
            this.ddlCymBlue.Name = "ddlCymBlue";
            this.ddlCymBlue.Size = new System.Drawing.Size(52, 21);
            this.ddlCymBlue.TabIndex = 37;
            // 
            // ddlGreen
            // 
            this.ddlGreen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlGreen.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlGreen.FormattingEnabled = true;
            this.ddlGreen.Location = new System.Drawing.Point(534, 345);
            this.ddlGreen.Margin = new System.Windows.Forms.Padding(4);
            this.ddlGreen.Name = "ddlGreen";
            this.ddlGreen.Size = new System.Drawing.Size(52, 21);
            this.ddlGreen.TabIndex = 36;
            // 
            // ddlCymGreen
            // 
            this.ddlCymGreen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlCymGreen.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlCymGreen.FormattingEnabled = true;
            this.ddlCymGreen.Location = new System.Drawing.Point(563, 176);
            this.ddlCymGreen.Margin = new System.Windows.Forms.Padding(4);
            this.ddlCymGreen.Name = "ddlCymGreen";
            this.ddlCymGreen.Size = new System.Drawing.Size(52, 21);
            this.ddlCymGreen.TabIndex = 35;
            // 
            // ddlCymYellow
            // 
            this.ddlCymYellow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlCymYellow.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlCymYellow.FormattingEnabled = true;
            this.ddlCymYellow.Location = new System.Drawing.Point(167, 156);
            this.ddlCymYellow.Margin = new System.Windows.Forms.Padding(4);
            this.ddlCymYellow.Name = "ddlCymYellow";
            this.ddlCymYellow.Size = new System.Drawing.Size(52, 21);
            this.ddlCymYellow.TabIndex = 35;
            // 
            // ddlBlue
            // 
            this.ddlBlue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlBlue.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlBlue.FormattingEnabled = true;
            this.ddlBlue.Location = new System.Drawing.Point(426, 258);
            this.ddlBlue.Margin = new System.Windows.Forms.Padding(4);
            this.ddlBlue.Name = "ddlBlue";
            this.ddlBlue.Size = new System.Drawing.Size(52, 21);
            this.ddlBlue.TabIndex = 35;
            // 
            // ddlRed
            // 
            this.ddlRed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlRed.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlRed.FormattingEnabled = true;
            this.ddlRed.Location = new System.Drawing.Point(165, 345);
            this.ddlRed.Margin = new System.Windows.Forms.Padding(4);
            this.ddlRed.Name = "ddlRed";
            this.ddlRed.Size = new System.Drawing.Size(52, 21);
            this.ddlRed.TabIndex = 34;
            // 
            // ddlYellow
            // 
            this.ddlYellow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlYellow.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlYellow.FormattingEnabled = true;
            this.ddlYellow.Location = new System.Drawing.Point(267, 258);
            this.ddlYellow.Margin = new System.Windows.Forms.Padding(4);
            this.ddlYellow.Name = "ddlYellow";
            this.ddlYellow.Size = new System.Drawing.Size(52, 21);
            this.ddlYellow.TabIndex = 33;
            // 
            // chkPedal
            // 
            this.chkPedal.AutoSize = true;
            this.chkPedal.BackColor = System.Drawing.Color.Transparent;
            this.chkPedal.Checked = true;
            this.chkPedal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPedal.ForeColor = System.Drawing.Color.Black;
            this.chkPedal.Location = new System.Drawing.Point(267, 394);
            this.chkPedal.Margin = new System.Windows.Forms.Padding(4);
            this.chkPedal.Name = "chkPedal";
            this.chkPedal.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkPedal.Size = new System.Drawing.Size(66, 21);
            this.chkPedal.TabIndex = 32;
            this.chkPedal.Text = "Boost";
            this.chkPedal.UseVisualStyleBackColor = false;
            // 
            // chkGreen
            // 
            this.chkGreen.AutoSize = true;
            this.chkGreen.BackColor = System.Drawing.Color.Transparent;
            this.chkGreen.Checked = true;
            this.chkGreen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGreen.ForeColor = System.Drawing.Color.White;
            this.chkGreen.Location = new System.Drawing.Point(460, 345);
            this.chkGreen.Margin = new System.Windows.Forms.Padding(4);
            this.chkGreen.Name = "chkGreen";
            this.chkGreen.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkGreen.Size = new System.Drawing.Size(66, 21);
            this.chkGreen.TabIndex = 31;
            this.chkGreen.Text = "Boost";
            this.chkGreen.UseVisualStyleBackColor = false;
            // 
            // chkCymBlue
            // 
            this.chkCymBlue.AutoSize = true;
            this.chkCymBlue.BackColor = System.Drawing.Color.Transparent;
            this.chkCymBlue.Checked = true;
            this.chkCymBlue.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCymBlue.ForeColor = System.Drawing.Color.White;
            this.chkCymBlue.Location = new System.Drawing.Point(371, 56);
            this.chkCymBlue.Margin = new System.Windows.Forms.Padding(4);
            this.chkCymBlue.Name = "chkCymBlue";
            this.chkCymBlue.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkCymBlue.Size = new System.Drawing.Size(66, 21);
            this.chkCymBlue.TabIndex = 30;
            this.chkCymBlue.Text = "Boost";
            this.chkCymBlue.UseVisualStyleBackColor = false;
            // 
            // chkCymGreen
            // 
            this.chkCymGreen.AutoSize = true;
            this.chkCymGreen.BackColor = System.Drawing.Color.Transparent;
            this.chkCymGreen.Checked = true;
            this.chkCymGreen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCymGreen.ForeColor = System.Drawing.Color.White;
            this.chkCymGreen.Location = new System.Drawing.Point(489, 176);
            this.chkCymGreen.Margin = new System.Windows.Forms.Padding(4);
            this.chkCymGreen.Name = "chkCymGreen";
            this.chkCymGreen.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkCymGreen.Size = new System.Drawing.Size(66, 21);
            this.chkCymGreen.TabIndex = 29;
            this.chkCymGreen.Text = "Boost";
            this.chkCymGreen.UseVisualStyleBackColor = false;
            // 
            // chkCymYellow
            // 
            this.chkCymYellow.AutoSize = true;
            this.chkCymYellow.BackColor = System.Drawing.Color.Transparent;
            this.chkCymYellow.Checked = true;
            this.chkCymYellow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCymYellow.ForeColor = System.Drawing.Color.White;
            this.chkCymYellow.Location = new System.Drawing.Point(93, 156);
            this.chkCymYellow.Margin = new System.Windows.Forms.Padding(4);
            this.chkCymYellow.Name = "chkCymYellow";
            this.chkCymYellow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkCymYellow.Size = new System.Drawing.Size(66, 21);
            this.chkCymYellow.TabIndex = 29;
            this.chkCymYellow.Text = "Boost";
            this.chkCymYellow.UseVisualStyleBackColor = false;
            // 
            // chkBlue
            // 
            this.chkBlue.AutoSize = true;
            this.chkBlue.BackColor = System.Drawing.Color.Transparent;
            this.chkBlue.Checked = true;
            this.chkBlue.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBlue.ForeColor = System.Drawing.Color.White;
            this.chkBlue.Location = new System.Drawing.Point(352, 258);
            this.chkBlue.Margin = new System.Windows.Forms.Padding(4);
            this.chkBlue.Name = "chkBlue";
            this.chkBlue.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkBlue.Size = new System.Drawing.Size(66, 21);
            this.chkBlue.TabIndex = 29;
            this.chkBlue.Text = "Boost";
            this.chkBlue.UseVisualStyleBackColor = false;
            // 
            // chkRed
            // 
            this.chkRed.AutoSize = true;
            this.chkRed.BackColor = System.Drawing.Color.Transparent;
            this.chkRed.Checked = true;
            this.chkRed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRed.ForeColor = System.Drawing.Color.White;
            this.chkRed.Location = new System.Drawing.Point(91, 345);
            this.chkRed.Margin = new System.Windows.Forms.Padding(4);
            this.chkRed.Name = "chkRed";
            this.chkRed.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkRed.Size = new System.Drawing.Size(66, 21);
            this.chkRed.TabIndex = 28;
            this.chkRed.Text = "Boost";
            this.chkRed.UseVisualStyleBackColor = false;
            // 
            // chkYellow
            // 
            this.chkYellow.AutoSize = true;
            this.chkYellow.BackColor = System.Drawing.Color.Transparent;
            this.chkYellow.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkYellow.Checked = true;
            this.chkYellow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkYellow.ForeColor = System.Drawing.Color.White;
            this.chkYellow.Location = new System.Drawing.Point(192, 258);
            this.chkYellow.Margin = new System.Windows.Forms.Padding(4);
            this.chkYellow.Name = "chkYellow";
            this.chkYellow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkYellow.Size = new System.Drawing.Size(66, 21);
            this.chkYellow.TabIndex = 27;
            this.chkYellow.Text = "Boost";
            this.chkYellow.UseVisualStyleBackColor = false;
            // 
            // pbCymGreen
            // 
            this.pbCymGreen.BackColor = System.Drawing.Color.Black;
            this.pbCymGreen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(175)))), ((int)(((byte)(230)))));
            this.pbCymGreen.Location = new System.Drawing.Point(489, 156);
            this.pbCymGreen.Margin = new System.Windows.Forms.Padding(4);
            this.pbCymGreen.Maximum = 127;
            this.pbCymGreen.Name = "pbCymGreen";
            this.pbCymGreen.Size = new System.Drawing.Size(128, 15);
            this.pbCymGreen.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbCymGreen.TabIndex = 24;
            // 
            // pbPedal
            // 
            this.pbPedal.Location = new System.Drawing.Point(267, 374);
            this.pbPedal.Margin = new System.Windows.Forms.Padding(4);
            this.pbPedal.Maximum = 127;
            this.pbPedal.Name = "pbPedal";
            this.pbPedal.Size = new System.Drawing.Size(128, 15);
            this.pbPedal.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbPedal.TabIndex = 26;
            // 
            // pbCymYellow
            // 
            this.pbCymYellow.BackColor = System.Drawing.Color.Black;
            this.pbCymYellow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(175)))), ((int)(((byte)(230)))));
            this.pbCymYellow.Location = new System.Drawing.Point(93, 136);
            this.pbCymYellow.Margin = new System.Windows.Forms.Padding(4);
            this.pbCymYellow.Maximum = 127;
            this.pbCymYellow.Name = "pbCymYellow";
            this.pbCymYellow.Size = new System.Drawing.Size(128, 15);
            this.pbCymYellow.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbCymYellow.TabIndex = 24;
            // 
            // pbGreen
            // 
            this.pbGreen.BackColor = System.Drawing.Color.Black;
            this.pbGreen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(175)))), ((int)(((byte)(230)))));
            this.pbGreen.Location = new System.Drawing.Point(460, 325);
            this.pbGreen.Margin = new System.Windows.Forms.Padding(4);
            this.pbGreen.Maximum = 127;
            this.pbGreen.Name = "pbGreen";
            this.pbGreen.Size = new System.Drawing.Size(128, 15);
            this.pbGreen.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbGreen.TabIndex = 25;
            // 
            // pbBlue
            // 
            this.pbBlue.BackColor = System.Drawing.Color.Black;
            this.pbBlue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(175)))), ((int)(((byte)(230)))));
            this.pbBlue.Location = new System.Drawing.Point(352, 238);
            this.pbBlue.Margin = new System.Windows.Forms.Padding(4);
            this.pbBlue.Maximum = 127;
            this.pbBlue.Name = "pbBlue";
            this.pbBlue.Size = new System.Drawing.Size(128, 15);
            this.pbBlue.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbBlue.TabIndex = 24;
            // 
            // pbRed
            // 
            this.pbRed.BackColor = System.Drawing.Color.Black;
            this.pbRed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(175)))), ((int)(((byte)(230)))));
            this.pbRed.Location = new System.Drawing.Point(91, 325);
            this.pbRed.Margin = new System.Windows.Forms.Padding(4);
            this.pbRed.MarqueeAnimationSpeed = 0;
            this.pbRed.Maximum = 127;
            this.pbRed.Name = "pbRed";
            this.pbRed.Size = new System.Drawing.Size(128, 15);
            this.pbRed.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbRed.TabIndex = 23;
            // 
            // pbCymBlue
            // 
            this.pbCymBlue.BackColor = System.Drawing.Color.Black;
            this.pbCymBlue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(175)))), ((int)(((byte)(230)))));
            this.pbCymBlue.Location = new System.Drawing.Point(371, 36);
            this.pbCymBlue.Margin = new System.Windows.Forms.Padding(4);
            this.pbCymBlue.Maximum = 127;
            this.pbCymBlue.Name = "pbCymBlue";
            this.pbCymBlue.Size = new System.Drawing.Size(128, 15);
            this.pbCymBlue.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbCymBlue.TabIndex = 22;
            // 
            // pbYellow
            // 
            this.pbYellow.BackColor = System.Drawing.Color.Black;
            this.pbYellow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(175)))), ((int)(((byte)(230)))));
            this.pbYellow.Location = new System.Drawing.Point(192, 238);
            this.pbYellow.Margin = new System.Windows.Forms.Padding(4);
            this.pbYellow.MarqueeAnimationSpeed = 0;
            this.pbYellow.Maximum = 127;
            this.pbYellow.Name = "pbYellow";
            this.pbYellow.Size = new System.Drawing.Size(128, 15);
            this.pbYellow.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbYellow.TabIndex = 21;
            // 
            // btnPedal
            // 
            this.btnPedal.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPedal.Location = new System.Drawing.Point(341, 345);
            this.btnPedal.Margin = new System.Windows.Forms.Padding(4);
            this.btnPedal.Name = "btnPedal";
            this.btnPedal.Size = new System.Drawing.Size(53, 25);
            this.btnPedal.TabIndex = 14;
            this.btnPedal.Text = "Hit";
            this.btnPedal.UseVisualStyleBackColor = true;
            this.btnPedal.Click += new System.EventHandler(this.ButtonPress);
            // 
            // btnCymBlue
            // 
            this.btnCymBlue.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCymBlue.Location = new System.Drawing.Point(446, 7);
            this.btnCymBlue.Margin = new System.Windows.Forms.Padding(4);
            this.btnCymBlue.Name = "btnCymBlue";
            this.btnCymBlue.Size = new System.Drawing.Size(53, 25);
            this.btnCymBlue.TabIndex = 13;
            this.btnCymBlue.Text = "Hit";
            this.btnCymBlue.UseVisualStyleBackColor = true;
            this.btnCymBlue.Click += new System.EventHandler(this.ButtonPress);
            // 
            // nupPedal
            // 
            this.nupPedal.Location = new System.Drawing.Point(267, 345);
            this.nupPedal.Margin = new System.Windows.Forms.Padding(4);
            this.nupPedal.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nupPedal.Name = "nupPedal";
            this.nupPedal.Size = new System.Drawing.Size(64, 22);
            this.nupPedal.TabIndex = 3;
            this.nupPedal.Value = new decimal(new int[] {
            36,
            0,
            0,
            0});
            // 
            // btnCymGreen
            // 
            this.btnCymGreen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCymGreen.Location = new System.Drawing.Point(563, 127);
            this.btnCymGreen.Margin = new System.Windows.Forms.Padding(4);
            this.btnCymGreen.Name = "btnCymGreen";
            this.btnCymGreen.Size = new System.Drawing.Size(53, 25);
            this.btnCymGreen.TabIndex = 10;
            this.btnCymGreen.Text = "Hit";
            this.btnCymGreen.UseVisualStyleBackColor = true;
            this.btnCymGreen.Click += new System.EventHandler(this.ButtonPress);
            // 
            // btnYellow
            // 
            this.btnYellow.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnYellow.Location = new System.Drawing.Point(267, 209);
            this.btnYellow.Margin = new System.Windows.Forms.Padding(4);
            this.btnYellow.Name = "btnYellow";
            this.btnYellow.Size = new System.Drawing.Size(53, 25);
            this.btnYellow.TabIndex = 12;
            this.btnYellow.Text = "Hit";
            this.btnYellow.UseVisualStyleBackColor = true;
            this.btnYellow.Click += new System.EventHandler(this.ButtonPress);
            // 
            // btnCymYellow
            // 
            this.btnCymYellow.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCymYellow.Location = new System.Drawing.Point(167, 107);
            this.btnCymYellow.Margin = new System.Windows.Forms.Padding(4);
            this.btnCymYellow.Name = "btnCymYellow";
            this.btnCymYellow.Size = new System.Drawing.Size(53, 25);
            this.btnCymYellow.TabIndex = 10;
            this.btnCymYellow.Text = "Hit";
            this.btnCymYellow.UseVisualStyleBackColor = true;
            this.btnCymYellow.Click += new System.EventHandler(this.ButtonPress);
            // 
            // nupCymBlue
            // 
            this.nupCymBlue.Location = new System.Drawing.Point(371, 7);
            this.nupCymBlue.Margin = new System.Windows.Forms.Padding(4);
            this.nupCymBlue.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nupCymBlue.Name = "nupCymBlue";
            this.nupCymBlue.Size = new System.Drawing.Size(64, 22);
            this.nupCymBlue.TabIndex = 8;
            this.nupCymBlue.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            // 
            // btnBlue
            // 
            this.btnBlue.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBlue.Location = new System.Drawing.Point(426, 209);
            this.btnBlue.Margin = new System.Windows.Forms.Padding(4);
            this.btnBlue.Name = "btnBlue";
            this.btnBlue.Size = new System.Drawing.Size(53, 25);
            this.btnBlue.TabIndex = 10;
            this.btnBlue.Text = "Hit";
            this.btnBlue.UseVisualStyleBackColor = true;
            this.btnBlue.Click += new System.EventHandler(this.ButtonPress);
            // 
            // nupCymGreen
            // 
            this.nupCymGreen.Location = new System.Drawing.Point(489, 127);
            this.nupCymGreen.Margin = new System.Windows.Forms.Padding(4);
            this.nupCymGreen.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nupCymGreen.Name = "nupCymGreen";
            this.nupCymGreen.Size = new System.Drawing.Size(64, 22);
            this.nupCymGreen.TabIndex = 5;
            this.nupCymGreen.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            // 
            // nupYellow
            // 
            this.nupYellow.Location = new System.Drawing.Point(192, 209);
            this.nupYellow.Margin = new System.Windows.Forms.Padding(4);
            this.nupYellow.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nupYellow.Name = "nupYellow";
            this.nupYellow.Size = new System.Drawing.Size(64, 22);
            this.nupYellow.TabIndex = 7;
            this.nupYellow.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            // 
            // nupCymYellow
            // 
            this.nupCymYellow.Location = new System.Drawing.Point(93, 107);
            this.nupCymYellow.Margin = new System.Windows.Forms.Padding(4);
            this.nupCymYellow.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nupCymYellow.Name = "nupCymYellow";
            this.nupCymYellow.Size = new System.Drawing.Size(64, 22);
            this.nupCymYellow.TabIndex = 5;
            this.nupCymYellow.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            // 
            // btnGreen
            // 
            this.btnGreen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGreen.Location = new System.Drawing.Point(534, 296);
            this.btnGreen.Margin = new System.Windows.Forms.Padding(4);
            this.btnGreen.Name = "btnGreen";
            this.btnGreen.Size = new System.Drawing.Size(53, 25);
            this.btnGreen.TabIndex = 11;
            this.btnGreen.Text = "Hit";
            this.btnGreen.UseVisualStyleBackColor = true;
            this.btnGreen.Click += new System.EventHandler(this.ButtonPress);
            // 
            // nupBlue
            // 
            this.nupBlue.Location = new System.Drawing.Point(352, 209);
            this.nupBlue.Margin = new System.Windows.Forms.Padding(4);
            this.nupBlue.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nupBlue.Name = "nupBlue";
            this.nupBlue.Size = new System.Drawing.Size(64, 22);
            this.nupBlue.TabIndex = 5;
            this.nupBlue.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            // 
            // btnRed
            // 
            this.btnRed.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRed.Location = new System.Drawing.Point(165, 296);
            this.btnRed.Margin = new System.Windows.Forms.Padding(4);
            this.btnRed.Name = "btnRed";
            this.btnRed.Size = new System.Drawing.Size(53, 25);
            this.btnRed.TabIndex = 9;
            this.btnRed.Text = "Hit";
            this.btnRed.UseVisualStyleBackColor = true;
            this.btnRed.Click += new System.EventHandler(this.ButtonPress);
            // 
            // nupGreen
            // 
            this.nupGreen.Location = new System.Drawing.Point(460, 296);
            this.nupGreen.Margin = new System.Windows.Forms.Padding(4);
            this.nupGreen.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nupGreen.Name = "nupGreen";
            this.nupGreen.Size = new System.Drawing.Size(64, 22);
            this.nupGreen.TabIndex = 6;
            this.nupGreen.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            // 
            // nupRed
            // 
            this.nupRed.Location = new System.Drawing.Point(91, 296);
            this.nupRed.Margin = new System.Windows.Forms.Padding(4);
            this.nupRed.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nupRed.Name = "nupRed";
            this.nupRed.Size = new System.Drawing.Size(64, 22);
            this.nupRed.TabIndex = 4;
            this.nupRed.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            // 
            // lowerValues
            // 
            this.lowerValues.Enabled = true;
            this.lowerValues.Interval = 20;
            this.lowerValues.Tick += new System.EventHandler(this.LowerValues);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.CreatePrompt = true;
            this.saveFileDialog.Title = "Save your current settings";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(565, 453);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(116, 28);
            this.btnSave.TabIndex = 22;
            this.btnSave.Text = "Save settings";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.SaveSettingsClick);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(437, 453);
            this.btnLoad.Margin = new System.Windows.Forms.Padding(4);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(116, 28);
            this.btnLoad.TabIndex = 23;
            this.btnLoad.Text = "Load settings";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.LoadSettingsClick);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Title = "Open your saved settings";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(309, 453);
            this.btnReset.Margin = new System.Windows.Forms.Padding(4);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(116, 28);
            this.btnReset.TabIndex = 24;
            this.btnReset.Text = "Reset settings";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.LoadDefaultSettings);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 481);
            this.Controls.Add(this.lblMIDIChannel);
            this.Controls.Add(this.lblMIDIDevice);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.nupMIDIChannel);
            this.Controls.Add(this.ddlMIDIDevice);
            this.Controls.Add(this.pnlHolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(698, 510);
            this.MinimumSize = new System.Drawing.Size(698, 510);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PS360 MIDI Drummer v0.031";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SaveDefaultSettings);
            ((System.ComponentModel.ISupportInitialize)(this.nupMIDIChannel)).EndInit();
            this.pnlHolder.ResumeLayout(false);
            this.pnlHolder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupPedal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupCymBlue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupCymGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupYellow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupCymYellow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupBlue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupRed)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHolder;

        private System.Windows.Forms.NumericUpDown nupGreen;
        private System.Windows.Forms.NumericUpDown nupRed;
        private System.Windows.Forms.NumericUpDown nupBlue;
        private System.Windows.Forms.NumericUpDown nupYellow;
        private System.Windows.Forms.NumericUpDown nupPedal;
        private System.Windows.Forms.NumericUpDown nupCymBlue;

        private System.Windows.Forms.Button btnGreen;
        private System.Windows.Forms.Button btnRed;
        private System.Windows.Forms.Button btnBlue;
        private System.Windows.Forms.Button btnYellow;
        private System.Windows.Forms.Button btnPedal;
        private System.Windows.Forms.Button btnCymBlue;

        private System.Windows.Forms.ProgressBar pbGreen;
        private System.Windows.Forms.ProgressBar pbRed;
        private System.Windows.Forms.ProgressBar pbBlue;
        private System.Windows.Forms.ProgressBar pbYellow;
        private System.Windows.Forms.ProgressBar pbPedal;
        private System.Windows.Forms.ProgressBar pbCymBlue;

        private System.Windows.Forms.Label lblMIDIChannel;
        private System.Windows.Forms.NumericUpDown nupMIDIChannel;
        private System.Windows.Forms.Label lblMIDIDevice;
        private System.Windows.Forms.ComboBox ddlMIDIDevice;

        private System.Windows.Forms.Timer lowerValues;
        private System.Windows.Forms.Timer checkForDrums;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.CheckBox chkGreen;
        private System.Windows.Forms.CheckBox chkCymBlue;
        private System.Windows.Forms.CheckBox chkBlue;
        private System.Windows.Forms.CheckBox chkRed;
        private System.Windows.Forms.CheckBox chkPedal;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.ComboBox ddlYellow;
        private System.Windows.Forms.CheckBox chkYellow;
        private System.Windows.Forms.ComboBox ddlCymBlue;
        private System.Windows.Forms.ComboBox ddlGreen;
        private System.Windows.Forms.ComboBox ddlBlue;
        private System.Windows.Forms.ComboBox ddlRed;
        private System.Windows.Forms.ComboBox ddlPedal;
        private System.Windows.Forms.ComboBox ddlCymGreen;
        private System.Windows.Forms.ComboBox ddlCymYellow;
        private System.Windows.Forms.CheckBox chkCymGreen;
        private System.Windows.Forms.CheckBox chkCymYellow;
        private System.Windows.Forms.ProgressBar pbCymGreen;
        private System.Windows.Forms.ProgressBar pbCymYellow;
        private System.Windows.Forms.Button btnCymGreen;
        private System.Windows.Forms.Button btnCymYellow;
        private System.Windows.Forms.NumericUpDown nupCymGreen;
        private System.Windows.Forms.NumericUpDown nupCymYellow;
    }
}

