using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RSA_Project
{
    class BigInt
    {
        public List<int> Digits;
        public BigInt()
        {
            Digits = new List<int>();
        }

        public BigInt(BigInt other)
        {
            Digits = new List<int>(other.Digits);
        }

        //convert each character in input string into integer 
        public void Fill_BigInt(string val)
        {
            for (int i = 0; i < val.Length; i++)
            {
                Digits.Add((Convert.ToInt32(val[i])) - 48);
            }
        }
        public void ConvertToBigint(string val)
        {
            int c;
            for (int i = 0; i < val.Length; i++)
            {
                c = (Convert.ToInt32(val[i]));
                if (0 <= c && c <= 99)
                {
                    int First = c % 10;
                    c /= 10;
                    int Second = c % 10;
                    c /= 10;
                    Digits.Add(2);
                    Digits.Add(Second);
                    Digits.Add(First);
                }
                else
                {
                    int First = c % 10;
                    c /= 10;
                    int Second = c % 10;
                    c /= 10;
                    int Third = c % 10;
                    c /= 10;
                    Digits.Add(Third);
                    Digits.Add(Second);
                    Digits.Add(First);
                }
            }
        }
        public string ConvertTOString(BigInt strVal)
        {
            string x = "";
            int j = strVal.Digits.Count - 1;
            for (int i = 0; i < j; i += 3)
            {
                int ch;
                if (strVal.Digits[i] == 2)
                {
                    ch = 10 * strVal.Digits[i + 1];
                    ch += strVal.Digits[i + 2];
                    x += (Convert.ToChar(ch));
                }
                else
                {
                    ch = 100 * strVal.Digits[i];
                    ch += 10 * strVal.Digits[i + 1];
                    ch += strVal.Digits[i + 2];
                    x += (Convert.ToChar(ch));
                }

            }
            return x;
        }

        //output
        //public void output(BigInt v)
        //{
        //    for (int i = 0; i < v.Digits.Count; i++)
        //    {
        //        Console.Write(v.Digits[i]);
        //    }
        //    Console.WriteLine();
        //    //return v.Digits;
        //}
        //function subtraction

        public BigInt left_Zeros(BigInt val)
        {
            if (val.Digits.ElementAt(0) != 0)
                return val;
            BigInt temp_val = new BigInt();

            int count = 0;
            for (int i = 0; i < val.Digits.Count; i++)
            {
                if (val.Digits[i] == 0)
                    count++;
                else
                    break;
            }
            if (count == val.Digits.Count)
                temp_val.Digits.Add(0);
            else
            {
                int size = val.Digits.Count - count;
                int[] arr = new int[size];
                val.Digits.CopyTo(count, arr, 0, size);
                temp_val.Digits = new List<int>(arr);
            }

            return temp_val;
        }
        public BigInt Sub(BigInt val1, BigInt val2)
        {
            int res_capacity = Math.Max(val1.Digits.Count, val2.Digits.Count);
            BigInt result = new BigInt();
            result.Digits = new List<int>();
            int[] res = new int[res_capacity];
            int k;
            //check if two numbers' digits are equal
            if (val1.Digits.Count == val2.Digits.Count)
            {
                for (int i = val1.Digits.Count - 1, j = val2.Digits.Count - 1; i >= 0; i--)
                {
                    if (j == -1)
                    {
                        res[res_capacity - 1] = val1.Digits[i];
                        res_capacity--;

                    }
                    else
                    {
                        k = i - 1;
                        if (val1.Digits[i] < val2.Digits[j] && i != 0)
                        {
                            while (val1.Digits[k] == 0)
                            {
                                val1.Digits[k] = 9;
                                k--;
                            }
                            val1.Digits[k]--;
                            val1.Digits[i] += 10;
                        }
                        res[res_capacity - 1] = val1.Digits[i] - val2.Digits[j];
                        res_capacity--;
                        j--; 
                    }
                }
            }
            //check if two numbers' digits are not equal
            else
            {
                for (int i = val1.Digits.Count - 1, j = val2.Digits.Count - 1; i >= 0; i--)
                {
                    if (j == -1)
                    {
                        res[res_capacity - 1] = val1.Digits[i];
                        res_capacity--;
                    }
                    else
                    {
                        k = i-1;
                        if (val1.Digits[i] < val2.Digits[j] && i != 0)
                        {
                            while (val1.Digits[k] == 0)
                            {
                                val1.Digits[k] = 9;
                                k--;
                            }
                            val1.Digits[k]--;
                            val1.Digits[i] += 10;
                        }
                        res[res_capacity - 1] = val1.Digits[i] - val2.Digits[j];
                        res_capacity--;
                        j--;
                    }
                }
            }
            result.Digits = new List<int>(res);
            return left_Zeros(result);
        }
        public void sub_test(string input, string output)
        {
            string b1 = "", b2 = "";
            BigInt res = new BigInt();
            BigInt v1;
            BigInt v2;

            FileStream fs1 = new FileStream(input, FileMode.Open);
            FileStream fs2 = new FileStream(output, FileMode.Open);

            StreamReader SR = new StreamReader(fs1);
            StreamWriter SW = new StreamWriter(fs2);

            int t = int.Parse(SR.ReadLine());

            for (int i = 0; i < t; i++)
            {
                SR.ReadLine();
                b1 = SR.ReadLine();
                b2 = SR.ReadLine();

                v1 = new BigInt();
                v2 = new BigInt();

                v1.Fill_BigInt(b1);
                v2.Fill_BigInt(b2);
                res = res.Sub(v1, v2);
                res = res.left_Zeros(res);

                StringBuilder builder = new StringBuilder();
                foreach (int digit in res.Digits)
                {
                    builder.Append(digit);
                }
                string result = builder.ToString();


                if (i < t - 1)
                {
                    SW.WriteLine(result);
                    SW.WriteLine();
                }
                else
                    SW.WriteLine(result);
            }

            SR.Close();
            SW.Close();
            fs1.Close();
            fs2.Close();
        }

        public BigInt Add(BigInt val1, BigInt val2)
        {
            int n1 = val1.Digits.Count;
            int n2 = val2.Digits.Count;
            int carry = 0;
            int counter = 0;
            int size = 0;
            int[] greater, smaller;
            if (n1 < n2)
            {
                size = n2 + 1;
                greater = new int[n2];
                smaller = new int[n1];
                counter = n1;
                for (int i = 0; i < n2; i++)
                {
                    greater[i] = val2.Digits.ElementAt(i);
                }

                for (int i = 0; i < n1; i++)
                {
                    smaller[i] = val1.Digits.ElementAt(i);
                }
            }
            else
            {
                size = n1 + 1;
                greater = new int[n1];
                smaller = new int[n2];
                counter = n2;
                for (int i = 0; i < n1; i++)
                {
                    greater[i] = val1.Digits.ElementAt(i);
                }

                for (int i = 0; i < n2; i++)
                {
                    smaller[i] = val2.Digits.ElementAt(i);
                }
            }
            int[] Result = new int[size];

            int d = Math.Abs(n1 - n2);
            int sum = 0;
            for (int i = counter - 1; i >= 0; i--)
            {
                sum = greater[i + d] + smaller[i] + carry;
                carry = sum / 10;
                Result[i + d] = sum % 10;

            }
            for (int i = d - 1; i >= 0; i--)
            {
                sum = greater[i] + carry;
                carry = sum / 10;
                Result[i] = sum % 10;
            }
            BigInt FinalResult = new BigInt();

            if (carry != 0)
            {
                int[] temp = new int[size];
                temp[0] = carry;
                int x = 0;
                for (int i = 1; i < Result.Length; i++)
                {
                    temp[i] = Result[x];
                    x++;
                }
                FinalResult.Digits = new List<int>(temp);
            }
            else
            {
                int[] temp = new int[size - 1];
                for (int i = temp.Length - 1; i >= 0; i--)
                {
                    temp[i] = Result[i];
                }
                FinalResult.Digits = new List<int>(temp);
            }
            return left_Zeros(FinalResult);

        }

        public void add_test(string input, string output)
        {
            string b1 = "", b2 = "";
            BigInt res = new BigInt();
            BigInt v1;
            BigInt v2;

            FileStream fs1 = new FileStream(input, FileMode.Open);
            FileStream fs2 = new FileStream(output, FileMode.Open);

            StreamReader SR = new StreamReader(fs1);
            StreamWriter SW = new StreamWriter(fs2);

            int t = int.Parse(SR.ReadLine());

            for (int i = 0; i < t; i++)
            {
                SR.ReadLine();
                b1 = SR.ReadLine();
                b2 = SR.ReadLine();

                v1 = new BigInt();
                v2 = new BigInt();

                v1.Fill_BigInt(b1);
                v2.Fill_BigInt(b2);
                res = res.Add(v1, v2);
                res = res.left_Zeros(res);

                StringBuilder builder = new StringBuilder();
                foreach (int digit in res.Digits)
                {
                    builder.Append(digit);
                }
                string result = builder.ToString();


                if (i < t - 1)
                {
                    SW.WriteLine(result);
                    SW.WriteLine();
                }
                else
                    SW.WriteLine(result);
            }

            SR.Close();
            SW.Close();
            fs1.Close();
            fs2.Close();
        }

        public BigInt[] Div(BigInt val1, BigInt val2)
        {
            BigInt[] QandR = new BigInt[2];

            BigInt two = new BigInt();
            two.Digits.Add(2);

            BigInt one = new BigInt();
            one.Digits.Add(1);

            BigInt zero = new BigInt();
            zero.Digits.Add(0);

            if (checkBigger(val1, val2) == 0)
            {
                QandR[0] = new BigInt(zero);
                QandR[1] = new BigInt(val1);
                return QandR;
            }

            QandR = Div(val1, Add(val2, val2));
            QandR[0] = Add(QandR[0], QandR[0]);

            if (checkBigger(QandR[1], val2) == 0)
                return QandR;

            QandR[0] = Add(QandR[0], one);

            QandR[1] = Sub(QandR[1], val2);

            return QandR;
        }
        public BigInt Mul(BigInt value1, BigInt value2)
        {
            int[] val1, val2, firstvalue, secondvalue;
            int counter = 0;
            BigInt FirstMul, SecondMul, ThirdMul, FinalResult, TempResult, SubResult, AddResult, FinalSubResult;

            val1 = value1.Digits.ToArray();
            val2 = value2.Digits.ToArray();
            double N;
            int diff = Math.Abs(val1.Length - val2.Length);

            if (val1.Length < val2.Length)
            {
                N = val2.Length;
                int k = 0;
                val1 = new int[val1.Length + diff];
                for (; k < diff; k++)
                {
                    val1[k] = 0;
                }
                for (int j = 0; j < value1.Digits.Count; j++)
                {
                    val1[k] = value1.Digits[j];
                    k++;
                }
            }
            else if (val1.Length > val2.Length)
            {
                N = val1.Length;
                int k = 0;
                val2 = new int[val2.Length + diff];
                for (; k < diff; k++)
                {
                    val2[k] = 0;
                }
                for (int j = 0; j < value2.Digits.Count; j++)
                {
                    val2[k] = value2.Digits[j];
                    k++;
                }
            }
            else
            {
                N = val1.Length;
            }

            double N2 = Math.Floor(N/2);
            double N3 = N - N2;

            if (val2.Length <= 2 || val1.Length <= 2)
            {
                BigInt Result = new BigInt();
                double firstVal = 0, secondVal = 0, multiplied;

                for (int i = val1.Length - 1; i >= 0; i--)
                {
                    firstVal += val1[i] * Math.Pow(10, counter);
                    counter++;
                }
                counter = 0;
                for (int i = val2.Length - 1; i >= 0; i--)
                {
                    secondVal += val2[i] * Math.Pow(10, counter);
                    counter++;
                }
                multiplied = firstVal * secondVal;
                string mul = multiplied.ToString();
                Result.Fill_BigInt(mul);
                return Result;
            }

            int[] a = new int[Convert.ToInt32(N2)];
            int[] b = new int[Convert.ToInt32(N3)];
            int[] c = new int[Convert.ToInt32(N2)];
            int[] d = new int[Convert.ToInt32(N3)];
            int x = 0;
            int z = 0;
            for (; z < N2; z++)
            {
                a[x] = val1[z];
                x++;
            }
            x = 0;
            for (int j = z ; j < N; j++)
            {
                b[x] = val1[j];
                x++;
            }
            x = 0;
            z = 0;
            for (; z < N2; z++)
            {
                c[x] = val2[z];
                x++;
            }
            x = 0;
            for (int j = z; j < N; j++)
            { 
                d[x] = val2[j];
                x++;
            }

            BigInt FinalA = new BigInt();
            BigInt FinalB = new BigInt();
            BigInt FinalC = new BigInt();
            BigInt FinalD = new BigInt();

            FinalA.Digits = new List<int>(a);
            FinalB.Digits = new List<int>(b);
            FinalC.Digits = new List<int>(c);
            FinalD.Digits = new List<int>(d);

            BigInt AplusB = Add(FinalA, FinalB);
            BigInt CplusD = Add(FinalC, FinalD);

            FirstMul = Mul(FinalA, FinalC);

            SecondMul = Mul(FinalB, FinalD);

            ThirdMul = Mul(AplusB, CplusD);

            TempResult = Sub(ThirdMul, FirstMul);
            SubResult = Sub(TempResult, SecondMul);

            int count = Convert.ToInt32(N3 * 2), size = FirstMul.Digits.Count + count;

            int[] FirstMulF = new int[size];
            int[] SubResultF = new int[SubResult.Digits.Count + Convert.ToInt32(N3)];

            int y = 0;
            for (int i = 0; i < size; i++)
            {
                if (y < FirstMul.Digits.Count)
                {
                    FirstMulF[i] = FirstMul.Digits.ElementAt(y);
                    y++;
                }
                else
                    FirstMulF[i] = 0;
            }
            y = 0;
            for (int i = 0; i < SubResult.Digits.Count + Convert.ToInt32(N3); i++)
            {
                if (y < SubResult.Digits.Count)
                {
                    SubResultF[i] = SubResult.Digits.ElementAt(y);
                    y++;
                }
                else
                    SubResultF[i] = 0;
            }
            FirstMul.Digits = new List<int>(FirstMulF);
            SubResult.Digits = new List<int>(SubResultF);
            AddResult = Add(FirstMul, SecondMul);
            FinalResult = Add(SubResult, AddResult);

            return left_Zeros(FinalResult);
        }
        public void mul_test(string input, string output)
        {
            string b1 = "", b2 = "";
            BigInt res = new BigInt();
            BigInt v1;
            BigInt v2;

            FileStream fs1 = new FileStream(input, FileMode.Open);
            FileStream fs2 = new FileStream(output, FileMode.Open);

            StreamReader SR = new StreamReader(fs1);
            StreamWriter SW = new StreamWriter(fs2);

            int t = int.Parse(SR.ReadLine());

            for (int i = 0; i < t; i++)
            {
                SR.ReadLine();
                b1 = SR.ReadLine();
                b2 = SR.ReadLine();

                v1 = new BigInt();
                v2 = new BigInt();

                v1.Fill_BigInt(b1);
                v2.Fill_BigInt(b2);
                res = res.Mul(v1, v2);
                res = res.left_Zeros(res);

                StringBuilder builder = new StringBuilder();
                foreach (int digit in res.Digits)
                {
                    builder.Append(digit);
                }
                string result = builder.ToString();


                if (i < t - 1)
                {
                    SW.WriteLine(result);
                    SW.WriteLine();
                }
                else
                    SW.WriteLine(result);
            }

            SR.Close();
            SW.Close();
            fs1.Close();
            fs2.Close();
        }
        public BigInt ModOfPower(BigInt val, BigInt pow, BigInt M)
        {
            BigInt newpow = new BigInt();
            newpow = Power(val, pow, M);
            return Div(newpow, M).ElementAt(1);
        }
        public BigInt Power(BigInt val, BigInt pow, BigInt M)
        {
            BigInt two = new BigInt();
            two.Digits.Add(2);

            BigInt one = new BigInt();
            one.Digits.Add(1);

            BigInt zero = new BigInt();
            zero.Digits.Add(0);

            BigInt mayar = new BigInt();
            BigInt mai = new BigInt();
            BigInt Haidy = new BigInt();
            BigInt mod = new BigInt();

            if (pow.Digits.Count == 1 && pow.Digits[0] == 0)
                return one;

            else if (checkBigger(Div(pow, two).ElementAt(1), zero) == 2)
            {
                mod = Power(val, Div(pow, two).ElementAt(0), M);
                return Div(Mul(mod, mod), M).ElementAt(1);
            }
            else
            {
                mayar = Div(val, M).ElementAt(1);
                mai = Power(val, Sub(pow, one), M);
                //StringBuilder builder = new StringBuilder();
                //foreach (int digit in mai.Digits)
                //{
                //    builder.Append(digit);
                //}
                //string maistr = builder.ToString();

                //builder = new StringBuilder();
                //foreach (int digit in mayar.Digits)
                //{
                //    builder.Append(digit);
                //}
                //string mayarstr = builder.ToString();

                Haidy = Div(Mul(mayar, mai), M).ElementAt(1);
                return Haidy;
            }
        }
        private int checkBigger(BigInt val1, BigInt val2)
        {
            val1 = left_Zeros(val1);
            val2 = left_Zeros(val2);

            if (val1.Digits.Count > val2.Digits.Count)
            {
                return 1;
            }
            else if (val1.Digits.Count == val2.Digits.Count)
            {
                for (int i = 0; i < val1.Digits.Count; i++)
                {
                    if (val1.Digits.ElementAt(i) > val2.Digits.ElementAt(i))
                        return 1;
                    else if (val1.Digits.ElementAt(i) < val2.Digits.ElementAt(i))
                        return 0;
                }
            }
            else if (val1.Digits.Count < val2.Digits.Count)
                return 0;

            return 2;
        }


        private BigInt Encrypt(BigInt E, BigInt M, BigInt N)
        {
            BigInt Result = new BigInt();
            Result = Power(E, M, N);
            return Result;
        }
        private BigInt Decrypt(BigInt EofM, BigInt D, BigInt N)
        {
            BigInt Result = new BigInt();
            Result = Power(EofM, D, N);
            return Result;
        }

        public void proj_test()
        {
            string b1 = "", b2 = "", b3 = "";
            BigInt N;
            BigInt E_D;
            BigInt M;
            BigInt N_1;
            BigInt E_D_1;
            BigInt M_1;
            int enc_dec = 0;
            int enc_dec_1 = 0;
            BigInt enc;
            BigInt dec;

            FileStream samp_test = new FileStream("SampleRSA_II\\SampleRSA.txt", FileMode.Open);
            FileStream samp_output = new FileStream("SampleRSA_II\\MyOutput.txt", FileMode.Open);

            FileStream comp_test = new FileStream("Complete Test\\TestRSA.txt", FileMode.Open);
            FileStream comp_output = new FileStream("Complete Test\\MyOutput.txt", FileMode.Open);


            StreamReader samp_r = new StreamReader(samp_test);
            StreamWriter samp_w = new StreamWriter(samp_output);

            StreamReader comp_r = new StreamReader(comp_test);
            StreamWriter comp_w = new StreamWriter(comp_output);

            int t = int.Parse(samp_r.ReadLine());
            for (int i = 0; i < t; i++)
            {
                N = new BigInt();
                E_D = new BigInt();
                M = new BigInt();

                enc = new BigInt();
                dec = new BigInt();

                b1 = samp_r.ReadLine();
                b2 = samp_r.ReadLine();
                b3 = samp_r.ReadLine();
                enc_dec = int.Parse(samp_r.ReadLine());

                N.Fill_BigInt(b1);
                M.Fill_BigInt(b2);
                E_D.Fill_BigInt(b3);
                if (enc_dec == 0)
                {
                    enc = enc.Encrypt(E_D, M, N);
                    StringBuilder builder = new StringBuilder();
                    foreach (int digit in enc.Digits)
                    {
                        builder.Append(digit);
                    }
                    string result = builder.ToString();
                    samp_w.WriteLine(result);
                }
                else if (enc_dec == 1)
                {
                    enc = enc.Decrypt(E_D, M, N);
                    StringBuilder builder = new StringBuilder();
                    foreach (int digit in enc.Digits)
                    {
                        builder.Append(digit);
                    }
                    string result = builder.ToString();
                    samp_w.WriteLine(result);
                }

            }

            samp_r.Close();
            samp_w.Close();
            samp_test.Close();
            samp_output.Close();

            Console.WriteLine("Do You Want To Continue Till The Complete Test(y/n)?");
            string y_n = Console.ReadLine();
            int total = 0;
            if (y_n == "y")
            {
                t = int.Parse(comp_r.ReadLine());
                for (int i = 0; i < t / 2; i++)
                {

                    N = new BigInt();
                    E_D = new BigInt();
                    M = new BigInt();

                    N_1 = new BigInt();
                    E_D_1 = new BigInt();
                    M_1 = new BigInt();

                    enc = new BigInt();
                    dec = new BigInt();

                    b1 = comp_r.ReadLine();
                    b2 = comp_r.ReadLine();
                    b3 = comp_r.ReadLine();
                    enc_dec = int.Parse(comp_r.ReadLine());

                    N.Fill_BigInt(b1);
                    M.Fill_BigInt(b2);
                    E_D.Fill_BigInt(b3);

                    b1 = comp_r.ReadLine();
                    b2 = comp_r.ReadLine();
                    b3 = comp_r.ReadLine();
                    enc_dec_1 = int.Parse(comp_r.ReadLine());

                    N_1.Fill_BigInt(b1);
                    M_1.Fill_BigInt(b2);
                    E_D_1.Fill_BigInt(b3);

                    int Before = System.Environment.TickCount;
                    enc = enc.Encrypt(E_D, M, N);
                    dec = dec.Decrypt(E_D_1, M_1, N_1);
                    int After = System.Environment.TickCount;
                    total = After - Before;

                    Console.WriteLine("Time for Case " + (i + 1) + ": " + total + " milliseconds");

                    StringBuilder builder = new StringBuilder();
                    foreach (int digit in enc.Digits)
                    {
                        builder.Append(digit);
                    }
                    string result = builder.ToString();
                    comp_w.WriteLine(result);

                    builder = new StringBuilder();
                    foreach (int digit in dec.Digits)
                    {
                        builder.Append(digit);
                    }
                    result = builder.ToString();
                    comp_w.WriteLine(result);
                }


            }

            comp_r.Close();
            comp_w.Close();
            comp_test.Close();
            comp_output.Close();

        }
        public BigInt[] Genarate(int size)
        {
            BigInt[] e_n = new BigInt[2];
            int[] p = new int[size];
            int[] q = new int[size];

            BigInt one = new BigInt();
            one.Digits.Add(1);

            BigInt phi = new BigInt();
            Random r = new Random();

            for (int i = 0; i < size; i++)
            {
                if (i == size - 1)
                {
                    p[i] = r.Next(size) - 1;
                    q[i] = r.Next(size) - 1;
                }
                else
                {
                    p[i] = r.Next(size);
                    q[i] = r.Next(size);
                }
            }
            BigInt P = new BigInt();
            P.Digits = new List<int>(p);
            BigInt Q = new BigInt();
            Q.Digits = new List<int>(q);

            phi = Mul(Sub(P, one), Sub(Q, one));
            e_n[0] = Mul(P, Q);

            return e_n;
        }
    }
}

