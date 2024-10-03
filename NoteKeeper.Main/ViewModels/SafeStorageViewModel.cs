using NoteKeeper.Main.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NoteKeeper.Main.ViewModels
{
    public class SafeStorageViewModel : BindableBase
    {
        private const string FileName = "notes.dat";
        private const int KeySize = 32;
        private const int IVSize = 16;



        private string? _key = "9lp6YXR7i63lih5YZ1sxrg9CsVgxxvPT6HIzkGaq296o169hldmC17GBQ18lp0DR";
        public string? Key
        {
            get => _key;
            set { SetProperty(ref _key, value); }
        }


        public void SaveAndEncrypt(string json)
        {
            var secKey = ConvertKeyToSecurityKey();

            using (Aes aes = Aes.Create())
            {
                aes.Key = secKey.Key;
                aes.IV = secKey.IV;

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (var sw = new StreamWriter(cs))
                    {
                        sw.Write(json);
                    }
                    File.WriteAllBytes(FileName, ms.ToArray());
                }
            }
        }

        public string? ReadAndDecrypt()
        {
            if (!File.Exists(FileName))
                return "[]";

            var secKey = ConvertKeyToSecurityKey();

            var fullCipher = File.ReadAllBytes(FileName);

            try
            {


                using (Aes aes = Aes.Create())
                {
                    aes.Key = secKey.Key;
                    aes.IV = secKey.IV;

                    using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                    using (var ms = new MemoryStream(fullCipher))
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    using (var sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
            catch
            {
                return "[]";
            }
        }


        private string GenerateKeyAndIv()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] key = new byte[KeySize];
                byte[] iv = new byte[IVSize];
                rng.GetBytes(key);
                rng.GetBytes(iv);
                return Convert.ToBase64String(key.Concat(iv).ToArray());
            }
        }
        private SecurityKey ConvertKeyToSecurityKey()
        {
            var normal = Convert.FromBase64String(Key);
            return new SecurityKey
            {
                Key = normal[..KeySize],
                IV = normal[KeySize..]
            };

        }

    }

    public class SecurityKey
    {
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }
    }
}
