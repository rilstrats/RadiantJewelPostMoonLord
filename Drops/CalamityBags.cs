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
        private static readonly Dictionary<int, CalamityBagType> bagIdToType = new()
        {
            {
                ModContent.ItemType<CalamityMod.Items.TreasureBags.RavagerBag>(),
                CalamityBagType.Ravager
            },
            {
                ModContent.ItemType<CalamityMod.Items.TreasureBags.DragonfollyBag>(),
                CalamityBagType.Default
            },
            {
                ModContent.ItemType<CalamityMod.Items.TreasureBags.ProvidenceBag>(),
                CalamityBagType.Default
            },
            {
                ModContent.ItemType<CalamityMod.Items.TreasureBags.StormWeaverBag>(),
                CalamityBagType.Default
            },
            {
                ModContent.ItemType<CalamityMod.Items.TreasureBags.CeaselessVoidBag>(),
                CalamityBagType.Default
            },
            {
                ModContent.ItemType<CalamityMod.Items.TreasureBags.SignusBag>(),
                CalamityBagType.Default
            },
            {
                ModContent.ItemType<CalamityMod.Items.TreasureBags.PolterghastBag>(),
                CalamityBagType.Default
            },
            {
                ModContent.ItemType<CalamityMod.Items.TreasureBags.OldDukeBag>(),
                CalamityBagType.Default
            },
            {
                ModContent.ItemType<CalamityMod.Items.TreasureBags.DevourerofGodsBag>(),
                CalamityBagType.Default
            },
            {
                ModContent.ItemType<CalamityMod.Items.TreasureBags.YharonBag>(),
                CalamityBagType.Default
            },
            {
                ModContent.ItemType<CalamityMod.Items.TreasureBags.DraedonBag>(),
                CalamityBagType.Default
            },
            {
                ModContent.ItemType<CalamityMod.Items.TreasureBags.CalamitasCoffer>(),
                CalamityBagType.Default
            },
        };

        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            if (!lateInstantiation)
            {
                return false;
            }

            return bagIdToType.ContainsKey(entity.type);
        }

        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            if (bagIdToType[item.type] == CalamityBagType.Ravager)
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

    public enum CalamityBagType
    {
        /// <summary>
        /// Boss bags with default behavior that can drop radiant jewel at any point
        /// </summary>
        Default,

        /// <summary>
        /// Ravager bag can drop radiant jewel if Providence has been defated
        /// </summary>
        Ravager,
    }
}
