using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Consumer
{
    public interface IProductConsumer
    {
        public object StartConsumingProducts(string queueName);

    }
}