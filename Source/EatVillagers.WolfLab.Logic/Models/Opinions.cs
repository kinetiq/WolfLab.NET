using EatVillagers.WolfLab.Logic.Extensions;

namespace EatVillagers.WolfLab.Logic.Models
{
    public class Opinion
    {
        public PlayerModel Owner;
        public PlayerModel Target;
        private decimal m_Aggro = .5M;
        public bool IsCleared = false;
        public bool IsEvil = false;

        public decimal Aggro
        {
            get { return m_Aggro; }

            set { m_Aggro = value.UpperCap(1).LowerCap(0); }
        }
    }
}
