using System.Collections.Generic;
using CalamityMod;
using MagicStorage.Common.DropRules;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace RadiantJewelPostMoonLord.Drops
{
    [ExtendsFromMod("CalamityMod")]
    public class CalamityBosses : GlobalNPC
    {
        private static readonly Dictionary<int, CalamityBossType> bossIdToType = new()
        {
            {
                ModContent.NPCType<CalamityMod.NPCs.Providence.Providence>(),
                CalamityBossType.Default
            },
            {
                ModContent.NPCType<CalamityMod.NPCs.StormWeaver.StormWeaverHead>(),
                CalamityBossType.Default
            },
            {
                ModContent.NPCType<CalamityMod.NPCs.CeaselessVoid.CeaselessVoid>(),
                CalamityBossType.Default
            },
            { ModContent.NPCType<CalamityMod.NPCs.Signus.Signus>(), CalamityBossType.Default },
            {
                ModContent.NPCType<CalamityMod.NPCs.Polterghast.Polterghast>(),
                CalamityBossType.Default
            },
            { ModContent.NPCType<CalamityMod.NPCs.OldDuke.OldDuke>(), CalamityBossType.Default },
            {
                ModContent.NPCType<CalamityMod.NPCs.DevourerofGods.DevourerofGodsHead>(),
                CalamityBossType.Default
            },
            { ModContent.NPCType<CalamityMod.NPCs.Yharon.Yharon>(), CalamityBossType.Default },
            {
                ModContent.NPCType<CalamityMod.NPCs.SupremeCalamitas.SupremeCalamitas>(),
                CalamityBossType.Default
            },
            {
                ModContent.NPCType<CalamityMod.NPCs.ExoMechs.Ares.AresBody>(),
                CalamityBossType.ExoMech
            },
            {
                ModContent.NPCType<CalamityMod.NPCs.ExoMechs.Thanatos.ThanatosHead>(),
                CalamityBossType.ExoMech
            },
            {
                // Artemis doesn't drop loot, just Apollo
                ModContent.NPCType<CalamityMod.NPCs.ExoMechs.Apollo.Apollo>(),
                CalamityBossType.ExoMech
            },
            {
                ModContent.NPCType<CalamityMod.NPCs.ProfanedGuardians.ProfanedGuardianCommander>(),
                CalamityBossType.NoBag
            },
            {
                ModContent.NPCType<CalamityMod.NPCs.Ravager.RavagerBody>(),
                CalamityBossType.Ravager
            },
        };


        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            if (!lateInstantiation)
            {
                return false;
            }

            return bossIdToType.ContainsKey(entity.type);
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (bossIdToType[npc.type] == CalamityBossType.Default)
            {
                //10% chance to drop 1 item in Normal Mode
                npcLoot.Add(
                    ItemDropRule
                        .ByCondition(
                            new Conditions.NotExpert(),
                            ModContent.ItemType<MagicStorage.Items.RadiantJewel>(),
                            chanceDenominator: 10,
                            minimumDropped: 1,
                            maximumDropped: 1,
                            chanceNumerator: 1
                        )
                        .WithPityDrops(MagicStorage.Items.RadiantJewelDrop.PITY_DROP_STRENGTH)
                );
            }

            if (bossIdToType[npc.type] == CalamityBossType.ExoMech)
            {
                //10% chance to drop 1 item in Normal Mode
                LeadingConditionRule rule = npcLoot.DefineConditionalDropSet(
                    CalamityMod.NPCs.ExoMechs.Ares.AresBody.CanDropLoot
                );
                rule.OnSuccess(
                    ItemDropRule
                        .ByCondition(
                            new Conditions.NotExpert(),
                            ModContent.ItemType<MagicStorage.Items.RadiantJewel>(),
                            chanceDenominator: 10,
                            minimumDropped: 1,
                            maximumDropped: 1,
                            chanceNumerator: 1
                        )
                        .WithPityDrops(MagicStorage.Items.RadiantJewelDrop.PITY_DROP_STRENGTH)
                );
                npcLoot.Add(rule);
            }

            if (bossIdToType[npc.type] == CalamityBossType.Ravager)
            {
                //10% chance to drop 1 item in Normal Mode
                LeadingConditionRule rule = new(
                    DropHelper.If(() => DownedBossSystem.downedProvidence)
                );
                rule.OnSuccess(
                    ItemDropRule
                        .ByCondition(
                            new Conditions.NotExpert(),
                            ModContent.ItemType<MagicStorage.Items.RadiantJewel>(),
                            chanceDenominator: 10,
                            minimumDropped: 1,
                            maximumDropped: 1,
                            chanceNumerator: 1
                        )
                        .WithPityDrops(MagicStorage.Items.RadiantJewelDrop.PITY_DROP_STRENGTH)
                );
                npcLoot.Add(rule);
            }

            if (bossIdToType[npc.type] == CalamityBossType.NoBag)
            {
                //10% chance to drop 1 item in Normal Mode
                //18% chance to drop 1 item in Expert Mode
                //25% chance to drop 1 item in Master Mode
                npcLoot.Add(
                    ItemDropRule
                        .ByCondition(
                            new Conditions.NotExpert(),
                            ModContent.ItemType<MagicStorage.Items.RadiantJewel>(),
                            chanceDenominator: 10,
                            minimumDropped: 1,
                            maximumDropped: 1,
                            chanceNumerator: 1
                        )
                        .WithPityDrops(MagicStorage.Items.RadiantJewelDrop.PITY_DROP_STRENGTH)
                );
                npcLoot.Add(
                    ItemDropRule
                        .ByCondition(
                            new MagicStorage.Items.IsExpertNotMaster(),
                            ModContent.ItemType<MagicStorage.Items.RadiantJewel>(),
                            chanceDenominator: 50,
                            minimumDropped: 1,
                            maximumDropped: 1,
                            chanceNumerator: 9
                        )
                        .WithPityDrops(MagicStorage.Items.RadiantJewelDrop.PITY_DROP_STRENGTH)
                );
                npcLoot.Add(
                    ItemDropRule
                        .ByCondition(
                            new Conditions.IsMasterMode(),
                            ModContent.ItemType<MagicStorage.Items.RadiantJewel>(),
                            chanceDenominator: 4,
                            minimumDropped: 1,
                            maximumDropped: 1,
                            chanceNumerator: 1
                        )
                        .WithPityDrops(MagicStorage.Items.RadiantJewelDrop.PITY_DROP_STRENGTH)
                );
            }
        }
    }

    public enum CalamityBossType
    {
        /// <summary>
        /// Bosses with default behavior that can drop radiant jewel if in classic mode
        /// </summary>
        Default,

        /// <summary>
        /// Exo Mechs can drop radiant jewel if:
        /// - death of last boss (like twins)
        /// - Classic mode
        /// </summary>
        ExoMech,

        /// <summary>
        /// Bosses that never drop a boss bag and can always drop radiant jewel
        /// </summary>
        NoBag,

        /// <summary>
        /// Ravager can drop radiant jewel if:
        /// - Providence has been defated
        /// - Classic mode
        /// </summary>
        Ravager,
    }
}
