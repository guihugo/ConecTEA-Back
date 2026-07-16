namespace Conectea.Application.Interfaces.Repositories;
public interface IEncryptionService
{
    string Encrypt(string content);

    string Decrypt(string encryptedContent);
}