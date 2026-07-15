using System.Security.Cryptography;

public class InvitationCodeGenerator : IInvitationCodeGenerator
{
    private const string Chars =
        "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";

    public string Generate()
    {
        Span<char> buffer = stackalloc char[8];

        for (int i = 0; i < buffer.Length; i++)
        {
            buffer[i] = Chars[
                RandomNumberGenerator.GetInt32(Chars.Length)
            ];
        }

        return new string(buffer);
    }
}