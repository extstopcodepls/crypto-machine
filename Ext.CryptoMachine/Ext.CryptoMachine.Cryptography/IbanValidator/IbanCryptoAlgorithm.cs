using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;

namespace Ext.CryptoMachine.Cryptography.IbanValidator
{
    public class IbanCryptoAlgorithm : BaseCryptoAlgorithm<IbanAlgorithmInput, IbanAlgorithmOutput>, ICryptoAlgorithm
    {
        public IAlgorithmOutput Process(IAlgorithmInput input)
        {
            return ProcessOut(input as IbanAlgorithmInput);
        }

        public override IbanAlgorithmOutput ProcessOut(IbanAlgorithmInput input)
        {
            var data = input.Input;

            var firstLetter = data[0];
            var secondLetter = data[1];

            data = new string(data.Skip(2).ToArray());

            var controlSum = data.Take(2);

            data = new string(data.Skip(2).ToArray());

            var mappedAlphabet = AlphabetMapper.GetMappedAlphabet();

            var fstMappedLetter = mappedAlphabet.MappedLetters.Single(l => l.Letter[0] == firstLetter);
            var sndMappedNumber = mappedAlphabet.MappedLetters.Single(l => l.Letter[0] == secondLetter);

            if (fstMappedLetter == null || sndMappedNumber == null)
                return new IbanAlgorithmOutput {Result = "Błąd podczas mapowania liter."};

            var fstNumber = fstMappedLetter.Mapping;
            var sndNumber = sndMappedNumber.Mapping;

            data += fstNumber;
            data += sndNumber;
            data += new string(controlSum.ToArray());

            var number = BigInteger.Parse(data);

            var result = number % 97;


            return new IbanAlgorithmOutput
            {
                Result = String.Format("Numer Iban ({1}): {0}", result == 1 ? "poprawny" : "nie poprawny", input.Input)
            };
        }
    }

    public class AlphabetMapper
    {
        public static MappedAlphabet GetMappedAlphabet()
        {
            var alphabet = Enumerable.Range('A', 'Z');

            var mappedAlphabet = 
                    alphabet
                        .Select(
                            s => 
                                new MappedLetter(
                                        ((char)s)
                                            .ToString(CultureInfo.InvariantCulture), 
                                        s - 55));
             

            return new MappedAlphabet(mappedAlphabet);
        }
    }

    public class MappedAlphabet
    {

        public MappedAlphabet(IEnumerable<MappedLetter> mappedAlphabet)
        {
            MappedLetters = mappedAlphabet;
        }

        public IEnumerable<MappedLetter> MappedLetters { get; private set; }
    }

    public class MappedLetter
    {
        public MappedLetter(
            String letter,
            int mapping)
        {
            Letter = letter;
            Mapping = mapping;
        }

        public String Letter { get; private set; }
        public int Mapping { get; private set; }
    }
}