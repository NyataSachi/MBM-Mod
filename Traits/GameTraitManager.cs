using MBMScripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM_Mod.Traits
{
    public static class GameTraitManager
    {
        public static void AddUpgradeTrait(int id = 0, ETrait trait = ETrait.None, int value = 1, string traitString = "")
        {
            if (!(Global.SelectedUnit is Character character))
                return;

            ETrait targetTrait;

            if (trait != ETrait.None)
            {
                targetTrait = trait;
            }
            else if (id != 0)
            {
                targetTrait = (ETrait)id;
            }
            else
            {
                return;
            }

            if (!character.TraitContains(targetTrait))
            {
                character.AddTrait(targetTrait);
            }

            character.AddTraitValue(targetTrait, value);


            /*Traits List
            ETrait.Trait44  = Beauty and Charm (lust by all race, 2-3x fee human race)
            ETrait.Trait45  = -Weak (50% more damage received human race)
            ETrait.Trait47  = Crest (+30% concentration on 1st partner)
            ETrait.Trait51  = -Dwarves hate (elf race)
            ETrait.Trait52  = Tenacity (negate some damage)
            ETrait.Trait56  = -Elves Hate (dwarf race)
            ETrait.Trait59  = Max Birth (dwarf race)
            ETrait.Trait60  = -Relentless (neko race)
            ETrait.Trait61  = Milk Tank (I forget)
            ETrait.Trait62  = -Greedy? Glotton?
            ETrait.Trait63  = Superfetation (more child when creamp)
            ETrait.Trait64  = -Eatalot? Glutton?
            ETrait.Trait65  = Calm (50% more regen when rest)
            ETrait.Trait66  = -Pray (-1 inu hps)
            ETrait.Trait67  = Strong genes (Drakonian)
            ETrait.Trait70  = -Picky?
            ETrait.Trait71  = Nice in brothel (x2-x5 fee)
            ETrait.Trait72  = Iron WIll (endurance?)
            ETrait.Trait73  = Steel Uterus (I can't remember)
            ETrait.Triat74  = Theif (steal yo nut juice)
            ETrait.Trait75  = Gamble's Bad Luck?
            ETrait.Trait76  = Genetic Engineering?
            ETrait.Trait77  = Humilation-+
            ETrait.Trait80  = -Sadis-+
            ETrait.Triat82  = Overexcitement-+
            ETrait.Trait83  = -Semen Tank-+
            ETrait.Trait85  = Grooming?-+
            ETrait.Trait87  = -Violent breeding-+
            ETrait.Trait90  = God's Child (Blessed Minotour)-+
            ETrait.Trait92  = dih too big?-+
            ETrait.Trait93  = Concentration*-+
            ETrait.Trait94  = -Growth Time*-+
            ETrait.Trait95  = -Daily food rate*-+
            ETrait.Trait96  = Max HP*-+
            ETrait.Trait97  = More Birth (max)*-+
            ETrait.Trait98  = More prag in the same time*-+
            ETrait.Trait99  = -Sex Time*-+
            ETrait.Trait100 = -Stamina Rate*-+
            ETrait.Trait101 = Prejudge-+
            ETrait.Trait102 = -Picky-+
            ETrait.Trait103 = God's Blessing
            ETrait.Trait104 = Crest
            ETrait.Trait105 = something HUGE
            ETrait.Trait106 = uhh very horny?
            ETrait.Trait107 = Drug addict oh it's the NPCs
            ETrait.Trait108 = dense idiot
            ETrait.Trait109 = Have Technique
            ETrait.Trait110 = Mosochist
            */
        }
        public static void RemoveDecreaseTrait(bool remove, int id = 0, ETrait trait = ETrait.None, int value = 1)
        {
            if (!(Global.SelectedUnit is Character character))
                return;

            ETrait targetTrait;
            float Tvalue = -(Math.Abs(value));

            if (trait != ETrait.None)
            {
                targetTrait = trait;
            }
            else if (id != 0)
            {
                targetTrait = (ETrait)id;
            }
            else
            {
                return;
            }

            if (character.TraitContains(targetTrait))
            {
                if (remove)
                {
                    character.RemoveTrait(targetTrait);
                }
                else
                    character.AddTraitValue(targetTrait, Tvalue);
            }
        }
    }
}
