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
        private static readonly HashSet<int> ids =
        [
            ModContent.NPCType<CalamityMod.NPCs.Providence.Providence>(),
            ModContent.NPCType<CalamityMod.NPCs.StormWeaver.StormWeaverHead>(),
            ModContent.NPCType<CalamityMod.NPCs.CeaselessVoid.CeaselessVoid>(),
            ModContent.NPCType<CalamityMod.NPCs.Signus.Signus>(),
            ModContent.NPCType<CalamityMod.NPCs.Polterghast.Polterghast>(),
            ModContent.NPCType<CalamityMod.NPCs.OldDuke.OldDuke>(),
            ModContent.NPCType<CalamityMod.NPCs.DevourerofGods.DevourerofGodsHead>(),
            ModContent.NPCType<CalamityMod.NPCs.Yharon.Yharon>(),
            ModContent.NPCType<CalamityMod.NPCs.SupremeCalamitas.SupremeCalamitas>(),
        ];

        private static readonly HashSet<int> exoMechIds =
        [
            ModContent.NPCType<CalamityMod.NPCs.ExoMechs.Ares.AresBody>(),
            ModContent.NPCType<CalamityMod.NPCs.ExoMechs.Thanatos.ThanatosHead>(),
            ModContent.NPCType<CalamityMod.NPCs.ExoMechs.Apollo.Apollo>(),
            // ModContent.NPCType<CalamityMod.NPCs.ExoMechs.Artemis.Artemis>(),
            // Artemis doesn't handle loot dropping, just Apollo
        ];

        private static readonly HashSet<int> noBagIds =
        [
            ModContent.NPCType<CalamityMod.NPCs.ProfanedGuardians.ProfanedGuardianCommander>(),
        ];

        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            if (!lateInstantiation)
            {
                return false;
            }

            return ids.Contains(entity.type)
                || exoMechIds.Contains(entity.type)
                || noBagIds.Contains(entity.type)
                || entity.type == ModContent.NPCType<CalamityMod.NPCs.Ravager.RavagerBody>();
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (ids.Contains(npc.type))
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

            if (exoMechIds.Contains(npc.type))
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

            if (npc.type == ModContent.NPCType<CalamityMod.NPCs.Ravager.RavagerBody>())
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

            if (noBagIds.Contains(npc.type))
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
}
