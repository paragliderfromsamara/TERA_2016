using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using TERA_2016.forAppTest;
using System.Globalization;

namespace TERA_2016
{
    public partial class mainForm : Form
    {
        public bool isTestApp = Properties.Settings.Default.isTestApp;
        public bool isConnected = false;
        public string user_type = "undefined";
        public string user_id = "undefined";
        public dbUsersForm dbUsersForm = null;
        public measureForms.manualTeraMeasureForm manualMeasureForm = null;
        public byte[] getSerialNumberCommand = { 0x13, 0x00};
        public TeraDevice currentDevice = null;
        public appTest appTest = null;
        public mainForm()
        {
            InitializeComponent();
            if (this.isTestApp) { this.Text += " (Тестовый режим)"; }
            Thread.CurrentThread.CurrentCulture = new CultureInfo("my");
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            dataMigration dm = new dataMigration();
            Signin signForm = new Signin();
            DialogResult result = signForm.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.Cancel) this.Close();
            else if (result == System.Windows.Forms.DialogResult.OK)
            {
                if (this.isTestApp) { this.appTest = new appTest(); }
                sesUserName.Text = String.Format("{0} {1}.{2}.", signForm.userLastName, signForm.userFirstName[0], signForm.userThirdName[0]);
                sesTabNum.Text = "Таб.номер: " + signForm.userTabNum;
                sesRole.Text = "Должность: " + signForm.userRole;
                user_type = signForm.userRole;
                user_id = signForm.userId;
                signForm.Dispose();
                checkTabsPermission();
                findDevices();
            }
        }

        private void findDevices()
        {
                this.isConnected = getDevicePorts();
                if (!this.isConnected)
                {
                    MessageBox.Show("Нет ни одного подключённого Тераомметра");
                    this.SwitchDeviceMenus(false);
                }
                else
                {
                    this.deviceList.SelectedIndex = 0;
                }
        }

        private void SwitchDeviceMenus(bool v)
        {
            deviceSettingsStripMenuItem.Enabled = testsToolStripMenuItem.Enabled = v;
            this.switchOffDeviceToolStripMenuItem.Enabled = v; //дисэйблит кнопку отключения устройства

        }

        private void checkTabsPermission()
        {
            UserGrants grants = new UserGrants(user_type);

        }
        public void switchMenuStripItems(bool v)
        {
            
        }

        private void dbUsersMenuItem_Click(object sender, EventArgs e)
        {
            dbUsersForm = new dbUsersForm(this);
            dbUsersForm.MdiParent = this;
            dbUsersForm.Show();
        }

        private void handMeasureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (manualMeasureForm == null)
            {
                checkCalibrationParams();
                manualMeasureForm = new measureForms.manualTeraMeasureForm(this);
                manualMeasureForm.MdiParent = this;
                manualMeasureForm.Show();
            }

        }

        /// <summary>
        /// Получение портов приборов по имени порта
        /// </summary>
        /// <param name="port_name"></param>
        /// <returns></returns>
        private bool getTeraPortByName(string port_name) //Получение порта ЦПС по названию
        {
            byte[] arr;
            this.CloseTeraPort();
            try
            {
                arr = getSerialAndCheckSumFromTera(port_name);
                if (arr[0] < 50 && arr[0] > 10) //Проверка валидности номера прибора
                {
                    int _check_sum = (int)arr[2] + (int)arr[3] * 256;
                    string serialNumber = TeraDevice.makeTeraSerial(arr[0], arr[1]);
                    deviceList.Items.Insert(0, serialNumber);
                    Thread.Sleep(100);
                    checkDeviceBySerial(serialNumber, _check_sum);
                    return true;
                }
            }
            catch//(Exception e)
            {
                // MessageBox.Show(e.Message);
            }
            return false;
        }
        /// <summary>
        /// Поиск прибора по имени порта и серийному номеру
        /// </summary>
        /// <param name="port_name"></param>
        /// <param name="serial_number"></param>
        /// <returns></returns>
        private bool getTeraPortByName(string port_name, string serial_number)
        {
            byte[] arr;
            string tmpSerial;
            try
            {
                arr = getSerialAndCheckSumFromTera(port_name);
                tmpSerial = TeraDevice.makeTeraSerial(arr[0], arr[1]);
                if (serial_number == tmpSerial) //Проверка соответствия найденного прибора
                {
                    int _check_sum = (int)arr[2] + (int)arr[3] * 256;
                    if (currentDevice.serial == tmpSerial)
                    {
                        currentDevice.checkSumFromDevice = (uint)_check_sum;
                        currentDevice.portName = teraPort.PortName;
                    }
                    else
                    {
                        checkDeviceBySerial(tmpSerial, _check_sum);
                        MessageBox.Show("Тераомметр " + deviceList.Text + " подключен");
                    }

                    return true;
                }
                
            }
            catch// (Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            return false;
        }
        public bool getDevicePorts()
        {
            string[] port_list = (!isTestApp) ? SerialPort.GetPortNames() : this.appTest.fakePortNumbers();
            bool f = false;
            for (int i = 0; i < port_list.Length; i++)
            {
                if (getTeraPortByName(port_list[i])) { f = true; }
            }
            
            return f;
        }

        /// <summary>
        /// Считывает n количество принятых данных в массив равный n. Функцию открытия/закрытия порта не выполняет
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public byte[] receiveByteArray(int n, bool needToClose)
        {
            byte[] arr = new byte[n];
            this.OpenTeraPort();
            for (int i=0; i<n; i++)
            {
                arr[i] = (byte)teraPort.ReadByte();
                Thread.Sleep(20);
            }
            if (needToClose) this.CloseTeraPort();
            return arr;
        }

        public void checkDeviceBySerial(string serialNumber, int _check_sum)
        {
            cleanCurrentDevice();
            currentDevice = new TeraDevice(this, serialNumber, _check_sum);
        }

        private void deviceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (deviceList.Items.Count == 0 || String.IsNullOrWhiteSpace(deviceList.Text)) return;
            if (connectToTera(deviceList.Text))
            {
                SwitchDeviceMenus(true);
            }
            else
            {
                MessageBox.Show( "Не удалось подключиться к Тераомметру " + deviceList.Text + "\nПроверьте правильность подключения.", "Не удалось подключиться к прибору", MessageBoxButtons.OK, MessageBoxIcon.Error);
                deviceList.Items.RemoveAt(deviceList.SelectedIndex);
                SwitchDeviceMenus(false);
            }
            //
        }
        private bool connectToTera(string serial)
        {
            string[] port_list = (!this.isTestApp) ? SerialPort.GetPortNames() : this.appTest.fakePortNumbers();
            bool f = false;
            for (int i = 0; i < port_list.Length; i++)
            {
                if (getTeraPortByName(port_list[i], serial)) { f = true; break; }
            }
            return f;
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            cleanCurrentDevice();
        }

        private void cleanDeviceList()
        {
            //for (int i = 0; i < deviceList.Items.Count; i++) deviceList.Items.RemoveAt(i);
        }
        private void cleanCurrentDevice()
        {
            if (this.currentDevice != null && !isTestApp) { this.currentDevice.Disconnect(); Thread.Sleep(200); this.currentDevice = null; Thread.Sleep(200); }
        }

        private void searchDevicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cleanCurrentDevice();
            deviceList.Items.Clear();
            findDevices();
        }

        private void switchOffDeviceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cleanCurrentDevice();
            SwitchDeviceMenus(false);
            deviceList.Items.RemoveAt(deviceList.SelectedIndex);
        }

        /// <summary>
        /// Проверка проверочной суммы параметров калибровки. Если она не равна, выводится сообщение, чтобы предложить обновление параметров. 
        /// </summary>
        private void checkCalibrationParams()
        {
            if (currentDevice == null && !isTestApp) return;
            if (currentDevice.checkSumFromDB != currentDevice.checkSumFromDevice)
            {
                DialogResult r = MessageBox.Show("Обновить настройки?", "Необходимо обновить настройки прибора в БД", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.Yes)
                {
                    this.currentDevice.syncCoeffs(true);
                }
            }
        }

        private byte[] getSerialAndCheckSumFromTera(string port_name)
        {
            if (!this.isTestApp)
            {
                byte[] arr;
                this.RenamePort(port_name);
                this.WriteTeraPort(TeraDevice.getSerialWidthCheckSumCmd);
                Thread.Sleep(200);
                arr = receiveByteArray(4, true);
                return arr;
            }
            else
            {
                int p = Convert.ToInt32(port_name);
                return this.appTest.fakeDevList[p];
            }
        }

        public void CloseTeraPort()
        {
            if (!this.isTestApp && this.teraPort.IsOpen) this.teraPort.Close(); 
        }

        public void OpenTeraPort()
        {
            if (!this.isTestApp && !this.teraPort.IsOpen) this.teraPort.Open();
        }

        public void WriteTeraPort(byte[] ByteArray)
        {
            if (this.isTestApp) return;
            this.OpenTeraPort();
            this.teraPort.Write(ByteArray, 0, ByteArray.Length);
        }

        public void RenamePort(string name)
        {
            this.CloseTeraPort();
            this.teraPort.PortName = name;
        }
    }
}
