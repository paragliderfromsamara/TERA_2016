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
    public partial class manualMeasureForm : Form
    {
        delegate void updateServiceFieldDelegate(string serviceInfo);
        private mainForm mForm = null;
        private deviceControl.TeraMeasure teraMeas = null;
        private int externalCamDiam, internalCamDiam;


        public manualMeasureForm(mainForm f)
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
        }

        private void manualMeasureForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mForm.manualMeasureForm = null;
        }

        private void startMeasureBut_Click(object sender, EventArgs e)
        {
            
            if (!teraMeas.isStart)
            {
                if (!this.mForm.teraPort.IsOpen) this.mForm.teraPort.Open();
                teraMeas.voltage = voltageComboBox.SelectedIndex + 1;
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
                teraMeas.startTest();
            }else
            {
                teraMeas.stopTest();
                this.mForm.teraPort.Close();
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
                    cameraLbl.Visible = cameraTypesCB.Visible = materialLength.Visible = materialLengthLbl.Visible = diametersLbl.Visible = false;
                    break;
                case "2":
                    materialLength.Visible = materialLengthLbl.Visible = true;
                    cameraLbl.Visible = cameraTypesCB.Visible = diametersLbl.Visible = false;
                    break;
                case "3":
                case "4":
                    cameraLbl.Visible = cameraTypesCB.Visible = diametersLbl.Visible = true;
                    materialLength.Visible = materialLengthLbl.Visible = false;
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
            diametersLbl.Text = String.Format("Внутренний диаметр охранного кольца: {0}мм; \nВнешний диаметр охранного кольца: {1}мм;", internalCamDiam, externalCamDiam);
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
    }
}
