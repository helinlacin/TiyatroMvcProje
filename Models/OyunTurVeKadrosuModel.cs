using EntityLayer;

namespace TiyatroProje.Models
{
    public class OyunTurVeKadrosuModel
    {
        public Oyun oyunModal { get; set; }
        public IEnumerable<Tur> turModal { get; set; }
        public IEnumerable<OyunKadrosu> oyunKadrosuModal { get; set; }
    }
}
