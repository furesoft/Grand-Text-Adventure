﻿using GrandTextAdventure.Core.Parsers.EntityParser.Syntax;

namespace GrandTextAdventure.Core.Parsers.EntityParser
{
    public interface IScriptVisitor
    {
        void Visit(BlockNode block);

        void Visit(EntityDefinitionNode definitionNode);
    }
}
