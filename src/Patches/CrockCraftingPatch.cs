using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.GameContent;
using GloomeClasses.src.Chef;

namespace GloomeClasses.src.Patches {

    // allows vanilla crocks to be used in recipes that output refurbished crocks
    [HarmonyPatch(typeof(BlockCrock))]
    [HarmonyPatchCategory(GloomeClassesModSystem.CrockCraftingPatchCategory)]
    public class CrockCraftingPatch {

        [HarmonyPrefix]
        [HarmonyPatch(nameof(BlockCrock.MatchesForCrafting))]
        private static bool MatchesForCraftingPrefix(ItemStack inputStack, GridRecipe gridRecipe, CraftingRecipeIngredient ingredient, ref bool __result) {
            // allow conversion recipes that output refurbished crocks
            if (gridRecipe.Output.ResolvedItemstack?.Collectible is BlockRefurbishedCrock) {
                bool hasSealingIngredient = false;
                for (int i = 0; i < gridRecipe.resolvedIngredients.Length; i++) {
                    var stack = gridRecipe.resolvedIngredients[i]?.ResolvedItemstack;
                    if (stack?.ItemAttributes?["canSealCrock"]?.AsBool(false) == true) {
                        hasSealingIngredient = true;
                        break;
                    }
                }
                // if no sealing ingredient, this is a conversion recipe - allow vanilla matching
                if (!hasSealingIngredient) {
                    __result = true;
                    return false; // skip original method
                }
            }
            return true; // run original method
        }
    }
}
