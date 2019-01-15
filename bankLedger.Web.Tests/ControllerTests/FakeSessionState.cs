using System.Collections.Generic;
using System.Web;

namespace bankLedger.Web.Tests.ControllerTests
{
    public class FakeSessionState : HttpSessionStateBase
    {
        private Dictionary<string, object> items = new Dictionary<string, object>();

        public override object this[string name]
        {
            get { return items.ContainsKey(name) ? items[name] : null; }
            set { items[name] = value; }
        }
    }
}