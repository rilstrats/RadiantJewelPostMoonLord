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
    public class CalamityBags : GlobalItem
    {
        private static readonly HashSet<int> ids =
        [
            ModContent.ItemType<CalamityMod.Items.TreasureBags.RavagerBag>(),
            ModContent.ItemType<CalamityMod.Items.TreasureBags.DragonfollyBag>(),
            ModContent.ItemType<CalamityMod.Items.TreasureBags.ProvidenceBag>(),
            ModContent.ItemType<CalamityMod.Items.TreasureBags.StormWeaverBag>(),
            ModContent.ItemType<CalamityMod.Items.TreasureBags.CeaselessVoidBag>(),
            ModContent.ItemType<CalamityMod.Items.TreasureBags.SignusBag>(),
            ModContent.ItemType<CalamityMod.Items.TreasureBags.PolterghastBag>(),
            ModContent.ItemType<CalamityMod.Items.TreasureBags.OldDukeBag>(),
            ModContent.ItemType<CalamityMod.Items.TreasureBags.DevourerofGodsBag>(),
            ModContent.ItemType<CalamityMod.Items.TreasureBags.YharonBag>(),
            ModContent.ItemType<CalamityMod.Items.TreasureBags.DraedonBag>(),
            ModContent.ItemType<CalamityMod.Items.TreasureBags.CalamitasCoffer>(),
        ];

        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            if (!lateInstantiation)
            {
                return false;
            }

            return ids.Contains(entity.type);
        }

        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            if (item.type == ModContent.ItemType<CalamityMod.Items.TreasureBags.RavagerBag>())
            {
                //18% chance to drop 1 item in Expert Mode
                //25% chance to drop 1 item in Master Mode
                LeadingConditionRule rule = new(
                    DropHelper.If(() => DownedBossSystem.downedProvidence)
                );
                rule.OnSuccess(
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
                rule.OnSuccess(
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
                itemLoot.Add(rule);
                return;
            }

            //18% chance to drop 1 item in Expert Mode
            //25% chance to drop 1 item in Master Mode
            itemLoot.Add(
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
            itemLoot.Add(
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
