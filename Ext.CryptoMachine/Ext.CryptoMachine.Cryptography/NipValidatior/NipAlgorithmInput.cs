using System;
using System.Collections.Generic;
using System.Linq;
using Ext.CryptoMachine.Cryptography.Cesar;

namespace Ext.CryptoMachine.Cryptography.NipValidatior
{
    public class NipAlgorithmInput : IAlgorithmInput
    {
        public String Input { get; set; }

        public static NipAlgorithmInput GetInput(IEnumerable<String> fileContent)
        {
            var data = fileContent as string[] ?? fileContent.ToArray();
            var content = String.Join(String.Empty, data[0].Split('-'));

            return new NipAlgorithmInput
            {
                Input = content
            };
        }
    }
}