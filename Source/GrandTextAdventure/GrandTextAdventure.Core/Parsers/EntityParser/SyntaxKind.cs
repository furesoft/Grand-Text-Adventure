namespace GrandTextAdventure.Core.Parsers.EntityParser
{
    /// <summary>
    ///  Represent the TokenType
    /// </summary>
    public enum SyntaxKind
    {
        /// <summary>
        ///  EndOfFile Token
        /// </summary>
        EOF,

        /// <summary>
        ///  Identifier Token
        /// </summary>
        Keyword,

        /// <summary>
        ///  String Token
        /// </summary>
        String,

        /// <summary>
        ///  Number Token
        /// </summary>
        Number,

        BadToken,
        Whitespace,
        EndToken,
        Boolean,
        Symbol,
        OpenSquare,
        CloseSquare,
        Comma,
        Comment,
        EndOfFile,
        PropertyToken,
        EntityModelToken,
        IntLiteralToken,
        StringLiteralToken,
        CommentToken,
        EntityToken,
        EqualsToken,
        IdentifierToken,
        IsToken,
        ApplyModelToken,
    }
}
