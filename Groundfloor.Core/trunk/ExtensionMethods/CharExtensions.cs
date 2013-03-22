
namespace System
{
    public static class CharExtensions
    {
        //
        // Summary:
        //     Indicates whether the specified Unicode character is categorized as a control
        //     character.
        //
        // Parameters:
        //   c:
        //     The Unicode character to evaluate.
        //
        // Returns:
        //     true if c is a control character; otherwise, false.
        public static bool IsControl(this char c)
        {
            return char.IsControl(c);
        }
        //
        // Summary:
        //     Indicates whether the specified Unicode character is categorized as a decimal
        //     digit.
        //
        // Parameters:
        //   c:
        //     The Unicode character to evaluate.
        //
        // Returns:
        //     true if c is a decimal digit; otherwise, false.
        public static bool IsDigit(this char c)
        {
            return char.IsDigit(c);
        }
        //
        // Summary:
        //     Indicates whether the specified Unicode character is categorized as a Unicode
        //     letter.
        //
        // Parameters:
        //   c:
        //     The Unicode character to evaluate.
        //
        // Returns:
        //     true if c is a letter; otherwise, false.
        public static bool IsLetter(this char c)
        {
            return char.IsLetter(c);
        }
        //
        // Summary:
        //     Indicates whether the specified Unicode character is categorized as a letter
        //     or a decimal digit.
        //
        // Parameters:
        //   c:
        //     The Unicode character to evaluate.
        //
        // Returns:
        //     true if c is a letter or a decimal digit; otherwise, false.
        public static bool IsLetterOrDigit(this char c)
        {
            return char.IsLetterOrDigit(c);
        }
        //
        // Summary:
        //     Indicates whether the specified Unicode character is categorized as a lowercase
        //     letter.
        //
        // Parameters:
        //   c:
        //     The Unicode character to evaluate.
        //
        // Returns:
        //     true if c is a lowercase letter; otherwise, false.
        public static bool IsLower(this char c)
        {
            return char.IsLower(c);
        }
        //
        // Summary:
        //     Indicates whether the specified Unicode character is categorized as a number.
        //
        // Parameters:
        //   c:
        //     The Unicode character to evaluate.
        //
        // Returns:
        //     true if c is a number; otherwise, false.
        public static bool IsNumber(this char c)
        {
            return char.IsNumber(c);
        }
        //
        // Summary:
        //     Indicates whether the specified Unicode character is categorized as a punctuation
        //     mark.
        //
        // Parameters:
        //   c:
        //     The Unicode character to evaluate.
        //
        // Returns:
        //     true if c is a punctuation mark; otherwise, false.
        public static bool IsPunctuation(this char c)
        {
            return char.IsPunctuation(c);
        }
        //
        // Summary:
        //     Indicates whether the specified Unicode character is categorized as a separator
        //     character.
        //
        // Parameters:
        //   c:
        //     The Unicode character to evaluate.
        //
        // Returns:
        //     true if c is a separator character; otherwise, false.
        public static bool IsSeparator(this char c)
        {
            return char.IsSeparator(c);
        }
        //
        // Summary:
        //     Indicates whether the specified Unicode character is categorized as a symbol
        //     character.
        //
        // Parameters:
        //   c:
        //     The Unicode character to evaluate.
        //
        // Returns:
        //     true if c is a symbol character; otherwise, false.
        public static bool IsSymbol(this char c)
        {
            return char.IsSymbol(c);
        }
        //
        // Summary:
        //     Indicates whether the specified Unicode character is categorized as an uppercase
        //     letter.
        //
        // Parameters:
        //   c:
        //     The Unicode character to evaluate.
        //
        // Returns:
        //     true if c is an uppercase letter; otherwise, false.
        public static bool IsUpper(this char c)
        {
            return char.IsUpper(c);
        }
        //
        // Summary:
        //     Indicates whether the specified Unicode character is categorized as white
        //     space.
        //
        // Parameters:
        //   c:
        //     The Unicode character to evaluate.
        //
        // Returns:
        //     true if c is white space; otherwise, false.
        public static bool IsWhiteSpace(this char c)
        {
            return char.IsWhiteSpace(c);
        }

        public static char Random(this char c)
        {
            var _random = new Random();
	        int num = _random.Next(0, 26); // Zero to 25

            if(c.IsUpper())
                return (char)('A' + num);
            else
                return (char)('a' + num);
        }
    }
}
