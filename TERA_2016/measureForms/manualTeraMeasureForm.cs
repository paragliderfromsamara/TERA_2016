using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace TERA_2016.measureForms
{
    public partial class manualTeraMeasureForm : MeasureFormBase
    {

        private int externalCamDiam, internalCamDiam;

        public manualTeraMeasureForm(mainForm f)
        {
            mForm = f;
            InitializeComponent();
            f.switchMenuStripItems(false);
            initFields();
            switchBringingParams();
            getCameraDiametersByCameraId();
            serviceParameters.Text = "";
            teraMeas = new deviceControl.TeraMeasure(this.mForm.currentDevice);
            teraMeas.mForm = this;
            cleanMeasInfo();
            
        }

        /// <summary>
        /// Подгружает поля из БД и устанавливает значения по умолчанию
        /// </summary>
        private void initFields()
        {
            DBControl dc = new DBControl(dbSettings.Default.dbName);
            MySqlDataAdapter da = new MySqlDataAdapter();

            voltageComboBox.Text = measureSettings.Default.voltage.ToString();
            dischargeDelay.Value = measureSettings.Default.dischargeDelay;
            polarizationDelay.Value = measureSettings.Default.polarizationDelay;
            isCyclicMeasure.Checked = measureSettings.Default.isCycleMeasure;
            cycleTimes.Value = measureSettings.Default.cycleTimes;
            averagingTimes.Value = measureSettings.Default.averagingTimes;
            externalCamDiam = measureSettings.Default.externalCameraDiameter;
            internalCamDiam = measureSettings.Default.internalCameraDiameter;
            normaField.Value = measureSettings.Default.normaValue;
            lengthQuantitMeasCb.SelectedIndex = measureSettings.Default.lengthBringingIdx;
            materialHeight.Value = measureSettings.Default.materialHeight;
            isDegreeViewCheckBox.Checked = measureSettings.Default.isDegreeView;
            minTimeToNorm.Value = measureSettings.Default.minTimeToNorm;

            da = new MySqlDataAdapter(dbSettings.Default.selectMaterials, dc.MyConn);
            da.Fill(isolation_materials);
            da = new MySqlDataAdapter(dbSettings.Default.selectCameraTypes, dc.MyConn);
            da.Fill(camera_types);
            da = new MySqlDataAdapter(dbSettings.Default.selectBringingTypes, dc.MyConn);
            da.Fill(bringing_types);
            dc.Dispose();
            da.Dispose();
            foreach(DataRow r in camera_types.Rows)
            {
                if (r["internal_diameter"].ToString() == internalCamDiam.ToString() && r["external_diameter"].ToString() == externalCamDiam.ToString())
                {
                    cameraTypesCB.Text = r["name"].ToString();
                    break;
                }
            }
            bringingTypeCB.SelectedValue = measureSettings.Default.bringingTypeId;
            materialTypes.SelectedValue = measureSettings.Default.materialTypeId;
            this.buildIsolationMaterialArr();
        }

        /// <summary>
        /// Выкачивает из БД коэффициенты температуры изоляционных материалов и забивает их в isolationMaterialCoeffsArr
        /// </summary>
        private void buildIsolationMaterialArr()
        {
            int materialsNumb = this.materialTypes.Items.Count;
            //double[] defArr = new double[] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 };
            double[][] resultArr = new double[materialsNumb][];          
            DBControl dc = new DBControl(dbSettings.Default.dbName);
            MySqlDataAdapter da = new MySqlDataAdapter();
            for(int i = 0; i < materialsNumb; i++)
            {
                string q = String.Format(dbSettings.Default.selectIsolationMaterialCoeffs, i+1);
                DataSet ds = new DataSet();
                da = new MySqlDataAdapter(q, dc.MyConn);
                da.Fill(ds);
                DataTable dt = ds.Tables[0];
                double[] coeffsArr = new double[dt.Rows.Count];
                for(int j=0; j< dt.Rows.Count; j++)
                {
                     coeffsArr[j] = Convert.ToDouble(dt.Rows[j]["coeff_val"].ToString());
                }
                resultArr[i] = coeffsArr;
            }
            this.isolationMaterialCoeffsArr = resultArr;
            /*
            string s = "";
            for(int i =0; i< defArr.Length; i++)
            {
                if (i > 0) s += "\n";
                for(int j=0; j< this.materialTypes.Items.Count; j++)
                {
                    s += string.Format("  {0}", this.isolationMaterialCoeffsArr[j][i]); 
                }
            }
            MessageBox.Show(s);
            */

        }

        private void manualMeasureForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mForm.manualMeasureForm = null;
        }

        private void startMeasureBut_Click(object sender, EventArgs e)
        {
            
            if (!teraMeas.isStart)
            {
                this.mForm.OpenTeraPort();
                teraMeas.voltageId = voltageComboBox.SelectedIndex + 1;
                teraMeas.voltageValue = Convert.ToInt32(voltageComboBox.Text);
                teraMeas.cycleMeasureAmount = Convert.ToInt32(this.cycleTimes.Value);
                teraMeas.isCyclic = this.isCyclicMeasure.Checked;
                teraMeas.isStatistic = this.averagingTimes.Value > 1;
                teraMeas.statisticMeasureAmount = Convert.ToInt32(this.averagingTimes.Value);
            
                measureSettings.Default.voltage = Convert.ToInt16(voltageComboBox.Text);
                measureSettings.Default.dischargeDelay = Convert.ToInt16(dischargeDelay.Value);
                measureSettings.Default.polarizationDelay = Convert.ToInt16(polarizationDelay.Value);
                measureSettings.Default.cycleTimes = Convert.ToInt16(cycleTimes.Value);
                measureSettings.Default.isCycleMeasure = isCyclicMeasure.Checked;
                measureSettings.Default.averagingTimes = Convert.ToInt16(averagingTimes.Value);
                measureSettings.Default.bringingLength = Convert.ToInt16(materialLength.Value);
                measureSettings.Default.internalCameraDiameter = internalCamDiam;
                measureSettings.Default.externalCameraDiameter = externalCamDiam;
                measureSettings.Default.materialTypeId = materialTypes.SelectedValue.ToString();
                measureSettings.Default.bringingTypeId = bringingTypeCB.SelectedValue.ToString();
                measureSettings.Default.lengthBringingIdx = this.lengthQuantitMeasCb.SelectedIndex;
                measureSettings.Default.normaValue = Convert.ToInt32(this.normaField.Value);
                measureSettings.Default.materialHeight = Convert.ToInt32(this.materialHeight.Value);
                measureSettings.Default.isDegreeView = isDegreeViewCheckBox.Checked;
                measureSettings.Default.minTimeToNorm = Convert.ToInt32(minTimeToNorm.Value);
                teraMeas.startTest();
            }else
            {
                teraMeas.stopTest();
                this.mForm.CloseTeraPort();
            }
            
        }

        private void bringingTypeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            switchBringingParams();
        }

        /// <summary>
        /// В зависимости от режима измерения скрывает или показывает настройки измеряемых параметров
        /// </summary>
        private void switchBringingParams()
        {
            switch (bringingTypeCB.SelectedValue.ToString())
            {
                case "1":
                    materialHeightLbl.Visible = materialHeight.Visible = bringingToLbl.Visible = lengthQuantitMeasCb.Visible = cameraLbl.Visible = cameraTypesCB.Visible = materialLength.Visible = materialLengthLbl.Visible = false;
                    break;
                case "2":
                    bringingToLbl.Visible = lengthQuantitMeasCb.Visible = materialLength.Visible = materialLengthLbl.Visible = true;
                    materialHeightLbl.Visible = materialHeight.Visible = cameraLbl.Visible = cameraTypesCB.Visible = false;
                    break;
                case "3":
                    cameraLbl.Visible = materialHeightLbl.Visible = materialHeight.Visible = cameraTypesCB.Visible = true;
                    bringingToLbl.Visible = lengthQuantitMeasCb.Visible = materialLength.Visible = materialLengthLbl.Visible = false;
                    break;
                case "4":
                    cameraLbl.Visible = cameraTypesCB.Visible = true;
                    materialHeightLbl.Visible = materialHeight.Visible = bringingToLbl.Visible = lengthQuantitMeasCb.Visible = materialLength.Visible = materialLengthLbl.Visible = false;
                    break;
            }
        }

        private void cameraTypesCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            getCameraDiametersByCameraId();
        }
        private void getCameraDiametersByCameraId()
        {
            string cId = cameraTypesCB.SelectedValue.ToString();
            foreach(DataRow r in camera_types.Rows)
            {
                if (r["id"].ToString() == cId)
                {
                    internalCamDiam = Convert.ToInt16(r["internal_diameter"].ToString());
                    externalCamDiam = Convert.ToInt16(r["external_diameter"].ToString());
                    break;
                }
            }
        }

        public void updateServiceField(string serviceInfo) //Для обновления поля результата из другого потока в котором проходит испытание
        {
            if (InvokeRequired)
            {
                BeginInvoke(new updateServiceFieldDelegate(updateServiceField), new object[] { serviceInfo });
                return;
            }
            else
            {
                this.serviceParameters.Text = serviceInfo;
            }
        }

        public void updateCycleNumberField(string cycleNumb) //Для обновления поля результата из другого потока в котором проходит испытание
        {
            if (InvokeRequired)
            {
                BeginInvoke(new updateCycleNumberFieldDelegate(updateCycleNumberField), new object[] { cycleNumb });
                return;
            }
            else
            {
                this.cycleCounterLbl.Text = "Цикл: " + cycleNumb;
            }
        }

        private void isCyclicMeasure_CheckedChanged(object sender, EventArgs e)
        {
            this.cycleTimes.Enabled = !this.isCyclicMeasure.Checked;
        }

        public void updateResultField(double result) //Для обновления поля результата из другого потока в котором проходит испытание
        {
            if (InvokeRequired)
            {
                BeginInvoke(new updateResultFieldDelegate(updateResultField), new object[] { result });
                return;
            }
            else
            {
                this.teraMeas.bringingCoeff = calculateBringingCoeff();
                if (isDegreeViewCheckBox.Checked)
                {
                    this.measureResultLbl.Text = deegreeResultView(result); //absoluteResultView(result);
                    this.normaLbl.Text = (this.normaField.Value > 0) ? "норма: " + deegreeResultView((double)this.normaField.Value / 1000) : "";
                }else
                {
                    this.measureResultLbl.Text = absoluteResultView(result); //absoluteResultView(result);
                    this.normaLbl.Text = (this.normaField.Value > 0) ? "норма: " + absoluteResultView((double)this.normaField.Value / 1000) : "";
                }

            }
        }

        /// <summary>
        /// Обновляем поля статистических испытаний
        /// </summary>
        /// <param name="statMeasInfo"></param>
        public void updateStatMeasInfo(string[] statMeasInfo) 
        {
            if (InvokeRequired)
            {
                BeginInvoke(new updateStatMeasInfoDelegate(updateStatMeasInfo), new object[] { statMeasInfo });
                return;
            }
            else
            {
                this.statMeasNumbOfLbl.Text = String.Format("измерение {0}", statMeasInfo[0]);
                this.midStatMeasValLbl.Text = String.Format("промежуточное значение: {0}", statMeasInfo[1]);
            }
        }

        /// <summary>
        /// Обновляем длительность испытаний
        /// </summary>
        /// <param name="statMeasInfo"></param>
        public void refreshMeasureTimer(int[] time)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new refreshMeasureTimerDelegate(refreshMeasureTimer), new object[] { time});
                return;
            }
            else
            {
                string m = time[1] > 9 ? time[1].ToString() : "0"+ time[1].ToString();
                string s = time[0] > 9 ? time[0].ToString() : "0" + time[0].ToString();
                this.measTimeLbl.Text = m+":"+s;
            }
        }

        private string getBringingName()
        {
            switch (bringingTypeCB.SelectedValue.ToString())
            {
                case "2":
                    return "∙"+lengthQuantitMeasCb.Text;
                case "3":
                    return "∙м";
                default:
                    return "";
            }
        }
        private string absoluteResultView(double r)
        {
            string[] quntMeas = new string[] { "МОм", "ГОм", "ТОм" };
            int qIdx = 0;
            double mult = 0;
            int rnd = 0;
            if (r > 1)
            {
                if ((r / 1000) > 1) { qIdx = 2; mult = 0.001; }
                else { qIdx = 1; mult = 1; }
            }
            else
            {
                qIdx = 0;
                mult = 1000;
            }
            r *= mult;
            if (r > 99) rnd = 2;
            else rnd = 3;
            return String.Format("{0} {1}{2}", Math.Round(r, rnd), quntMeas[qIdx], getBringingName());
        }
        private string deegreeResultView(double r)
        {
            double dg = 0;
            double maxDg = 9; //максимальный порядок
            r *= 1000.0;
            for (dg=0; dg <= maxDg; dg++)
            {
                double curVal = Math.Pow(10, dg);
                double nextVal = curVal * 10;
                bool cResult = (dg == 0) ? (r >= 0) : r >= curVal;
                cResult &= r < nextVal;
                if (cResult) break;
            }
            r /= Math.Pow(10, dg);
            return String.Format("{0} Е{1} МОм{0}", Math.Round(r, 2), dg, getBringingName());
        }

        
        private double calculateBringingCoeff()
        {
            double coeff = 1.0;
            coeff *= this.getIsolationCoeff((int)this.materialTypes.SelectedIndex, (int)temperatureField.Value-5);
            switch (bringingTypeCB.SelectedValue.ToString())
            {
                case "1":
                    break;
                case "2":
                    double p = (this.lengthQuantitMeasCb.SelectedIndex > 2) ? 3 : this.lengthQuantitMeasCb.SelectedIndex - 2;
                    coeff =  coeff*((double)this.materialLength.Value / Math.Pow(10.0, p));
                    break;
                case "3":
                    break;
                case "4":
                    break;
            
            }
            label8.Text = coeff.ToString();
            return coeff;
            /*
             * float ConvertByMeasureMode(float _R) //Конвертирует результат в соответствии с текущим режимом испытания
                {
                  byte por;
                  por = SaveData.gh._Mater - 2;
                  if (por == 0xFF) por = 0;
                  _R = _R*(float)(Koefs[por][SaveData.gh._Tizm - 5])/100.0;

                  if (Mode == M_Cab)
                  {
                    if ((SaveData.gh._Uizm)&0x80) _R = _R*(float)(SaveData.gh._Lcab_m)/1000.0;
                    else _R = _R*(float)(SaveData.gh._Lcab_dm)/10000.0;
                    if (!((SaveData.gh._Uizm)&0x40)) _R = _R*1000;
                  }else if (Mode == M_Mater_volume)
                  {
                     por = (byte)(((word)(SaveData.gh._Dobr1) + (word)(SaveData.gh._Dobr2)) >> 1);
                     if (SaveData.gh._Hobr) _R = (_R * 3.14159 * (float)por * (float)por) / ((float)SaveData.gh._Hobr * 4.0); else _R = 0;
                  }else if (Mode == M_Mater_field)
                  {
                     if (SaveData.gh._Dobr1 < SaveData.gh._Dobr2) 
                       _R = _R*3.14159*(float)((word)SaveData.gh._Dobr1 + (word)SaveData.gh._Dobr2)/((float)(SaveData.gh._Dobr2 - SaveData.gh._Dobr1)); 
                         else _R = 0;
                  }
                  return _R;
                }
             */
        }

        private void cleanMeasInfo()
        {
            this.cycleCounterLbl.Text = this.statMeasNumbOfLbl.Text = this.midStatMeasValLbl.Text = this.measTimeLbl.Text = "";
            this.measureResultLbl.Text = "0.0 МОм";
        }


    }
}
