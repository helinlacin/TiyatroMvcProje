using EntityLayer;

namespace TiyatroProje.Models
{
    public class BiletSeansMusteriModel
    {
        public OyunSalonMusteri BiletModal { get; set; }
        public IEnumerable<OyunSalon> SeansModal { get; set; }
        public IEnumerable<Musteri> MusteriModal { get; set;}
    }
}
