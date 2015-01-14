using System;
using System.Linq;
using System.Net.Security;

namespace Ext.CryptoMachine.Cryptography.Cesar
{
    public class CesarCryptoAlgorithm : BaseCryptoAlgorithm<CesarAlgorithmInput, CesarAlgorithmOutput>, ICryptoAlgorithm
    {
        public IAlgorithmOutput Process(IAlgorithmInput input)
        {
            return ProcessOut(input as CesarAlgorithmInput);
        }

        public override CesarAlgorithmOutput ProcessOut(CesarAlgorithmInput input)
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

        private static CesarAlgorithmOutput Decrypt(CesarAlgorithmInput input)
        {
            var ttext = input.Input.ToUpper();

            var array = ttext.ToCharArray();

            var result =
                    array
                        .Select(
                            ch => ch - 'A')
                        .Select(
                            nn => (nn - input.Move)%26)
                        .Aggregate(
                            String.Empty,
                            (current, nn) => current + (char) (nn + 'A')) + Environment.NewLine;

            return new CesarAlgorithmOutput
            {
                Result = result
            };
        }

        private static CesarAlgorithmOutput Encrypt(CesarAlgorithmInput input)
        {
            var ttext = input.Input.ToUpper();

            var array = ttext.ToCharArray();

            var result = "decrypt" + Environment.NewLine;

            result +=
                    array
                        .Select(
                            ch => ch - 'A')
                        .Select(
                            nn => (nn + input.Move)%26)
                        .Aggregate(
                            String.Empty,
                            (current, nn) => current + (char) (nn + 'A')) + Environment.NewLine;

            result += input.Move;

            return new CesarAlgorithmOutput
            {
                Result = result
                    
            };
        }
    }
}