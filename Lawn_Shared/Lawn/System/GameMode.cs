﻿using System;

namespace Lawn
{
    public enum GameMode
    {
        Adventure,

        SurvivalNormalStage1,
        SurvivalNormalStage2,
        SurvivalNormalStage3,
        SurvivalNormalStage4,
        SurvivalNormalStage5,
        SurvivalHardStage1,
        SurvivalHardStage2,
        SurvivalHardStage3,
        SurvivalHardStage4,
        SurvivalHardStage5,
        SurvivalEndlessStage1,
        SurvivalEndlessStage2,
        SurvivalEndlessStage3,
        SurvivalEndlessStage4,
        SurvivalEndlessStage5,

        ChallengeWarAndPeas,
        ChallengeWallnutBowling,
        ChallengeSlotMachine,
        ChallengeRainingSeeds,
        ChallengeBeghouled,
        ChallengeInvisighoul,
        ChallengeSeeingStars,
        ChallengeBeghouledTwist,
        ChallengeLittleTrouble,
        ChallengePortalCombat,
        ChallengeColumn,
        ChallengeBobsledBonanza,
        ChallengeSpeed,
        ChallengeWhackAZombie,
        ChallengeLastStand,
        ChallengeWarAndPeas2,
        ChallengeWallnutBowling2,
        ChallengePogoParty,
        ChallengeFinalBoss,
        ChallengeArtChallenge1,
        ChallengeSunnyDay,
        ChallengeResodded,
        ChallengeBigTime,
        ChallengeArtChallenge2,
        ChallengeAirRaid,
        ChallengeIce,
        ChallengeZenGarden,
        ChallengeHighGravity,
        ChallengeGraveDanger,
        ChallengeShovel,
        ChallengeStormyNight,
        ChallengeBungeeBlitz,
        ChallengeSquirrel,
        TreeOfWisdom,

        ScaryPotter1,
        ScaryPotter2,
        ScaryPotter3,
        ScaryPotter4,
        ScaryPotter5,
        ScaryPotter6,
        ScaryPotter7,
        ScaryPotter8,
        ScaryPotter9,
        ScaryPotterEndless,

        PuzzleIZombie1,
        PuzzleIZombie2,
        PuzzleIZombie3,
        PuzzleIZombie4,
        PuzzleIZombie5,
        PuzzleIZombie6,
        PuzzleIZombie7,
        PuzzleIZombie8,
        PuzzleIZombie9,
        PuzzleIZombieEndless,

        Upsell,
        Intro,

        Quickplay1,
        Quickplay2,
        Quickplay3,
        Quickplay4,
        Quickplay5,
        Quickplay6,
        Quickplay7,
        Quickplay8,
        Quickplay9,
        Quickplay10,
        Quickplay11,
        Quickplay12,
        Quickplay13,
        Quickplay14,
        Quickplay15,
        Quickplay16,
        Quickplay17,
        Quickplay18,
        Quickplay19,
        Quickplay20,
        Quickplay21,
        Quickplay22,
        Quickplay23,
        Quickplay24,
        Quickplay25,
        Quickplay26,
        Quickplay27,
        Quickplay28,
        Quickplay29,
        Quickplay30,
        Quickplay31,
        Quickplay32,
        Quickplay33,
        Quickplay34,
        Quickplay35,
        Quickplay36,
        Quickplay37,
        Quickplay38,
        Quickplay39,
        Quickplay40,
        Quickplay41,
        Quickplay42,
        Quickplay43,
        Quickplay44,
        Quickplay45,
        Quickplay46,
        Quickplay47,
        Quickplay48,
        Quickplay49,
        Quickplay50,

        ChallengeZombiquarium,
        ChallengePlayableZombies = 1 << PuzzleIZombie9,
        GameModeCount,

        AdventureCount = 1,
        AdventureStart = Adventure,
        
        SurvivalCount = 15,
        SurvivalStart = SurvivalNormalStage1,
        ChallengeStart = SurvivalStart,
        ScaryPotterCount = GameConstants.VASEBREAKER_LEVEL_COUNT,
        ScaryPotterStart = ScaryPotter1,
        PuzzleIZombieCount = GameConstants.I_ZOMBIE_LEVEL_COUNT,
        PuzzleIZombieStart = PuzzleIZombie1,
        MiniGameCount = GameConstants.MINI_GAME_COUNT,
        MiniGameStart = ChallengeWarAndPeas,
        QuickplayCount = GameConstants.NUM_LEVELS,
        QuickplayStart = Quickplay1
    }
}
