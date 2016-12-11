using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using MySql.Data.MySqlClient;
using System.Data;

namespace TERA_2016.measureForms
{
    /// <summary>
    /// Базовый класс для форм измерений
    /// </summary>
    public partial class MeasureFormBase : Form
    {

        protected mainForm mForm = null;
        protected deviceControl.TeraMeasure teraMeas = null; 
        protected double[][] isolationMaterialCoeffsArr;

        protected delegate void updateServiceFieldDelegate(string serviceInfo);
        protected delegate void updateResultFieldDelegate(double result);
        protected delegate void updateCycleNumberFieldDelegate(string cycleNumb);
        protected delegate void updateStatMeasInfoDelegate(string[] statMeasInfo);
        protected delegate void refreshMeasureTimerDelegate(int[] time);
        protected delegate void switchFieldsMeasureOnOffDelegate(bool flag);

        protected double getIsolationCoeff(int mIdx, int tIdx)
        {
            try
            {
                return isolationMaterialCoeffsArr[mIdx][tIdx];
            }
            catch
            {
                return 1.0;
            }
        }
    }
}
