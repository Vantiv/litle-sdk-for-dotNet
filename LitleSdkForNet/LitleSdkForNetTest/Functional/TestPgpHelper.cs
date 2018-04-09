using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Litle.Sdk;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    public class TestPgpHelper
    {
        private string _testDir;
        private string _merchantPublickeyId;
        private string _passphrase;
        private string _vantivPublicKeyId;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _testDir = Path.Combine(Properties.Settings.Default.requestDirectory, "testPgp");
            if (!Directory.Exists(_testDir))
            {
                Directory.CreateDirectory(_testDir);
            }
            _merchantPublickeyId = Environment.GetEnvironmentVariable("merchantPublicKeyId");
            Console.WriteLine("Merchant Public Key ID:" + _merchantPublickeyId);
            _passphrase = Environment.GetEnvironmentVariable("pgpPassphrase");
            Console.WriteLine("Pgp Passphrase:" + _passphrase);
            _vantivPublicKeyId = Environment.GetEnvironmentVariable("vantivPublicKeyId");
            Console.WriteLine(("Vantiv Public Key ID:" + _vantivPublicKeyId));
        }

        [Test]
        public void TestEncryptionDecryption()
        {
            var testFilepath = Path.Combine(_testDir, "test_pgp.txt");
            DeleteFile(testFilepath);

            string[] lines = {"The purpose of this file is to test 'PgpHelper.EncryptFile and PgpHelper.DecryptFile' methods", 
                "Second Line", 
                "Third Line", 
                "!@#$%^&*()_+-=[]{}/><;':~"};
            
            File.WriteAllLines(testFilepath, lines);
            
            //Encrypt
            var encryptedFilePath = testFilepath.Replace(".txt", ".asc");
            DeleteFile(encryptedFilePath);
            PgpHelper.EncryptFile(testFilepath, encryptedFilePath, _merchantPublickeyId);
            
            // Check if encrypted file is created
            var entries = Directory.EnumerateFiles(_testDir);
            Assert.True(entries.Contains(encryptedFilePath));
            
            //Decrypt
            var decryptedFilePath = Path.Combine(_testDir, "test_pgp_decrypted.txt");
            PgpHelper.DecryptFile(encryptedFilePath, decryptedFilePath, _passphrase);
            
            // Check if decrypted file is created
            entries = Directory.EnumerateFiles(_testDir);
            Assert.True(entries.Contains(decryptedFilePath));
            
            // Compare decrypted file with original file
            string[] original = File.ReadAllLines(testFilepath);
            string[] decrypted = File.ReadAllLines(decryptedFilePath);
            Assert.AreEqual(original, decrypted);
        }


        [Test]
        public void TestInvalidPublicKeyId()
        {
            var testFilepath = Path.Combine(_testDir, "test_pgp.txt");
            DeleteFile(testFilepath);

            string[] lines = {"The purpose of this file is to test 'PgpHelper.EncryptFile and PgpHelper.DecryptFile' methods", 
                "Second Line", 
                "Third Line", 
                "!@#$%^&*()_+-=[]{}/><;':~"};
            
            File.WriteAllLines(testFilepath, lines);
            
            var encryptedFilePath = testFilepath.Replace(".txt", ".asc");
            DeleteFile(encryptedFilePath);
            try
            {
                PgpHelper.EncryptFile(testFilepath, encryptedFilePath, "BadPublicKeyId");
                Assert.Fail("LitleOnline exception expected but was not thrown");
            }
            catch (LitleOnlineException e)
            {
                Assert.True(e.Message.Contains("Please make sure that the recipient Key ID is correct and is added to your gpg keyring"));
            }
        }

        [Test]
        public void TestNonExistantFileToEncrypt()
        {
            var testFilepath = "bad_file_path";
            var encryptedFilePath = Path.Combine(_testDir, "test_pgp.asc");
            try
            {
                PgpHelper.EncryptFile(testFilepath, encryptedFilePath, _merchantPublickeyId);
                Assert.Fail("LitleOnline exception expected but was not thrown");
            }
            catch (LitleOnlineException e)
            {
                Assert.True(e.Message.Contains("Please make sure the input file exists and has read permission."));
            }
        }

        [Test]
        public void TestInvalidPassphrase()
        {
            var testFilepath = Path.Combine(_testDir, "test_pgp.txt");
            DeleteFile(testFilepath);

            string[] lines = {"The purpose of this file is to test 'PgpHelper.EncryptFile and PgpHelper.DecryptFile' methods", 
                "Second Line", 
                "Third Line", 
                "!@#$%^&*()_+-=[]{}/><;':~"};
            
            File.WriteAllLines(testFilepath, lines);
            
            //Encrypt
            var encryptedFilePath = testFilepath.Replace(".txt", ".asc");
            DeleteFile(encryptedFilePath);
            PgpHelper.EncryptFile(testFilepath, encryptedFilePath, _merchantPublickeyId);
            
            var decryptedFilePath = Path.Combine(_testDir, "test_pgp_decrypted.txt");
            try
            {
                PgpHelper.DecryptFile(encryptedFilePath, decryptedFilePath, "bad_passphrase");
                Assert.Fail("LitleOnline exception expected but was not thrown");
            }
            catch (LitleOnlineException e)
            {
                Console.WriteLine(e.Message);
                Assert.True(e.Message.Contains("Please make sure that the passphrase is correct."));
            }            
        }

        [Test]
        public void TestNoSecretKeyToDecrypt()
        {
            var testFilepath = Path.Combine(_testDir, "test_pgp.txt");
            DeleteFile(testFilepath);

            string[] lines = {"The purpose of this file is to test 'PgpHelper.EncryptFile and PgpHelper.DecryptFile' methods", 
                "Second Line", 
                "Third Line", 
                "!@#$%^&*()_+-=[]{}/><;':~"};
            
            File.WriteAllLines(testFilepath, lines);
            
            //Encrypt
            var encryptedFilePath = testFilepath.Replace(".txt", ".asc");
            DeleteFile(encryptedFilePath);
            PgpHelper.EncryptFile(testFilepath, encryptedFilePath, _vantivPublicKeyId);
            
            var decryptedFilePath = Path.Combine(_testDir, "test_pgp_decrypted.txt");
            try
            {
                PgpHelper.DecryptFile(encryptedFilePath, decryptedFilePath, _passphrase);
                Assert.Fail("LitleOnline exception expected but was not thrown");
            }
            catch (LitleOnlineException e)
            {
                Assert.True(e.Message.Contains("Please make sure that your merchant secret key is added to your gpg keyring."));
            }         
        }

        [Test]
        public void TestNonExistantFileToDecrypt()
        {
            var encryptedFilePath = "bad_file_path";
            var decryptedFilePath = Path.Combine(_testDir, "test_pgp_decrypted.txt");
            try
            {
                PgpHelper.DecryptFile(encryptedFilePath, decryptedFilePath, _passphrase);
                Assert.Fail("LitleOnline exception expected but was not thrown");
            }
            catch (LitleOnlineException e)
            {
                Assert.True(e.Message.Contains("Please make sure the input file exists and has read permission."));
            }
        }

        private static void DeleteFile(string filepath)
        {
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
        }
    }
}