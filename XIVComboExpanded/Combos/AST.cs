using Dalamud.Game.ClientState.JobGauge.Enums;
using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboExpandedPlugin.Combos
{
    internal static class AST
    {
        public const byte JobID = 33;

        public const uint
            Benefic = 3594,
            Benefic2 = 3610,
            Draw = 3590,
            Balance = 4401,
            Bole = 4404,
            Arrow = 4402,
            Spear = 4403,
            Ewer = 4405,
            Spire = 4406,
            MinorArcana = 7443,
            SleeveDraw = 7448,
            Malefic4 = 16555,
            LucidDreaming = 7562,
            Ascend = 3603,
            Swiftcast = 7561,
            CrownPlay = 25869,
            Astrodyne = 25870,
            FallMalefic = 25871,
            Play = 17055;

        public static class Buffs
        {
            public const short
            Swiftcast = 167,
            LordOfCrownsDrawn = 2054,
            LadyOfCrownsDrawn = 2055;
        }

        public static class Debuffs
        {
            // public const short placeholder = 0;
        }

        public static class Levels
        {
            public const byte
                Benefic2 = 26,
                MinorArcana = 50,
                Draw = 30,
                CrownPlay = 70;


        }
    }

    internal class AstrologianCardsOnDrawFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.AstrologianCardsOnDrawFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == AST.Play)
            {
                var gauge = GetJobGauge<ASTGauge>();
                if (level >= AST.Levels.Draw && gauge.DrawnCard == CardType.NONE)
                    return AST.Draw;
            }

            return actionID;
        }
    }

    internal class AstrologianCrownPlayFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.AstrologianCrownPlayFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == AST.MinorArcana)
            {
                if (level >= AST.Levels.CrownPlay && (HasEffect(AST.Buffs.LordOfCrownsDrawn) || HasEffect(AST.Buffs.LadyOfCrownsDrawn)))
                    return AST.CrownPlay;
            }

            return actionID;
        }
    }

    internal class AstrologianBeneficFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.AstrologianBeneficFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == AST.Benefic2)
            {
                if (level < AST.Levels.Benefic2)
                    return AST.Benefic;

            }
            return actionID;
        }
        internal class AstrologianAscendFeature : CustomCombo
        {
            protected override CustomComboPreset Preset => CustomComboPreset.AstrologianAscendFeature;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID == AST.Swiftcast)
                {
                    if (IsEnabled(CustomComboPreset.AstrologianAscendFeature))
                    {
                        if (HasEffect(AST.Buffs.Swiftcast))
                            return AST.Ascend;
                    }
                    return OriginalHook(AST.Swiftcast);
                }
                return actionID;
            }
        }
    }
}