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


            return new CesarAlgorithmOutput
            {
                Result =
                    array.Select(
                            ch => ch - 'A')
                         .Select(
                            nn => (nn - 3)%26)
                         .Aggregate(
                            String.Empty,
                            (current, nn) => current + (char) (nn + 'A'))
            };
        }

        private static CesarAlgorithmOutput Encrypt(CesarAlgorithmInput input)
        {
            var ttext = input.Input.ToUpper();

            var array = ttext.ToCharArray();


            return new CesarAlgorithmOutput
            {
                Result =
                    array.Select(
                            ch => ch - 'A')
                         .Select(
                            nn => (nn + 3) % 26)
                         .Aggregate(
                            String.Empty,
                            (current, nn) => current + (char)(nn + 'A'))
            };
        }
    }
}