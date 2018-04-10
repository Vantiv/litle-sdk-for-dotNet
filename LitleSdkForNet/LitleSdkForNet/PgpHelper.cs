using System;
using System.Diagnostics;
using System.IO;


namespace Litle.Sdk
{
    struct ProcResult
    {
        public string output;
        public string error;
        public int status;
    }
    
    
    public class PgpHelper
    {
        private const int Success = 0;
        private static string GpgPath = Properties.Settings.Default.gnuPgDir;
        private const string GpgExecutable = "gpg.exe";
        private const string GpgConfExecutable = "gpgconf.exe";
        
        public static void EncryptFile(string inputFileName, string outputFileName, string recipientKeyId)
        {
            const string commandFormat = @"--yes --batch --encrypt --trust-model always --output {0}  --armour --recipient {1} {2}";

            var procResult = ExecuteCommandSync(string.Format(commandFormat, outputFileName, recipientKeyId, inputFileName), GpgExecutable);
            if (procResult.status != Success)
            {
                if (procResult.error.ToLower().Contains("no public key"))
                {
                    throw new LitleOnlineException("Please make sure that the recipient Key ID is correct and is added to your gpg keyring.\n" + procResult.error);
                }
                
                else if (procResult.error.Contains(string.Format("can't open '{0}'", inputFileName)))
                {
                    throw new LitleOnlineException("Please make sure the input file exists and has read permission.\n" + procResult.error);
                }

                else
                {
                    throw new LitleOnlineException(procResult.error);
                }
            }
            
            Console.WriteLine("Encrypted with public key " + recipientKeyId);
        }

        public static void DecryptFile(string inputFileName, string outputFileName, string passphrase)
        {
            string commandFormat = @"--kill gpg-agent";
            var result = ExecuteCommandSync(commandFormat, GpgConfExecutable);
            Console.WriteLine("Status: " + result.status);
            Console.WriteLine("Output: " + result.output);
            Console.WriteLine("Error: " + result.error);
            
            commandFormat = @"--passphrase-fd 0 --batch --trust-model always --pinentry-mode loopback --output {0} --decrypt {1}";
            if (File.Exists(outputFileName))
            {
                File.Delete(outputFileName);
            }
            var procResult = ExecuteCommandSyncWithPassphrase(string.Format(commandFormat, outputFileName, inputFileName), passphrase);
            if (procResult.status != Success)
            {
                if (procResult.error.ToLower().Contains("gpg: public key decryption failed: bad passphrase"))
                {
                    throw new LitleOnlineException("Please make sure that the passphrase is correct.\n" + procResult.error);
                }
                
                else if (procResult.error.ToLower().Contains("gpg: decryption failed: no secret key"))
                {
                    throw new LitleOnlineException("Please make sure that your merchant secret key is added to your gpg keyring.\n" + procResult.error);
                }
                
                else if (procResult.error.Contains(string.Format("can't open '{0}'", inputFileName)))
                {
                    throw new LitleOnlineException("Please make sure the input file exists and has read permission.\n" + procResult.error);
                }

                else
                {
                    throw new LitleOnlineException(procResult.error);
                }
            }
        }

        public static string ImportPrivateKey(string keyFilePath, string passphrase)
        {
            const string commandFormat = @"--import --passphrase-fd 0 --pinentry-mode loopback {0}";
            
            var procResult = ExecuteCommandSyncWithPassphrase(string.Format(commandFormat, keyFilePath), passphrase);
            if (procResult.status != Success)
            {
                throw new LitleOnlineException(procResult.error);
            }

            return ExtractKeyId(procResult.error);
        }
        
        public static string ImportPublicKey(string keyFilePath)
        {
            const string commandFormat = @"--import {0}";
            
            var procResult = ExecuteCommandSync(string.Format(commandFormat, keyFilePath), GpgExecutable);
            if (procResult.status != Success)
            {
                throw new LitleOnlineException(procResult.error);
            }

            return ExtractKeyId(procResult.error);
        }
        

        private static ProcResult ExecuteCommandSyncWithPassphrase(string command, string passphrase)
        {
            string path = string.Format(@"{0}\" + GpgExecutable, GpgPath);

            var procStartInfo = new ProcessStartInfo(path, command)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true
            };

            var proc = new Process { StartInfo = procStartInfo };
            proc.Start();
            proc.StandardInput.WriteLine(passphrase);
            proc.StandardInput.Flush();

            return new ProcResult
            {
                output = proc.StandardOutput.ReadToEnd(),
                error = proc.StandardError.ReadToEnd(),
                status = proc.ExitCode
            };
        }


        private static ProcResult ExecuteCommandSync(string command, string executable) 
        {
            string path = string.Format(@"{0}\" + executable, GpgPath);
            var procStartInfo = new ProcessStartInfo(path, command)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true
            };

            var proc = new Process { StartInfo = procStartInfo };
            proc.Start();
            proc.StandardInput.Flush();
            
            return new ProcResult
            {
                output = proc.StandardOutput.ReadToEnd(),
                error = proc.StandardError.ReadToEnd(),
                status = proc.ExitCode
            };
        }

        private static string ExtractKeyId(string result)
        {
            return result.Split(':')[1].Split(' ')[2].Substring(8);
        }
    }
}

