namespace TERA_2016
{
    partial class mainForm
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
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.dbToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dbUsersMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dbMaterialsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dbMeasureProgrammsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dbTestsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.handMeasureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoTestsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deviceSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calibrationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainToolStrip = new System.Windows.Forms.StatusStrip();
            this.sesUserName = new System.Windows.Forms.ToolStripStatusLabel();
            this.sesTabNum = new System.Windows.Forms.ToolStripStatusLabel();
            this.sesRole = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainMenu.SuspendLayout();
            this.mainToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dbToolStripMenuItem,
            this.testsToolStripMenuItem,
            this.deviceSettingsToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(826, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "menuStrip1";
            // 
            // dbToolStripMenuItem
            // 
            this.dbToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dbUsersMenuItem,
            this.dbMaterialsToolStripMenuItem,
            this.dbMeasureProgrammsToolStripMenuItem,
            this.dbTestsToolStripMenuItem});
            this.dbToolStripMenuItem.Name = "dbToolStripMenuItem";
            this.dbToolStripMenuItem.Size = new System.Drawing.Size(89, 20);
            this.dbToolStripMenuItem.Text = "Базы данных";
            // 
            // dbUsersMenuItem
            // 
            this.dbUsersMenuItem.Name = "dbUsersMenuItem";
            this.dbUsersMenuItem.Size = new System.Drawing.Size(215, 22);
            this.dbUsersMenuItem.Text = "БД Пользователей";
            this.dbUsersMenuItem.Click += new System.EventHandler(this.dbUsersMenuItem_Click);
            // 
            // dbMaterialsToolStripMenuItem
            // 
            this.dbMaterialsToolStripMenuItem.Name = "dbMaterialsToolStripMenuItem";
            this.dbMaterialsToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.dbMaterialsToolStripMenuItem.Text = "БД Материалов";
            // 
            // dbMeasureProgrammsToolStripMenuItem
            // 
            this.dbMeasureProgrammsToolStripMenuItem.Name = "dbMeasureProgrammsToolStripMenuItem";
            this.dbMeasureProgrammsToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.dbMeasureProgrammsToolStripMenuItem.Text = "БД Программ измерений";
            // 
            // dbTestsToolStripMenuItem
            // 
            this.dbTestsToolStripMenuItem.Name = "dbTestsToolStripMenuItem";
            this.dbTestsToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.dbTestsToolStripMenuItem.Text = "БД Испытаний";
            // 
            // testsToolStripMenuItem
            // 
            this.testsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.handMeasureToolStripMenuItem,
            this.autoTestsToolStripMenuItem});
            this.testsToolStripMenuItem.Name = "testsToolStripMenuItem";
            this.testsToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.testsToolStripMenuItem.Text = "Испытания";
            // 
            // handMeasureToolStripMenuItem
            // 
            this.handMeasureToolStripMenuItem.Name = "handMeasureToolStripMenuItem";
            this.handMeasureToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.handMeasureToolStripMenuItem.Text = "Ручные испытания";
            this.handMeasureToolStripMenuItem.Click += new System.EventHandler(this.handMeasureToolStripMenuItem_Click);
            // 
            // autoTestsToolStripMenuItem
            // 
            this.autoTestsToolStripMenuItem.Name = "autoTestsToolStripMenuItem";
            this.autoTestsToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.autoTestsToolStripMenuItem.Text = "Автоматические испытания";
            // 
            // deviceSettingsToolStripMenuItem
            // 
            this.deviceSettingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.calibrationToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.deviceSettingsToolStripMenuItem.Name = "deviceSettingsToolStripMenuItem";
            this.deviceSettingsToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.deviceSettingsToolStripMenuItem.Text = "Настройка";
            // 
            // calibrationToolStripMenuItem
            // 
            this.calibrationToolStripMenuItem.Name = "calibrationToolStripMenuItem";
            this.calibrationToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.calibrationToolStripMenuItem.Text = "Калибровка";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.settingsToolStripMenuItem.Text = "Настройки";
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sesUserName,
            this.sesTabNum,
            this.sesRole});
            this.mainToolStrip.Location = new System.Drawing.Point(0, 401);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(826, 22);
            this.mainToolStrip.TabIndex = 1;
            this.mainToolStrip.Text = "Табельный номер";
            // 
            // sesUserName
            // 
            this.sesUserName.Name = "sesUserName";
            this.sesUserName.Size = new System.Drawing.Size(85, 17);
            this.sesUserName.Text = "Фамилия И.О.";
            // 
            // sesTabNum
            // 
            this.sesTabNum.Name = "sesTabNum";
            this.sesTabNum.Size = new System.Drawing.Size(108, 17);
            this.sesTabNum.Text = "Табельный номер";
            // 
            // sesRole
            // 
            this.sesRole.Name = "sesRole";
            this.sesRole.Size = new System.Drawing.Size(46, 17);
            this.sesRole.Text = "Группа";
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 423);
            this.Controls.Add(this.mainToolStrip);
            this.Controls.Add(this.mainMenu);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mainMenu;
            this.Name = "mainForm";
            this.Text = "Тераомметр ТОмМ-01 Клиент";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem dbToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dbUsersMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dbMaterialsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dbMeasureProgrammsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dbTestsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem handMeasureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoTestsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deviceSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem calibrationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.StatusStrip mainToolStrip;
        private System.Windows.Forms.ToolStripStatusLabel sesUserName;
        private System.Windows.Forms.ToolStripStatusLabel sesTabNum;
        private System.Windows.Forms.ToolStripStatusLabel sesRole;
    }
}

