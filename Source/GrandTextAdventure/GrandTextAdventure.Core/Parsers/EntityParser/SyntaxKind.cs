﻿namespace GrandTextAdventure.Core.Parsers.EntityParser
{
    /// <summary>
    ///  Represent the TokenType
    /// </summary>
    public enum SyntaxKind
    {
        Comment,

        Number,

        BadToken,

        EndToken,

        Comma,
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
