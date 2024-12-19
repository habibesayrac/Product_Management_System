using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Consumer
{
    public interface IOrderConsumer
    {
        public object StartConsumingOrders(string queueName);

    }
}