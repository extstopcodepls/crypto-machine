using System;
using System.Collections.Generic;
using System.Linq;

namespace Ext.CryptoMachine.Cryptography.Vinegre
{
    public class VinegreCryptoAlgorithm : BaseCryptoAlgorithm<VinegreAlgorithmInput, VinegreAlgorithmOutput>, ICryptoAlgorithm
    {
        private static readonly char[,] Alphabets = new char[26, 26];

        public VinegreCryptoAlgorithm()
        {
            GenerateAlphabets();
        }

        private static void GenerateAlphabets()
        {
            var range = Enumerable.Range('A', 'Z' - 'A' + 1).Select(c => (char)c).ToList();
            for (var i = 0; i < Alphabets.GetLength(1); i++)
            {
                for (var j = 0; j < range.Count; j++)
                {
                    var ch = range[j] - 'A';
                    var nn = (ch + i)%26;
                    Alphabets[i, j] = (char)(nn + 'A');
                }
            }
        }


        public IAlgorithmOutput Process(IAlgorithmInput input)
        {
            return ProcessOut(input as VinegreAlgorithmInput);
        }

        public override VinegreAlgorithmOutput ProcessOut(VinegreAlgorithmInput input)
        {
            switch (input.Operation)
            {
                case Operation.Encrypt:
                    return Encrypt(input);
                case Operation.Decrypt:
                    return Decrypt(input);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static VinegreAlgorithmOutput Encrypt(VinegreAlgorithmInput input)
        {
            var ttext = input.Input.ToUpper().ToList();

            ttext.RemoveAll(Char.IsWhiteSpace);

            var array = ttext;
            var password =
                    input
                        .Password
                        .ToUpper()
                        .ToList()
                        .Take(ttext.Count);

            var xIndex = 0;
            var yIndex = 0;

            var charPass = array.Zip(password, (text, pass) => new {Text = text, Pass = pass});

            var result = "decrypt" + Environment.NewLine;

            foreach (var ch in charPass)
            {
                for (var i = 0; i < Alphabets.GetLength(1); i++)
                {
                    if (ch.Text != Alphabets[0, i]) continue;
                    
                    xIndex = i;
                    break;
                }

                for (var i = 0; i < Alphabets.GetLength(1); i++)
                {
                    if (ch.Pass != Alphabets[i, 0]) continue;
                    
                    yIndex = i;
                    break;
                }

                result += Alphabets[xIndex, yIndex];
            }

            result += Environment.NewLine + input.Password;

            return new VinegreAlgorithmOutput
            {
                Result = result
            };
        }


        private static VinegreAlgorithmOutput Decrypt(VinegreAlgorithmInput input)
        {
            var ttext = input.Input.ToUpper().ToList();

            ttext.RemoveAll(Char.IsWhiteSpace);

            var array = ttext;

            var password =
                    input
                        .Password
                        .ToUpper()
                        .Take(ttext.Count)
                        .ToList();

            var invertedPass = new List<char>();

            foreach (var t in password)
            {
                var ch = t - 'A';
                var nn = (char)((26 - ch) % 26);
                invertedPass.Add((char)(nn + 'A'));
            }

            var xIndex = 0;
            var yIndex = 0;

            var charPass = array.Zip(invertedPass, (text, pass) => new { Text = text, Pass = pass });

            var result = String.Empty;

            foreach (var ch in charPass)
            {
                for (var i = 0; i < Alphabets.GetLength(1); i++)
                {
                    if (ch.Text != Alphabets[0, i]) continue;

                    xIndex = i;
                    break;
                }

                for (var i = 0; i < Alphabets.GetLength(1); i++)
                {
                    if (ch.Pass != Alphabets[i, 0]) continue;

                    yIndex = i;
                    break;
                }

                result += Alphabets[xIndex, yIndex];
            }

            result += Environment.NewLine + input.Password;

            return new VinegreAlgorithmOutput
            {
                Result = result
            };
        }
    }
}