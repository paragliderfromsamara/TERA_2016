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
            this.components = new System.ComponentModel.Container();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.dbToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dbUsersMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dbMaterialsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dbMeasureProgrammsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dbTestsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.handMeasureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoTestsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cntrlConnectionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchDevicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.switchOffDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deviceSettingsStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deviceList = new System.Windows.Forms.ToolStripComboBox();
            this.mainToolStrip = new System.Windows.Forms.StatusStrip();
            this.sesUserName = new System.Windows.Forms.ToolStripStatusLabel();
            this.sesTabNum = new System.Windows.Forms.ToolStripStatusLabel();
            this.sesRole = new System.Windows.Forms.ToolStripStatusLabel();
            this.connectedDevice = new System.Windows.Forms.ToolStripStatusLabel();
            this.teraPort = new System.IO.Ports.SerialPort(this.components);
            this.mainMenu.SuspendLayout();
            this.mainToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dbToolStripMenuItem,
            this.testsToolStripMenuItem,
            this.cntrlConnectionMenuItem,
            this.deviceList});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(826, 27);
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
            this.dbToolStripMenuItem.Size = new System.Drawing.Size(89, 23);
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
            this.testsToolStripMenuItem.Size = new System.Drawing.Size(81, 23);
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
            // cntrlConnectionMenuItem
            // 
            this.cntrlConnectionMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchDevicesToolStripMenuItem,
            this.switchOffDeviceToolStripMenuItem,
            this.deviceSettingsStripMenuItem});
            this.cntrlConnectionMenuItem.Name = "cntrlConnectionMenuItem";
            this.cntrlConnectionMenuItem.Size = new System.Drawing.Size(146, 23);
            this.cntrlConnectionMenuItem.Text = "Управление прибором";
            // 
            // searchDevicesToolStripMenuItem
            // 
            this.searchDevicesToolStripMenuItem.Name = "searchDevicesToolStripMenuItem";
            this.searchDevicesToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.searchDevicesToolStripMenuItem.Text = "Искать приборы";
            this.searchDevicesToolStripMenuItem.Click += new System.EventHandler(this.searchDevicesToolStripMenuItem_Click);
            // 
            // switchOffDeviceToolStripMenuItem
            // 
            this.switchOffDeviceToolStripMenuItem.Name = "switchOffDeviceToolStripMenuItem";
            this.switchOffDeviceToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.switchOffDeviceToolStripMenuItem.Text = "Отключить прибор";
            this.switchOffDeviceToolStripMenuItem.Click += new System.EventHandler(this.switchOffDeviceToolStripMenuItem_Click);
            // 
            // deviceSettingsStripMenuItem
            // 
            this.deviceSettingsStripMenuItem.Name = "deviceSettingsStripMenuItem";
            this.deviceSettingsStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.deviceSettingsStripMenuItem.Text = "Настройка";
            // 
            // deviceList
            // 
            this.deviceList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.deviceList.Name = "deviceList";
            this.deviceList.Size = new System.Drawing.Size(121, 23);
            this.deviceList.SelectedIndexChanged += new System.EventHandler(this.deviceList_SelectedIndexChanged);
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sesUserName,
            this.sesTabNum,
            this.sesRole,
            this.connectedDevice});
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
            // connectedDevice
            // 
            this.connectedDevice.Name = "connectedDevice";
            this.connectedDevice.Size = new System.Drawing.Size(0, 17);
            // 
            // teraPort
            // 
            this.teraPort.ReadTimeout = 200;
            this.teraPort.WriteTimeout = 200;
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
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_FormClosing);
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
        private System.Windows.Forms.StatusStrip mainToolStrip;
        private System.Windows.Forms.ToolStripStatusLabel sesUserName;
        private System.Windows.Forms.ToolStripStatusLabel sesTabNum;
        private System.Windows.Forms.ToolStripStatusLabel sesRole;
        public System.IO.Ports.SerialPort teraPort;
        private System.Windows.Forms.ToolStripStatusLabel connectedDevice;
        private System.Windows.Forms.ToolStripComboBox deviceList;
        private System.Windows.Forms.ToolStripMenuItem cntrlConnectionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchDevicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem switchOffDeviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deviceSettingsStripMenuItem;
    }
}

