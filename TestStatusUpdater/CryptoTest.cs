using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatusUpdater.Tools;

namespace TestStatusUpdater
{
    [TestClass]
    public class CryptoTest
    {
        [TestMethod]
        public void CryptAndDecrypt()
        {
            const string encryptionPassword = "supersecret";
            string textToEncrypt = "Test decryp";
            var stringEncryption = Crypto.Encrypt(textToEncrypt, encryptionPassword);
            var stringDecrypt = Crypto.Decrypt(stringEncryption, encryptionPassword);
            Assert.AreEqual(textToEncrypt, stringDecrypt);
        }
    }
}
