using System;
using System.Collections.Generic;
using System.Linq;

namespace Ext.CryptoMachine.Cryptography.IbanValidator
{
    public class IbanAlgorithmInput : IAlgorithmInput
    {
        public String Input { get; set; }

        public static IbanAlgorithmInput GetInput(IEnumerable<String> fileContent)
        {
            var data = fileContent as string[] ?? fileContent.ToArray();
            var content = data[0];

            return new IbanAlgorithmInput
            {
                Input = content
            };
        }
    }
}