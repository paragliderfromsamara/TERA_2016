using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;
using System.Threading;
namespace TERA_2016
{
    /// <summary>
    /// Класс в котором хранится функционал по управлению тераомметром
    /// </summary>
    public class TeraDevice
    {
        public string serial = "unknown"; //серийный номер прибора
        public string portName = "unknown"; //порт, к которому прибор подключен
        public uint checkSumFromDevice = 0; //проверочная сумма для коэффициентов коррекции из прибора
        public uint checkSumFromDB = 0; //Проверочная сумма из БД
        public float[] rangeCoeffs = new float[] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f}; //коэффициенты коррекции по диапазону
        public float[] voltageCoeffs = new float[] { 0.0f, 0.0f, 0.0f };     //коэффициенты коррекции по напряжению
        /// <summary>
        /// Массив интегрирующих емкостей. Адрес в массиве == номеру диапазона
        /// </summary>
        public double[] capacitiesList = new double[] { 91.1, 11.1, 1.1, 0.1 }; //массив интегрирующих емкостей
        public double zeroResistance = 0.0003; //нулевое сопротивление, зависит от номинала защитного резистора
        public double refVoltage = 4.8; //опорное напряжение
        private DataRow dataRow = null;
        private mainForm mForm = null;
        /*комманды для управления прибором*/
        public static byte[] getSerialNumberCmd = { 0x13, 0x00}; //Запрос серийного номера
        public static byte[] getSerialWidthCheckSumCmd = { 0x13, 0xFF }; //Запрос серийного номера контрольной суммы
        public static byte[] getRangeAndVoltageCoeffs = { 0x80, 0x10 }; //Запрос коэффициентов
        public static byte[] disconnectDeviceCmd = { 0x31, 0x00 }; //отключение устройства
        public static byte[] startIntegratorCmd = { 0x99, 0x77 }; //запуск интегрирования
        public static byte[] retentionVoltageCmd = { 0x99, 0x00 }; //удержание напряжения при установленном времени поляризации, иначе при установленном времени поляризации через 4 секунды произойдёт отключение источника напряжения
        public static byte[] setVoltageCmd = { 0x77, 0xC0 }; //запуск источника напряжения, в зависимости от выставляемого напряжения второй байт будет принимать значения для 10В - 0 
        private string testString = "";

        /*--------------------------------*/
        public TeraDevice()
        {

        }
        public TeraDevice(mainForm f, string _serial_number)
        {
            this.serial = _serial_number;
            this.mForm = f;
        }
        public TeraDevice(mainForm f, string _serial_number, int _check_sum)
        {
            this.serial = _serial_number;
            this.mForm = f;
            this.checkSumFromDevice = (uint)_check_sum;
            this.portName = this.mForm.teraPort.PortName;
            if (!checkExistingInDB()) syncCoeffs(true); //если в базе не было такого прибора, то загружаем коэффициенты из прибора в БД
        }

        /// <summary>
        /// Ищет устройство в базе, если есть заполняет переменные класса. Если нет, создаёт добавляет в базу устройство
        /// </summary>
        private bool checkExistingInDB()
        {
            string query = String.Format(dbSettings.Default.selectDeviceBySerial, this.serial);
            DataSet data_set = new DataSet();
            DBControl dc = new DBControl(dbSettings.Default.dbName);
            MySqlDataAdapter da = new MySqlDataAdapter(query, dc.MyConn);

            da.Fill(data_set);

            if (data_set.Tables[0].Rows.Count > 0)
            {
                this.dataRow = data_set.Tables[0].Rows[0];
                fillFromDB();
                //MessageBox.Show("Тераомметр найден в БД");
                return true; 
            }
            else
            {
                query = String.Format(dbSettings.Default.insertDevice, this.serial);
                MySqlCommand cmd = new MySqlCommand(query, dc.MyConn);
                dc.MyConn.Open();
                cmd.ExecuteNonQuery();
                dc.MyConn.Close();
                this.checkSumFromDB = this.checkSumFromDevice;
                return false;
            }
        }

        /// <summary>
        /// Синхронизирует коэффициенты с устройством. Если fromDevToPC == true то с устройства на ПК, если false то наоборот
        /// </summary>
        /// <param name="isDevToPC"></param>
        public void syncCoeffs(bool fromDevToPC)
        {
            deviceControl.coeffsSynchronyzeStatus stsForm = new deviceControl.coeffsSynchronyzeStatus(fromDevToPC, this.serial);
            stsForm.Show();
            if (fromDevToPC)
            {
                DBControl dc = new DBControl(dbSettings.Default.dbName);
                string query = dbSettings.Default.updateRangeAndVoltageCoeffs;
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = dc.MyConn;
                this.sendCommand(getRangeAndVoltageCoeffs, false);
                stsForm.completeStatus(1);
                Thread.Sleep(200);
                this.receiveCoeffs();
                this.mForm.CloseTeraPort();
                stsForm.completeStatus(2);
                cmd.CommandText = String.Format(query, this.serial, this.rangeCoeffs[0], this.rangeCoeffs[1], this.rangeCoeffs[2], this.rangeCoeffs[3], this.rangeCoeffs[4], this.voltageCoeffs[0], this.voltageCoeffs[1], this.voltageCoeffs[2], this.checkSumFromDevice);
                dc.MyConn.Open();
                cmd.ExecuteNonQuery();
                dc.MyConn.Close();
                stsForm.completeStatus(3);
                Thread.Sleep(1000);
                stsForm.Close();
            }
            else
            {

            }
        }

        /// <summary>
        /// Запускает интегрирование
        /// </summary>
        public void startIntegrator()
        {
             this.sendCommand(startIntegratorCmd);
        }

        public int[] checkResult()
        {
            int[] result = { };
            if (!this.mForm.isTestApp)
            {
                if (this.mForm.teraPort.BytesToRead == 8)
                {
                    result = new int[5];
                    result[0] = this.mForm.teraPort.ReadByte();
                    result[1] = this.mForm.teraPort.ReadByte();
                    result[2] = this.mForm.teraPort.ReadByte() + this.mForm.teraPort.ReadByte() * 256;
                    result[3] = this.mForm.teraPort.ReadByte() + this.mForm.teraPort.ReadByte() * 256;
                    result[4] = this.mForm.teraPort.ReadByte() + this.mForm.teraPort.ReadByte() * 256;

                }
            }else
            {
                Random r = new Random();
                result = new int[] { 0, 2, r.Next(130, 133), r.Next(46, 48), r.Next(500, 510)};

            }

            return result;
        }

        /// <summary>
        /// Установка напряжения, если 0 то выключаем
        /// </summary>
        /// <param name="volt"></param>
        public void setVoltage(byte volt)
        {
            byte[] newCmd;
            byte b1 = setVoltageCmd[0];
            byte b2 = setVoltageCmd[1];
            b2 |= volt;
            newCmd = new byte[] { b1, b2 };
            this.sendCommand(newCmd);
        }
        /// <summary>
        /// Посылает флаг удержания напряжения, чтобы источник не выключился
        /// </summary>
        public void retentionVoltage()
        {
            this.sendCommand(retentionVoltageCmd);
        }
        private void receiveCoeffs()
        {
            float[] range_coeffs = new float[this.rangeCoeffs.Length];
            float[] voltage_coeffs = new float[this.voltageCoeffs.Length];
            if (this.mForm.isTestApp)
            {
                this.rangeCoeffs = this.mForm.appTest.rangeCoeffs();
                this.voltageCoeffs = this.mForm.appTest.voltageCoeffs();
                Thread.Sleep(100);
                return;
            }
            //если приложение не в тестовом режиме
            for (int j=0; j< this.rangeCoeffs.Length; j++)
            {

                range_coeffs[j] = (float)receiveDouble();
                
            }
            for (int j = 0; j < this.voltageCoeffs.Length; j++)
            {
                voltage_coeffs[j] = (float)receiveDouble();
            }
            this.rangeCoeffs = range_coeffs;
            this.voltageCoeffs = voltage_coeffs;
            
        }


        private float receiveDouble()
        {
            float val = 0;
            Single v = 0;
            byte[] arr = new byte[4];
            byte[] tmp = new byte[4];
            try
            {
                tmp = this.mForm.receiveByteArray(4, false);
                arr = new byte[] { tmp[3], tmp[0], tmp[1], tmp[2]};
                v = BitConverter.ToSingle(arr, 0);
                val = ((float)v);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            //testString += val.ToString()+ "; " + v.ToString() + String.Format("; {0}-{1}-{2}-{3}", arr[0], arr[1], arr[2], arr[3]) + ";\n";
            return val;
        }
        /// <summary>
        /// Заполняет свойства класса из БД
        /// </summary>
        private void fillFromDB()
        {
            this.rangeCoeffs = new float[] {
                                                ServiceFunctions.convertToFloat(dataRow["zero_range_coeff"]),
                                                ServiceFunctions.convertToFloat(dataRow["first_range_coeff"]),
                                                ServiceFunctions.convertToFloat(dataRow["second_range_coeff"]),
                                                ServiceFunctions.convertToFloat(dataRow["third_range_coeff"]),
                                                ServiceFunctions.convertToFloat(dataRow["third_range_additional_coeff"])
                                            };
            this.voltageCoeffs = new float[]
                                            {
                                                ServiceFunctions.convertToFloat(dataRow["one_hundred_volts_coeff"]),
                                                ServiceFunctions.convertToFloat(dataRow["five_hundred_volts_coeff"]),
                                                ServiceFunctions.convertToFloat(dataRow["thousand_volts_coeff"])
                                            };
            this.checkSumFromDB = (uint)dataRow["coeffs_check_sum"];
        }

        /// <summary>
        /// Отсоединяет прибор
        /// </summary>
        public void Disconnect()
        {
            try
            {
                sendCommand(disconnectDeviceCmd, true);
            }
            catch(Exception e)
            {
                MessageBox.Show("Не удалось корректно отключить прибор", e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        public void connectionError(string m)
        {
            MessageBox.Show(m, "Ошибка связи с прибором ТОмМ-01 " + this.serial, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Посылает команду в прибор, с рассчётом на то, что порт уже открыт
        /// </summary>
        /// <param name="bArr"></param>
        private void sendCommand(byte[] bArr)
        {
            try
            {
                this.mForm.WriteTeraPort(bArr);
            }
            catch(Exception e)
            {
                this.connectionError(e.Message);
            }
            
        }
        /// <summary>
        /// Посылает комманду в Прибор открывая/закрывая порт
        /// </summary>
        /// <param name="bArr"></param>
        /// <param name="needClose"></param>
        private void sendCommand(byte[] bArr, bool needClose)
        {
            this.mForm.RenamePort(this.portName);
            this.sendCommand(bArr);
            if (needClose){this.mForm.CloseTeraPort();}
        }

        public static string makeTeraSerial(byte iY, byte iN)
        {
            string y = "20"+((iY < 10) ? "0" : "") + iY.ToString();
            string n = ((iN < 10) ? "0" : "") + iN.ToString();
            return y + "-" + n;
        }
    }
}
