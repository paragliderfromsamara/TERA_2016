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
        public mainForm()
        {
            InitializeComponent();
            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            dataMigration dm = new dataMigration();
            Signin signForm = new Signin();
            DialogResult result = signForm.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.Cancel) this.Close();
            else if (result == System.Windows.Forms.DialogResult.OK)
            {
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
            isConnected = getDevicePorts();
            if (!isConnected)
            {
                MessageBox.Show("Нет ни одного подключённого Тераомметра");
                SwitchDeviceMenus(false);
            }
            else
            {
                deviceList.SelectedIndex = 0;
            }
        }

        private void SwitchDeviceMenus(bool v)
        {
            deviceSettingsToolStripMenuItem.Enabled = testsToolStripMenuItem.Enabled = v;
            
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
            if (teraPort.IsOpen) teraPort.Close();
            try
            {
                teraPort.PortName = port_name;
                teraPort.Open();
                teraPort.Write(TeraDevice.getSerialWidthCheckSumCmd, 0, TeraDevice.getSerialWidthCheckSumCmd.Length);
                Thread.Sleep(200);
                arr = receiveByteArray(4);
                teraPort.Close();

                if (arr[0] < 50 && arr[0] > 10) //Проверка валидности номера ЦПС
                {
                    int _check_sum = (int)arr[2] + (int)arr[3] * 256;
                    string serialNumber = String.Format("20{0}-{1}", arr[0], arr[1]);
                    deviceList.Items.Insert(0, serialNumber);
                    Thread.Sleep(100);
                    checkDeviceBySerial(serialNumber, _check_sum);
                    return true;
                }
                
            }
            catch// (Exception e)
            {
               //  MessageBox.Show(e.Message);
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
            if (teraPort.IsOpen) teraPort.Close();
            try
            {
                teraPort.PortName = port_name;
                teraPort.Open();
                teraPort.Write(TeraDevice.getSerialWidthCheckSumCmd, 0, TeraDevice.getSerialWidthCheckSumCmd.Length);
                Thread.Sleep(200);
                arr = receiveByteArray(4);
                teraPort.Close();
                tmpSerial = String.Format("20{0}-{1}", arr[0], arr[1]);
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
            catch //(Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            return false;
        }
        public bool getDevicePorts()
        {
            string[] port_list = SerialPort.GetPortNames();
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
        public byte[] receiveByteArray(int n)
        {
            byte[] arr = new byte[n];
            for(int i=0; i<n; i++)
            {
                arr[i] = (byte)teraPort.ReadByte();
                Thread.Sleep(20);
            }
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
            string[] port_list = SerialPort.GetPortNames();
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
            for (int i = 0; i < deviceList.Items.Count; i++) deviceList.Items.RemoveAt(i);
        }
        private void cleanCurrentDevice()
        {
            if (this.currentDevice != null) { this.currentDevice.Disconnect(); Thread.Sleep(200); this.currentDevice = null; Thread.Sleep(200); }
        }

        private void searchDevicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cleanCurrentDevice();
            cleanDeviceList();
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
            if (currentDevice == null) return;
            if (currentDevice.checkSumFromDB != currentDevice.checkSumFromDevice)
            {
                DialogResult r = MessageBox.Show("Обновить настройки?", "Необходимо обновить настройки прибора в БД", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.Yes)
                {
                    this.currentDevice.syncCoeffs(true);
                }
            }
        }
    }
}
