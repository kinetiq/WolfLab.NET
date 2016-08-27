using EatVillagers.Village.Logic.Extensions;

namespace EatVillagers.Village.Logic.Models
{
    public class GoodOpinion
    {
        public PlayerModel Owner;
        public PlayerModel Target;
        private decimal m_Suspicion = .5M;
        public bool IsCleared = false;
        public bool IsEvil = false;

        public decimal Suspicion
        {
            get { return m_Suspicion; }

            set { m_Suspicion = value.UpperCap(1).LowerCap(0); }
        }
    }

    public class EvilOpinion
    {
        public PlayerModel Owner;
        public PlayerModel Target;
        private decimal m_EatPriority = 0.5M;
        public bool IsEvil;

        public decimal EatPriority
        {
            get { return m_EatPriority; }

            set { m_EatPriority = value.UpperCap(1).LowerCap(0); }
        }
    }

}
