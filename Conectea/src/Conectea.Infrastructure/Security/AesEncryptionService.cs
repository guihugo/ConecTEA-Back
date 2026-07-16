using System.Security.Cryptography;
using System.Text;
using Conectea.Application.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;

namespace Conectea.Infrastructure.Security;

public class AesEncryptionService : IEncryptionService
{
    private readonly byte[] _key;

    public AesEncryptionService(EncryptionSettings settings)
    {
        _key = Convert.FromBase64String(settings.Key);

        if (_key.Length != 32)
            throw new InvalidOperationException(
                "Encryption key must be 32 bytes.");
    }

    public string Encrypt(string plainText)
    {
        byte[] nonce = RandomNumberGenerator.GetBytes(12);
        byte[] plaintextBytes = Encoding.UTF8.GetBytes(plainText);

        byte[] cipherText = new byte[plaintextBytes.Length];
        byte[] tag = new byte[16];

        using var aes = new AesGcm(_key);

        aes.Encrypt(
            nonce,
            plaintextBytes,
            cipherText,
            tag);

        byte[] result = new byte[
            nonce.Length +
            tag.Length +
            cipherText.Length];

        Buffer.BlockCopy(nonce, 0, result, 0, nonce.Length);
        Buffer.BlockCopy(tag, 0, result, nonce.Length, tag.Length);
        Buffer.BlockCopy(cipherText, 0, result, nonce.Length + tag.Length, cipherText.Length);

        return Convert.ToBase64String(result);
    }

    public string Decrypt(string encryptedText)
    {
        byte[] data = Convert.FromBase64String(encryptedText);

        byte[] nonce = data[..12];
        byte[] tag = data[12..28];
        byte[] cipherText = data[28..];

        byte[] plainText = new byte[cipherText.Length];

        using var aes = new AesGcm(_key);

        aes.Decrypt(
            nonce,
            cipherText,
            tag,
            plainText);

        return Encoding.UTF8.GetString(plainText);
    }
}