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
                int mCountLimit = this.teraMeasure.statisticMeasureAmount;
                int averageResult = 0;
                int result = 0;
                this.teraMeasure.tDevice.setVoltage((byte)this.teraMeasure.voltage);
                Thread.Sleep(500);
                //int[] cmd = uTest.makeCmd;
                //uTest.setDiaps();
                do
                {
                    this.teraMeasure.tDevice.startIntegrator();
                    if (!this.teraMeasure.getMeasureResult()) { TeraMeasure.measureError("Превышено время ожидания результата"); break; }
                    /*if (this.teraMeasure.isStatistic)
                    {
                        mCount = 0;
                        averageResult = 0;
                        while (mCount < mCountLimit)
                        {
                            result = this.teraMeasure.getMeasureResult();
                            averageResult += result;
                            mCount++;
                        }
                        result = (averageResult) / (mCountLimit);
                    }
                    else
                    {
                        result = this.teraMeasure.getMeasureResult();
                    }
                    //счетчик циклов
                    //this.teraMeasure.mForm.updateResultField(result);
                    */
                    //this.pause();
                } while (isFinished(mCount));

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


            bool isFinished(int mCount)
            {
                if (this.teraMeasure.isStatistic)
                {
                    return mCount < this.teraMeasure.statisticMeasureAmount;
                }
                else
                {
                    return true;
                }
            }
        }
        public TeraDevice tDevice = null;
        public measureForms.manualMeasureForm mForm = null;
        private measureThread mThread = null;

        public bool isStart = false;            //true испытание запущенно, false не производится
        public bool isStatistic = false;        //Статиcтическое true обычное false
        public int statisticMeasureAmount = 1;  //Количество статистических испытаний
        public int betweenMeasurePause = 100;   //Пауза между испытаниями
        public uint modeId = 0;                  //Режим измерения абсолютный/R*L/pS/pV
        public int voltage = 0;                //измерительное напряжение
        public byte cameraInnerRadius = 0;           //внутренний диаметр охранного кольца
        public byte cameraOuterRadius = 0;           //внешний диаметр охранного кольца
        public int polDelay = 0;                //время поляризации
        public int depolDelay = 0;              //время деполяризации
        public int cableLength = 1000;          //длина кабеля

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
                string s = String.Format("Статус: {0}; Диапазон: {1}; Длительность: {2}; Начальное состояние: {3}; Конечное состояние: {4};", resultArray[0], resultArray[1], resultArray[2], resultArray[3], resultArray[4]);
                this.measStatus = resultArray[0];
                this.rangeId = resultArray[1];
                this.measTime = resultArray[2];
                this.firstMeasure = resultArray[3];
                this.lastMeasure = resultArray[4];
                this.mForm.updateServiceField(s);
            }
            return f;
        }

        public static void measureError(string m)
        {
            MessageBox.Show("Ошибка испытания", m, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}
