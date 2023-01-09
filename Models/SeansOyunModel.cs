using EntityLayer;

namespace TiyatroProje.Models
{
    public class SeansOyunModel
    {
        public OyunSalon SeansModal { get; set; }
        public IEnumerable<Oyun> OyunModal { get; set; }
        public IEnumerable<Tur> TurModal { get; set; }
        public IEnumerable<OyunKadrosu> OyunKadrosuModal { get; set; }
    }
}
