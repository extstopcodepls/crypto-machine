using System;
using System.Linq;

namespace Ext.CryptoMachine.Cryptography.Midnight
{
    public class MidnightCryptoAlgorithm : BaseCryptoAlgorithm<MidnightAlgorithmInput, MidnightAlgorithmOutput>, ICryptoAlgorithm
    {
        public IAlgorithmOutput Process(IAlgorithmInput input)
        {
            return ProcessOut(input as MidnightAlgorithmInput);
        }

        public override MidnightAlgorithmOutput ProcessOut(MidnightAlgorithmInput input)
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

        private static MidnightAlgorithmOutput Decrypt(MidnightAlgorithmInput input)
        {
            var ttext = input.Input.ToUpper();

            var array = ttext.ToCharArray().Reverse();

            var i = input.Move;

            var result = String.Empty;

            foreach (var textChar in array)
            {
                var ch = textChar - 'A';
                var nn = Math.Abs((ch - i) % 26);
                result += (char) (nn + 'A');
                i--;
            }

            result = result.Reverse().Aggregate(String.Empty, (s, c) => s += c);

            return new MidnightAlgorithmOutput
            {
                Result = result
            };
        }

        private static MidnightAlgorithmOutput Encrypt(MidnightAlgorithmInput input)
        {
            var ttext = input.Input.ToUpper();

            var array = ttext.ToCharArray();

            var result = "decrypt" + Environment.NewLine;

            var i = input.Move;

            foreach (var textChar in array)
            {
                var ch = textChar - 'A';
                var nn = (ch + i) % 26;
                result += (char)(nn + 'A');
                i++;
            }

            result += Environment.NewLine + (i - 1);

            return new MidnightAlgorithmOutput
            {
                Result = result
            };
        }
    }
}