using System;
using System.Collections.Generic;
using System.Linq;

namespace Ext.CryptoMachine.Cryptography.Vinegre
{
    public class VinegreAlgorithmInput : IAlgorithmInput
    {
        public Operation Operation { get; set; }
        public String Input { get; set; }
        public String Password { get; set; }

        public static VinegreAlgorithmInput GetInput(IEnumerable<String> fileContent)
        {
            var data = fileContent as string[] ?? fileContent.ToArray();

            var operation = OperationProcess(data[0]);

            var content = data[1];

            var password = data[2];

            return new VinegreAlgorithmInput
            {
                Operation = operation,
                Input = content,
                Password = password
            };
        }

        private static Operation OperationProcess(String data)
        {
            switch (data)
            {
                case "encrypt":
                    return Operation.Encrypt;
                case "decrypt":
                    return Operation.Decrypt;
                default:
                    return Operation.Encrypt;
            }
        }
    }
}