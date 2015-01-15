using System;
using System.Linq;

namespace Ext.CryptoMachine.Cryptography.NipValidatior
{
    public class NipCryptoAlgorithm : BaseCryptoAlgorithm<NipAlgorithmInput, NipAlgorithmOutput>, ICryptoAlgorithm
    {
        public IAlgorithmOutput Process(IAlgorithmInput input)
        {
            return ProcessOut(input as NipAlgorithmInput);
        }

        public override NipAlgorithmOutput ProcessOut(NipAlgorithmInput input)
        {
            var nip = input.Input;

            if (nip == null)
                return new NipAlgorithmOutput {Result = "Błąd wyjścia"};

            var array =
                nip
                    .Select(
                        c =>
                            Int32.Parse(c.ToString()))
                    .ToList();


            if (array.Count != 10)
                return new NipAlgorithmOutput {Result = "Nie poprawne wejście "};

            var wagi = new[] {6, 5, 7, 2, 3, 4, 5, 6, 7, 0};
            var suma =
                    array
                        .Zip(
                            wagi,
                            (cyfra, waga) =>
                                cyfra*waga)
                        .Sum();

            return new NipAlgorithmOutput
            {
                Result = String.Format("{0}", ((suma%11) == (array[9])) ? "Nip poprawny" : "Nip nie poprawny")
            };
        }
    }
}