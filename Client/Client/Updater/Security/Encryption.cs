﻿// This file is part of Mystery Dungeon eXtended.

// Mystery Dungeon eXtended is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// Mystery Dungeon eXtended is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with Mystery Dungeon eXtended.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace PMDCP.Updater.Security
{
    public class Encryption
    {
        #region Fields

        TripleDESCryptoServiceProvider TripleDes = new TripleDESCryptoServiceProvider();
        string svKey = "justsomewordstobeusedasacryptionkey";

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Encryption"/> class.
        /// </summary>
        /// <param name="key">The encryption key.</param>
        public Encryption(string key) {
            // Initialize the crypto provider.
            TripleDes.Key = TruncateHash(key, TripleDes.KeySize / 8);
            TripleDes.IV = TruncateHash("", TripleDes.BlockSize / 8);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Encryption"/> class.
        /// </summary>
        public Encryption() {
            TripleDes.Key = TruncateHash(svKey, TripleDes.KeySize / 8);
            TripleDes.IV = TruncateHash("", TripleDes.BlockSize / 8);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Decrypts the bytes.
        /// </summary>
        /// <param name="encryptedBytes">The encrypted bytes.</param>
        /// <returns></returns>
        public byte[] DecryptBytes(byte[] encryptedBytes) {
            // Create the stream.
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            // Create the decoder to write to the stream.
            CryptoStream decStream = new CryptoStream(ms, TripleDes.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write);

            // Use the crypto stream to write the byte array to the stream.
            decStream.Write(encryptedBytes, 0, encryptedBytes.Length);
            decStream.FlushFinalBlock();

            // Convert the plaintext stream to a string.
            return ms.ToArray();
        }

        /// <summary>
        /// Decrypts the data.
        /// </summary>
        /// <param name="encryptedtext">The encrypted data.</param>
        /// <returns>The decrypted string.</returns>
        public string DecryptData(string encryptedtext) {
            try {
                // Convert the encrypted text string to a byte array.
                byte[] encryptedBytes = Convert.FromBase64String(encryptedtext);

                // Create the stream.
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                // Create the decoder to write to the stream.
                CryptoStream decStream = new CryptoStream(ms, TripleDes.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write);

                // Use the crypto stream to write the byte array to the stream.
                decStream.Write(encryptedBytes, 0, encryptedBytes.Length);
                decStream.FlushFinalBlock();

                // Convert the plaintext stream to a string.
                return System.Text.Encoding.Unicode.GetString(ms.ToArray());
            } catch {
                return "sdcksndcsac ascascscdds";
            }
        }

        /// <summary>
        /// Encrypts the bytes.
        /// </summary>
        /// <param name="bytesToEncrypt">The bytes to encrypt.</param>
        /// <returns></returns>
        public byte[] EncryptBytes(byte[] bytesToEncrypt) {
            // Create the stream.
            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            // Create the encoder to write to the stream.
            CryptoStream encStream = new CryptoStream(ms, TripleDes.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write);

            // Use the crypto stream to write the byte array to the stream.
            encStream.Write(bytesToEncrypt, 0, bytesToEncrypt.Length);
            encStream.FlushFinalBlock();

            // Convert the encrypted stream to a printable string.
            return ms.ToArray();
        }

        /// <summary>
        /// Encrypts the data.
        /// </summary>
        /// <param name="plaintext">The data to encrypt.</param>
        /// <returns>The encrypted string.</returns>
        public string EncryptData(string plaintext) {
            // Convert the plaintext string to a byte array.
            byte[] plaintextBytes = System.Text.Encoding.Unicode.GetBytes(plaintext);

            // Create the stream.
            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            // Create the encoder to write to the stream.
            CryptoStream encStream = new CryptoStream(ms, TripleDes.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write);

            // Use the crypto stream to write the byte array to the stream.
            encStream.Write(plaintextBytes, 0, plaintextBytes.Length);
            encStream.FlushFinalBlock();

            // Convert the encrypted stream to a printable string.
            return Convert.ToBase64String(ms.ToArray());
        }

        /// <summary>
        /// Sets the encryption key.
        /// </summary>
        /// <param name="key">The key.</param>
        public void SetKey(string key) {
            // Initialize the crypto provider.
            TripleDes.Key = TruncateHash(key, TripleDes.KeySize / 8);
            TripleDes.IV = TruncateHash("", TripleDes.BlockSize / 8);
        }

        private byte[] TruncateHash(string key, int length) {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            // Hash the key.
            byte[] keyBytes = System.Text.Encoding.Unicode.GetBytes(key);
            byte[] hash = sha1.ComputeHash(keyBytes);
            // Truncate or pad the hash.
            Array.Resize(ref hash, length);
            return hash;
        }

        #endregion Methods
    }
}
