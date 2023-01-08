using EntityLayer;

namespace TiyatroProje.Models
{
    public class BiletSeansModel
    {
        public OyunSalonMusteri BiletModal { get; set; }
        public IEnumerable<OyunSalon> SeansModal { get; set; }
    }
}
