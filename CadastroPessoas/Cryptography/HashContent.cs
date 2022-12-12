
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public static class HashContent
{
    public static string Hash(this string value)
    {
        var key = "$2a$06$.rCVZVOThsIa97pEDOxvGu";

        return BCrypt.Net.BCrypt.HashPassword(value, key);
    }
}
