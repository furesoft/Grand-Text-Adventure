﻿using System;
using System.Collections.Generic;
using GrandTextAdventure.Core.TextProcessing.Interfaces;

namespace GrandTextAdventure.Core.TextProcessing.Synonyms
{
    /// <summary>
    /// Mapping of verb synonyms to verb enum codes. This is where the parser will reduce the different input verb
    /// synonyms to the verbs in the enumeration.
    /// </summary>
    public class VerbSynonyms : IVerbSynonyms
    {
        private readonly Dictionary<string, VerbCodes> _synonymMappings = new();

        /// <summary>
        /// Constructor that maps the default set of synonyms supported by the game engine.
        /// </summary>
        public VerbSynonyms()
        {
            Add("walk", VerbCodes.Go);
            Add("go", VerbCodes.Go);
            Add("hop", VerbCodes.Go);
            Add("run", VerbCodes.Go);
            Add("shuffle", VerbCodes.Go);
            Add("crawl", VerbCodes.Go);
            Add("slide", VerbCodes.Go);
            Add("scuffle", VerbCodes.Go);
            Add("wiggle", VerbCodes.Go);
            Add("skip", VerbCodes.Go);
            Add("prance", VerbCodes.Go);
            Add("mince", VerbCodes.Go);

            Add("pick", VerbCodes.Take);
            Add("grab", VerbCodes.Take);
            Add("collect", VerbCodes.Take);
            Add("steal", VerbCodes.Take);
            Add("get", VerbCodes.Take);
            Add("accept", VerbCodes.Take);
            Add("capture", VerbCodes.Take);
            Add("earn", VerbCodes.Take);
            Add("hold", VerbCodes.Take);
            Add("reach", VerbCodes.Take);
            Add("acquire", VerbCodes.Take);
            Add("attain", VerbCodes.Take);
            Add("catch", VerbCodes.Take);
            Add("clasp", VerbCodes.Take);
            Add("clutch", VerbCodes.Take);
            Add("ensnare", VerbCodes.Take);
            Add("grasp", VerbCodes.Take);
            Add("obtain", VerbCodes.Take);
            Add("reap", VerbCodes.Take);
            Add("snag", VerbCodes.Take);
            Add("secure", VerbCodes.Take);
            Add("snatch", VerbCodes.Take);
            Add("gain", VerbCodes.Take);
            Add("gather", VerbCodes.Take);
            Add("take", VerbCodes.Take);
            Add("haul", VerbCodes.Take);

            Add("drop", VerbCodes.Drop);
            Add("abandon", VerbCodes.Drop);
            Add("release", VerbCodes.Drop);
            Add("discard", VerbCodes.Drop);
            Add("desert", VerbCodes.Drop);
            Add("dismiss", VerbCodes.Drop);
            Add("reject", VerbCodes.Drop);
            Add("disown", VerbCodes.Drop);
            Add("forfeit", VerbCodes.Drop);
            Add("relinquish", VerbCodes.Drop);
            Add("renounce", VerbCodes.Drop);
            Add("resign", VerbCodes.Drop);
            Add("sacrifice", VerbCodes.Drop);
            Add("terminate", VerbCodes.Drop);

            Add("use", VerbCodes.Use);
            Add("employ", VerbCodes.Use);
            Add("apply", VerbCodes.Use);
            Add("work", VerbCodes.Use);
            Add("ply", VerbCodes.Use);
            Add("exert", VerbCodes.Use);
            Add("wield", VerbCodes.Use);
            Add("pull", VerbCodes.Use);
            Add("push", VerbCodes.Use);
            Add("flick", VerbCodes.Use);
            Add("flip", VerbCodes.Use);
            Add("turn", VerbCodes.Use);
            Add("unlock", VerbCodes.Use);
            Add("switch", VerbCodes.Use);
            Add("climb", VerbCodes.Use);
            Add("open", VerbCodes.Use);

            Add("look", VerbCodes.Look);
            Add("examine", VerbCodes.Look);
            Add("peek", VerbCodes.Look);
            Add("review", VerbCodes.Look);
            Add("stare", VerbCodes.Look);
            Add("view", VerbCodes.Look);
            Add("cast", VerbCodes.Look);
            Add("gander", VerbCodes.Look);
            Add("gaze", VerbCodes.Look);
            Add("inspect", VerbCodes.Look);
            Add("observe", VerbCodes.Look);
            Add("watch", VerbCodes.Look);
            Add("see", VerbCodes.Look);
            Add("glance", VerbCodes.Look);
            Add("lift", VerbCodes.Look);
            Add("read", VerbCodes.Look);

            Add("help", VerbCodes.Hint);
            Add("hint", VerbCodes.Hint);
            Add("hints", VerbCodes.Hint);
            Add("clue", VerbCodes.Hint);
            Add("clues", VerbCodes.Hint);
            Add("sos", VerbCodes.Hint);

            Add("attack", VerbCodes.Attack);
            Add("hit", VerbCodes.Attack);
            Add("barge", VerbCodes.Attack);
            Add("kick", VerbCodes.Attack);
            Add("wallop", VerbCodes.Attack);
            Add("smash", VerbCodes.Attack);
            Add("stab", VerbCodes.Attack);
            Add("slap", VerbCodes.Attack);
            Add("punch", VerbCodes.Attack);
            Add("chop", VerbCodes.Attack);

            Add("teleport", VerbCodes.Visit);
            Add("visit", VerbCodes.Visit);
            Add("goto", VerbCodes.Visit);

            Add("eat", VerbCodes.Eat);
            Add("gobble", VerbCodes.Eat);
            Add("consume", VerbCodes.Eat);
            Add("munch", VerbCodes.Eat);
            Add("gulp", VerbCodes.Eat);
            Add("swallow", VerbCodes.Eat);

            Add("quit", VerbCodes.Quit);
            Add("exit", VerbCodes.Quit);

            Add("suicide", VerbCodes.Suicide);

            Add("money", VerbCodes.Money);
            Add("coins", VerbCodes.Money);
            Add("dollar", VerbCodes.Money);

            Add("location", VerbCodes.Location);

            Add("enter", VerbCodes.Enter);
            Add("drive", VerbCodes.Enter);

            Add("leave", VerbCodes.Leave);
        }

        /// <summary>
        /// Add a new verb synonym mapping into the dictionary.
        /// </summary>
        /// <param name="synonym">The synonym to be mapped.</param>
        /// <param name="verb">The verb that the synonym maps onto.</param>
        /// <exception cref="ArgumentNullException">If either the synonym or verb inputs are null of empty, an
        /// ArgumentNullException is thrown.</exception>
        public void Add(string synonym, VerbCodes verb)
        {
            if (string.IsNullOrEmpty(synonym))
            {
                throw new ArgumentNullException(nameof(synonym));
            }

            _synonymMappings.Add(synonym, verb);
        }

        /// <summary>
        /// Return the base verb by providing one of it's synonyms. This is used by the parser to reduce potential
        /// synonyms down to the base verb to add into a command
        /// </summary>
        /// <param name="synonym">The synonym to return a verb for.</param>
        /// <returns>The mapped verb</returns>
        /// <exception cref="ArgumentNullException">If the synonym is null or empty throw an ArgumentNullException.</exception>
        public VerbCodes GetVerbForSynonym(string synonym)
        {
            if (string.IsNullOrEmpty(synonym))
            {
                throw new ArgumentNullException(nameof(synonym));
            }

            if (_synonymMappings.ContainsKey(synonym))
            {
                return _synonymMappings[synonym];
            }
            else
            {
                return VerbCodes.NoCommand;
            }
        }
    }
}
