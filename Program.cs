using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RSA_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            BigInt p = new BigInt();
            p.proj_test();

            //while (true)
            //{

            //    BigInt Result = new BigInt();
            //    Result[0] = new BigInt();
            //    Result[1] = new BigInt();
            //    BigInt Val1 = new BigInt();
            //    BigInt Val2 = new BigInt();
            //    BigInt Val3 = new BigInt();
            //    string S1, S2, S3;
            //    S1 = Console.ReadLine();
            //    S2 = Console.ReadLine();
            //    S3 = Console.ReadLine();
            //    Val1.Fill_BigInt(S1);
            //    Val2.Fill_BigInt(S2);
            //    Val3.Fill_BigInt(S3);



            //    string x = "";
            //    x = Console.ReadLine();
            //    BigInt b = new BigInt();
            //    b.ConvertToBigint(x);

            //    Result = Result.ModOfPower(Val1, Val2, Val3);

            //    Console.Write("mod of power:");
            //    for (int i = 0; i < Result.Digits.Count; i++)
            //    {
            //        Console.Write(Convert.ToString(Result.Digits.ElementAt(i)));
            //    }
            //    Console.WriteLine();


            //    x = Result.ConvertTOString(Result);
            //    Console.WriteLine(x);

            //    string sub_input = "SubtractTestCases.txt"; o ??? τ
            //     string sub_output = "SubOutput.txt";
            //    BigInt result = new BigInt();
            //    result.sub_test(sub_input, sub_output);

            //    string Add_input = "AddTestCases.txt";
            //    string Add_output = "AddOutput.txt";
            //    result.add_test(Add_input, Add_output);

            //    string Mul_input = "MultiplyTestCases.txt";
            //    string Mul_output = "MulOutput.txt";
            //    result.mul_test(Mul_input, Mul_output);

            //    string Div_input = "DivisionTestCases.txt";
            //    string Div_output = "DivOutput.txt";
            //    result.div_test(Div_input, Div_output);
            //}
        }
    }
}
