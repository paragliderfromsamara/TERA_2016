using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TERA_2016.forAppTest
{
    public class appTest
    {

        public byte[][] fakeDevList = new byte[][] {
                                                       new byte[]{ 15, 36, 114, 152},
                                                       new byte[]{ 14, 13, 66, 25},
                                                       new byte[]{ 12, 33, 45, 210},
                                                       new byte[]{ 16, 3, 195, 133},
                                                       new byte[]{ 13, 23, 251, 23},
                                                       new byte[]{ 15, 2, 111, 222}
                                                   }; 

        public appTest()
        {
           
        }

        /// <summary>
        /// Возвращает фэйковые номера портов в виде адресов массива с фэйковыми устройствами
        /// </summary>
        /// <returns></returns>
        public string[] fakePortNumbers()
        {
            string[] s = new string[fakeDevList.Length];
            for(int i=0; i< fakeDevList.Length; i++)
            {
                s[i] = i.ToString();
            }
            return s;
        }
        /// <summary>
        /// Выдаёт случайное число от 0.9 до 1.1
        /// </summary>
        /// <returns></returns>
        private float getRandomNearOne()
        {
            Random e = new Random();
            int v = e.Next(80, 120);
            double vf = (double)v / (double)100.0;
            return (float)vf;
        }

        public float[] voltageCoeffs()
        {
            TeraDevice t = new TeraDevice();
            float[] a = new float[t.voltageCoeffs.Length];
            for (int i = 0; i < t.voltageCoeffs.Length; i++)
            {
                a[i] = getRandomNearOne();
            }
            return a;
        }
        public float[] rangeCoeffs()
        {
            TeraDevice t = new TeraDevice();
            float[] a = new float[t.rangeCoeffs.Length];
            for(int i=0; i< t.rangeCoeffs.Length; i++)
            {
                a[i] = getRandomNearOne();
            }
            return a;
        }
}       
}
