using System;
using System.IO;
using System.Text;

/// <summary>
/// C# implementation of ASCII85 encoding. 
/// Based on C code from http://www.stillhq.com/cgi-bin/cvsweb/ascii85/
/// </summary>
/// <remarks>
/// Jeff Atwood
/// http://www.codinghorror.com/blog/archives/000410.html
/// </remarks>
class Ascii85
{
    /// <summary>
    /// Prefix mark that identifies an encoded ASCII85 string, traditionally '<~'
    /// </summary>
    public string PrefixMark = "<~";
    /// <summary>
    /// Suffix mark that identifies an encoded ASCII85 string, traditionally '~>'
    /// </summary>
    public string SuffixMark = "~>";
    /// <summary>
    /// Maximum line length for encoded ASCII85 string; 
    /// set to zero for one unbroken line.
    /// </summary>
    public int LineLength = 75;
    /// <summary>
    /// Add the Prefix and Suffix marks when encoding, and enforce their presence for decoding
    /// </summary>
    public bool EnforceMarks = true;

    const int _asciiOffset = 33;

    /* Cambio no fusionado mediante combinación del proyecto 'Hamekoz.Fiscal(net9.0)'
    Antes:
        byte[] _encodedBlock = new byte[5];
        byte[] _decodedBlock = new byte[4];
    Después:
        readonly byte[] _encodedBlock = new byte[5];
        readonly byte[] _decodedBlock = new byte[4];
    */

    /* Cambio no fusionado mediante combinación del proyecto 'Hamekoz.Fiscal(net9.0)'
    Antes:
        uint[] pow85 = { 85 * 85 * 85 * 85, 85 * 85 * 85, 85 * 85, 85, 1 };
    Después:
        readonly uint[] pow85 = { 85 * 85 * 85 * 85, 85 * 85 * 85, 85 * 85, 85, 1 };
    */
    readonly byte[] _encodedBlock = new byte[5];
    readonly byte[] _decodedBlock = new byte[4];
    uint _tuple;
    int _linePos;

    readonly uint[] pow85 = { 85 * 85 * 85 * 85, 85 * 85 * 85, 85 * 85, 85, 1 };

    /// <summary>
    /// Decodes an ASCII85 encoded string into the original binary data
    /// </summary>
    /// <param name="s">ASCII85 encoded string</param>
    /// <returns>byte array of decoded binary data</returns>
    public byte[] Decode(string s)
    {
        if (EnforceMarks)
        {
            if (!s.StartsWith(PrefixMark) | !s.EndsWith(SuffixMark))
            {
                throw new Exception("ASCII85 encoded data should begin with '" + PrefixMark +
                "' and end with '" + SuffixMark + "'");
            }
        }

        // strip prefix and suffix if present
        if (s.StartsWith(PrefixMark))
        {
            s = s.Substring(PrefixMark.Length);
        }
        if (s.EndsWith(SuffixMark))
        {
            s = s.Substring(0, s.Length - SuffixMark.Length);
        }

        var ms = new MemoryStream();
        int count = 0;
        bool processChar = false;

        foreach (char c in s)
        {
            switch (c)
            {
                case 'z':
                    if (count != 0)
                    {
                        throw new Exception("The character 'z' is invalid inside an ASCII85 block.");
                    }
                    _decodedBlock[0] = 0;
                    _decodedBlock[1] = 0;
                    _decodedBlock[2] = 0;
                    _decodedBlock[3] = 0;
                    ms.Write(_decodedBlock, 0, _decodedBlock.Length);
                    processChar = false;
                    break;
                case '\n':
                case '\r':
                case '\t':
                case '\0':
                case '\f':
                case '\b':
                    processChar = false;
                    break;
                default:
                    if (c < '!' || c > 'u')
                    {
                        throw new Exception("Bad character '" + c + "' found. ASCII85 only allows characters '!' to 'u'.");
                    }
                    processChar = true;
                    break;
            }

            if (processChar)
            {
                _tuple += ((uint)(c - _asciiOffset) * pow85[count]);
                count++;
                if (count == _encodedBlock.Length)
                {
                    DecodeBlock();
                    ms.Write(_decodedBlock, 0, _decodedBlock.Length);
                    _tuple = 0;
                    count = 0;
                }
            }
        }

        // if we have some bytes left over at the end..
        if (count != 0)
        {
            if (count == 1)
            {
                throw new Exception("The last block of ASCII85 data cannot be a single byte.");
            }
            count--;
            _tuple += pow85[count];
            DecodeBlock(count);
            for (int i = 0; i < count; i++)
            {
                ms.WriteByte(_decodedBlock[i]);
            }
        }

        return ms.ToArray();
    }

    /// <summary>
    /// Encodes binary data into a plaintext ASCII85 format string
    /// </summary>
    /// <param name="ba">binary data to encode</param>
    /// <returns>ASCII85 encoded string</returns>
    public string Encode(byte[] ba)
    {
        var sb = new StringBuilder((int)(ba.Length * (_encodedBlock.Length / _decodedBlock.Length)));
        _linePos = 0;

        if (EnforceMarks)
        {
            AppendString(sb, PrefixMark);
        }

        int count = 0;
        _tuple = 0;
        foreach (byte b in ba)
        {
            if (count >= _decodedBlock.Length - 1)
            {
                _tuple |= b;
                if (_tuple == 0)
                {
                    AppendChar(sb, 'z');
                }
                else
                {
                    EncodeBlock(sb);
                }
                _tuple = 0;
                count = 0;
            }
            else
            {
                _tuple |= (uint)(b << (24 - (count * 8)));
                count++;
            }
        }

        // if we have some bytes left over at the end..
        if (count > 0)
        {
            EncodeBlock(count + 1, sb);
        }

        if (EnforceMarks)
        {
            AppendString(sb, SuffixMark);
        }
        return sb.ToString();
    }

    void EncodeBlock(StringBuilder sb)
    {
        EncodeBlock(_encodedBlock.Length, sb);
    }

    void EncodeBlock(int count, StringBuilder sb)
    {
        for (int i = _encodedBlock.Length - 1; i >= 0; i--)
        {
            _encodedBlock[i] = (byte)((_tuple % 85) + _asciiOffset);
            _tuple /= 85;
        }

        for (int i = 0; i < count; i++)
        {
            char c = (char)_encodedBlock[i];
            AppendChar(sb, c);
        }

    }

    void DecodeBlock()
    {
        DecodeBlock(_decodedBlock.Length);
    }

    void DecodeBlock(int bytes)
    {
        for (int i = 0; i < bytes; i++)
        {
            _decodedBlock[i] = (byte)(_tuple >> 24 - (i * 8));
        }
    }

    void AppendString(StringBuilder sb, string s)
    {
        if (LineLength > 0 && (_linePos + s.Length > LineLength))
        {
            _linePos = 0;
            sb.Append('\n');
        }
        else
        {
            _linePos += s.Length;
        }
        sb.Append(s);
    }

    void AppendChar(StringBuilder sb, char c)
    {
        sb.Append(c);
        _linePos++;
        if (LineLength > 0 && (_linePos >= LineLength))
        {
            _linePos = 0;
            sb.Append('\n');
        }
    }

}