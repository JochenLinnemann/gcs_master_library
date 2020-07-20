namespace SpellParser.Models
{
    public class SplittermondSpell : RitualMagicSpell
    {
        #region Static Fields

        internal const string SplittermondSpellType = "splittermond_spell";

        #endregion

        #region Constructors

        public SplittermondSpell()
        {
            Type = SplittermondSpellType;
        }

        public SplittermondSpell(RitualMagicSpell spell)
            : base(spell)
        {
            Type = SplittermondSpellType;
        }

        #endregion
    }
}