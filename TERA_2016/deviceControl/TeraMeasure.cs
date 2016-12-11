using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace TERA_2016.deviceControl
{
    public class TeraMeasure
    {
        class measureTimerThread
        {
            public Thread thread;
            public TeraMeasure teraMeasure;
            public measureTimerThread(TeraMeasure tst)
            {
                this.teraMeasure = tst;
                this.thread = new Thread(this.timer);
                this.thread.Start();//передача параметра в поток
            }

            void timer()
            {
                int m = 0;
                int s = 0;
                this.teraMeasure.mForm.refreshMeasureTimer(new int[] { s, m });
                Thread.Sleep(1000);
                do
                {
                    Thread.Sleep(1000);
                    s++;
                    if (s == 60) { s = 0; m++; } 
                    this.teraMeasure.mForm.refreshMeasureTimer(new int[] { s, m });
                } while (true);
            }
        }
        class measureThread
        {
            public Thread thread;
            public TeraMeasure teraMeasure;


            public measureThread(TeraMeasure tst) //Конструктор получает имя функции и номер до кторого ведется счет
            {
                this.teraMeasure = tst;
                this.thread = new Thread(this.measure);
                this.thread.Start();//передача параметра в поток
            }


     
            void measure()//Функция потока, передаем параметр
            {
                int mCount = 0;
                int cycleCount = 0;
                int mCountLimit = this.teraMeasure.statisticMeasureAmount;
                int cycleCountLimit = this.teraMeasure.cycleMeasureAmount;
                double averageResult = 0;
                double result = 0;
                this.teraMeasure.tDevice.setVoltage((byte)this.teraMeasure.voltageId);
                Thread.Sleep(500);
                //int[] cmd = uTest.makeCmd;
                //uTest.setDiaps();
                do
                {
                    this.teraMeasure.mForm.updateCycleNumberField((cycleCount+1).ToString());
                    this.teraMeasure.tDevice.startIntegrator();
                    if (!this.teraMeasure.getMeasureResult()) { TeraMeasure.measureError("Превышено время ожидания результата"); break; }
                    result = this.teraMeasure.convertAdcResult();
                    averageResult += result;
                    if (this.teraMeasure.isStatistic) this.teraMeasure.mForm.updateStatMeasInfo(new string[] { String.Format("{0} из {1}", mCount+1, mCountLimit), this.teraMeasure.absoluteResultView(result) });
                    mCount++;
                    if (mCount < mCountLimit) continue;
                    result = (averageResult) / ((double)mCountLimit);
                    this.teraMeasure.mForm.updateResultField(result);
                    averageResult = 0;
                    mCount = 0;
                    cycleCount++;
                    if (!this.teraMeasure.isCyclic && cycleCount == this.teraMeasure.cycleMeasureAmount || this.teraMeasure.measStatus > 0) break;
                    //счетчик циклов
                    //this.teraMeasure.mForm.updateResultField(this.teraMeasure.absoluteResultView(result));
                    //this.pause();
                } while (true);
                
                //this.teraMeasure.measTestForm.switchButtons(false);
            }

            void pause()
            {
                if (this.teraMeasure.betweenMeasurePause > 0)
                {
                    Thread.Sleep(this.teraMeasure.betweenMeasurePause);
                }
                else Thread.Sleep(1);
            }


            bool isFinished(int cycleCount)
            {
                if (!this.teraMeasure.isCyclic)
                {
                    return true;
                }
                else
                {
                   return cycleCount < this.teraMeasure.cycleMeasureAmount;
                }
            }
        }
        public TeraDevice tDevice = null;
        public measureForms.manualTeraMeasureForm mForm = null;
        private measureThread mThread = null;
        private measureTimerThread tThread = null;

        public bool isStart = false;            //true испытание запущенно, false не производится
        public bool isStatistic = false;        //Статиcтическое true обычное false
        public bool isCyclic = false;            //Цикличное измерение
        public int statisticMeasureAmount = 1;  //Количество статистических испытаний
        public int cycleMeasureAmount = 1; //количество циклов измерений
        public int betweenMeasurePause = 100;   //Пауза между испытаниями
        public int minTimeToNorm = 1;


        public int voltageId = 0;                //код измерительного напряжения от 0 до 3
        public int voltageValue = 0;            //Значение измерительного напряжения
        public int polDelay = 0;                //время поляризации
        public int depolDelay = 0;              //время деполяризации
        public double bringingCoeff = 1.0;          //коэффициент приведения
        public int firstMeasure = 0;
        public int lastMeasure = 0;
        public int measTime = 0;
        public int rangeId = 0;
        public int measStatus = 0;

        public TeraMeasure(TeraDevice d)
        {
            tDevice = d;
        }


        

        public void startTest() //запуск испытаний
        {
            if (mThread == null)
            {
                //setDiap();
                mThread = new measureThread(this);
                tThread = new measureTimerThread(this);
                //this.measTestForm.switchButtons(true);
            }
            this.isStart = true;
        }
        public void stopTest() //остановка испытаний
        {
            if (mThread != null)
            {
                //measTestForm.mainForm.stendPort.Close();
                mThread.thread.Abort();
                mThread = null;
                tThread.thread.Abort();
                tThread = null;
                //this.measTestForm.switchButtons(false);

            }
            this.isStart = false;
        }

        private bool getMeasureResult()
        {
            this.tDevice.startIntegrator();
            bool f = false;
            int maxMeasTime = 400;
            int time = 0;
            int[] resultArray;
            do
            {
                resultArray = this.tDevice.checkResult();
                if (resultArray.Length == 5) { f = true;  break; };
                Thread.Sleep(100);
                time++;
            } while (time < maxMeasTime);
            if (f)
            {
                //string s = String.Format("Статус: {0}; Диапазон: {1}; Длительность: {2}; Начальное состояние: {3}; Конечное состояние: {4};", resultArray[0], resultArray[1], resultArray[2], resultArray[3], resultArray[4]);
                this.measStatus = resultArray[0];
                this.rangeId = resultArray[1];
                this.measTime = resultArray[2];
                this.firstMeasure = resultArray[3];
                this.lastMeasure = resultArray[4];
                //this.mForm.updateServiceField(s);
            }
            return f;
        }

        private string absoluteResultView(double r)
        {
            string[] quntMeas = new string[] { "МОм", "ГОм", "ТОм" };
            int qIdx = 0;
            double mult = 0;
            int rnd = 0;
            if (r>1)
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
            return String.Format("{0} {1}", Math.Round(r, rnd), quntMeas[qIdx]); 
        }


        private double convertAdcResult()
        {
            double _R = 0;
            double integratorDifference = (this.lastMeasure > this.firstMeasure) ? ((double)this.lastMeasure - (double)this.firstMeasure) : 0;
            double capacity = this.tDevice.capacitiesList[this.rangeId];
            double v = (double)this.voltageValue/100;
            double[] limit = new double[] { 20000, 200000, 1000000, 2000000 };
            double mTime = Convert.ToDouble(this.measTime);
            string s = String.Format("Статус: {0}; Диапазон: {1}; Длительность: {2}; Начальное состояние: {3}; Конечное состояние: {4};", this.measStatus, this.rangeId, this.measTime, this.firstMeasure, this.lastMeasure);
            this.mForm.updateServiceField(s);
            if (integratorDifference>0)
            {
                _R = (2048.0 * v * (double)(this.measTime)) / (this.tDevice.refVoltage * integratorDifference * this.tDevice.capacitiesList[this.rangeId]);
            }else
            {
                _R = 0;
            }    
            if (mTime == Convert.ToDouble(1499) && _R < (limit[this.voltageId]) && (_R > 0))
                if (integratorDifference > 0) _R = (2048.0 * v * (double)(this.measTime)) / (this.tDevice.refVoltage * (integratorDifference - (double)this.tDevice.rangeCoeffs[5]) * this.tDevice.capacitiesList[this.rangeId]);
            
            _R *= (double)this.tDevice.rangeCoeffs[this.rangeId];      			                    // Умножаем на коэф. коррекции от диапазона.
            if (this.voltageValue > 10) _R *= (double)this.tDevice.voltageCoeffs[this.voltageId-2]; // Умножаем на коэф. коррекции от напряжения если оно не 10В.
            if (_R > 0.0004) _R -= this.tDevice.zeroResistance;                                     // Вычитаем 300кОм последовательно включён
            if (_R < 0.005 && _R > 0) _R += 0.00013;                                                // Если меньше 5МОм то добавляем 170 кОм
            return _R * this.bringingCoeff;
        }

        public static void measureError(string m)
        {
            MessageBox.Show("Ошибка испытания", m, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}
