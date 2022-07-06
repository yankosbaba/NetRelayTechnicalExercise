using System.Collections.Generic;
using System.Text;

namespace NetRelayTechnical.Model
{
    public class PayoutResponse
    {
        public int numberOfPayouts { get; set; }
        public List<Payout> payouts { get; set; }
    }
}
